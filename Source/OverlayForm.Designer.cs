namespace squad_dma.Source
{
    partial class OverlayForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            overlayCanvas = new SkiaSharp.Views.Desktop.SKGLControl();
            SuspendLayout();
            // 
            // overlayCanvas
            // 
            overlayCanvas.BackColor = Color.Black;
            overlayCanvas.Dock = DockStyle.Fill;
            overlayCanvas.Location = new Point(0, 0);
            overlayCanvas.Margin = new Padding(0);
            overlayCanvas.Name = "overlayCanvas";
            overlayCanvas.Size = new Size(1904, 1041);
            overlayCanvas.TabIndex = 0;
            overlayCanvas.VSync = false;
            overlayCanvas.PaintSurface += OverlayCanvas_PaintSurface;
            // 
            // OverlayForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1904, 1041);
            Controls.Add(overlayCanvas);
            Name = "OverlayForm";
            StartPosition = FormStartPosition.Manual;
            Text = "Overlay";
            TopMost = true;
            ResumeLayout(false);
        }

        #endregion

        private SkiaSharp.Views.Desktop.SKGLControl overlayCanvas;
    }
}