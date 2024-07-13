using System.Collections.ObjectModel;
using System.Numerics;

namespace squad_dma
{
    /// <summary>
    /// Class containing Game instance.
    /// </summary>
    public class Game
    {
        private readonly ulong _squadBase;
        private volatile bool _inGame = false;
        private RegistredActors _actors;
        private UActor _localUPlayer;
        private ulong _gameWorld;
        private ulong _gameInstance;
        private ulong _localPlayer;
        private ulong _playerController;
        private Vector3 _absoluteLocation;
        private string _currentLevel = string.Empty;

        public enum GameStatus
        {
            NotFound,
            Menu,
            InGame,
        }

        #region Getters
        public bool InGame
        {
            get => _inGame;
        }

        public string MapName
        {
            get => _currentLevel;
        }

        public UActor LocalPlayer
        {
            get => _localUPlayer;
        }

        public ReadOnlyDictionary<ulong, UActor> Actors
        {
            get => _actors?.Actors;
        }

        public Vector3 AbsoluteLocation {
            get => _absoluteLocation;
        }
        #endregion

        /// <summary>
        /// Game Constructor.
        /// </summary>
        public Game(ulong squadBase)
        {
            _squadBase = squadBase;
        }

        #region GameLoop
        /// <summary>
        /// Main Game Loop executed by Memory Worker Thread.
        /// </summary>
        public void GameLoop()
        {
            try
            {
                if (!this._inGame)
                {
                    throw new GameEnded("Game has ended!");
                }
                UpdateLocalPlayerInfo();
                this._actors.UpdateList();
                this._actors.UpdateAllPlayers();
            }
            catch (DMAShutdown)
            {
                HandleDMAShutdown();
            }
            catch (GameEnded e)
            {
                HandleGameEnded(e);
            }
            catch (Exception ex)
            {
                HandleUnexpectedException(ex);
            }
        }
        #endregion

        #region Methods

        /// <summary>
        /// Handles the scenario when DMA shutdown occurs.
        /// </summary>
        private void HandleDMAShutdown()
        {
            Program.Log("DMA shutdown");
            this._inGame = false;
        }

        /// <summary>
        /// Handles the scenario when the game ends.
        /// </summary>
        /// <param name="e">The GameEnded exception instance containing details about the game end.</param>
        private void HandleGameEnded(GameEnded e)
        {
            Program.Log("Game has ended!");

            this._inGame = false;
            Memory.GameStatus = Game.GameStatus.Menu;
            Memory.Restart();
        }

        /// <summary>
        /// Handles unexpected exceptions that occur during the game loop.
        /// </summary>
        /// <param name="ex">The exception instance that was thrown.</param>
        private void HandleUnexpectedException(Exception ex)
        {
            Program.Log($"CRITICAL ERROR - Game ended due to unhandled exception: {ex}");
            this._inGame = false;
        }

        /// <summary>
        /// Waits until game has started before returning to caller.
        /// </summary>
        /// 
        public void WaitForGame()
        {
            while (!this.GetGameWorld() || !this.GetGameInstance() || !this.GetCurrentLevel() || !this.InitActors() || !this.GetLocalPlayer())
            {
                Thread.Sleep(1500);
            }
            Thread.Sleep(1000);
            Program.Log("Game has started!!");
            this._inGame = true;
            Memory.GameStatus = Game.GameStatus.InGame;
            Thread.Sleep(1500);
        }

