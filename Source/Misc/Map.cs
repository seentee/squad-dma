using SkiaSharp;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace squad_dma
{
    /// <summary>
    /// Defines map position for the 2D Map.
    /// </summary>
    public struct MapPosition
    {
        public MapPosition()
        {
        }
        /// <summary>
        /// Contains the Skia Interface (UI) Scaling Value.
        /// </summary>
        public float UIScale = 0;
        /// <summary>
        /// X coordinate on Bitmap.
        /// </summary>
        public float X = 0;
        /// <summary>
        /// Y coordinate on Bitmap.
        /// </summary>
        public float Y = 0;
        /// <summary>
        /// Unit 'height' as determined by Vector3.Z
        /// </summary>
        public float Height = 0;

        /// <summary>
        /// Get exact player location (with optional X,Y offsets).
        /// </summary>
        public SKPoint GetPoint(float xOff = 0, float yOff = 0)
        {
            return new SKPoint(X + xOff, Y + yOff);
        }
        /// <summary>
        /// Gets the point where the Aimline 'Line' ends. Applies UI Scaling internally.
        /// </summary>
        private SKPoint GetAimlineEndpoint(double radians, float aimlineLength)
        {
            aimlineLength *= UIScale;
            return new SKPoint((float)(this.X + Math.Cos(radians) * aimlineLength), (float)(this.Y + Math.Sin(radians) * aimlineLength));
        }
        /// <summary>
        /// Draws a Player Marker on this location.
        /// </summary>
        public void DrawPlayerMarker(SKCanvas canvas, UActor player, int aimlineLength)
        {
            var radians = player.Rotation.X.ToRadians();
            SKPaint paint;
            
            paint = player.GetEntityPaint();

            canvas.DrawCircle(this.GetPoint(), 6 * UIScale, paint);
            canvas.DrawLine(this.GetPoint(), this.GetAimlineEndpoint(radians, aimlineLength), paint);
        }
        /// <summary>
        /// Draws a Projectile on this location.
        /// </summary>
        public void DrawProjectile(SKCanvas canvas, UActor projectile)
        {
            SKPaint paint = projectile.GetProjectilePaint();

            canvas.DrawCircle(this.GetPoint(), 4 * UIScale, paint);
        }
        /// <summary>
        /// Draws a Tech Marker on this location.
        /// </summary>
        public void DrawTechMarker(SKCanvas canvas, UActor actor)
        {
            var scale = 0.2f;
            if (actor.ActorType == ActorType.Mine) {
                scale /= 1.5f;
            }

            if (!Names.BitMaps.TryGetValue(actor.ActorType, out SKBitmap skBitMap)) {
                return;
            }
            var icon = skBitMap;

            var iconWidth = icon.Width * scale;
            var iconHeight = icon.Height * scale;
            var point = this.GetPoint();
            var rotation = actor.Rotation.X + 90;
            if (Names.DoNotRotate.Contains(actor.ActorType)) {
                rotation = 0;
            } else if (Names.RotateBy45Degrees.Contains(actor.ActorType)) {
                rotation -= 45;
            }
            SKMatrix matrix = SKMatrix.CreateTranslation(point.X, point.Y);
            matrix = SKMatrix.Concat(matrix, SKMatrix.CreateRotationDegrees(rotation));
            matrix = SKMatrix.Concat(matrix, SKMatrix.CreateTranslation(-iconWidth / 2, -iconHeight / 2));

            canvas.Save();
            canvas.SetMatrix(matrix);
            canvas.DrawBitmap(icon, SKRect.Create(iconWidth, iconHeight), SKPaints.PaintBitmap);
            canvas.Restore();
        }
        /// <summary>
        /// Draws Player Text on this location.
        /// </summary>
        public void DrawActorText(SKCanvas canvas, UActor actor, string[] lines)
        {
            SKPaint text = actor.GetTextPaint();

            float spacing = 3 * UIScale;
            foreach (var line in lines)
            {
                var coords = this.GetPoint(9 * UIScale, spacing);

                canvas.DrawText(line, coords, Extensions.GetTextOutlinePaint());
                canvas.DrawText(line, coords, text);
                spacing += 12 * UIScale;
            }
        }
        /// <summary>
        /// Draws player tool tip based on if theyre alive or not
        /// </summary>
        public void DrawToolTip(SKCanvas canvas, UActor actor)
        {
            if (!actor.IsAlive)
            {
                //DrawCorpseTooltip(canvas, player);
                return;
            }

            DrawHostileTooltip(canvas, actor);
        }
        /// <summary>
        /// Draws tool tip of hostile players 
        /// </summary>
        private void DrawHostileTooltip(SKCanvas canvas, UActor actor)
        {
            var lines = new List<string>();

            lines.Insert(0, actor.Name);

            DrawToolTip(canvas, string.Join("\n", lines));
        }
        /// <summary>
        /// Draws the tool tip for players/hostiles
        /// </summary>
        private void DrawToolTip(SKCanvas canvas, string tooltipText)
        {
            var lines = tooltipText.Split('\n');
            var maxWidth = 0f;

            foreach (var line in lines)
            {
                var width = SKPaints.TextBase.MeasureText(line);
                maxWidth = Math.Max(maxWidth, width);
            }

            var textSpacing = 12 * UIScale;
            var padding = 3 * UIScale;

            var height = lines.Length * textSpacing;

            var left = X + padding;
            var top = Y - padding;
            var right = left + maxWidth + padding * 2;
            var bottom = top + height + padding * 2;

            var backgroundRect = new SKRect(left, top, right, bottom);
            canvas.DrawRect(backgroundRect, SKPaints.PaintTransparentBacker);

            var y = bottom - (padding * 1.5f);
            foreach (var line in lines)
            {
                canvas.DrawText(line, left + padding, y, SKPaints.TextBase);
                y -= textSpacing;
            }
        }
    }

    /// <summary>
    /// Defines a Map for use in the GUI.
    /// </summary>
    public class Map
    {
        /// <summary>
        /// Name of map (Ex: Customs)
        /// </summary>
        public readonly string Name;
        /// <summary>
        /// 'MapConfig' class instance
        /// </summary>
        public readonly MapConfig ConfigFile;
        /// <summary>
        /// File path to Map .JSON Config
        /// </summary>
        public readonly string ConfigFilePath;

        public Map(string name, MapConfig config, string configPath, string mapID)
        {
            Name = name;
            ConfigFile = config;
            ConfigFilePath = configPath;
            MapID = mapID;
        }

        public readonly string MapID;
    }

    /// <summary>
    /// Contains multiple map parameters used by the GUI.
    /// </summary>
    public class MapParameters
    {
        /// <summary>
        /// Contains the Skia Interface (UI) Scaling Value.
        /// </summary>
        public float UIScale;
        /// <summary>
        /// Contains the 'index' of which map layer to display.
        /// For example: Labs has 3 floors, so there is a Bitmap image for 'each' floor.
        /// Index is dependent on LocalPlayer height.
        /// </summary>
        public int MapLayerIndex;
        /// <summary>
        /// Rectangular 'zoomed' bounds of the Bitmap to display.
        /// </summary>
        public SKRect Bounds;
        /// <summary>
        /// Regular -> Zoomed 'X' Scale correction.
        /// </summary>
        public float XScale;
        /// <summary>
        /// Regular -> Zoomed 'Y' Scale correction.
        /// </summary>
        public float YScale;
    }

    /// <summary>
    /// Defines a .JSON Map Config File
    /// </summary>
    public class MapConfig
    {
        [JsonIgnore]
        private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions()
        {
            WriteIndented = true,
        };

        [JsonPropertyName("mapID")]
        public List<string> MapID { get; set; } // New property for map IDs

        [JsonPropertyName("x")]
        public float X { get; set; }

        [JsonPropertyName("y")]
        public float Y { get; set; }

        [JsonPropertyName("scale")]
        public float Scale { get; set; }

        // Updated to match new JSON format
        [JsonPropertyName("mapLayers")]
        public List<MapLayer> MapLayers { get; set; }

        public static MapConfig LoadFromFile(string file)
        {
            var json = File.ReadAllText(file);
            return JsonSerializer.Deserialize<MapConfig>(json, _jsonOptions);
        }

        public void Save(Map map)
        {
            var json = JsonSerializer.Serialize(this, _jsonOptions);
            File.WriteAllText(map.ConfigFilePath, json);
        }
    }

    public class MapLayer
    {
        [JsonPropertyName("minHeight")]
        public float MinHeight { get; set; }

        [JsonPropertyName("filename")]
        public string Filename { get; set; }
    }
}
