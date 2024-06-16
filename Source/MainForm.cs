using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Numerics;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using MaterialSkin;
using MaterialSkin.Controls;
using squad_dma.Properties;

namespace squad_dma
{
    public partial class frmMain : MaterialForm
    {
        private readonly Config _config;
        private readonly SKGLControl _mapCanvas;
        private readonly Stopwatch _fpsWatch = new();
        private readonly object _renderLock = new();
        private readonly object _loadMapBitmapsLock = new();
        private readonly System.Timers.Timer _mapChangeTimer = new(100);
        private readonly List<Map> _maps = new(); // Contains all maps from \\Maps folder

        private bool _isFreeMapToggled = false;
        private float _uiScale = 1.0f;
        private UActor _closestPlayerToMouse = null;
        private bool _isDragging = false;
        private Point _lastMousePosition = Point.Empty;
        private int _fps = 0;
        private int _mapSelectionIndex = 0;
        private Map _selectedMap;
        private SKBitmap[] _loadedBitmaps;
        private MapPosition _mapPanPosition = new();

        private const int ZOOM_INTERVAL = 10;
        private int targetZoomValue = 0;
        private System.Windows.Forms.Timer zoomTimer;

        private const float DRAG_SENSITIVITY = 3.5f;

        private const double PAN_SMOOTHNESS = 0.1;
        private const int PAN_INTERVAL = 10;
        private SKPoint targetPanPosition;
        private System.Windows.Forms.Timer panTimer;

        #region Getters
        /// <summary>
        /// Radar has found process and is ready.
        /// </summary>
        private bool Ready
        {
            get => Memory.Ready;
        }

        /// <summary>
        /// Radar has found Local Game World.
        /// </summary>
        private bool InGame
        {
            get => Memory.InGame;
        }
        private string MapName
        {
            get => Memory.MapName;
        }

        /// <summary>
        /// LocalPlayer (who is running Radar) 'Player' object.
        /// </summary>
        private UActor LocalPlayer
        {
            get => Memory.LocalPlayer;
        }

        /// <summary>
        /// All Actors in Game World (including tech)
        /// </summary>
        private ReadOnlyDictionary<ulong, UActor> AllActors
        {
            get => Memory.Actors;
        }
        
        private Vector3 AbsoluteLocation
        {
            get => Memory.AbsoluteLocation;
        }
        #endregion

        #region Constructor
        /// <summary>
        /// GUI Constructor.
        /// </summary>
        public frmMain()
        {
            _config = Program.Config;

            InitializeComponent();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.EnforceBackcolorOnAllComponents = true;
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Grey800, Primary.Grey800, Primary.Indigo100, Accent.Orange400, TextShade.WHITE);

            LoadConfig();
            LoadMaps();

            _mapCanvas = skMapCanvas;
            _mapCanvas.VSync = _config.VSync;

            _mapChangeTimer.AutoReset = false;
            _mapChangeTimer.Elapsed += MapChangeTimer_Elapsed;

            this.DoubleBuffered = true;

            _fpsWatch.Start();

            zoomTimer = new System.Windows.Forms.Timer();
            zoomTimer.Interval = ZOOM_INTERVAL;
            zoomTimer.Tick += ZoomTimer_Tick;

            panTimer = new System.Windows.Forms.Timer();
            panTimer.Interval = PAN_INTERVAL;
            panTimer.Tick += PanTimer_Tick;
        }
        #endregion

        #region Overrides
        /// <summary>
        /// Form closing event.
        /// </summary>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = true; // Cancel shutdown
            this.Enabled = false; // Lock window

