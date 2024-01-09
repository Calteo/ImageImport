namespace ImageImport
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            layoutPanel = new TableLayoutPanel();
            buttonImport = new Button();
            labelSource = new Label();
            buttonSelectSource = new Button();
            comboBoxSource = new ComboBox();
            labelProfile = new Label();
            comboBoxProfile = new ComboBox();
            buttonEditProfile = new Button();
            textBoxProtocol = new TextBox();
            contextMenuProtocol = new ContextMenuStrip(components);
            clearProtocolButton = new ToolStripMenuItem();
            outputProtocolButton = new ToolStripMenuItem();
            dateTimeProtocolButton = new ToolStripMenuItem();
            contextProtocolButton = new ToolStripMenuItem();
            threadProtocolButton = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            noneProtocolButton = new ToolStripMenuItem();
            informationProtocolButton = new ToolStripMenuItem();
            warningProtocolButton = new ToolStripMenuItem();
            errorProtocolButton = new ToolStripMenuItem();
            criticalProtocolButton = new ToolStripMenuItem();
            verboseProtocolButton = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            activityProtocolButton = new ToolStripMenuItem();
            labelFiles = new Label();
            labelFilesLabel = new Label();
            checkBoxOverwrite = new CheckBox();
            checkBoxOnlyNewFiles = new CheckBox();
            checkBoxDeleteAfterImport = new CheckBox();
            menuStrip = new MenuStrip();
            fileMenuItem = new ToolStripMenuItem();
            autoPlayMenuItem = new ToolStripMenuItem();
            quitMenuItem = new ToolStripMenuItem();
            helpMenuItem = new ToolStripMenuItem();
            licenceMenuItem = new ToolStripMenuItem();
            versionMenuItem = new ToolStripMenuItem();
            statusStrip = new StatusStrip();
            statusLabel = new ToolStripStatusLabel();
            statusSpring = new ToolStripStatusLabel();
            statusProgressBar = new ToolStripProgressBar();
            bindingSource = new BindingSource(components);
            layoutPanel.SuspendLayout();
            contextMenuProtocol.SuspendLayout();
            menuStrip.SuspendLayout();
            statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bindingSource).BeginInit();
            SuspendLayout();
            // 
            // layoutPanel
            // 
            layoutPanel.BackColor = Color.Transparent;
            layoutPanel.ColumnCount = 3;
            layoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            layoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            layoutPanel.Controls.Add(buttonImport, 3, 2);
            layoutPanel.Controls.Add(labelSource, 0, 0);
            layoutPanel.Controls.Add(buttonSelectSource, 2, 0);
            layoutPanel.Controls.Add(comboBoxSource, 1, 0);
            layoutPanel.Controls.Add(labelProfile, 0, 1);
            layoutPanel.Controls.Add(comboBoxProfile, 1, 1);
            layoutPanel.Controls.Add(buttonEditProfile, 2, 1);
            layoutPanel.Controls.Add(textBoxProtocol, 0, 6);
            layoutPanel.Controls.Add(labelFiles, 1, 2);
            layoutPanel.Controls.Add(labelFilesLabel, 0, 2);
            layoutPanel.Controls.Add(checkBoxOverwrite, 1, 4);
            layoutPanel.Controls.Add(checkBoxOnlyNewFiles, 1, 3);
            layoutPanel.Controls.Add(checkBoxDeleteAfterImport, 1, 5);
            layoutPanel.Dock = DockStyle.Fill;
            layoutPanel.Location = new Point(0, 28);
            layoutPanel.Margin = new Padding(4);
            layoutPanel.Name = "layoutPanel";
            layoutPanel.RowCount = 7;
            layoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F));
            layoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            layoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            layoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            layoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            layoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            layoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutPanel.Size = new Size(1213, 660);
            layoutPanel.TabIndex = 0;
            // 
            // buttonImport
            // 
            buttonImport.Dock = DockStyle.Fill;
            buttonImport.Enabled = false;
            buttonImport.Location = new Point(1066, 89);
            buttonImport.Name = "buttonImport";
            buttonImport.Size = new Size(144, 34);
            buttonImport.TabIndex = 9;
            buttonImport.Text = "&Import";
            buttonImport.UseVisualStyleBackColor = true;
            buttonImport.Click += ButtonImportClick;
            // 
            // labelSource
            // 
            labelSource.Dock = DockStyle.Fill;
            labelSource.Location = new Point(4, 0);
            labelSource.Margin = new Padding(4, 0, 4, 0);
            labelSource.Name = "labelSource";
            labelSource.Size = new Size(142, 46);
            labelSource.TabIndex = 0;
            labelSource.Text = "Source";
            labelSource.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // buttonSelectSource
            // 
            buttonSelectSource.Dock = DockStyle.Fill;
            buttonSelectSource.Location = new Point(1066, 3);
            buttonSelectSource.Name = "buttonSelectSource";
            buttonSelectSource.Size = new Size(144, 40);
            buttonSelectSource.TabIndex = 2;
            buttonSelectSource.Text = "&Select";
            buttonSelectSource.UseVisualStyleBackColor = true;
            buttonSelectSource.Click += ButtonSelectSourceClick;
            // 
            // comboBoxSource
            // 
            comboBoxSource.Dock = DockStyle.Fill;
            comboBoxSource.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxSource.FormattingEnabled = true;
            comboBoxSource.Location = new Point(153, 3);
            comboBoxSource.Name = "comboBoxSource";
            comboBoxSource.Size = new Size(907, 36);
            comboBoxSource.TabIndex = 1;
            comboBoxSource.Format += ComboBoxSourceFormat;
            comboBoxSource.SelectedValueChanged += ComboBoxSourceSelectedValueChanged;
            // 
            // labelProfile
            // 
            labelProfile.AutoSize = true;
            labelProfile.Dock = DockStyle.Fill;
            labelProfile.Location = new Point(3, 46);
            labelProfile.Name = "labelProfile";
            labelProfile.Size = new Size(144, 40);
            labelProfile.TabIndex = 3;
            labelProfile.Text = "Profile";
            // 
            // comboBoxProfile
            // 
            comboBoxProfile.Dock = DockStyle.Fill;
            comboBoxProfile.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxProfile.FormattingEnabled = true;
            comboBoxProfile.Location = new Point(153, 49);
            comboBoxProfile.Name = "comboBoxProfile";
            comboBoxProfile.Size = new Size(907, 36);
            comboBoxProfile.TabIndex = 4;
            comboBoxProfile.Format += ComboBoxProfileFormat;
            comboBoxProfile.SelectedValueChanged += ComboBoxProfileSelectedValueChanged;
            // 
            // buttonEditProfile
            // 
            buttonEditProfile.Dock = DockStyle.Fill;
            buttonEditProfile.Location = new Point(1066, 49);
            buttonEditProfile.Name = "buttonEditProfile";
            buttonEditProfile.Size = new Size(144, 34);
            buttonEditProfile.TabIndex = 5;
            buttonEditProfile.Text = "&Edit";
            buttonEditProfile.UseVisualStyleBackColor = true;
            buttonEditProfile.Click += ButtonEditProfileClick;
            // 
            // textBoxProtocol
            // 
            layoutPanel.SetColumnSpan(textBoxProtocol, 3);
            textBoxProtocol.ContextMenuStrip = contextMenuProtocol;
            textBoxProtocol.Dock = DockStyle.Fill;
            textBoxProtocol.Font = new Font("Consolas", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxProtocol.Location = new Point(3, 249);
            textBoxProtocol.MaxLength = 100000;
            textBoxProtocol.Multiline = true;
            textBoxProtocol.Name = "textBoxProtocol";
            textBoxProtocol.ReadOnly = true;
            textBoxProtocol.ScrollBars = ScrollBars.Both;
            textBoxProtocol.Size = new Size(1207, 408);
            textBoxProtocol.TabIndex = 6;
            textBoxProtocol.WordWrap = false;
            // 
            // contextMenuProtocol
            // 
            contextMenuProtocol.ImageScalingSize = new Size(20, 20);
            contextMenuProtocol.Items.AddRange(new ToolStripItem[] { clearProtocolButton, outputProtocolButton, toolStripSeparator1, noneProtocolButton, informationProtocolButton, warningProtocolButton, errorProtocolButton, criticalProtocolButton, verboseProtocolButton, toolStripSeparator2, activityProtocolButton });
            contextMenuProtocol.Name = "contextMenuProtocol";
            contextMenuProtocol.Size = new Size(157, 232);
            // 
            // clearProtocolButton
            // 
            clearProtocolButton.Name = "clearProtocolButton";
            clearProtocolButton.Size = new Size(156, 24);
            clearProtocolButton.Text = "Clear";
            clearProtocolButton.ToolTipText = "Clears the protocol window.";
            clearProtocolButton.Click += ClearProtocolButtonClick;
            // 
            // outputProtocolButton
            // 
            outputProtocolButton.DropDownItems.AddRange(new ToolStripItem[] { dateTimeProtocolButton, contextProtocolButton, threadProtocolButton });
            outputProtocolButton.Name = "outputProtocolButton";
            outputProtocolButton.Size = new Size(156, 24);
            outputProtocolButton.Text = "Output";
            // 
            // dateTimeProtocolButton
            // 
            dateTimeProtocolButton.Name = "dateTimeProtocolButton";
            dateTimeProtocolButton.Size = new Size(167, 26);
            dateTimeProtocolButton.Text = "Date/Tiime";
            dateTimeProtocolButton.Click += OutputProtocolButtonClick;
            // 
            // contextProtocolButton
            // 
            contextProtocolButton.Name = "contextProtocolButton";
            contextProtocolButton.Size = new Size(167, 26);
            contextProtocolButton.Text = "Context";
            contextProtocolButton.Click += OutputProtocolButtonClick;
            // 
            // threadProtocolButton
            // 
            threadProtocolButton.Name = "threadProtocolButton";
            threadProtocolButton.Size = new Size(167, 26);
            threadProtocolButton.Text = "Thread";
            threadProtocolButton.Click += OutputProtocolButtonClick;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(153, 6);
            // 
            // noneProtocolButton
            // 
            noneProtocolButton.Name = "noneProtocolButton";
            noneProtocolButton.Size = new Size(156, 24);
            noneProtocolButton.Text = "None";
            noneProtocolButton.Click += LevelProtocolButtonClick;
            // 
            // informationProtocolButton
            // 
            informationProtocolButton.Name = "informationProtocolButton";
            informationProtocolButton.Size = new Size(156, 24);
            informationProtocolButton.Text = "Information";
            informationProtocolButton.Click += LevelProtocolButtonClick;
            // 
            // warningProtocolButton
            // 
            warningProtocolButton.Name = "warningProtocolButton";
            warningProtocolButton.Size = new Size(156, 24);
            warningProtocolButton.Text = "Warning";
            warningProtocolButton.Click += LevelProtocolButtonClick;
            // 
            // errorProtocolButton
            // 
            errorProtocolButton.Name = "errorProtocolButton";
            errorProtocolButton.Size = new Size(156, 24);
            errorProtocolButton.Text = "Error";
            errorProtocolButton.Click += LevelProtocolButtonClick;
            // 
            // criticalProtocolButton
            // 
            criticalProtocolButton.Name = "criticalProtocolButton";
            criticalProtocolButton.Size = new Size(156, 24);
            criticalProtocolButton.Text = "Critial";
            criticalProtocolButton.Click += LevelProtocolButtonClick;
            // 
            // verboseProtocolButton
            // 
            verboseProtocolButton.Name = "verboseProtocolButton";
            verboseProtocolButton.Size = new Size(156, 24);
            verboseProtocolButton.Text = "Verbose";
            verboseProtocolButton.Click += LevelProtocolButtonClick;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(153, 6);
            // 
            // activityProtocolButton
            // 
            activityProtocolButton.Name = "activityProtocolButton";
            activityProtocolButton.Size = new Size(156, 24);
            activityProtocolButton.Tag = "";
            activityProtocolButton.Text = "Activity";
            activityProtocolButton.Click += ActivityProtocolButtonClick;
            // 
            // labelFiles
            // 
            labelFiles.Dock = DockStyle.Fill;
            labelFiles.Location = new Point(153, 86);
            labelFiles.Name = "labelFiles";
            labelFiles.Size = new Size(907, 40);
            labelFiles.TabIndex = 7;
            // 
            // labelFilesLabel
            // 
            labelFilesLabel.Dock = DockStyle.Fill;
            labelFilesLabel.Location = new Point(3, 86);
            labelFilesLabel.Name = "labelFilesLabel";
            labelFilesLabel.Size = new Size(144, 40);
            labelFilesLabel.TabIndex = 8;
            labelFilesLabel.Text = "Files";
            // 
            // checkBoxOverwrite
            // 
            checkBoxOverwrite.Dock = DockStyle.Fill;
            checkBoxOverwrite.Location = new Point(153, 169);
            checkBoxOverwrite.Name = "checkBoxOverwrite";
            checkBoxOverwrite.Size = new Size(907, 34);
            checkBoxOverwrite.TabIndex = 10;
            checkBoxOverwrite.Text = "Overwrite existing files";
            checkBoxOverwrite.UseVisualStyleBackColor = true;
            checkBoxOverwrite.CheckedChanged += CheckBoxOverwriteCheckedChanged;
            // 
            // checkBoxOnlyNewFiles
            // 
            checkBoxOnlyNewFiles.Dock = DockStyle.Fill;
            checkBoxOnlyNewFiles.Location = new Point(153, 129);
            checkBoxOnlyNewFiles.Name = "checkBoxOnlyNewFiles";
            checkBoxOnlyNewFiles.Size = new Size(907, 34);
            checkBoxOnlyNewFiles.TabIndex = 11;
            checkBoxOnlyNewFiles.Text = "Import only new files";
            checkBoxOnlyNewFiles.UseVisualStyleBackColor = true;
            checkBoxOnlyNewFiles.CheckedChanged += CheckBoxOnlyNewFilesCheckedChanged;
            // 
            // checkBoxDeleteAfterImport
            // 
            checkBoxDeleteAfterImport.AutoSize = true;
            checkBoxDeleteAfterImport.Dock = DockStyle.Fill;
            checkBoxDeleteAfterImport.Location = new Point(153, 209);
            checkBoxDeleteAfterImport.Name = "checkBoxDeleteAfterImport";
            checkBoxDeleteAfterImport.Size = new Size(907, 34);
            checkBoxDeleteAfterImport.TabIndex = 12;
            checkBoxDeleteAfterImport.Text = "Delete after import";
            checkBoxDeleteAfterImport.ThreeState = true;
            checkBoxDeleteAfterImport.UseVisualStyleBackColor = true;
            checkBoxDeleteAfterImport.CheckStateChanged += CheckBoxDeleteAfterImportCheckStateChanged;
            // 
            // menuStrip
            // 
            menuStrip.ImageScalingSize = new Size(20, 20);
            menuStrip.Items.AddRange(new ToolStripItem[] { fileMenuItem, helpMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(1213, 28);
            menuStrip.TabIndex = 6;
            // 
            // fileMenuItem
            // 
            fileMenuItem.DropDownItems.AddRange(new ToolStripItem[] { autoPlayMenuItem, quitMenuItem });
            fileMenuItem.Name = "fileMenuItem";
            fileMenuItem.Size = new Size(46, 24);
            fileMenuItem.Text = "File";
            // 
            // autoPlayMenuItem
            // 
            autoPlayMenuItem.Name = "autoPlayMenuItem";
            autoPlayMenuItem.Size = new Size(235, 26);
            autoPlayMenuItem.Text = "AutoPlay Registration";
            autoPlayMenuItem.Click += AutoPlayMenuItemClick;
            // 
            // quitMenuItem
            // 
            quitMenuItem.Name = "quitMenuItem";
            quitMenuItem.ShortcutKeys = Keys.Alt | Keys.F4;
            quitMenuItem.Size = new Size(235, 26);
            quitMenuItem.Text = "Quit";
            quitMenuItem.Click += QuitMenuItemClick;
            // 
            // helpMenuItem
            // 
            helpMenuItem.DropDownItems.AddRange(new ToolStripItem[] { licenceMenuItem, versionMenuItem });
            helpMenuItem.Name = "helpMenuItem";
            helpMenuItem.Size = new Size(55, 24);
            helpMenuItem.Text = "Help";
            // 
            // licenceMenuItem
            // 
            licenceMenuItem.Name = "licenceMenuItem";
            licenceMenuItem.Size = new Size(141, 26);
            licenceMenuItem.Text = "Licence";
            licenceMenuItem.Click += LicenceMenuItemClick;
            // 
            // versionMenuItem
            // 
            versionMenuItem.Name = "versionMenuItem";
            versionMenuItem.Size = new Size(141, 26);
            versionMenuItem.Text = "Version";
            versionMenuItem.Click += VersionMenuItemClick;
            // 
            // statusStrip
            // 
            statusStrip.ImageScalingSize = new Size(20, 20);
            statusStrip.Items.AddRange(new ToolStripItem[] { statusLabel, statusSpring, statusProgressBar });
            statusStrip.Location = new Point(0, 688);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(1213, 26);
            statusStrip.TabIndex = 6;
            // 
            // statusLabel
            // 
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(53, 20);
            statusLabel.Text = "Ready.";
            // 
            // statusSpring
            // 
            statusSpring.Name = "statusSpring";
            statusSpring.Size = new Size(1145, 20);
            statusSpring.Spring = true;
            // 
            // statusProgressBar
            // 
            statusProgressBar.MarqueeAnimationSpeed = 25;
            statusProgressBar.Name = "statusProgressBar";
            statusProgressBar.Size = new Size(500, 18);
            statusProgressBar.Step = 1;
            statusProgressBar.Style = ProgressBarStyle.Continuous;
            statusProgressBar.Visible = false;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(11F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1213, 714);
            Controls.Add(layoutPanel);
            Controls.Add(statusStrip);
            Controls.Add(menuStrip);
            Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip;
            Margin = new Padding(4, 2, 4, 2);
            Name = "MainForm";
            Text = "Image Import";
            Load += MainFormLoad;
            Shown += MainFormShown;
            layoutPanel.ResumeLayout(false);
            layoutPanel.PerformLayout();
            contextMenuProtocol.ResumeLayout(false);
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bindingSource).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel layoutPanel;
        private Label labelSource;
        private Button buttonSelectSource;
        private ComboBox comboBoxSource;
        private Label labelProfile;
        private ComboBox comboBoxProfile;
        private Button buttonEditProfile;
        private MenuStrip menuStrip;
        private ToolStripMenuItem fileMenuItem;
        private ToolStripMenuItem quitMenuItem;
        private ToolStripMenuItem helpMenuItem;
        private ToolStripMenuItem licenceMenuItem;
        private ToolStripMenuItem versionMenuItem;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusLabel;
        private ToolStripStatusLabel statusSpring;
        private ToolStripProgressBar statusProgressBar;
        private TextBox textBoxProtocol;
        private ContextMenuStrip contextMenuProtocol;
        private ToolStripMenuItem clearProtocolButton;
        private ToolStripMenuItem verboseProtocolButton;
        private Label labelFiles;
        private Label labelFilesLabel;
        private Button buttonImport;
        private CheckBox checkBoxOverwrite;
        private CheckBox checkBoxOnlyNewFiles;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem activityProtocolButton;
        private ToolStripMenuItem noneProtocolButton;
        private ToolStripMenuItem informationProtocolButton;
        private ToolStripMenuItem warningProtocolButton;
        private ToolStripMenuItem errorProtocolButton;
        private ToolStripMenuItem criticalProtocolButton;
        private ToolStripMenuItem outputProtocolButton;
        private ToolStripMenuItem dateTimeProtocolButton;
        private ToolStripMenuItem contextProtocolButton;
        private ToolStripMenuItem threadProtocolButton;
        private ToolStripMenuItem autoPlayMenuItem;
        private BindingSource bindingSource;
        private CheckBox checkBoxDeleteAfterImport;
    }
}