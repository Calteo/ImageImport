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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.layoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.buttonImport = new System.Windows.Forms.Button();
            this.labelSource = new System.Windows.Forms.Label();
            this.buttonSelectSource = new System.Windows.Forms.Button();
            this.comboBoxSource = new System.Windows.Forms.ComboBox();
            this.labelProfile = new System.Windows.Forms.Label();
            this.comboBoxProfile = new System.Windows.Forms.ComboBox();
            this.buttonEditProfile = new System.Windows.Forms.Button();
            this.textBoxProtocol = new System.Windows.Forms.TextBox();
            this.contextMenuProtocol = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.clearProtocolButton = new System.Windows.Forms.ToolStripMenuItem();
            this.outputProtocolButton = new System.Windows.Forms.ToolStripMenuItem();
            this.dateTimeProtocolButton = new System.Windows.Forms.ToolStripMenuItem();
            this.contextProtocolButton = new System.Windows.Forms.ToolStripMenuItem();
            this.threadProtocolButton = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.noneProtocolButton = new System.Windows.Forms.ToolStripMenuItem();
            this.informationProtocolButton = new System.Windows.Forms.ToolStripMenuItem();
            this.warningProtocolButton = new System.Windows.Forms.ToolStripMenuItem();
            this.errorProtocolButton = new System.Windows.Forms.ToolStripMenuItem();
            this.criticalProtocolButton = new System.Windows.Forms.ToolStripMenuItem();
            this.verboseProtocolButton = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.activityProtocolButton = new System.Windows.Forms.ToolStripMenuItem();
            this.labelFiles = new System.Windows.Forms.Label();
            this.labelFilesLabel = new System.Windows.Forms.Label();
            this.checkBoxOverwrite = new System.Windows.Forms.CheckBox();
            this.checkBoxOnlyNewFiles = new System.Windows.Forms.CheckBox();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoPlayMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.licenceMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.versionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusSpring = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.layoutPanel.SuspendLayout();
            this.contextMenuProtocol.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutPanel
            // 
            this.layoutPanel.BackColor = System.Drawing.Color.Transparent;
            this.layoutPanel.ColumnCount = 3;
            this.layoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.layoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.layoutPanel.Controls.Add(this.buttonImport, 3, 2);
            this.layoutPanel.Controls.Add(this.labelSource, 0, 0);
            this.layoutPanel.Controls.Add(this.buttonSelectSource, 2, 0);
            this.layoutPanel.Controls.Add(this.comboBoxSource, 1, 0);
            this.layoutPanel.Controls.Add(this.labelProfile, 0, 1);
            this.layoutPanel.Controls.Add(this.comboBoxProfile, 1, 1);
            this.layoutPanel.Controls.Add(this.buttonEditProfile, 2, 1);
            this.layoutPanel.Controls.Add(this.textBoxProtocol, 0, 5);
            this.layoutPanel.Controls.Add(this.labelFiles, 1, 2);
            this.layoutPanel.Controls.Add(this.labelFilesLabel, 0, 2);
            this.layoutPanel.Controls.Add(this.checkBoxOverwrite, 1, 4);
            this.layoutPanel.Controls.Add(this.checkBoxOnlyNewFiles, 1, 3);
            this.layoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutPanel.Location = new System.Drawing.Point(0, 28);
            this.layoutPanel.Margin = new System.Windows.Forms.Padding(4);
            this.layoutPanel.Name = "layoutPanel";
            this.layoutPanel.RowCount = 6;
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutPanel.Size = new System.Drawing.Size(1213, 660);
            this.layoutPanel.TabIndex = 0;
            // 
            // buttonImport
            // 
            this.buttonImport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonImport.Enabled = false;
            this.buttonImport.Location = new System.Drawing.Point(1066, 89);
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.Size = new System.Drawing.Size(144, 34);
            this.buttonImport.TabIndex = 9;
            this.buttonImport.Text = "&Import";
            this.buttonImport.UseVisualStyleBackColor = true;
            this.buttonImport.Click += new System.EventHandler(this.ButtonImportClick);
            // 
            // labelSource
            // 
            this.labelSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelSource.Location = new System.Drawing.Point(4, 0);
            this.labelSource.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSource.Name = "labelSource";
            this.labelSource.Size = new System.Drawing.Size(142, 46);
            this.labelSource.TabIndex = 0;
            this.labelSource.Text = "Source";
            this.labelSource.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonSelectSource
            // 
            this.buttonSelectSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonSelectSource.Location = new System.Drawing.Point(1066, 3);
            this.buttonSelectSource.Name = "buttonSelectSource";
            this.buttonSelectSource.Size = new System.Drawing.Size(144, 40);
            this.buttonSelectSource.TabIndex = 2;
            this.buttonSelectSource.Text = "&Select";
            this.buttonSelectSource.UseVisualStyleBackColor = true;
            this.buttonSelectSource.Click += new System.EventHandler(this.ButtonSelectSourceClick);
            // 
            // comboBoxSource
            // 
            this.comboBoxSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSource.FormattingEnabled = true;
            this.comboBoxSource.Location = new System.Drawing.Point(153, 3);
            this.comboBoxSource.Name = "comboBoxSource";
            this.comboBoxSource.Size = new System.Drawing.Size(907, 36);
            this.comboBoxSource.TabIndex = 1;
            this.comboBoxSource.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.ComboBoxSourceFormat);
            this.comboBoxSource.SelectedValueChanged += new System.EventHandler(this.ComboBoxSourceSelectedValueChanged);
            // 
            // labelProfile
            // 
            this.labelProfile.AutoSize = true;
            this.labelProfile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelProfile.Location = new System.Drawing.Point(3, 46);
            this.labelProfile.Name = "labelProfile";
            this.labelProfile.Size = new System.Drawing.Size(144, 40);
            this.labelProfile.TabIndex = 3;
            this.labelProfile.Text = "Profile";
            // 
            // comboBoxProfile
            // 
            this.comboBoxProfile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxProfile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProfile.FormattingEnabled = true;
            this.comboBoxProfile.Location = new System.Drawing.Point(153, 49);
            this.comboBoxProfile.Name = "comboBoxProfile";
            this.comboBoxProfile.Size = new System.Drawing.Size(907, 36);
            this.comboBoxProfile.TabIndex = 4;
            this.comboBoxProfile.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.ComboBoxProfileFormat);
            this.comboBoxProfile.SelectedValueChanged += new System.EventHandler(this.ComboBoxProfileSelectedValueChanged);
            // 
            // buttonEditProfile
            // 
            this.buttonEditProfile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonEditProfile.Location = new System.Drawing.Point(1066, 49);
            this.buttonEditProfile.Name = "buttonEditProfile";
            this.buttonEditProfile.Size = new System.Drawing.Size(144, 34);
            this.buttonEditProfile.TabIndex = 5;
            this.buttonEditProfile.Text = "&Edit";
            this.buttonEditProfile.UseVisualStyleBackColor = true;
            this.buttonEditProfile.Click += new System.EventHandler(this.ButtonEditProfileClick);
            // 
            // textBoxProtocol
            // 
            this.layoutPanel.SetColumnSpan(this.textBoxProtocol, 3);
            this.textBoxProtocol.ContextMenuStrip = this.contextMenuProtocol;
            this.textBoxProtocol.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxProtocol.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxProtocol.Location = new System.Drawing.Point(3, 209);
            this.textBoxProtocol.MaxLength = 100000;
            this.textBoxProtocol.Multiline = true;
            this.textBoxProtocol.Name = "textBoxProtocol";
            this.textBoxProtocol.ReadOnly = true;
            this.textBoxProtocol.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxProtocol.Size = new System.Drawing.Size(1207, 448);
            this.textBoxProtocol.TabIndex = 6;
            this.textBoxProtocol.WordWrap = false;
            // 
            // contextMenuProtocol
            // 
            this.contextMenuProtocol.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuProtocol.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearProtocolButton,
            this.outputProtocolButton,
            this.toolStripSeparator1,
            this.noneProtocolButton,
            this.informationProtocolButton,
            this.warningProtocolButton,
            this.errorProtocolButton,
            this.criticalProtocolButton,
            this.verboseProtocolButton,
            this.toolStripSeparator2,
            this.activityProtocolButton});
            this.contextMenuProtocol.Name = "contextMenuProtocol";
            this.contextMenuProtocol.Size = new System.Drawing.Size(157, 232);
            // 
            // clearProtocolButton
            // 
            this.clearProtocolButton.Name = "clearProtocolButton";
            this.clearProtocolButton.Size = new System.Drawing.Size(156, 24);
            this.clearProtocolButton.Text = "Clear";
            this.clearProtocolButton.ToolTipText = "Clears the protocol window.";
            this.clearProtocolButton.Click += new System.EventHandler(this.ClearProtocolButtonClick);
            // 
            // outputProtocolButton
            // 
            this.outputProtocolButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dateTimeProtocolButton,
            this.contextProtocolButton,
            this.threadProtocolButton});
            this.outputProtocolButton.Name = "outputProtocolButton";
            this.outputProtocolButton.Size = new System.Drawing.Size(156, 24);
            this.outputProtocolButton.Text = "Output";
            // 
            // dateTimeProtocolButton
            // 
            this.dateTimeProtocolButton.Name = "dateTimeProtocolButton";
            this.dateTimeProtocolButton.Size = new System.Drawing.Size(167, 26);
            this.dateTimeProtocolButton.Text = "Date/Tiime";
            this.dateTimeProtocolButton.Click += new System.EventHandler(this.OutputProtocolButtonClick);
            // 
            // contextProtocolButton
            // 
            this.contextProtocolButton.Name = "contextProtocolButton";
            this.contextProtocolButton.Size = new System.Drawing.Size(167, 26);
            this.contextProtocolButton.Text = "Context";
            this.contextProtocolButton.Click += new System.EventHandler(this.OutputProtocolButtonClick);
            // 
            // threadProtocolButton
            // 
            this.threadProtocolButton.Name = "threadProtocolButton";
            this.threadProtocolButton.Size = new System.Drawing.Size(167, 26);
            this.threadProtocolButton.Text = "Thread";
            this.threadProtocolButton.Click += new System.EventHandler(this.OutputProtocolButtonClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(153, 6);
            // 
            // noneProtocolButton
            // 
            this.noneProtocolButton.Name = "noneProtocolButton";
            this.noneProtocolButton.Size = new System.Drawing.Size(156, 24);
            this.noneProtocolButton.Text = "None";
            this.noneProtocolButton.Click += new System.EventHandler(this.LevelProtocolButtonClick);
            // 
            // informationProtocolButton
            // 
            this.informationProtocolButton.Name = "informationProtocolButton";
            this.informationProtocolButton.Size = new System.Drawing.Size(156, 24);
            this.informationProtocolButton.Text = "Information";
            this.informationProtocolButton.Click += new System.EventHandler(this.LevelProtocolButtonClick);
            // 
            // warningProtocolButton
            // 
            this.warningProtocolButton.Name = "warningProtocolButton";
            this.warningProtocolButton.Size = new System.Drawing.Size(156, 24);
            this.warningProtocolButton.Text = "Warning";
            this.warningProtocolButton.Click += new System.EventHandler(this.LevelProtocolButtonClick);
            // 
            // errorProtocolButton
            // 
            this.errorProtocolButton.Name = "errorProtocolButton";
            this.errorProtocolButton.Size = new System.Drawing.Size(156, 24);
            this.errorProtocolButton.Text = "Error";
            this.errorProtocolButton.Click += new System.EventHandler(this.LevelProtocolButtonClick);
            // 
            // criticalProtocolButton
            // 
            this.criticalProtocolButton.Name = "criticalProtocolButton";
            this.criticalProtocolButton.Size = new System.Drawing.Size(156, 24);
            this.criticalProtocolButton.Text = "Critial";
            this.criticalProtocolButton.Click += new System.EventHandler(this.LevelProtocolButtonClick);
            // 
            // verboseProtocolButton
            // 
            this.verboseProtocolButton.Name = "verboseProtocolButton";
            this.verboseProtocolButton.Size = new System.Drawing.Size(156, 24);
            this.verboseProtocolButton.Text = "Verbose";
            this.verboseProtocolButton.Click += new System.EventHandler(this.LevelProtocolButtonClick);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(153, 6);
            // 
            // activityProtocolButton
            // 
            this.activityProtocolButton.Name = "activityProtocolButton";
            this.activityProtocolButton.Size = new System.Drawing.Size(156, 24);
            this.activityProtocolButton.Tag = "";
            this.activityProtocolButton.Text = "Activity";
            this.activityProtocolButton.Click += new System.EventHandler(this.ActivityProtocolButtonClick);
            // 
            // labelFiles
            // 
            this.labelFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelFiles.Location = new System.Drawing.Point(153, 86);
            this.labelFiles.Name = "labelFiles";
            this.labelFiles.Size = new System.Drawing.Size(907, 40);
            this.labelFiles.TabIndex = 7;
            // 
            // labelFilesLabel
            // 
            this.labelFilesLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelFilesLabel.Location = new System.Drawing.Point(3, 86);
            this.labelFilesLabel.Name = "labelFilesLabel";
            this.labelFilesLabel.Size = new System.Drawing.Size(144, 40);
            this.labelFilesLabel.TabIndex = 8;
            this.labelFilesLabel.Text = "Files";
            // 
            // checkBoxOverwrite
            // 
            this.checkBoxOverwrite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBoxOverwrite.Location = new System.Drawing.Point(153, 169);
            this.checkBoxOverwrite.Name = "checkBoxOverwrite";
            this.checkBoxOverwrite.Size = new System.Drawing.Size(907, 34);
            this.checkBoxOverwrite.TabIndex = 10;
            this.checkBoxOverwrite.Text = "Overwrite existing files";
            this.checkBoxOverwrite.UseVisualStyleBackColor = true;
            this.checkBoxOverwrite.CheckedChanged += new System.EventHandler(this.CheckBoxOverwriteCheckedChanged);
            // 
            // checkBoxOnlyNewFiles
            // 
            this.checkBoxOnlyNewFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBoxOnlyNewFiles.Location = new System.Drawing.Point(153, 129);
            this.checkBoxOnlyNewFiles.Name = "checkBoxOnlyNewFiles";
            this.checkBoxOnlyNewFiles.Size = new System.Drawing.Size(907, 34);
            this.checkBoxOnlyNewFiles.TabIndex = 11;
            this.checkBoxOnlyNewFiles.Text = "Import only new files";
            this.checkBoxOnlyNewFiles.UseVisualStyleBackColor = true;
            this.checkBoxOnlyNewFiles.CheckedChanged += new System.EventHandler(this.CheckBoxOnlyNewFilesCheckedChanged);
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenuItem,
            this.helpMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1213, 28);
            this.menuStrip.TabIndex = 6;
            // 
            // fileMenuItem
            // 
            this.fileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.autoPlayMenuItem,
            this.quitMenuItem});
            this.fileMenuItem.Name = "fileMenuItem";
            this.fileMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileMenuItem.Text = "File";
            // 
            // autoPlayMenuItem
            // 
            this.autoPlayMenuItem.Name = "autoPlayMenuItem";
            this.autoPlayMenuItem.Size = new System.Drawing.Size(235, 26);
            this.autoPlayMenuItem.Text = "AutoPlay Registration";
            this.autoPlayMenuItem.Click += new System.EventHandler(this.AutoPlayMenuItemClick);
            // 
            // quitMenuItem
            // 
            this.quitMenuItem.Name = "quitMenuItem";
            this.quitMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.quitMenuItem.Size = new System.Drawing.Size(235, 26);
            this.quitMenuItem.Text = "Quit";
            this.quitMenuItem.Click += new System.EventHandler(this.QuitMenuItemClick);
            // 
            // helpMenuItem
            // 
            this.helpMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.licenceMenuItem,
            this.versionMenuItem});
            this.helpMenuItem.Name = "helpMenuItem";
            this.helpMenuItem.Size = new System.Drawing.Size(55, 24);
            this.helpMenuItem.Text = "Help";
            // 
            // licenceMenuItem
            // 
            this.licenceMenuItem.Name = "licenceMenuItem";
            this.licenceMenuItem.Size = new System.Drawing.Size(141, 26);
            this.licenceMenuItem.Text = "Licence";
            this.licenceMenuItem.Click += new System.EventHandler(this.LicenceMenuItemClick);
            // 
            // versionMenuItem
            // 
            this.versionMenuItem.Name = "versionMenuItem";
            this.versionMenuItem.Size = new System.Drawing.Size(141, 26);
            this.versionMenuItem.Text = "Version";
            this.versionMenuItem.Click += new System.EventHandler(this.VersionMenuItemClick);
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel,
            this.statusSpring,
            this.statusProgressBar});
            this.statusStrip.Location = new System.Drawing.Point(0, 688);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1213, 26);
            this.statusStrip.TabIndex = 6;
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(53, 20);
            this.statusLabel.Text = "Ready.";
            // 
            // statusSpring
            // 
            this.statusSpring.Name = "statusSpring";
            this.statusSpring.Size = new System.Drawing.Size(1145, 20);
            this.statusSpring.Spring = true;
            // 
            // statusProgressBar
            // 
            this.statusProgressBar.MarqueeAnimationSpeed = 25;
            this.statusProgressBar.Name = "statusProgressBar";
            this.statusProgressBar.Size = new System.Drawing.Size(500, 18);
            this.statusProgressBar.Step = 1;
            this.statusProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.statusProgressBar.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1213, 714);
            this.Controls.Add(this.layoutPanel);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.Name = "MainForm";
            this.Text = "Image Import";
            this.Load += new System.EventHandler(this.MainFormLoad);
            this.Shown += new System.EventHandler(this.MainFormShown);
            this.layoutPanel.ResumeLayout(false);
            this.layoutPanel.PerformLayout();
            this.contextMenuProtocol.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}