            CleanupLoadedBitmaps();
            Config.SaveConfig(_config); // Save Config to Config.json
            Memory.Shutdown(); // Wait for Memory Thread to gracefully exit
            e.Cancel = false; // Ready to close
            base.OnFormClosing(e); // Proceed with closing
        }

        /// <summary>
        /// Process hotkey presses.sc
        /// </summary>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) => keyData switch
        {
            Keys.F1 => ZoomIn(5),
            Keys.F2 => ZoomOut(5),
            Keys.F4 => swAimview.Checked = !swAimview.Checked,
            Keys.F5 => ToggleMap(),
            Keys.F6 => swNames.Checked = !swNames.Checked,
            _ => base.ProcessCmdKey(ref msg, keyData),
        };

        /// <summary>
        /// Process mousewheel events.
        /// </summary>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (tabControlMain.SelectedIndex == 0) // Main Radar Tab should be open
            {
                var zoomSens = (double)_config.ZoomSensitivity / 100;
                int zoomDelta = -(int)(e.Delta * zoomSens);

                if (zoomDelta < 0)
                    ZoomIn(-zoomDelta);
                else if (zoomDelta > 0)
                    ZoomOut(zoomDelta);

                if (this._isFreeMapToggled && zoomDelta < 0) // Only move the zoom position when scrolling in
                {
                    var mousePos = this._mapCanvas.PointToClient(Cursor.Position);
                    var mapParams = GetMapLocation();
                    var mapMousePos = new SKPoint(
                        mapParams.Bounds.Left + mousePos.X / mapParams.XScale,
                        mapParams.Bounds.Top + mousePos.Y / mapParams.YScale
                    );

                    this.targetPanPosition = mapMousePos;

                    if (!this.panTimer.Enabled)
                        this.panTimer.Start();
                }

                return;
            }

            base.OnMouseWheel(e);
        }
        #endregion

        #region GUI Events / Functions
        #region General Helper Functions
        private bool ToggleMap()
        {
            if (!btnToggleMap.Enabled)
                return false;

            if (_mapSelectionIndex == _maps.Count - 1)
                _mapSelectionIndex = 0; // Start over when end of maps reached
            else
                _mapSelectionIndex++; // Move onto next map

            tabRadar.Text = $"Radar ({_maps[_mapSelectionIndex].Name})";
            _mapChangeTimer.Restart(); // Start delay
            Program.Log("Toggled Map");

            return true;
        }

        private void InitiateColors()
        {
            UpdatePaintColorControls();
            UpdateThemeColors();
        }

        private void InitiateUIScaling()
        {
            _uiScale = (.01f * _config.UIScale);

            #region Update Paints/Text
            SKPaints.TextBaseOutline.StrokeWidth = 2 * _uiScale;
            SKPaints.TextRadarStatus.TextSize = 48 * _uiScale;
            SKPaints.PaintBase.StrokeWidth = 3 * _uiScale;
            SKPaints.PaintTransparentBacker.StrokeWidth = 1 * _uiScale;
            #endregion

            InitiateFontSize();
        }

        private void InitiateFont()
        {
            var fontToUse = SKTypeface.FromFamilyName(cboFont.Text);
            SKPaints.TextBase.Typeface = fontToUse;
            SKPaints.TextBaseOutline.Typeface = fontToUse;
            SKPaints.TextRadarStatus.Typeface = fontToUse;
        }

        private void InitiateFontSize()
        {
            SKPaints.TextBase.TextSize = _config.FontSize * _uiScale;
            SKPaints.TextBaseOutline.TextSize = _config.FontSize * _uiScale;
        }

        private DialogResult ShowErrorDialog(string message)
        {
            return new MaterialDialog(this, "Error", message, "OK", false, "", true).ShowDialog(this);
        }

        private DialogResult ShowConfirmationDialog(string message, string title)
        {
            return new MaterialDialog(this, title, message, "Yes", true, "No", true).ShowDialog(this);
        }

        private void LoadMaps()
        {
            var dir = new DirectoryInfo($"{Environment.CurrentDirectory}\\Maps");
            if (!dir.Exists)
                dir.Create();

            var configs = dir.GetFiles("*.json");
            //Debug.WriteLine($"Found {configs.Length} .json map configs.");
            if (configs.Length == 0)
                throw new IOException("No .json map configs found!");

            foreach (var config in configs)
            {
                var name = Path.GetFileNameWithoutExtension(config.Name);
                //Debug.WriteLine($"Loading Map: {name}");
                var mapConfig = MapConfig.LoadFromFile(config.FullName); // Assuming LoadFromFile is updated to handle new JSON format
                //Add map ID to map config
                var mapID = mapConfig.MapID[0];
                var map = new Map(name.ToUpper(), mapConfig, config.FullName, mapID);
                // Assuming map.ConfigFile now has a 'mapLayers' property that is a List of a new type matching the JSON structure
                map.ConfigFile.MapLayers = map.ConfigFile
                    .MapLayers
                    .OrderBy(x => x.MinHeight)
                    .ToList();

                _maps.Add(map);
            }
        }

        private void LoadConfig()
        {
            #region Settings
            #region General
            // Radar
            swRadarStats.Checked = _config.ShowRadarStats;
            mcRadarStats.Visible = _config.ShowRadarStats;
            swRadarVsync.Checked = _config.VSync;
            swRadarEnemyCount.Checked = _config.EnemyCount;

            // User Interface
            swAimview.Checked = _config.AimviewEnabled;
            swNames.Checked = _config.ShowNames;
            sldrAimlineLength.Value = _config.PlayerAimLineLength;
            sldrZoomSensitivity.Value = _config.ZoomSensitivity;

            sldrUIScale.Value = _config.UIScale;
            cboFont.SelectedIndex = _config.Font;
            sldrFontSize.Value = _config.FontSize;
            #endregion

            #endregion

            InitiateColors();
            InitiateFont();
            InitiateUIScaling();
        }
        #endregion

        #region General Event Handlers
        private async void frmMain_Shown(object sender, EventArgs e)
        {
            while (_mapCanvas.GRContext is null)
                await Task.Delay(1);

            _mapCanvas.GRContext.SetResourceCacheLimit(503316480); // Fixes low FPS on big maps

            while (true)
            {
                await Task.Run(() => Thread.SpinWait(50000)); // High performance async delay
                _mapCanvas.Refresh(); // draw next frame
            }
        }

        private void MapChangeTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.BeginInvoke(
                new MethodInvoker(
                    delegate
                    {
                        btnToggleMap.Enabled = false;
                        btnToggleMap.Text = "Loading...";
                    }
                )
            );

            lock (_renderLock)
            {
                try
                {
                    _selectedMap = _maps[_mapSelectionIndex]; // Swap map

                    if (_loadedBitmaps is not null)
                    {
                        foreach (var bitmap in _loadedBitmaps)
                            bitmap?.Dispose(); // Cleanup resources
                    }

                    _loadedBitmaps = new SKBitmap[_selectedMap.ConfigFile.MapLayers.Count];

                    for (int i = 0; i < _loadedBitmaps.Length; i++)
                    {
                        using (
                            var stream = File.Open(
                                _selectedMap.ConfigFile.MapLayers[i].Filename,
                                FileMode.Open,
                                FileAccess.Read))
                        {
                            _loadedBitmaps[i] = SKBitmap.Decode(stream); // Load new bitmap(s)
                            _loadedBitmaps[i].SetImmutable();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(
                        $"ERROR loading {_selectedMap.ConfigFile.MapLayers[0].Filename}: {ex}"
                    );
                }
                finally
                {
                    this.BeginInvoke(
                        new MethodInvoker(
                            delegate
                            {
                                btnToggleMap.Enabled = true;
                                btnToggleMap.Text = "Toggle Map (F5)";
                            }
                        )
                    );
                }
            }
        }

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {}
        #endregion

        #region Radar Tab
        #region Helper Functions
        private void UpdateWindowTitle()
        {
            bool inGame = this.InGame;
            var localPlayer = this.LocalPlayer;

            if (inGame && localPlayer is not null)
            {
                UpdateSelectedMap();

                if (_fpsWatch.ElapsedMilliseconds >= 1000)
                {
                    // RE-ENABLE & EXPLORE WHAT THIS DOES
                    //_mapCanvas.GRContext.PurgeResources(); // Seems to fix mem leak issue on increasing resource cache

                    #region Radar Stats
                    var fps = _fps;
                    var memTicks = Memory.Ticks;

                    if (lblRadarFPSValue.Text != fps.ToString())
                        lblRadarFPSValue.Text = $"{fps}";

                    if (lblRadarMemSValue.Text != memTicks.ToString())
                        lblRadarMemSValue.Text = $"{memTicks}";
                    #endregion

                    _fpsWatch.Restart();
                    _fps = 0;
                }
                else
                {
                    _fps++;
                }
            }
        }

        private void UpdateSelectedMap()
        {
            string currentMap = this.MapName;
            string currentMapPrefix = currentMap.ToLower().Substring(0, Math.Min(4, currentMap.Length));

            if (_selectedMap is null || !_selectedMap.MapID.ToLower().StartsWith(currentMapPrefix))
            {
                var selectedMapName = _maps.FirstOrDefault(x => x.MapID.ToLower().StartsWith(currentMapPrefix) || x.MapID.ToLower() == currentMap.ToLower());

                if (selectedMapName is not null)
                {
                    _selectedMap = selectedMapName;

                    // Init map
                    CleanupLoadedBitmaps();
                    LoadMapBitmaps();
                }
            }
        }

        private void CleanupLoadedBitmaps()
        {
            if (_loadedBitmaps is not null)
            {
                Parallel.ForEach(_loadedBitmaps, bitmap =>
                {
                    bitmap?.Dispose();
                });

                _loadedBitmaps = null;
            }
        }

        private void LoadMapBitmaps()
        {
            var mapLayers = _selectedMap.ConfigFile.MapLayers;
            _loadedBitmaps = new SKBitmap[mapLayers.Count];

            Parallel.ForEach(mapLayers, (mapLayer, _, _) =>
            {
                lock (_loadMapBitmapsLock)
                {
                    using (var stream = File.Open(mapLayer.Filename, FileMode.Open, FileAccess.Read))
                    {
                        _loadedBitmaps[mapLayers.IndexOf(mapLayer)] = SKBitmap.Decode(stream);
                        _loadedBitmaps[mapLayers.IndexOf(mapLayer)].SetImmutable();
                    }
                }
            });
        }

        private bool IsReadyToRender()
        {
            bool isReady = this.Ready;
            bool inGame = this.InGame;
            bool localPlayerExists = this.LocalPlayer is not null;
            bool selectedMapLoaded = this._selectedMap is not null;

            if (!isReady)
                return false; // Game process not running

            if (!inGame)
                return false; // Waiting for game start

            if (!localPlayerExists)
                return false; // Cannot find local player

            if (!selectedMapLoaded)
                return false; // Map not loaded

            return true; // Ready to render
        }

        private int GetMapLayerIndex(float playerHeight)
        {
            for (int i = _loadedBitmaps.Length - 1; i >= 0; i--)
            {
                if (playerHeight > _selectedMap.ConfigFile.MapLayers[i].MinHeight)
                {
                    return i;
                }
            }

            return 0; // Default to the first layer if no match is found
        }

        private MapParameters GetMapParameters(MapPosition localPlayerPos)
        {
            int mapLayerIndex = GetMapLayerIndex(localPlayerPos.Height);

            var bitmap = _loadedBitmaps[mapLayerIndex];
            float zoomFactor = 0.01f * _config.DefaultZoom;
            float zoomWidth = bitmap.Width * zoomFactor;
            float zoomHeight = bitmap.Height * zoomFactor;

            var bounds = new SKRect(
                localPlayerPos.X - zoomWidth / 2,
                localPlayerPos.Y - zoomHeight / 2,
                localPlayerPos.X + zoomWidth / 2,
                localPlayerPos.Y + zoomHeight / 2
            ).AspectFill(_mapCanvas.CanvasSize);

            return new MapParameters
            {
                UIScale = _uiScale,
                MapLayerIndex = mapLayerIndex,
                Bounds = bounds,
                XScale = (float)_mapCanvas.Width / bounds.Width, // Set scale for this frame
                YScale = (float)_mapCanvas.Height / bounds.Height // Set scale for this frame
            };
        }

        private MapParameters GetMapLocation()
        {
            var localPlayer = this.LocalPlayer;
            if (localPlayer is not null)
            {
                var localPlayerPos = localPlayer.Position + AbsoluteLocation;
                var localPlayerMapPos = localPlayerPos.ToMapPos(_selectedMap);

                if (_isFreeMapToggled)
                {
                    _mapPanPosition.Height = localPlayerMapPos.Height;
                    return GetMapParameters(_mapPanPosition);
                }
                else
                    return GetMapParameters(localPlayerMapPos);
            }
            else
            {
                return GetMapParameters(_mapPanPosition);
            }
        }

        private void DrawMap(SKCanvas canvas)
        {
            if (mcRadarMapSetup.Visible) // Print coordinates (to make it easy to setup JSON configs)
            {
                var localPlayer = this.LocalPlayer;
                var localPlayerPos = localPlayer.Position + AbsoluteLocation;
                lblRadarMapSetup.Text = $"Map Setup - X,Y,Z: {localPlayerPos.X}, {localPlayerPos.Y}, {localPlayerPos.Z}";
            }
            else if (lblRadarMapSetup.Text != "Map Setup" && !mcRadarMapSetup.Visible)
            {
                lblRadarMapSetup.Text = "Map Setup";
            }

            // Prepare to draw Game Map
            var mapParams = GetMapLocation();

            var mapCanvasBounds = new SKRect() // Drawing Destination
            {
                Left = _mapCanvas.Left,
                Right = _mapCanvas.Right,
                Top = _mapCanvas.Top,
                Bottom = _mapCanvas.Bottom
            };

            // Draw Game Map
            canvas.DrawBitmap(
                _loadedBitmaps[mapParams.MapLayerIndex],
                mapParams.Bounds,
                mapCanvasBounds,
                SKPaints.PaintBitmap
            );
        }

        private void DrawActors(SKCanvas canvas)
        {
            var localPlayer = this.LocalPlayer;

            if (this.InGame && localPlayer is not null)
            {
                var allPlayers = this.AllActors?.Select(x => x.Value);

                if (allPlayers is not null)
                {
                    var localPlayerPos = localPlayer.Position + AbsoluteLocation;
                    var localPlayerMapPos = localPlayerPos.ToMapPos(_selectedMap);
                    var mapParams = GetMapLocation();

                    // Draw LocalPlayer

                    var localPlayerZoomedPos = localPlayerMapPos.ToZoomedPos(mapParams);
                    localPlayerZoomedPos.DrawPlayerMarker(
                        canvas,
                        localPlayer,
                        sldrAimlineLength.Value
                    );


                    foreach (var actor in allPlayers) // Draw actors
                    {
                        var actorPos = actor.Position + AbsoluteLocation;
                        if (actor.ActorType == ActorType.Player && !actor.IsAlive || (Math.Abs(actorPos.X - AbsoluteLocation.X) + Math.Abs(actorPos.Y - AbsoluteLocation.Y) + Math.Abs(actorPos.Z - AbsoluteLocation.Z) < 1.0))
                            continue;

                        var actorMapPos = actorPos.ToMapPos(_selectedMap);
                        var actorZoomedPos = actorMapPos.ToZoomedPos(mapParams);

                        actor.ZoomedPosition = new Vector2() // Cache Position as Vec2 for MouseMove event
                        {
                            X = actorZoomedPos.X,
                            Y = actorZoomedPos.Y
                        };

                        int aimlineLength = 15;

                        // Draw
                        DrawActor(canvas, actor, actorZoomedPos, aimlineLength, localPlayerMapPos);
                    }
                }
            }
        }

        private void DrawActor(SKCanvas canvas, UActor actor, MapPosition actorZoomedPos, int aimlineLength, MapPosition localPlayerMapPos)
        {
            if (this.InGame && this.LocalPlayer is not null)
            {
                string[] lines = null;
                var height = actorZoomedPos.Height - localPlayerMapPos.Height;

                var dist = Vector3.Distance(this.LocalPlayer.Position, actor.Position);

                // if (dist < 2) {
                //     return;
                // }

                
                lines = new string[1] { $"{(int)Math.Round(height / 100)},{(int)Math.Round(dist / 100)}" };

                if (actor.ActorType == ActorType.Player)
                    lines[0] += $" ({(int) actor.Health})";
                if (actor.ErrorCount > 10)
                    lines[0] = "ERROR"; // In case POS stops updating, let us know!

                actorZoomedPos.DrawActorText(
                    canvas,
                    actor,
                    lines
                );

                if (actor.ActorType == ActorType.Player) {
                    actorZoomedPos.DrawPlayerMarker(
                        canvas,
                        actor,
                        aimlineLength
                    );
                } else {
                    actorZoomedPos.DrawTechMarker(canvas, actor);
                }
            }
        }
        private void DrawToolTips(SKCanvas canvas)
        {
            var localPlayer = this.LocalPlayer;
            var mapParams = GetMapLocation();

            if (localPlayer is not null)
            {
                if (_closestPlayerToMouse is not null)
                {
                    var playerZoomedPos = (_closestPlayerToMouse
                        .Position + AbsoluteLocation)
                        .ToMapPos(_selectedMap)
                        .ToZoomedPos(mapParams);
                    playerZoomedPos.DrawToolTip(canvas, _closestPlayerToMouse);
                }
            }
        }

        private void DrawStatusText(SKCanvas canvas)
        {
            bool isReady = this.Ready;
            bool inGame = this.InGame;
            var localPlayer = this.LocalPlayer;
            var selectedMap = this._selectedMap;

            string statusText;
            if (!isReady)
            {
                statusText = "Game Process Not Running";
            }
            else if (!inGame)
            {
                statusText = "Waiting for Game Start...";

                if (selectedMap is not null)
                {
                    this._selectedMap = null;

                    lblRadarFPSValue.Text = "0";
                    lblRadarMemSValue.Text = "0";
                }
            }
            else if (localPlayer is null)
            {
                statusText = "Cannot find LocalPlayer";
            }
            else if (selectedMap is null)
            {
                statusText = "Loading Map";
            }
            else
            {
                return; // No status text to draw
            }

            var centerX = _mapCanvas.Width / 2;
            var centerY = _mapCanvas.Height / 2;

            canvas.DrawText(statusText, centerX, centerY, SKPaints.TextRadarStatus);
        }

        private void ClearPlayerRefs()
        {
            _closestPlayerToMouse = null;
        }

        private T FindClosestObject<T>(IEnumerable<T> objects, Vector2 position, Func<T, Vector2> positionSelector, float threshold)
            where T : class
        {
            if (objects is null || !objects.Any())
                return null;

            var closestObject = objects.Aggregate(
                (x1, x2) =>
                    x2 == null || Vector2.Distance(positionSelector(x1), position)
                    < Vector2.Distance(positionSelector(x2), position)
                        ? x1
                        : x2
            );

            if (closestObject is not null && Vector2.Distance(positionSelector(closestObject), position) < threshold)
                return closestObject;

            return null;
        }

        private void PanTimer_Tick(object sender, EventArgs e)
        {
            var panDifference = new SKPoint(
                this.targetPanPosition.X - this._mapPanPosition.X,
                this.targetPanPosition.Y - this._mapPanPosition.Y
            );

            if (panDifference.Length > 0.1)
            {
                this._mapPanPosition.X += (float)(panDifference.X * PAN_SMOOTHNESS);
                this._mapPanPosition.Y += (float)(panDifference.Y * PAN_SMOOTHNESS);
            }
            else
            {
                this.panTimer.Stop();
            }
        }

        private void ZoomTimer_Tick(object sender, EventArgs e)
        {
            int zoomDifference = this.targetZoomValue - _config.DefaultZoom;

            if (zoomDifference != 0)
            {
                int zoomStep = Math.Sign(zoomDifference);
                _config.DefaultZoom += zoomStep;
            }
            else
            {
                this.zoomTimer.Stop();
            }
        }

        private bool ZoomIn(int amt)
        {
            this.targetZoomValue = Math.Max(10, _config.DefaultZoom - amt);
            this.zoomTimer.Start();

            return true;
        }

        private bool ZoomOut(int amt)
        {
            this.targetZoomValue = Math.Min(200, _config.DefaultZoom + amt);
            this.zoomTimer.Start();

            return false;
        }
        #endregion

        #region Event Handlers
        private void btnToggleMapFree_Click(object sender, EventArgs e)
        {
            if (_isFreeMapToggled)
            {
                btnToggleMapFree.Icon = Resources.tick;
                _isFreeMapToggled = false;

                lock (_renderLock)
                {
                    var localPlayer = this.LocalPlayer;
                    if (localPlayer is not null)
                    {
                        var localPlayerMapPos = (localPlayer.Position + AbsoluteLocation).ToMapPos(_selectedMap);
                        _mapPanPosition = new MapPosition()
                        {
                            X = localPlayerMapPos.X,
                            Y = localPlayerMapPos.Y,
                            Height = localPlayerMapPos.Height
                        };
                    }
                }
            }
            else
            {
                btnToggleMapFree.Icon = Resources.cross;
                _isFreeMapToggled = true;
            }
        }

        private void btnMapSetupApply_Click(object sender, EventArgs e)
        {
            if (float.TryParse(txtMapSetupX.Text, out float x)
                && float.TryParse(txtMapSetupY.Text, out float y)
                && float.TryParse(txtMapSetupScale.Text, out float scale))
            {
                lock (_renderLock)
                {
                    if (_selectedMap is not null)
                    {
                        _selectedMap.ConfigFile.X = x;
                        _selectedMap.ConfigFile.Y = y;
                        _selectedMap.ConfigFile.Scale = scale;
                        _selectedMap.ConfigFile.Save(_selectedMap);
                    }
                }
            }
            else
            {
                ShowErrorDialog("Invalid value(s) provided in the map setup textboxes.");
            }
        }

        private void skMapCanvas_MouseMovePlayer(object sender, MouseEventArgs e)
        {
            if (this.InGame && this.LocalPlayer is not null) // Must be in-game
            {
                var mouse = new Vector2(e.X, e.Y);

                var players = this.AllActors
                    ?.Select(x => x.Value); // Get all players except LocalPlayer & Exfil'd Players

                _closestPlayerToMouse = FindClosestObject(players, mouse, x => x.ZoomedPosition, 12 * _uiScale);
            }
            else if (this.InGame)
            {
                ClearPlayerRefs();
            }

            if (this._isDragging && this._isFreeMapToggled)
            {
                if (!this._lastMousePosition.IsEmpty)
                {
                    int dx = e.X - this._lastMousePosition.X;
                    int dy = e.Y - this._lastMousePosition.Y;

                    dx = (int)(dx * DRAG_SENSITIVITY);
                    dy = (int)(dy * DRAG_SENSITIVITY);

                    this.targetPanPosition.X -= dx;
                    this.targetPanPosition.Y -= dy;

                    if (!this.panTimer.Enabled)
                        this.panTimer.Start();
                }

                this._lastMousePosition = e.Location;
            }
        }

        private void skMapCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && this._isFreeMapToggled)
            {
                this._isDragging = true;
                this._lastMousePosition = e.Location;
            }
        }

        private void skMapCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (this._isDragging)
            {
                this._isDragging = false;
                this._lastMousePosition = e.Location;
            }
        }

        private void skMapCanvas_PaintSurface(object sender, SKPaintGLSurfaceEventArgs e)
        {
            try
            {
                SKCanvas canvas = e.Surface.Canvas;
                canvas.Clear();

                UpdateWindowTitle();

                if (IsReadyToRender())
                {
                    lock (_renderLock)
                    {
                        DrawMap(canvas);

                        DrawActors(canvas);

                        DrawToolTips(canvas);
                    }
                }
                else
                {
                    DrawStatusText(canvas);
                }

                canvas.Flush();
            }
            catch { }
        }

        private void btnToggleMap_Click(object sender, EventArgs e)
        {
            ToggleMap();
        }

        private void swRadarStats_CheckedChanged(object sender, EventArgs e)
        {
            var enabled = swRadarStats.Checked;
            _config.ShowRadarStats = enabled;
            mcRadarStats.Visible = enabled;
        }
        #endregion
        #endregion

        #region Settings
        #region General
        #region Event Handlers
        private void swMapHelper_CheckedChanged(object sender, EventArgs e)
        {
            if (swMapHelper.Checked)
            {
                mcRadarMapSetup.Visible = true;
                txtMapSetupX.Text = _selectedMap?.ConfigFile.X.ToString() ?? "0";
                txtMapSetupY.Text = _selectedMap?.ConfigFile.Y.ToString() ?? "0";
                txtMapSetupScale.Text = _selectedMap?.ConfigFile.Scale.ToString() ?? "0";
            }
            else
                mcRadarMapSetup.Visible = false;
        }

        private void swAimview_CheckedChanged(object sender, EventArgs e)
        {
            _config.AimviewEnabled = swAimview.Checked;
        }

        private void swNames_CheckedChanged(object sender, EventArgs e)
        {
            _config.ShowNames = swNames.Checked;
        }

        private void sldrUIScale_onValueChanged(object sender, int newValue)
        {
            _config.UIScale = newValue;
            _uiScale = (.01f * newValue);

            InitiateUIScaling();
        }

        private void btnRestartRadar_Click(object sender, EventArgs e)
        {
            Memory.Restart();
        }

        private void swRadarVsync_CheckedChanged(object sender, EventArgs e)
        {
            var enabled = swRadarVsync.Checked;
            _config.VSync = enabled;

            if (_mapCanvas is not null)
                _mapCanvas.VSync = enabled;
        }

        private void swRadarEnemyCount_CheckedChanged(object sender, EventArgs e)
        {
            var enabled = swRadarEnemyCount.Checked;

            _config.EnemyCount = enabled;
        }

        private void cboFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            _config.Font = cboFont.SelectedIndex;
            InitiateFont();
        }

        private void sldrFontSize_onValueChanged(object sender, int newValue)
        {
            _config.FontSize = newValue;
            InitiateFontSize();
        }

        private void sldrZoomSensitivity_onValueChanged(object sender, int newValue)
        {
            _config.ZoomSensitivity = newValue;
        }
        #endregion
        #endregion

        #region Colors
        #region Helper Functions
        private void UpdateThemeColors()
        {
            Color primary = picOtherPrimary.BackColor;
            Color darkPrimary = picOtherPrimaryDark.BackColor;
            Color lightPrimary = picOtherPrimaryLight.BackColor;
            Color accent = picOtherAccent.BackColor;

            MaterialSkinManager.Instance.ColorScheme = new ColorScheme(primary, darkPrimary, lightPrimary, accent, TextShade.WHITE);

            UpdatePaintColorControls();

            this.Invalidate();
            this.Refresh();
        }

        private Color PaintColorToColor(string name)
        {
            PaintColor.Colors color = _config.PaintColors[name];
            return Color.FromArgb(color.A, color.R, color.G, color.B);
        }

        private Color DefaultPaintColorToColor(string name)
        {
            PaintColor.Colors color = _config.DefaultPaintColors[name];
            return Color.FromArgb(color.A, color.R, color.G, color.B);
        }

        private void UpdatePaintColorControls()
        {
            var colors = _config.PaintColors;

            Action<PictureBox, string> setColor = (pictureBox, name) =>
            {
                if (colors.ContainsKey(name))
                {
                    pictureBox.BackColor = PaintColorToColor(name);
                }
                else
                {
                    colors.Add(name, _config.DefaultPaintColors[name]);
                    pictureBox.BackColor = DefaultPaintColorToColor(name);
                }
            };

            // Other
            setColor(picOtherPrimary, "Primary");
            setColor(picOtherPrimaryDark, "PrimaryDark");
            setColor(picOtherPrimaryLight, "PrimaryLight");
            setColor(picOtherAccent, "Accent");
        }

        private void UpdatePaintColorByName(string name, PictureBox pictureBox)
        {
            if (colDialog.ShowDialog() == DialogResult.OK)
            {
                Color col = colDialog.Color;
                pictureBox.BackColor = col;

                var paintColorToUse = new PaintColor.Colors
                {
                    A = col.A,
                    R = col.R,
                    G = col.G,
                    B = col.B
                };

                if (_config.PaintColors.ContainsKey(name))
                {
                    _config.PaintColors[name] = paintColorToUse;
                }
                else
                {
                    _config.PaintColors.Add(name, paintColorToUse);
                }
            }
        }
        #endregion

        #region Event Handlers

        private void picOtherPrimary_Click(object sender, EventArgs e)
        {
            UpdatePaintColorByName("Primary", picOtherPrimary);
            UpdateThemeColors();
        }

        private void picOtherPrimaryDark_Click(object sender, EventArgs e)
        {
            UpdatePaintColorByName("PrimaryDark", picOtherPrimaryDark);
            UpdateThemeColors();
        }

        private void picOtherPrimaryLight_Click(object sender, EventArgs e)
        {
            UpdatePaintColorByName("PrimaryLight", picOtherPrimaryLight);
            UpdateThemeColors();
        }

        private void picOtherAccent_Click(object sender, EventArgs e)
        {
            UpdatePaintColorByName("Accent", picOtherAccent);
            UpdateThemeColors();
        }

        private void btnResetTheme_Click(object sender, EventArgs e)
        {
            _config.PaintColors["Primary"] = _config.DefaultPaintColors["Primary"];
            _config.PaintColors["PrimaryDark"] = _config.DefaultPaintColors["PrimaryDark"];
            _config.PaintColors["PrimaryLight"] = _config.DefaultPaintColors["PrimaryLight"];
            _config.PaintColors["Accent"] = _config.DefaultPaintColors["Accent"];

            picOtherPrimary.BackColor = DefaultPaintColorToColor("Primary");
            picOtherPrimaryDark.BackColor = DefaultPaintColorToColor("PrimaryDark");
            picOtherPrimaryLight.BackColor = DefaultPaintColorToColor("PrimaryLight");
            picOtherAccent.BackColor = DefaultPaintColorToColor("Accent");

            UpdateThemeColors();
        }
        #endregion
        #endregion
        #endregion
        #endregion

        private void toolTip_Popup(object sender, PopupEventArgs e)
        {

        }
    }
}