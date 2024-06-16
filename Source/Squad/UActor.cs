using System.Diagnostics;
using System.Numerics;

namespace squad_dma
{
    /// <summary>
    /// Class containing Game Player Data.
    /// </summary>
    public class UActor
    {
        private readonly object _posLock = new(); // sync access to this.Position (non-atomic)

        #region PlayerProperties
        public uint NameId { get; set; }
        /// <summary>
        /// Player name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Player's current health
        /// </summary>
        public float Health { get; set; } = -1;
        /// <summary>
        /// Team
        /// </summary>
        public Team Team { get; set; } = Team.Unknown;
        /// <summary>
        /// Team
        /// </summary>
        public ActorType ActorType { get; set; } = ActorType.Player;

        private Vector3 _pos = new Vector3(0, 0, 0); // backing field
        /// <summary>
        /// Player's Unity Position in Local Game World.
        /// </summary>
        ///
        public Vector3 Position // 96 bits, cannot set atomically
        {
            get
            {
                lock (_posLock)
                {
                    return _pos;
                }
            }
            set
            {
                lock (_posLock)
                {
                    _pos = value;
                }
            }
        }
        /// <summary>
        /// Cached 'Zoomed Position' on the Radar GUI. Used for mouseover events.
        /// </summary>
        public Vector2 ZoomedPosition { get; set; } = new();
        /// <summary>
        /// Player's Rotation (direction/pitch) in Local Game World.
        /// 90 degree offset ~already~ applied to account for 2D-Map orientation.
        /// </summary>
        public Vector2 Rotation { get; set; } = new Vector2(0, 0); // 64 bits will be atomic
        /// <summary>
        /// Consecutive number of errors that this Player object has 'errored out' while updating.
        /// </summary>
        public int ErrorCount { get; set; } = 0;
        #endregion

        #region Getters
        /// <summary>
        /// Player Address
        /// </summary>
        public ulong Base { get; }
        /// <summary>
        /// Player is Alive/Not Dead.
        /// </summary>
        public bool IsAlive {
            get {
                return Health > 0.0;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Player Constructor.
        /// </summary>
        public UActor(ulong actorBase)
        {
            Debug.WriteLine("Actor Constructor: Initialization started.");

            this.Base = actorBase;
        }
        #endregion
    }
}