        /// <summary>
        /// Gets Game Object Manager ptr
        /// </summary>
        private bool GetGameWorld()
        {
            try
            {
                _gameWorld = Memory.ReadPtr(_squadBase + Offsets.GameObjects.GWorld);
                // Program.Log($"Found Game World at 0x{_gameWorld:X},\n0x{_gameWorld + 0x00F8:X}=0x16,\n0x{_gameWorld + 0x0158:X}=0x28,\n0x{_gameWorld + 0x01A8:X}=0x50,\n0x{_gameWorld + 0x0270:X}=0x370,\n0x{_gameWorld + 0x05E8:X}=0x90,\n0x{_gameWorld + 0x06D0:X}=0xC8");
                return true;
            }
            catch (DMAShutdown) { throw; }
            catch (Exception ex)
            {
                throw new GameNotRunningException($"ERROR getting Game World, game may not be running: {ex}");
            }
        }
        /// <summary>
        /// Gets GameInstance
        /// </summary>
        private bool GetGameInstance()
        {
            try
            {
                _gameInstance = Memory.ReadPtr(_gameWorld + Offsets.World.OwningGameInstance);
                // Program.Log($"Found Game Instance at 0x{_gameInstance:X}");
                return true;
            }
            catch (DMAShutdown) { throw; }
            catch (Exception ex)
            {
                throw new GameNotRunningException($"ERROR getting Game Instance, game may not be running: {ex}");
            }
        }
        /// <summary>
        /// Gets GameInstance
        /// </summary>
        private bool GetCurrentLevel()
        {
            try
            {
                var currentLayer = Memory.ReadPtr(_gameInstance + Offsets.GameInstance.CurrentLayer);
                var currentLevelIdPtr = currentLayer + Offsets.SQLayer.LevelID;
                var currentLevelId = Memory.ReadValue<uint>(currentLevelIdPtr);
                _currentLevel = Memory.GetNamesById([currentLevelId])[currentLevelId];
                Program.Log("Current level is " + _currentLevel);
                return true;
            }
            catch (DMAShutdown) { throw; }
            catch (Exception ex)
            {
                throw new GameNotRunningException($"ERROR getting Current Layer, game may not be running: {ex}");
            }
        }
        /// <summary>
        /// Initializes actors list
        /// </summary>
        private bool InitActors() {
            try
            {
                var persistentLevel = Memory.ReadPtr(_gameWorld + Offsets.World.PersistentLevel);
                // Program.Log($"Found PersistentLevel at 0x{persistentLevel:X}");
                _actors = new RegistredActors(persistentLevel);
                return true;
            }
            catch (DMAShutdown) { throw; }
            catch (Exception ex)
            {
                throw new GameNotRunningException($"ERROR Initializing actors, game may not be running: {ex}");
            }
        }
        /// <summary>
        /// Gets LocalPlayer
        /// </summary>
        private bool GetLocalPlayer()
        {
            try
            {
                var localPlayers = Memory.ReadPtr(_gameInstance + Offsets.GameInstance.LocalPlayers);
                _localPlayer = Memory.ReadPtr(localPlayers);
                // Program.Log($"Found LocalPlayer at 0x{_localPlayer:X}");
                _localUPlayer = new UActor(_localPlayer);
                _localUPlayer.Team = Team.Unknown;
                GetPlayerController();
                return true;
            }
            catch (DMAShutdown) { throw; }
            catch (Exception ex)
            {
                throw new GameNotRunningException($"ERROR getting LocalPlayer, game may not be running: {ex}");
            }
        }
        private bool UpdateLocalPlayerInfo() {
            try {
                GetCameraCache();
                return true;
            }
            catch (DMAShutdown) { throw; }
        }
        /// <summary>
        /// Gets PlayerController
        /// </summary>
        private bool GetPlayerController()
        {
            try
            {
                _playerController = Memory.ReadPtr(_localPlayer + Offsets.UPlayer.PlayerController);
                // Program.Log($"Found PlayerController at 0x{_playerController:X}");
                return true;
            }
            catch (DMAShutdown) { throw; }
            catch (Exception ex)
            {
                throw new GameNotRunningException($"ERROR getting PlayerController, game may not be running: {ex}");
            }
        }
        /// <summary>
        /// Gets CameraCache
        /// </summary>
        private bool GetCameraCache()
        {
            try
            {
                var cameraInfoScatterMap = new ScatterReadMap(1);
                var cameraManagerRound = cameraInfoScatterMap.AddRound();
                var cameraInfoRound = cameraInfoScatterMap.AddRound();
                
                var cameraManagerPtr = cameraManagerRound.AddEntry<ulong>(0, 0, _playerController + Offsets.PlayerController.PlayerCameraManager);
                cameraManagerRound.AddEntry<int>(0, 11, _gameWorld + Offsets.World.WorldOrigin);
                cameraManagerRound.AddEntry<int>(0, 12, _gameWorld + Offsets.World.WorldOrigin + 0x4);
                cameraManagerRound.AddEntry<int>(0, 13, _gameWorld + Offsets.World.WorldOrigin + 0x8);
                cameraInfoRound.AddEntry<Vector3>(0, 1, cameraManagerPtr, null, Offsets.Camera.CameraLocation);
                cameraInfoRound.AddEntry<Vector3>(0, 2, cameraManagerPtr, null, Offsets.Camera.CameraRotation);
                
                cameraInfoScatterMap.Execute();

                if (!cameraInfoScatterMap.Results[0][1].TryGetResult<Vector3>(out var location)) {
                    return false;
                }
                if (!cameraInfoScatterMap.Results[0][2].TryGetResult<Vector3>(out var rotation)) {
                    return false;
                }
                if (cameraInfoScatterMap.Results[0][11].TryGetResult<int>(out var absoluteX)
                && cameraInfoScatterMap.Results[0][12].TryGetResult<int>(out var absoluteY)
                && cameraInfoScatterMap.Results[0][13].TryGetResult<int>(out var absoluteZ)) {
                    _absoluteLocation = new Vector3(absoluteX, absoluteY, absoluteZ);
                    // Program.Log(_absoluteLocation.ToString());
                }
                _localUPlayer.Position = location;
                _localUPlayer.Rotation = new Vector2(rotation.Y, rotation.X);
                _localUPlayer.Rotation3D = rotation;
                return true;
            }
            catch (DMAShutdown) { throw; }
            catch (Exception ex)
            {
                throw new GameNotRunningException($"ERROR getting CameraCache, game may not be running: {ex}");
            }
        }
        #endregion
    }

    #region Exceptions
    public class GameNotRunningException : Exception
    {
        public GameNotRunningException()
        {
        }

        public GameNotRunningException(string message)
            : base(message)
        {
        }

        public GameNotRunningException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class GameEnded : Exception
    {
        public GameEnded()
        {

        }

        public GameEnded(string message)
            : base(message)
        {
        }

        public GameEnded(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
    #endregion
}
