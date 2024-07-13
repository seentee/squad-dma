using System.Collections.ObjectModel;
using System.Numerics;
using SkiaSharp;
using SkiaSharp.Views.Desktop;

namespace squad_dma.Source
{
    public partial class OverlayForm : Form
    {
        private readonly object _renderLock = new();
        private static int ScreenCenterX = 1920 / 2;
        private static int ScreenCenterY = 1080 / 2;
        #region Getters
        /// <summary>
        /// Radar has found process and is ready.
        /// </summary>
        private static bool Ready
        {
            get => Memory.Ready;
        }

        /// <summary>
        /// Radar has found Local Game World.
        /// </summary>
        private static bool InGame
        {
            get => Memory.InGame;
        }
        /// <summary>
        /// LocalPlayer (who is running Radar) 'Player' object.
        /// </summary>
        private static UActor LocalPlayer
        {
            get => Memory.LocalPlayer;
        }

        /// <summary>
        /// All Actors in Game World (including tech)
        /// </summary>
        private static ReadOnlyDictionary<ulong, UActor> AllActors
        {
            get => Memory.Actors;
        }
        #endregion

        #region Constuctor
        public OverlayForm()
        {
            InitializeComponent();
            this.BackColor = Color.LimeGreen;
            this.TransparencyKey = Color.LimeGreen;
            this.FormBorderStyle = FormBorderStyle.None;

            this.Shown += Overlay_Shown;
            ScreenCenterX = overlayCanvas.Width / 2;
            ScreenCenterY = overlayCanvas.Height / 2;
        }
        #endregion

        #region Helper functions
        private static Vector3 WorldToScreenX(Vector3 WorldLocation)
        {
            Vector3 Rotation = LocalPlayer.Rotation3D;
            Matrix4x4 tempMatrix = Matrix(Rotation);

            Vector3 vAxisX, vAxisY, vAxisZ;

            vAxisX = new Vector3(tempMatrix.M11, tempMatrix.M12, tempMatrix.M13);
            vAxisY = new Vector3(tempMatrix.M21, tempMatrix.M22, tempMatrix.M23);
            vAxisZ = new Vector3(tempMatrix.M31, tempMatrix.M32, tempMatrix.M33);

            Vector3 vDelta = WorldLocation - LocalPlayer.Position;
            Vector3 vTransformed = new(Vector3.Dot(vDelta, vAxisY), Vector3.Dot(vDelta, vAxisZ), Vector3.Dot(vDelta, vAxisX));

            Vector3 Screenlocation = new(-1.0f, -1.0f, 0);
            if (vTransformed.Z < 1.0f)
                return Screenlocation;

            float FovAngle = 90; // todo

            Screenlocation.X = ScreenCenterX + vTransformed.X * (ScreenCenterX / (float) Math.Tan(FovAngle * (float) Math.PI / 360.0f)) / vTransformed.Z;
            Screenlocation.Y = ScreenCenterY - vTransformed.Y * (ScreenCenterX / (float) Math.Tan(FovAngle * (float) Math.PI / 360.0f)) / vTransformed.Z;

            return Screenlocation;
        }

        private static Matrix4x4 Matrix(Vector3 rot)
        {
            float radPitch = rot.X * (float) Math.PI / 180f;
            float radYaw = rot.Y * (float) Math.PI / 180f;
            float radRoll = rot.Z * (float) Math.PI / 180f;

            float SP = (float) Math.Sin(radPitch);
            float CP = (float) Math.Cos(radPitch);
            float SY = (float) Math.Sin(radYaw);
            float CY = (float) Math.Cos(radYaw);
            float SR = (float) Math.Sin(radRoll);
            float CR = (float) Math.Cos(radRoll);

            Matrix4x4 matrix;
            matrix.M11 = CP * CY;
            matrix.M12 = CP * SY;
            matrix.M13 = SP;
            matrix.M14 = 0.0f;

            matrix.M21 = SR * SP * CY - CR * SY;
            matrix.M22 = SR * SP * SY + CR * CY;
            matrix.M23 = -SR * CP;
            matrix.M24 = 0.0f;

            matrix.M31 = -(CR * SP * CY + SR * SY);
            matrix.M32 = CY * SR - CR * SP * SY;
            matrix.M33 = CR * CP;
            matrix.M34 = 0.0f;

            matrix.M41 = 0;
            matrix.M42 = 0;
            matrix.M43 = 0;
            matrix.M44 = 1.0f;

            return matrix;
        }

        private void DrawActors(SKCanvas canvas)
        {
            if (InGame)
            {
                var allPlayers = AllActors?.Select(x => x.Value);

                if (allPlayers is not null)
                {
                    foreach (var actor in allPlayers) // Draw actors
                    {
                        var actorPos = actor.Position;
                        if (actor.ActorType != ActorType.Player || !actor.IsAlive || (Math.Abs(actorPos.X) + Math.Abs(actorPos.Y) + Math.Abs(actorPos.Z) < 1.0))
                            continue;

                        // Draw
                        DrawActor(canvas, actor);
                    }
                }
            }
        }

        private void DrawActor(SKCanvas canvas, UActor actor)
        {
            Vector3 screenLocation = WorldToScreenX(actor.Position);

            if (screenLocation.X < 0 || screenLocation.Y < 0) {
                return;
            }

            var height = actor.Position.Z - LocalPlayer.Position.Z;
            var dist = Vector3.Distance(LocalPlayer.Position, actor.Position);

            canvas.DrawLine(new SKPoint(ScreenCenterX, ScreenCenterY * 2), new SKPoint(screenLocation.X, screenLocation.Y), actor.GetEntityPaint());
            canvas.DrawText($"{(int)Math.Round(height / 100)},{(int)Math.Round(dist / 100)}", screenLocation.X - 30, screenLocation.Y, actor.GetTextPaint());
            canvas.DrawText($"{(int)actor.Health}", screenLocation.X - 15, screenLocation.Y - 10, actor.GetTextPaint());
        }

        private static bool IsReadyToRender()
        {
            if (!Ready)
                return false; // Game process not running

            if (!InGame)
                return false; // Waiting for game start

            if (LocalPlayer is null)
                return false; // Cannot find local player

            return true; // Ready to render
        }

        private void DrawStatusText(SKCanvas canvas)
        {
            string statusText;
            if (!Ready)
            {
                statusText = "Game Process Not Running";
            }
            else if (!InGame)
            {
                statusText = "Waiting for Game Start...";
            }
            else if (LocalPlayer is null)
            {
                statusText = "Cannot find LocalPlayer";
            }
            else
            {
                return; // No status text to draw
            }

            canvas.DrawText(statusText, ScreenCenterX, ScreenCenterY, SKPaints.TextRadarStatus);
        }
        #endregion

        #region Event handlers
        private async void Overlay_Shown(object sender, EventArgs e)
        {
            while (overlayCanvas.GRContext is null)
                await Task.Delay(1);

            while (true)
            {
                await Task.Run(() => Thread.SpinWait(50000)); // High performance async delay
                overlayCanvas.Refresh(); // draw next frame
            }
        }

        private void OverlayCanvas_PaintSurface(object sender, SKPaintGLSurfaceEventArgs e)
        {
            try
            {
                SKCanvas canvas = e.Surface.Canvas;
                canvas.Clear(Color.LimeGreen.ToSKColor());

                if (IsReadyToRender())
                {
                    lock (_renderLock)
                    {
                        DrawActors(canvas);
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
        #endregion
    }
}
