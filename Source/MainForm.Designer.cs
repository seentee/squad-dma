namespace squad_dma
{
    partial class frmMain
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
            if (disposing && (components is not null))
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            colDialog = new ColorDialog();
            toolTip = new ToolTip(components);
            btnResetTheme = new MaterialSkin.Controls.MaterialButton();
            swMapHelper = new MaterialSkin.Controls.MaterialSwitch();
            btnToggleMap = new MaterialSkin.Controls.MaterialButton();
            btnRestartRadar = new MaterialSkin.Controls.MaterialButton();
            swRadarStats = new MaterialSkin.Controls.MaterialSwitch();
            swRadarVsync = new MaterialSkin.Controls.MaterialSwitch();
            swRadarEnemyCount = new MaterialSkin.Controls.MaterialSwitch();
            swAimview = new MaterialSkin.Controls.MaterialSwitch();
            swNames = new MaterialSkin.Controls.MaterialSwitch();
            sldrAimlineLength = new MaterialSkin.Controls.MaterialSlider();
            sldrUIScale = new MaterialSkin.Controls.MaterialSlider();
            sldrFontSize = new MaterialSkin.Controls.MaterialSlider();
            cboFont = new MaterialSkin.Controls.MaterialComboBox();
            txtMapSetupX = new MaterialSkin.Controls.MaterialTextBox2();
            txtMapSetupY = new MaterialSkin.Controls.MaterialTextBox2();
            txtMapSetupScale = new MaterialSkin.Controls.MaterialTextBox2();
            btnMapSetupApply = new MaterialSkin.Controls.MaterialButton();
            btnToggleMapFree = new MaterialSkin.Controls.MaterialButton();
            iconList = new ImageList(components);
            tabSettings = new TabPage();
            tabSelector = new MaterialSkin.Controls.MaterialTabSelector();
            tabControlSettings = new MaterialSkin.Controls.MaterialTabControl();
            tabSettingsGeneral = new TabPage();
            mcSettingsGeneralUI = new MaterialSkin.Controls.MaterialCard();
            sldrZoomSensitivity = new MaterialSkin.Controls.MaterialSlider();
            lblSettingsGeneralUI = new MaterialSkin.Controls.MaterialLabel();
            mcSettingsGeneralRadar = new MaterialSkin.Controls.MaterialCard();
            lblSettingsGeneralRadar = new MaterialSkin.Controls.MaterialLabel();
            tabSettingsColors = new TabPage();
            mcSettingsColorsOther = new MaterialSkin.Controls.MaterialCard();
            picOtherAccent = new PictureBox();
            lblSettingsColorOtherAccent = new MaterialSkin.Controls.MaterialLabel();
            picOtherPrimaryLight = new PictureBox();
            ssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss = new MaterialSkin.Controls.MaterialLabel();
            picOtherPrimaryDark = new PictureBox();
            lblSettingsColorOtherPrimaryDark = new MaterialSkin.Controls.MaterialLabel();
            picOtherPrimary = new PictureBox();
            lblSettingsColorOtherPrimary = new MaterialSkin.Controls.MaterialLabel();
            lblSettingsColorsOther = new MaterialSkin.Controls.MaterialLabel();
            tabRadar = new TabPage();
            mcRadarStats = new MaterialSkin.Controls.MaterialCard();
            lblRadarMemSValue = new MaterialSkin.Controls.MaterialLabel();
            lblRadarMemS = new MaterialSkin.Controls.MaterialLabel();
            lblRadarFPSValue = new MaterialSkin.Controls.MaterialLabel();
            lblRadarFPS = new MaterialSkin.Controls.MaterialLabel();
            mcRadarSettings = new MaterialSkin.Controls.MaterialCard();
            mcRadarMapSetup = new MaterialSkin.Controls.MaterialCard();
            lblRadarMapSetup = new MaterialSkin.Controls.MaterialLabel();
            skMapCanvas = new SkiaSharp.Views.Desktop.SKGLControl();
            tabControlMain = new MaterialSkin.Controls.MaterialTabControl();
            tabSettings.SuspendLayout();
            tabControlSettings.SuspendLayout();
            tabSettingsGeneral.SuspendLayout();
            mcSettingsGeneralUI.SuspendLayout();
            mcSettingsGeneralRadar.SuspendLayout();
            tabSettingsColors.SuspendLayout();
            mcSettingsColorsOther.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picOtherAccent).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picOtherPrimaryLight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picOtherPrimaryDark).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picOtherPrimary).BeginInit();
            tabRadar.SuspendLayout();
            mcRadarStats.SuspendLayout();
            mcRadarSettings.SuspendLayout();
            mcRadarMapSetup.SuspendLayout();
            tabControlMain.SuspendLayout();
            SuspendLayout();
            // 
            // colDialog
            // 
            colDialog.FullOpen = true;
            // 
            // toolTip
            // 
            toolTip.Popup += toolTip_Popup;
            // 
            // btnResetTheme
            // 
            btnResetTheme.AutoSize = false;
            btnResetTheme.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnResetTheme.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnResetTheme.Depth = 0;
            btnResetTheme.Font = new Font("Segoe UI", 8F);
            btnResetTheme.HighEmphasis = true;
            btnResetTheme.Icon = null;
            btnResetTheme.Location = new Point(81, 170);
            btnResetTheme.Margin = new Padding(4, 6, 4, 6);
            btnResetTheme.MouseState = MaterialSkin.MouseState.HOVER;
            btnResetTheme.Name = "btnResetTheme";
            btnResetTheme.NoAccentTextColor = Color.Empty;
            btnResetTheme.Size = new Size(105, 36);
            btnResetTheme.TabIndex = 59;
            btnResetTheme.Text = "Reset Theme";
            toolTip.SetToolTip(btnResetTheme, "Manually triggers radar restart");
            btnResetTheme.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            btnResetTheme.UseAccentColor = true;
            btnResetTheme.UseVisualStyleBackColor = true;
            btnResetTheme.Click += btnResetTheme_Click;
            // 
            // swMapHelper
            // 
            swMapHelper.Depth = 0;
            swMapHelper.Font = new Font("Segoe UI", 9F);
            swMapHelper.Location = new Point(15, 45);
            swMapHelper.Margin = new Padding(0);
            swMapHelper.MouseLocation = new Point(-1, -1);
            swMapHelper.MouseState = MaterialSkin.MouseState.HOVER;
            swMapHelper.Name = "swMapHelper";
            swMapHelper.Ripple = true;
            swMapHelper.Size = new Size(146, 28);
            swMapHelper.TabIndex = 0;
            swMapHelper.Text = "Map Helper";
            toolTip.SetToolTip(swMapHelper, "Shows the 'Map Setup' panel");
            swMapHelper.UseVisualStyleBackColor = true;
            swMapHelper.CheckedChanged += swMapHelper_CheckedChanged;
            // 
            // btnToggleMap
            // 
            btnToggleMap.AutoSize = false;
            btnToggleMap.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnToggleMap.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnToggleMap.Depth = 0;
            btnToggleMap.HighEmphasis = true;
            btnToggleMap.Icon = null;
            btnToggleMap.Location = new Point(482, 77);
            btnToggleMap.Margin = new Padding(4, 6, 4, 6);
            btnToggleMap.MouseState = MaterialSkin.MouseState.HOVER;
            btnToggleMap.Name = "btnToggleMap";
            btnToggleMap.NoAccentTextColor = Color.Empty;
            btnToggleMap.Size = new Size(143, 36);
            btnToggleMap.TabIndex = 1;
            btnToggleMap.Text = "Toggle Map (F5)";
            toolTip.SetToolTip(btnToggleMap, "Manually toggles active map");
            btnToggleMap.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            btnToggleMap.UseAccentColor = true;
            btnToggleMap.UseVisualStyleBackColor = true;
            btnToggleMap.Click += btnToggleMap_Click;
            // 
            // btnRestartRadar
            // 
            btnRestartRadar.AutoSize = false;
            btnRestartRadar.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnRestartRadar.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnRestartRadar.Depth = 0;
            btnRestartRadar.Font = new Font("Segoe UI", 8F);
            btnRestartRadar.HighEmphasis = true;
            btnRestartRadar.Icon = null;
            btnRestartRadar.Location = new Point(331, 77);
            btnRestartRadar.Margin = new Padding(4, 6, 4, 6);
            btnRestartRadar.MouseState = MaterialSkin.MouseState.HOVER;
            btnRestartRadar.Name = "btnRestartRadar";
            btnRestartRadar.NoAccentTextColor = Color.Empty;
            btnRestartRadar.Size = new Size(143, 36);
            btnRestartRadar.TabIndex = 2;
            btnRestartRadar.Text = "Restart Radar";
            toolTip.SetToolTip(btnRestartRadar, "Manually triggers radar restart");
            btnRestartRadar.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            btnRestartRadar.UseAccentColor = true;
            btnRestartRadar.UseVisualStyleBackColor = true;
            btnRestartRadar.Click += btnRestartRadar_Click;
            // 
            // swRadarStats
            // 
            swRadarStats.Depth = 0;
            swRadarStats.Font = new Font("Segoe UI", 9F);
            swRadarStats.Location = new Point(15, 85);
            swRadarStats.Margin = new Padding(0);
            swRadarStats.MouseLocation = new Point(-1, -1);
            swRadarStats.MouseState = MaterialSkin.MouseState.HOVER;
            swRadarStats.Name = "swRadarStats";
            swRadarStats.Ripple = true;
            swRadarStats.Size = new Size(144, 28);
            swRadarStats.TabIndex = 31;
            swRadarStats.Text = "Radar Stats";
            toolTip.SetToolTip(swRadarStats, "Shows radar stats on radar tab");
            swRadarStats.UseVisualStyleBackColor = true;
            swRadarStats.CheckedChanged += swRadarStats_CheckedChanged;
            // 
            // swRadarVsync
            // 
            swRadarVsync.Depth = 0;
            swRadarVsync.Font = new Font("Segoe UI", 9F);
            swRadarVsync.Location = new Point(172, 45);
            swRadarVsync.Margin = new Padding(0);
            swRadarVsync.MouseLocation = new Point(-1, -1);
            swRadarVsync.MouseState = MaterialSkin.MouseState.HOVER;
            swRadarVsync.Name = "swRadarVsync";
            swRadarVsync.Ripple = true;
            swRadarVsync.Size = new Size(118, 28);
            swRadarVsync.TabIndex = 32;
            swRadarVsync.Text = "VSync";
            toolTip.SetToolTip(swRadarVsync, "Shows radar stats on radar tab");
            swRadarVsync.UseVisualStyleBackColor = true;
            swRadarVsync.CheckedChanged += swRadarVsync_CheckedChanged;
            // 
            // swRadarEnemyCount
            // 
            swRadarEnemyCount.Depth = 0;
            swRadarEnemyCount.Font = new Font("Segoe UI", 9F);
            swRadarEnemyCount.Location = new Point(172, 85);
            swRadarEnemyCount.Margin = new Padding(0);
            swRadarEnemyCount.MouseLocation = new Point(-1, -1);
            swRadarEnemyCount.MouseState = MaterialSkin.MouseState.HOVER;
            swRadarEnemyCount.Name = "swRadarEnemyCount";
            swRadarEnemyCount.Ripple = true;
            swRadarEnemyCount.Size = new Size(152, 28);
            swRadarEnemyCount.TabIndex = 33;
            swRadarEnemyCount.Text = "Enemy Count";
            toolTip.SetToolTip(swRadarEnemyCount, "Shows radar stats on radar tab");
            swRadarEnemyCount.UseVisualStyleBackColor = true;
            swRadarEnemyCount.CheckedChanged += swRadarEnemyCount_CheckedChanged;
            // 
            // swAimview
            // 
            swAimview.Depth = 0;
            swAimview.Font = new Font("Segoe UI", 9F);
            swAimview.Location = new Point(17, 44);
            swAimview.Margin = new Padding(0);
            swAimview.MouseLocation = new Point(-1, -1);
            swAimview.MouseState = MaterialSkin.MouseState.HOVER;
            swAimview.Name = "swAimview";
            swAimview.Ripple = true;
            swAimview.Size = new Size(159, 28);
            swAimview.TabIndex = 2;
            swAimview.Text = "Aimview (F4)";
            toolTip.SetToolTip(swAimview, "Displays the 3D aimview");
            swAimview.UseVisualStyleBackColor = true;
            swAimview.CheckedChanged += swAimview_CheckedChanged;
            // 
            // swNames
            // 
            swNames.Depth = 0;
            swNames.Font = new Font("Segoe UI", 9F);
            swNames.Location = new Point(172, 44);
            swNames.Margin = new Padding(0);
            swNames.MouseLocation = new Point(-1, -1);
            swNames.MouseState = MaterialSkin.MouseState.HOVER;
            swNames.Name = "swNames";
            swNames.Ripple = true;
            swNames.Size = new Size(140, 28);
            swNames.TabIndex = 3;
            swNames.Text = "Names (F6)";
            toolTip.SetToolTip(swNames, "Shows player names on the radar");
            swNames.UseVisualStyleBackColor = true;
            swNames.CheckedChanged += swNames_CheckedChanged;
            // 
            // sldrAimlineLength
            // 
            sldrAimlineLength.Depth = 0;
            sldrAimlineLength.ForeColor = Color.Black;
            sldrAimlineLength.Location = new Point(17, 75);
            sldrAimlineLength.MouseState = MaterialSkin.MouseState.HOVER;
            sldrAimlineLength.Name = "sldrAimlineLength";
            sldrAimlineLength.RangeMax = 1000;
            sldrAimlineLength.RangeMin = 10;
            sldrAimlineLength.Size = new Size(304, 40);
            sldrAimlineLength.TabIndex = 30;
            sldrAimlineLength.Text = "Aimline Length";
            toolTip.SetToolTip(sldrAimlineLength, "Length of the 'bar' or 'aim line' on the localplayer");
            sldrAimlineLength.UseAccentColor = true;
            sldrAimlineLength.Value = 500;
            sldrAimlineLength.ValueMax = 1000;
            sldrAimlineLength.ValueSuffix = "m";
            // 
            // sldrUIScale
            // 
            sldrUIScale.Depth = 0;
            sldrUIScale.ForeColor = Color.Black;
            sldrUIScale.Location = new Point(331, 75);
            sldrUIScale.MouseState = MaterialSkin.MouseState.HOVER;
            sldrUIScale.Name = "sldrUIScale";
            sldrUIScale.RangeMax = 200;
            sldrUIScale.RangeMin = 10;
            sldrUIScale.Size = new Size(241, 40);
            sldrUIScale.TabIndex = 31;
            sldrUIScale.Text = "UI Scale";
            toolTip.SetToolTip(sldrUIScale, "Scales the UI fonts etc, useful for larger screen resolutions");
            sldrUIScale.UseAccentColor = true;
            sldrUIScale.Value = 100;
            sldrUIScale.ValueMax = 200;
            sldrUIScale.ValueSuffix = "%";
            sldrUIScale.onValueChanged += sldrUIScale_onValueChanged;
            // 
            // sldrFontSize
            // 
            sldrFontSize.Depth = 0;
            sldrFontSize.ForeColor = Color.Black;
            sldrFontSize.Location = new Point(194, 121);
            sldrFontSize.MouseState = MaterialSkin.MouseState.HOVER;
            sldrFontSize.Name = "sldrFontSize";
            sldrFontSize.RangeMax = 36;
            sldrFontSize.RangeMin = 1;
            sldrFontSize.Size = new Size(241, 40);
            sldrFontSize.TabIndex = 39;
            sldrFontSize.Text = "Font Size";
            toolTip.SetToolTip(sldrFontSize, "The font size to use for the text on the radar");
            sldrFontSize.UseAccentColor = true;
            sldrFontSize.Value = 13;
            sldrFontSize.ValueMax = 36;
            sldrFontSize.ValueSuffix = "px";
            sldrFontSize.onValueChanged += sldrFontSize_onValueChanged;
            // 
            // cboFont
            // 
            cboFont.AutoResize = false;
            cboFont.BackColor = Color.FromArgb(255, 255, 255);
            cboFont.Depth = 0;
            cboFont.DrawMode = DrawMode.OwnerDrawVariable;
            cboFont.DropDownHeight = 292;
            cboFont.DropDownStyle = ComboBoxStyle.DropDownList;
            cboFont.DropDownWidth = 121;
            cboFont.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            cboFont.ForeColor = Color.FromArgb(222, 0, 0, 0);
            cboFont.FormattingEnabled = true;
            cboFont.Hint = "Font";
            cboFont.IntegralHeight = false;
            cboFont.ItemHeight = 29;
            cboFont.Items.AddRange(new object[] { "Arial", "Calibri", "Candara", "Consolas", "Constantia", "Corbel", "Helvetica", "Lato", "Roboto", "Segoe UI", "Tahoma", "Trebuchet MS", "Verdana" });
            cboFont.Location = new Point(17, 121);
            cboFont.MaxDropDownItems = 10;
            cboFont.MouseState = MaterialSkin.MouseState.OUT;
            cboFont.Name = "cboFont";
            cboFont.Size = new Size(171, 35);
            cboFont.StartIndex = 0;
            cboFont.TabIndex = 39;
            toolTip.SetToolTip(cboFont, "The item to add to the loot filter");
            cboFont.UseTallSize = false;
            cboFont.SelectedIndexChanged += cboFont_SelectedIndexChanged;
            // 
            // txtMapSetupX
            // 
            txtMapSetupX.AnimateReadOnly = false;
            txtMapSetupX.BackgroundImageLayout = ImageLayout.None;
            txtMapSetupX.CharacterCasing = CharacterCasing.Normal;
            txtMapSetupX.Depth = 0;
            txtMapSetupX.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            txtMapSetupX.HelperText = "X";
            txtMapSetupX.HideSelection = true;
            txtMapSetupX.Hint = "X";
            txtMapSetupX.LeadingIcon = null;
            txtMapSetupX.Location = new Point(17, 45);
            txtMapSetupX.MaxLength = 32767;
            txtMapSetupX.MouseState = MaterialSkin.MouseState.OUT;
            txtMapSetupX.Name = "txtMapSetupX";
            txtMapSetupX.PasswordChar = '\0';
            txtMapSetupX.PrefixSuffixText = null;
            txtMapSetupX.ReadOnly = false;
            txtMapSetupX.RightToLeft = RightToLeft.No;
            txtMapSetupX.SelectedText = "";
            txtMapSetupX.SelectionLength = 0;
            txtMapSetupX.SelectionStart = 0;
            txtMapSetupX.ShortcutsEnabled = true;
            txtMapSetupX.Size = new Size(78, 36);
            txtMapSetupX.TabIndex = 32;
            txtMapSetupX.TabStop = false;
            txtMapSetupX.TextAlign = HorizontalAlignment.Left;
            toolTip.SetToolTip(txtMapSetupX, "The X coordinate");
            txtMapSetupX.TrailingIcon = null;
            txtMapSetupX.UseSystemPasswordChar = false;
            txtMapSetupX.UseTallSize = false;
            // 
            // txtMapSetupY
            // 
            txtMapSetupY.AnimateReadOnly = false;
            txtMapSetupY.BackgroundImageLayout = ImageLayout.None;
            txtMapSetupY.CharacterCasing = CharacterCasing.Normal;
            txtMapSetupY.Depth = 0;
            txtMapSetupY.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            txtMapSetupY.HelperText = "Y";
            txtMapSetupY.HideSelection = true;
            txtMapSetupY.Hint = "Y";
            txtMapSetupY.LeadingIcon = null;
            txtMapSetupY.Location = new Point(101, 45);
            txtMapSetupY.MaxLength = 32767;
            txtMapSetupY.MouseState = MaterialSkin.MouseState.OUT;
            txtMapSetupY.Name = "txtMapSetupY";
            txtMapSetupY.PasswordChar = '\0';
            txtMapSetupY.PrefixSuffixText = null;
            txtMapSetupY.ReadOnly = false;
            txtMapSetupY.RightToLeft = RightToLeft.No;
            txtMapSetupY.SelectedText = "";
            txtMapSetupY.SelectionLength = 0;
            txtMapSetupY.SelectionStart = 0;
            txtMapSetupY.ShortcutsEnabled = true;
            txtMapSetupY.Size = new Size(78, 36);
            txtMapSetupY.TabIndex = 33;
            txtMapSetupY.TabStop = false;
            txtMapSetupY.TextAlign = HorizontalAlignment.Left;
            toolTip.SetToolTip(txtMapSetupY, "The Y coordinate");
            txtMapSetupY.TrailingIcon = null;
            txtMapSetupY.UseSystemPasswordChar = false;
            txtMapSetupY.UseTallSize = false;
            // 
            // txtMapSetupScale
            // 
            txtMapSetupScale.AnimateReadOnly = false;
            txtMapSetupScale.BackgroundImageLayout = ImageLayout.None;
            txtMapSetupScale.CharacterCasing = CharacterCasing.Normal;
            txtMapSetupScale.Depth = 0;
            txtMapSetupScale.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            txtMapSetupScale.HelperText = "Scale";
            txtMapSetupScale.HideSelection = true;
            txtMapSetupScale.Hint = "Scale";
            txtMapSetupScale.LeadingIcon = null;
            txtMapSetupScale.Location = new Point(185, 45);
            txtMapSetupScale.MaxLength = 32767;
            txtMapSetupScale.MouseState = MaterialSkin.MouseState.OUT;
            txtMapSetupScale.Name = "txtMapSetupScale";
            txtMapSetupScale.PasswordChar = '\0';
            txtMapSetupScale.PrefixSuffixText = null;
            txtMapSetupScale.ReadOnly = false;
            txtMapSetupScale.RightToLeft = RightToLeft.No;
            txtMapSetupScale.SelectedText = "";
            txtMapSetupScale.SelectionLength = 0;
            txtMapSetupScale.SelectionStart = 0;
            txtMapSetupScale.ShortcutsEnabled = true;
            txtMapSetupScale.Size = new Size(78, 36);
            txtMapSetupScale.TabIndex = 34;
            txtMapSetupScale.TabStop = false;
            txtMapSetupScale.TextAlign = HorizontalAlignment.Left;
            toolTip.SetToolTip(txtMapSetupScale, "The scale");
            txtMapSetupScale.TrailingIcon = null;
            txtMapSetupScale.UseSystemPasswordChar = false;
            txtMapSetupScale.UseTallSize = false;
            // 
            // btnMapSetupApply
            // 
            btnMapSetupApply.AutoSize = false;
            btnMapSetupApply.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnMapSetupApply.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnMapSetupApply.Depth = 0;
            btnMapSetupApply.Font = new Font("Segoe UI", 8F);
            btnMapSetupApply.HighEmphasis = true;
            btnMapSetupApply.Icon = null;
            btnMapSetupApply.Location = new Point(369, 45);
            btnMapSetupApply.Margin = new Padding(4, 6, 4, 6);
            btnMapSetupApply.MouseState = MaterialSkin.MouseState.HOVER;
            btnMapSetupApply.Name = "btnMapSetupApply";
            btnMapSetupApply.NoAccentTextColor = Color.Empty;
            btnMapSetupApply.Size = new Size(57, 36);
            btnMapSetupApply.TabIndex = 35;
            btnMapSetupApply.Text = "Apply";
            toolTip.SetToolTip(btnMapSetupApply, "Manually triggers radar restart");
            btnMapSetupApply.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            btnMapSetupApply.UseAccentColor = true;
            btnMapSetupApply.UseVisualStyleBackColor = true;
            btnMapSetupApply.Click += btnMapSetupApply_Click;
            // 
            // btnToggleMapFree
            // 
            btnToggleMapFree.AutoSize = false;
            btnToggleMapFree.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnToggleMapFree.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnToggleMapFree.Depth = 0;
            btnToggleMapFree.HighEmphasis = true;
            btnToggleMapFree.Icon = Properties.Resources.tick;
            btnToggleMapFree.Location = new Point(6, 5);
            btnToggleMapFree.Margin = new Padding(4, 6, 4, 6);
            btnToggleMapFree.MouseState = MaterialSkin.MouseState.HOVER;
            btnToggleMapFree.Name = "btnToggleMapFree";
            btnToggleMapFree.NoAccentTextColor = Color.Empty;
            btnToggleMapFree.Size = new Size(107, 36);
            btnToggleMapFree.TabIndex = 47;
            btnToggleMapFree.Text = "Follow";
            toolTip.SetToolTip(btnToggleMapFree, "Toggles radar following functionality");
            btnToggleMapFree.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Text;
            btnToggleMapFree.UseAccentColor = true;
            btnToggleMapFree.UseVisualStyleBackColor = true;
            btnToggleMapFree.Click += btnToggleMapFree_Click;
            // 
            // iconList
            // 
            iconList.ColorDepth = ColorDepth.Depth32Bit;
            iconList.ImageStream = (ImageListStreamer)resources.GetObject("iconList.ImageStream");
            iconList.TransparentColor = Color.Transparent;
            iconList.Images.SetKeyName(0, "radar.png");
            iconList.Images.SetKeyName(1, "settings.png");
            iconList.Images.SetKeyName(2, "loadouts.png");
            iconList.Images.SetKeyName(3, "watchlist.png");
            iconList.Images.SetKeyName(4, "loot.png");
            // 
            // tabSettings
            // 
            tabSettings.BackColor = Color.White;
            tabSettings.Controls.Add(tabSelector);
            tabSettings.Controls.Add(tabControlSettings);
            tabSettings.ImageKey = "settings.png";
            tabSettings.Location = new Point(4, 39);
            tabSettings.Margin = new Padding(0);
            tabSettings.Name = "tabSettings";
            tabSettings.Size = new Size(2092, 1044);
            tabSettings.TabIndex = 1;
            tabSettings.Text = "Settings";
            // 
            // tabSelector
            // 
            tabSelector.BaseTabControl = tabControlSettings;
            tabSelector.CharacterCasing = MaterialSkin.Controls.MaterialTabSelector.CustomCharacterCasing.Normal;
            tabSelector.Depth = 0;
            tabSelector.Dock = DockStyle.Top;
            tabSelector.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            tabSelector.Location = new Point(0, 0);
            tabSelector.Margin = new Padding(0);
            tabSelector.MouseState = MaterialSkin.MouseState.HOVER;
            tabSelector.Name = "tabSelector";
            tabSelector.Size = new Size(2092, 36);
            tabSelector.TabIndex = 29;
            tabSelector.TabIndicatorHeight = 1;
            tabSelector.Text = "tabSelectorSettings";
            // 
            // tabControlSettings
            // 
            tabControlSettings.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            tabControlSettings.Controls.Add(tabSettingsGeneral);
            tabControlSettings.Controls.Add(tabSettingsColors);
            tabControlSettings.Depth = 0;
            tabControlSettings.Font = new Font("Segoe UI", 9F);
            tabControlSettings.Location = new Point(0, 36);
            tabControlSettings.Margin = new Padding(0);
            tabControlSettings.MouseState = MaterialSkin.MouseState.HOVER;
            tabControlSettings.Multiline = true;
            tabControlSettings.Name = "tabControlSettings";
            tabControlSettings.SelectedIndex = 0;
            tabControlSettings.Size = new Size(1289, 667);
            tabControlSettings.TabIndex = 28;
            // 
            // tabSettingsGeneral
            // 
            tabSettingsGeneral.BackColor = Color.White;
            tabSettingsGeneral.Controls.Add(mcSettingsGeneralUI);
            tabSettingsGeneral.Controls.Add(mcSettingsGeneralRadar);
            tabSettingsGeneral.ImageKey = "(none)";
            tabSettingsGeneral.Location = new Point(4, 24);
            tabSettingsGeneral.Margin = new Padding(0);
            tabSettingsGeneral.Name = "tabSettingsGeneral";
            tabSettingsGeneral.Size = new Size(1281, 639);
            tabSettingsGeneral.TabIndex = 0;
            tabSettingsGeneral.Text = "General";
            // 
            // mcSettingsGeneralUI
            // 
            mcSettingsGeneralUI.BackColor = Color.FromArgb(255, 255, 255);
            mcSettingsGeneralUI.Controls.Add(sldrZoomSensitivity);
            mcSettingsGeneralUI.Controls.Add(cboFont);
            mcSettingsGeneralUI.Controls.Add(sldrFontSize);
            mcSettingsGeneralUI.Controls.Add(lblSettingsGeneralUI);
            mcSettingsGeneralUI.Controls.Add(sldrUIScale);
            mcSettingsGeneralUI.Controls.Add(sldrAimlineLength);
            mcSettingsGeneralUI.Controls.Add(swNames);
            mcSettingsGeneralUI.Controls.Add(swAimview);
            mcSettingsGeneralUI.Depth = 0;
            mcSettingsGeneralUI.ForeColor = Color.FromArgb(222, 0, 0, 0);
            mcSettingsGeneralUI.Location = new Point(15, 166);
            mcSettingsGeneralUI.Margin = new Padding(14);
            mcSettingsGeneralUI.MouseState = MaterialSkin.MouseState.HOVER;
            mcSettingsGeneralUI.Name = "mcSettingsGeneralUI";
            mcSettingsGeneralUI.Padding = new Padding(14);
            mcSettingsGeneralUI.Size = new Size(633, 220);
            mcSettingsGeneralUI.TabIndex = 31;
            // 
            // sldrZoomSensitivity
            // 
            sldrZoomSensitivity.Depth = 0;
            sldrZoomSensitivity.ForeColor = Color.Black;
            sldrZoomSensitivity.Location = new Point(17, 167);
            sldrZoomSensitivity.MouseState = MaterialSkin.MouseState.HOVER;
            sldrZoomSensitivity.Name = "sldrZoomSensitivity";
            sldrZoomSensitivity.RangeMax = 30;
            sldrZoomSensitivity.RangeMin = 1;
            sldrZoomSensitivity.Size = new Size(304, 40);
            sldrZoomSensitivity.TabIndex = 41;
            sldrZoomSensitivity.Text = "Zoom Sensitivity";
            sldrZoomSensitivity.UseAccentColor = true;
            sldrZoomSensitivity.Value = 15;
            sldrZoomSensitivity.ValueMax = 30;
            sldrZoomSensitivity.onValueChanged += sldrZoomSensitivity_onValueChanged;
            // 
            // lblSettingsGeneralUI
            // 
            lblSettingsGeneralUI.AutoSize = true;
            lblSettingsGeneralUI.Depth = 0;
            lblSettingsGeneralUI.Font = new Font("Roboto Medium", 20F, FontStyle.Bold, GraphicsUnit.Pixel);
            lblSettingsGeneralUI.FontType = MaterialSkin.MaterialSkinManager.fontType.H6;
            lblSettingsGeneralUI.HighEmphasis = true;
            lblSettingsGeneralUI.Location = new Point(17, 12);
            lblSettingsGeneralUI.MouseState = MaterialSkin.MouseState.HOVER;
            lblSettingsGeneralUI.Name = "lblSettingsGeneralUI";
            lblSettingsGeneralUI.Size = new Size(128, 24);
            lblSettingsGeneralUI.TabIndex = 33;
            lblSettingsGeneralUI.Text = "User Interface";
            lblSettingsGeneralUI.UseAccent = true;
            // 
            // mcSettingsGeneralRadar
            // 
            mcSettingsGeneralRadar.BackColor = Color.FromArgb(255, 255, 255);
            mcSettingsGeneralRadar.Controls.Add(swRadarEnemyCount);
            mcSettingsGeneralRadar.Controls.Add(swRadarVsync);
            mcSettingsGeneralRadar.Controls.Add(swRadarStats);
            mcSettingsGeneralRadar.Controls.Add(lblSettingsGeneralRadar);
            mcSettingsGeneralRadar.Controls.Add(btnRestartRadar);
            mcSettingsGeneralRadar.Controls.Add(btnToggleMap);
            mcSettingsGeneralRadar.Controls.Add(swMapHelper);
            mcSettingsGeneralRadar.Depth = 0;
            mcSettingsGeneralRadar.ForeColor = Color.FromArgb(222, 0, 0, 0);
            mcSettingsGeneralRadar.Location = new Point(15, 14);
            mcSettingsGeneralRadar.Margin = new Padding(14);
            mcSettingsGeneralRadar.MouseState = MaterialSkin.MouseState.HOVER;
            mcSettingsGeneralRadar.Name = "mcSettingsGeneralRadar";
            mcSettingsGeneralRadar.Padding = new Padding(14);
            mcSettingsGeneralRadar.Size = new Size(633, 136);
            mcSettingsGeneralRadar.TabIndex = 30;
            // 
            // lblSettingsGeneralRadar
            // 
            lblSettingsGeneralRadar.AutoSize = true;
            lblSettingsGeneralRadar.Depth = 0;
            lblSettingsGeneralRadar.Font = new Font("Roboto Medium", 20F, FontStyle.Bold, GraphicsUnit.Pixel);
            lblSettingsGeneralRadar.FontType = MaterialSkin.MaterialSkinManager.fontType.H6;
            lblSettingsGeneralRadar.HighEmphasis = true;
            lblSettingsGeneralRadar.Location = new Point(17, 15);
            lblSettingsGeneralRadar.MouseState = MaterialSkin.MouseState.HOVER;
            lblSettingsGeneralRadar.Name = "lblSettingsGeneralRadar";
            lblSettingsGeneralRadar.Size = new Size(53, 24);
            lblSettingsGeneralRadar.TabIndex = 30;
            lblSettingsGeneralRadar.Text = "Radar";
            lblSettingsGeneralRadar.UseAccent = true;
            // 
            // tabSettingsColors
            // 
            tabSettingsColors.BackColor = Color.White;
            tabSettingsColors.Controls.Add(mcSettingsColorsOther);
            tabSettingsColors.Location = new Point(4, 24);
            tabSettingsColors.Margin = new Padding(0);
            tabSettingsColors.Name = "tabSettingsColors";
            tabSettingsColors.Size = new Size(1281, 639);
            tabSettingsColors.TabIndex = 4;
            tabSettingsColors.Text = "Colors";
            // 
            // mcSettingsColorsOther
            // 
            mcSettingsColorsOther.BackColor = Color.FromArgb(255, 255, 255);
            mcSettingsColorsOther.Controls.Add(btnResetTheme);
            mcSettingsColorsOther.Controls.Add(picOtherAccent);
            mcSettingsColorsOther.Controls.Add(lblSettingsColorOtherAccent);
            mcSettingsColorsOther.Controls.Add(picOtherPrimaryLight);
            mcSettingsColorsOther.Controls.Add(ssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss);
            mcSettingsColorsOther.Controls.Add(picOtherPrimaryDark);
            mcSettingsColorsOther.Controls.Add(lblSettingsColorOtherPrimaryDark);
            mcSettingsColorsOther.Controls.Add(picOtherPrimary);
            mcSettingsColorsOther.Controls.Add(lblSettingsColorOtherPrimary);
            mcSettingsColorsOther.Controls.Add(lblSettingsColorsOther);
            mcSettingsColorsOther.Depth = 0;
            mcSettingsColorsOther.ForeColor = Color.FromArgb(222, 0, 0, 0);
            mcSettingsColorsOther.Location = new Point(14, 14);
            mcSettingsColorsOther.Margin = new Padding(14);
            mcSettingsColorsOther.MouseState = MaterialSkin.MouseState.HOVER;
            mcSettingsColorsOther.Name = "mcSettingsColorsOther";
            mcSettingsColorsOther.Padding = new Padding(14);
            mcSettingsColorsOther.Size = new Size(186, 220);
            mcSettingsColorsOther.TabIndex = 43;
            // 
            // picOtherAccent
            // 
            picOtherAccent.BackColor = Color.Transparent;
            picOtherAccent.BorderStyle = BorderStyle.FixedSingle;
            picOtherAccent.Location = new Point(99, 138);
            picOtherAccent.Name = "picOtherAccent";
            picOtherAccent.Size = new Size(70, 23);
            picOtherAccent.TabIndex = 58;
            picOtherAccent.TabStop = false;
            picOtherAccent.Click += picOtherAccent_Click;
            // 
            // lblSettingsColorOtherAccent
            // 
            lblSettingsColorOtherAccent.AutoSize = true;
            lblSettingsColorOtherAccent.Depth = 0;
            lblSettingsColorOtherAccent.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblSettingsColorOtherAccent.FontType = MaterialSkin.MaterialSkinManager.fontType.Body2;
            lblSettingsColorOtherAccent.Location = new Point(46, 138);
            lblSettingsColorOtherAccent.MouseState = MaterialSkin.MouseState.HOVER;
            lblSettingsColorOtherAccent.Name = "lblSettingsColorOtherAccent";
            lblSettingsColorOtherAccent.Size = new Size(47, 17);
            lblSettingsColorOtherAccent.TabIndex = 57;
            lblSettingsColorOtherAccent.Text = "Accent:";
            // 
            // picOtherPrimaryLight
            // 
            picOtherPrimaryLight.BackColor = Color.Transparent;
            picOtherPrimaryLight.BorderStyle = BorderStyle.FixedSingle;
            picOtherPrimaryLight.Location = new Point(99, 108);
            picOtherPrimaryLight.Name = "picOtherPrimaryLight";
            picOtherPrimaryLight.Size = new Size(70, 23);
            picOtherPrimaryLight.TabIndex = 56;
            picOtherPrimaryLight.TabStop = false;
            picOtherPrimaryLight.Click += picOtherPrimaryLight_Click;
            // 
            // ssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss
            // 
            ssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss.AutoSize = true;
            ssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss.Depth = 0;
            ssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Pixel);
            ssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss.FontType = MaterialSkin.MaterialSkinManager.fontType.Body2;
            ssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss.Location = new Point(5, 108);
            ssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss.MouseState = MaterialSkin.MouseState.HOVER;
            ssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss.Name = "ssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss";
            ssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss.Size = new Size(88, 17);
            ssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss.TabIndex = 55;
            ssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss.Text = "Primary Light:";
            // 
            // picOtherPrimaryDark
            // 
            picOtherPrimaryDark.BackColor = Color.Transparent;
            picOtherPrimaryDark.BorderStyle = BorderStyle.FixedSingle;
            picOtherPrimaryDark.Location = new Point(99, 78);
            picOtherPrimaryDark.Name = "picOtherPrimaryDark";
            picOtherPrimaryDark.Size = new Size(70, 23);
            picOtherPrimaryDark.TabIndex = 54;
            picOtherPrimaryDark.TabStop = false;
            picOtherPrimaryDark.Click += picOtherPrimaryDark_Click;
            // 
            // lblSettingsColorOtherPrimaryDark
            // 
            lblSettingsColorOtherPrimaryDark.AutoSize = true;
            lblSettingsColorOtherPrimaryDark.Depth = 0;
            lblSettingsColorOtherPrimaryDark.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblSettingsColorOtherPrimaryDark.FontType = MaterialSkin.MaterialSkinManager.fontType.Body2;
            lblSettingsColorOtherPrimaryDark.Location = new Point(8, 78);
            lblSettingsColorOtherPrimaryDark.MouseState = MaterialSkin.MouseState.HOVER;
            lblSettingsColorOtherPrimaryDark.Name = "lblSettingsColorOtherPrimaryDark";
            lblSettingsColorOtherPrimaryDark.Size = new Size(85, 17);
            lblSettingsColorOtherPrimaryDark.TabIndex = 53;
            lblSettingsColorOtherPrimaryDark.Text = "Primary Dark:";
            // 
            // picOtherPrimary
            // 
            picOtherPrimary.BackColor = Color.Transparent;
            picOtherPrimary.BorderStyle = BorderStyle.FixedSingle;
            picOtherPrimary.Location = new Point(99, 48);
            picOtherPrimary.Name = "picOtherPrimary";
            picOtherPrimary.Size = new Size(70, 23);
            picOtherPrimary.TabIndex = 52;
            picOtherPrimary.TabStop = false;
            picOtherPrimary.Click += picOtherPrimary_Click;
            // 
            // lblSettingsColorOtherPrimary
            // 
            lblSettingsColorOtherPrimary.AutoSize = true;
            lblSettingsColorOtherPrimary.Depth = 0;
            lblSettingsColorOtherPrimary.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblSettingsColorOtherPrimary.FontType = MaterialSkin.MaterialSkinManager.fontType.Body2;
            lblSettingsColorOtherPrimary.Location = new Point(40, 48);
            lblSettingsColorOtherPrimary.MouseState = MaterialSkin.MouseState.HOVER;
            lblSettingsColorOtherPrimary.Name = "lblSettingsColorOtherPrimary";
            lblSettingsColorOtherPrimary.Size = new Size(53, 17);
            lblSettingsColorOtherPrimary.TabIndex = 51;
            lblSettingsColorOtherPrimary.Text = "Primary:";
            // 
            // lblSettingsColorsOther
            // 
            lblSettingsColorsOther.AutoSize = true;
            lblSettingsColorsOther.Depth = 0;
            lblSettingsColorsOther.Font = new Font("Roboto Medium", 20F, FontStyle.Bold, GraphicsUnit.Pixel);
            lblSettingsColorsOther.FontType = MaterialSkin.MaterialSkinManager.fontType.H6;
            lblSettingsColorsOther.HighEmphasis = true;
            lblSettingsColorsOther.Location = new Point(17, 12);
            lblSettingsColorsOther.MouseState = MaterialSkin.MouseState.HOVER;
            lblSettingsColorsOther.Name = "lblSettingsColorsOther";
            lblSettingsColorsOther.Size = new Size(51, 24);
            lblSettingsColorsOther.TabIndex = 33;
            lblSettingsColorsOther.Text = "Other";
            lblSettingsColorsOther.UseAccent = true;
            // 
            // tabRadar
            // 
            tabRadar.BackColor = Color.White;
            tabRadar.Controls.Add(mcRadarStats);
            tabRadar.Controls.Add(mcRadarSettings);
            tabRadar.Controls.Add(mcRadarMapSetup);
            tabRadar.Controls.Add(skMapCanvas);
            tabRadar.ImageKey = "radar.png";
            tabRadar.Location = new Point(4, 39);
            tabRadar.Name = "tabRadar";
            tabRadar.Padding = new Padding(3);
            tabRadar.Size = new Size(1645, 785);
            tabRadar.TabIndex = 0;
            tabRadar.Text = "Radar";
            // 
            // mcRadarStats
            // 
            mcRadarStats.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            mcRadarStats.BackColor = Color.FromArgb(255, 255, 255);
            mcRadarStats.Controls.Add(lblRadarMemSValue);
            mcRadarStats.Controls.Add(lblRadarMemS);
            mcRadarStats.Controls.Add(lblRadarFPSValue);
            mcRadarStats.Controls.Add(lblRadarFPS);
            mcRadarStats.Depth = 0;
            mcRadarStats.ForeColor = Color.FromArgb(222, 0, 0, 0);
            mcRadarStats.Location = new Point(1538, 744);
            mcRadarStats.Margin = new Padding(14);
            mcRadarStats.MouseState = MaterialSkin.MouseState.HOVER;
            mcRadarStats.Name = "mcRadarStats";
            mcRadarStats.Padding = new Padding(14);
            mcRadarStats.Size = new Size(102, 36);
            mcRadarStats.TabIndex = 36;
            mcRadarStats.Visible = false;
            // 
            // lblRadarMemSValue
            // 
            lblRadarMemSValue.AutoSize = true;
            lblRadarMemSValue.Depth = 0;
            lblRadarMemSValue.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblRadarMemSValue.FontType = MaterialSkin.MaterialSkinManager.fontType.Caption;
            lblRadarMemSValue.Location = new Point(72, 17);
            lblRadarMemSValue.MouseState = MaterialSkin.MouseState.HOVER;
            lblRadarMemSValue.Name = "lblRadarMemSValue";
            lblRadarMemSValue.Size = new Size(8, 14);
            lblRadarMemSValue.TabIndex = 10;
            lblRadarMemSValue.Text = "0";
            // 
            // lblRadarMemS
            // 
            lblRadarMemS.AutoSize = true;
            lblRadarMemS.Depth = 0;
            lblRadarMemS.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblRadarMemS.FontType = MaterialSkin.MaterialSkinManager.fontType.Caption;
            lblRadarMemS.Location = new Point(24, 17);
            lblRadarMemS.MouseState = MaterialSkin.MouseState.HOVER;
            lblRadarMemS.Name = "lblRadarMemS";
            lblRadarMemS.Size = new Size(42, 14);
            lblRadarMemS.TabIndex = 9;
            lblRadarMemS.Text = "Mem/s:";
            // 
            // lblRadarFPSValue
            // 
            lblRadarFPSValue.AutoSize = true;
            lblRadarFPSValue.Depth = 0;
            lblRadarFPSValue.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblRadarFPSValue.FontType = MaterialSkin.MaterialSkinManager.fontType.Caption;
            lblRadarFPSValue.Location = new Point(72, 3);
            lblRadarFPSValue.MouseState = MaterialSkin.MouseState.HOVER;
            lblRadarFPSValue.Name = "lblRadarFPSValue";
            lblRadarFPSValue.Size = new Size(8, 14);
            lblRadarFPSValue.TabIndex = 5;
            lblRadarFPSValue.Text = "0";
            // 
            // lblRadarFPS
            // 
            lblRadarFPS.AutoSize = true;
            lblRadarFPS.Depth = 0;
            lblRadarFPS.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblRadarFPS.FontType = MaterialSkin.MaterialSkinManager.fontType.Caption;
            lblRadarFPS.Location = new Point(5, 3);
            lblRadarFPS.MouseState = MaterialSkin.MouseState.HOVER;
            lblRadarFPS.Name = "lblRadarFPS";
            lblRadarFPS.Size = new Size(61, 14);
            lblRadarFPS.TabIndex = 0;
            lblRadarFPS.Text = "Radar FPS:";
            // 
            // mcRadarSettings
            // 
            mcRadarSettings.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            mcRadarSettings.BackColor = Color.FromArgb(255, 255, 255);
            mcRadarSettings.Controls.Add(btnToggleMapFree);
            mcRadarSettings.Depth = 0;
            mcRadarSettings.ForeColor = Color.FromArgb(222, 0, 0, 0);
            mcRadarSettings.Location = new Point(1516, 5);
            mcRadarSettings.Margin = new Padding(14);
            mcRadarSettings.MouseState = MaterialSkin.MouseState.HOVER;
            mcRadarSettings.Name = "mcRadarSettings";
            mcRadarSettings.Padding = new Padding(14);
            mcRadarSettings.Size = new Size(124, 46);
            mcRadarSettings.TabIndex = 48;
            // 
            // mcRadarMapSetup
            // 
            mcRadarMapSetup.BackColor = Color.FromArgb(255, 255, 255);
            mcRadarMapSetup.Controls.Add(btnMapSetupApply);
            mcRadarMapSetup.Controls.Add(txtMapSetupScale);
            mcRadarMapSetup.Controls.Add(txtMapSetupY);
            mcRadarMapSetup.Controls.Add(txtMapSetupX);
            mcRadarMapSetup.Controls.Add(lblRadarMapSetup);
            mcRadarMapSetup.Depth = 0;
            mcRadarMapSetup.ForeColor = Color.FromArgb(222, 0, 0, 0);
            mcRadarMapSetup.Location = new Point(5, 5);
            mcRadarMapSetup.Margin = new Padding(14);
            mcRadarMapSetup.MouseState = MaterialSkin.MouseState.HOVER;
            mcRadarMapSetup.Name = "mcRadarMapSetup";
            mcRadarMapSetup.Padding = new Padding(14);
            mcRadarMapSetup.Size = new Size(444, 95);
            mcRadarMapSetup.TabIndex = 18;
            mcRadarMapSetup.Visible = false;
            // 
            // lblRadarMapSetup
            // 
            lblRadarMapSetup.AutoSize = true;
            lblRadarMapSetup.Depth = 0;
            lblRadarMapSetup.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblRadarMapSetup.FontType = MaterialSkin.MaterialSkinManager.fontType.Subtitle1;
            lblRadarMapSetup.HighEmphasis = true;
            lblRadarMapSetup.Location = new Point(17, 14);
            lblRadarMapSetup.MouseState = MaterialSkin.MouseState.HOVER;
            lblRadarMapSetup.Name = "lblRadarMapSetup";
            lblRadarMapSetup.Size = new Size(78, 19);
            lblRadarMapSetup.TabIndex = 31;
            lblRadarMapSetup.Text = "Map Setup";
            lblRadarMapSetup.UseAccent = true;
            // 
            // skMapCanvas
            // 
            skMapCanvas.BackColor = Color.Black;
            skMapCanvas.Dock = DockStyle.Fill;
            skMapCanvas.Location = new Point(3, 3);
            skMapCanvas.Margin = new Padding(4, 3, 4, 3);
            skMapCanvas.Name = "skMapCanvas";
            skMapCanvas.Size = new Size(1639, 779);
            skMapCanvas.TabIndex = 13;
            skMapCanvas.VSync = false;
            skMapCanvas.PaintSurface += skMapCanvas_PaintSurface;
            skMapCanvas.MouseDown += skMapCanvas_MouseDown;
            skMapCanvas.MouseMove += skMapCanvas_MouseMovePlayer;
            skMapCanvas.MouseUp += skMapCanvas_MouseUp;
            // 
            // tabControlMain
            // 
            tabControlMain.Controls.Add(tabRadar);
            tabControlMain.Controls.Add(tabSettings);
            tabControlMain.Depth = 0;
            tabControlMain.Dock = DockStyle.Fill;
            tabControlMain.ImageList = iconList;
            tabControlMain.Location = new Point(0, 24);
            tabControlMain.Margin = new Padding(0);
            tabControlMain.MouseState = MaterialSkin.MouseState.HOVER;
            tabControlMain.Multiline = true;
            tabControlMain.Name = "tabControlMain";
            tabControlMain.SelectedIndex = 0;
            tabControlMain.Size = new Size(1653, 828);
            tabControlMain.TabIndex = 10;
            tabControlMain.SelectedIndexChanged += TabControl_SelectedIndexChanged;
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1653, 852);
            Controls.Add(tabControlMain);
            Margin = new Padding(4, 3, 4, 3);
            Name = "frmMain";
            Padding = new Padding(0, 24, 0, 0);
            Text = "Squad";
            Shown += frmMain_Shown;
            tabSettings.ResumeLayout(false);
            tabControlSettings.ResumeLayout(false);
            tabSettingsGeneral.ResumeLayout(false);
            mcSettingsGeneralUI.ResumeLayout(false);
            mcSettingsGeneralUI.PerformLayout();
            mcSettingsGeneralRadar.ResumeLayout(false);
            mcSettingsGeneralRadar.PerformLayout();
            tabSettingsColors.ResumeLayout(false);
            mcSettingsColorsOther.ResumeLayout(false);
            mcSettingsColorsOther.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picOtherAccent).EndInit();
            ((System.ComponentModel.ISupportInitialize)picOtherPrimaryLight).EndInit();
            ((System.ComponentModel.ISupportInitialize)picOtherPrimaryDark).EndInit();
            ((System.ComponentModel.ISupportInitialize)picOtherPrimary).EndInit();
            tabRadar.ResumeLayout(false);
            mcRadarStats.ResumeLayout(false);
            mcRadarStats.PerformLayout();
            mcRadarSettings.ResumeLayout(false);
            mcRadarMapSetup.ResumeLayout(false);
            mcRadarMapSetup.PerformLayout();
            tabControlMain.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private ColorDialog colDialog;
        private ToolTip toolTip;
        private ImageList iconList;
        private ColumnHeader colLootFilterItemEnabled;
        private TabPage tabSettings;
        private MaterialSkin.Controls.MaterialTabSelector tabSelector;
        private MaterialSkin.Controls.MaterialTabControl tabControlSettings;
        private TabPage tabSettingsGeneral;
        private MaterialSkin.Controls.MaterialCard mcSettingsGeneralUI;
        private MaterialSkin.Controls.MaterialSlider sldrZoomSensitivity;
        private MaterialSkin.Controls.MaterialComboBox cboFont;
        private MaterialSkin.Controls.MaterialSlider sldrFontSize;
        private MaterialSkin.Controls.MaterialLabel lblSettingsGeneralUI;
        private MaterialSkin.Controls.MaterialSlider sldrUIScale;
        private MaterialSkin.Controls.MaterialSlider sldrAimlineLength;
        private MaterialSkin.Controls.MaterialSwitch swNames;
        private MaterialSkin.Controls.MaterialSwitch swAimview;
        private MaterialSkin.Controls.MaterialCard mcSettingsGeneralRadar;
        private MaterialSkin.Controls.MaterialSwitch swRadarEnemyCount;
        private MaterialSkin.Controls.MaterialSwitch swRadarVsync;
        private MaterialSkin.Controls.MaterialSwitch swRadarStats;
        private MaterialSkin.Controls.MaterialLabel lblSettingsGeneralRadar;
        private MaterialSkin.Controls.MaterialButton btnRestartRadar;
        private MaterialSkin.Controls.MaterialButton btnToggleMap;
        private MaterialSkin.Controls.MaterialSwitch swMapHelper;
        private TabPage tabSettingsColors;
        private MaterialSkin.Controls.MaterialCard mcSettingsColorsOther;
        private MaterialSkin.Controls.MaterialButton btnResetTheme;
        private PictureBox picOtherAccent;
        private MaterialSkin.Controls.MaterialLabel lblSettingsColorOtherAccent;
        private PictureBox picOtherPrimaryLight;
        private MaterialSkin.Controls.MaterialLabel ssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss;
        private PictureBox picOtherPrimaryDark;
        private MaterialSkin.Controls.MaterialLabel lblSettingsColorOtherPrimaryDark;
        private PictureBox picOtherPrimary;
        private MaterialSkin.Controls.MaterialLabel lblSettingsColorOtherPrimary;
        private MaterialSkin.Controls.MaterialLabel lblSettingsColorsOther;
        private TabPage tabRadar;
        private MaterialSkin.Controls.MaterialCard mcRadarStats;
        private MaterialSkin.Controls.MaterialLabel lblRadarMemSValue;
        private MaterialSkin.Controls.MaterialLabel lblRadarMemS;
        private MaterialSkin.Controls.MaterialLabel lblRadarFPSValue;
        private MaterialSkin.Controls.MaterialLabel lblRadarFPS;
        private MaterialSkin.Controls.MaterialCard mcRadarSettings;
        private MaterialSkin.Controls.MaterialButton btnToggleMapFree;
        private MaterialSkin.Controls.MaterialCard mcRadarMapSetup;
        private MaterialSkin.Controls.MaterialButton btnMapSetupApply;
        private MaterialSkin.Controls.MaterialTextBox2 txtMapSetupScale;
        private MaterialSkin.Controls.MaterialTextBox2 txtMapSetupY;
        private MaterialSkin.Controls.MaterialTextBox2 txtMapSetupX;
        private MaterialSkin.Controls.MaterialLabel lblRadarMapSetup;
        private SkiaSharp.Views.Desktop.SKGLControl skMapCanvas;
        private MaterialSkin.Controls.MaterialTabControl tabControlMain;
    }
}

