using SkiaSharp;

namespace squad_dma
{
    /// <summary>
    /// Extension methods go here.
    /// </summary>
    public static class Extensions
    {
        private static SKPaint textOutlinePaint = null;
        private static SKPaint projectilePaint = null;
        private static Dictionary<Team, SKPaint> teamEntityPaints = [];
        private static Dictionary<Team, SKPaint> teamTextPaints = [];
        private static readonly Dictionary<Team, SKColor> TeamColors = new(){
            {Team.RU, new SKColor(0, 255, 0)},
            {Team.US, new SKColor(255, 0, 0)},
            {Team.AU, new SKColor(255, 205, 0)},
            {Team.UK, new SKColor(200, 16, 46)},
            {Team.CA, new SKColor(255, 0, 0)},
            {Team.CN, new SKColor(0, 51, 102)},
            {Team.ME, new SKColor(30, 155, 227)},
            {Team.TR, new SKColor(64, 224, 208)},
            {Team.INS, new SKColor(173, 115, 0)},
            {Team.IMF, new SKColor(0, 64, 79)},
            {Team.Unknown, new SKColor(255, 255, 255)},
            {Team.GE_Wagner, new SKColor(128, 128, 128)},
            {Team.GE_UA, new SKColor(255, 255, 0)},
            {Team.GE_FI, new SKColor(0, 47, 108)},
            {Team.GE_IS, new SKColor(0, 56, 184)},
        };
        #region Generic Extensions
        /// <summary>
        /// Restarts a timer from 0. (Timer will be started if not already running)
        /// </summary>
        public static void Restart(this System.Timers.Timer t)
        {
            t.Stop();
            t.Start();
        }

        /// <summary>
        /// Converts 'Degrees' to 'Radians'.
        /// </summary>
        public static double ToRadians(this float degrees)
        {
            return (Math.PI / 180) * degrees;
        }
        #endregion

        #region GUI Extensions
        /// <summary>
        /// Convert game position to 'Bitmap' Map Position coordinates.
        /// </summary>
        public static MapPosition ToMapPos(this System.Numerics.Vector3 vector, Map map)
        {
            return new MapPosition()
            {
                X = map.ConfigFile.X + (vector.X * map.ConfigFile.Scale),
                Y = map.ConfigFile.Y + (vector.Y * map.ConfigFile.Scale), // Invert 'Y' unity 0,0 bottom left, C# top left
                Height = vector.Z // Keep as float, calculation done later
            };
        }

        /// <summary>
        /// Gets 'Zoomed' map position coordinates.
        /// </summary>
        public static MapPosition ToZoomedPos(this MapPosition location, MapParameters mapParams)
        {
            return new MapPosition()
            {
                UIScale = mapParams.UIScale,
                X = (location.X - mapParams.Bounds.Left) * mapParams.XScale,
                Y = (location.Y - mapParams.Bounds.Top) * mapParams.YScale,
                Height = location.Height
            };
        }

        /// <summary>
        /// Gets drawing paintbrush based on Player Type
        /// </summary>
        public static SKPaint GetEntityPaint(this UActor actor) {
            if (teamEntityPaints.TryGetValue(actor.Team, out SKPaint value)) {
                return value;
            }
            SKPaint basePaint = SKPaints.PaintBase.Clone();
            basePaint.Color = TeamColors[actor.Team];
            teamEntityPaints[actor.Team] = basePaint;
            return basePaint;
        }

        /// <summary>
        /// Gets text paintbrush based on Player Type
        /// </summary>
        public static SKPaint GetTextPaint(this UActor actor)
        {
            if (teamTextPaints.TryGetValue(actor.Team, out SKPaint value)) {
                return value;
            }
            SKPaint baseText = SKPaints.TextBase.Clone();
            baseText.Color = TeamColors[actor.Team];
            teamTextPaints[actor.Team] = baseText;
            return baseText;
        }

        /// <summary>
        /// Gets projectile drawing paintbrush
        /// </summary>
        public static SKPaint GetProjectilePaint(this UActor actor) {
            if (projectilePaint != null) {
                return projectilePaint;
            }
            SKPaint basePaint = SKPaints.PaintBase.Clone();
            basePaint.Color = new SKColor(255, 0, 255);
            projectilePaint = basePaint;
            return basePaint;
        }

        /// <summary>
        /// Determines the text outline color.
        /// </summary>
        public static SKPaint GetTextOutlinePaint()
        {
            if (textOutlinePaint != null) {
                return textOutlinePaint;
            }
            SKPaint paintToUse = SKPaints.TextBaseOutline.Clone();
            paintToUse.Color = new SKColor(0, 0, 0);
            textOutlinePaint = paintToUse;
            return paintToUse;
        }
        #endregion
    }
}
