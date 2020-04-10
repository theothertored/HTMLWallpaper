namespace HTMLWallpaper
{
    partial class FrmOptions
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOptions));
            this._tbxUrl = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this._btnSave = new System.Windows.Forms.Button();
            this._cbxReloadLocalOnChange = new System.Windows.Forms.CheckBox();
            this._btnSaveAndClose = new System.Windows.Forms.Button();
            this._ofdLocalPage = new System.Windows.Forms.OpenFileDialog();
            this._btnPickLocalPage = new System.Windows.Forms.Button();
            this._ilFileTvIcons = new System.Windows.Forms.ImageList(this.components);
            this._gbLocalProjectOptions = new System.Windows.Forms.GroupBox();
            this._btnRefreshFiles = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this._tvFilesToWatch = new System.Windows.Forms.TreeView();
            this._cbxStartWithWindows = new System.Windows.Forms.CheckBox();
            this._cbxHideOnBattery = new System.Windows.Forms.CheckBox();
            this._btnDiscardAndClose = new System.Windows.Forms.Button();
            this._gbLocalProjectOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // _tbxUrl
            // 
            this._tbxUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._tbxUrl.Location = new System.Drawing.Point(12, 25);
            this._tbxUrl.Name = "_tbxUrl";
            this._tbxUrl.Size = new System.Drawing.Size(388, 20);
            this._tbxUrl.TabIndex = 0;
            this._tbxUrl.TextChanged += new System.EventHandler(this._tbxUrl_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Wallpaper URL";
            // 
            // _btnSave
            // 
            this._btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnSave.Location = new System.Drawing.Point(358, 571);
            this._btnSave.Name = "_btnSave";
            this._btnSave.Size = new System.Drawing.Size(123, 23);
            this._btnSave.TabIndex = 2;
            this._btnSave.Text = "Save";
            this._btnSave.UseVisualStyleBackColor = true;
            this._btnSave.Click += new System.EventHandler(this._btnSave_Click);
            // 
            // _cbxReloadLocalOnChange
            // 
            this._cbxReloadLocalOnChange.AutoSize = true;
            this._cbxReloadLocalOnChange.Location = new System.Drawing.Point(9, 24);
            this._cbxReloadLocalOnChange.Name = "_cbxReloadLocalOnChange";
            this._cbxReloadLocalOnChange.Size = new System.Drawing.Size(237, 17);
            this._cbxReloadLocalOnChange.TabIndex = 3;
            this._cbxReloadLocalOnChange.Text = "Enable reloading local pages on file changes";
            this._cbxReloadLocalOnChange.UseVisualStyleBackColor = true;
            this._cbxReloadLocalOnChange.CheckedChanged += new System.EventHandler(this._cbxReloadLocalOnChange_CheckedChanged);
            // 
            // _btnSaveAndClose
            // 
            this._btnSaveAndClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnSaveAndClose.Location = new System.Drawing.Point(229, 571);
            this._btnSaveAndClose.Name = "_btnSaveAndClose";
            this._btnSaveAndClose.Size = new System.Drawing.Size(123, 23);
            this._btnSaveAndClose.TabIndex = 4;
            this._btnSaveAndClose.Text = "Save && close";
            this._btnSaveAndClose.UseVisualStyleBackColor = true;
            this._btnSaveAndClose.Click += new System.EventHandler(this._btnSaveAndClose_Click);
            // 
            // _ofdLocalPage
            // 
            this._ofdLocalPage.FileName = "index.html";
            this._ofdLocalPage.Filter = "HTML files (*.html, *.htm)|*.html;*.htm|All files (*.*)|*.*";
            this._ofdLocalPage.Title = "Pick a local html page";
            // 
            // _btnPickLocalPage
            // 
            this._btnPickLocalPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnPickLocalPage.Location = new System.Drawing.Point(406, 24);
            this._btnPickLocalPage.Name = "_btnPickLocalPage";
            this._btnPickLocalPage.Size = new System.Drawing.Size(75, 23);
            this._btnPickLocalPage.TabIndex = 5;
            this._btnPickLocalPage.Text = "Local...";
            this._btnPickLocalPage.UseVisualStyleBackColor = true;
            this._btnPickLocalPage.Click += new System.EventHandler(this._btnPickLocalPage_Click);
            // 
            // _ilFileTvIcons
            // 
            this._ilFileTvIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("_ilFileTvIcons.ImageStream")));
            this._ilFileTvIcons.TransparentColor = System.Drawing.Color.Transparent;
            this._ilFileTvIcons.Images.SetKeyName(0, "file");
            this._ilFileTvIcons.Images.SetKeyName(1, "folder_closed");
            this._ilFileTvIcons.Images.SetKeyName(2, "folder_open");
            // 
            // _gbLocalProjectOptions
            // 
            this._gbLocalProjectOptions.Controls.Add(this._btnRefreshFiles);
            this._gbLocalProjectOptions.Controls.Add(this.label2);
            this._gbLocalProjectOptions.Controls.Add(this._tvFilesToWatch);
            this._gbLocalProjectOptions.Controls.Add(this._cbxReloadLocalOnChange);
            this._gbLocalProjectOptions.Location = new System.Drawing.Point(12, 97);
            this._gbLocalProjectOptions.Name = "_gbLocalProjectOptions";
            this._gbLocalProjectOptions.Size = new System.Drawing.Size(469, 468);
            this._gbLocalProjectOptions.TabIndex = 9;
            this._gbLocalProjectOptions.TabStop = false;
            this._gbLocalProjectOptions.Text = "Local project options";
            // 
            // _btnRefreshFiles
            // 
            this._btnRefreshFiles.Location = new System.Drawing.Point(6, 439);
            this._btnRefreshFiles.Name = "_btnRefreshFiles";
            this._btnRefreshFiles.Size = new System.Drawing.Size(457, 23);
            this._btnRefreshFiles.TabIndex = 6;
            this._btnRefreshFiles.Text = "Refresh files";
            this._btnRefreshFiles.UseVisualStyleBackColor = true;
            this._btnRefreshFiles.Click += new System.EventHandler(this._btnRefreshFiles_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(361, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Pick files to watch. Checking a directory will select all files contained within." +
    "";
            // 
            // _tvFilesToWatch
            // 
            this._tvFilesToWatch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._tvFilesToWatch.CheckBoxes = true;
            this._tvFilesToWatch.Indent = 16;
            this._tvFilesToWatch.ItemHeight = 20;
            this._tvFilesToWatch.LineColor = System.Drawing.Color.DimGray;
            this._tvFilesToWatch.Location = new System.Drawing.Point(6, 65);
            this._tvFilesToWatch.Name = "_tvFilesToWatch";
            this._tvFilesToWatch.Size = new System.Drawing.Size(457, 368);
            this._tvFilesToWatch.TabIndex = 4;
            this._tvFilesToWatch.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this._tvFilesToWatch_AfterCheck);
            // 
            // _cbxStartWithWindows
            // 
            this._cbxStartWithWindows.AutoSize = true;
            this._cbxStartWithWindows.Location = new System.Drawing.Point(15, 51);
            this._cbxStartWithWindows.Name = "_cbxStartWithWindows";
            this._cbxStartWithWindows.Size = new System.Drawing.Size(114, 17);
            this._cbxStartWithWindows.TabIndex = 10;
            this._cbxStartWithWindows.Text = "Start with windows";
            this._cbxStartWithWindows.UseVisualStyleBackColor = true;
            // 
            // _cbxHideOnBattery
            // 
            this._cbxHideOnBattery.AutoSize = true;
            this._cbxHideOnBattery.Location = new System.Drawing.Point(15, 74);
            this._cbxHideOnBattery.Name = "_cbxHideOnBattery";
            this._cbxHideOnBattery.Size = new System.Drawing.Size(217, 17);
            this._cbxHideOnBattery.TabIndex = 11;
            this._cbxHideOnBattery.Text = "Hide wallpaper when laptop is on battery";
            this._cbxHideOnBattery.UseVisualStyleBackColor = true;
            // 
            // _btnDiscardAndClose
            // 
            this._btnDiscardAndClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnDiscardAndClose.Location = new System.Drawing.Point(12, 571);
            this._btnDiscardAndClose.Name = "_btnDiscardAndClose";
            this._btnDiscardAndClose.Size = new System.Drawing.Size(123, 23);
            this._btnDiscardAndClose.TabIndex = 12;
            this._btnDiscardAndClose.Text = "Discard && close";
            this._btnDiscardAndClose.UseVisualStyleBackColor = true;
            this._btnDiscardAndClose.Click += new System.EventHandler(this._btnDiscardAndClose_Click);
            // 
            // FrmOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 606);
            this.Controls.Add(this._btnDiscardAndClose);
            this.Controls.Add(this._cbxHideOnBattery);
            this.Controls.Add(this._cbxStartWithWindows);
            this.Controls.Add(this._gbLocalProjectOptions);
            this.Controls.Add(this._btnPickLocalPage);
            this.Controls.Add(this._btnSaveAndClose);
            this.Controls.Add(this._btnSave);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._tbxUrl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HTMLWallpaper options";
            this._gbLocalProjectOptions.ResumeLayout(false);
            this._gbLocalProjectOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _tbxUrl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button _btnSave;
        private System.Windows.Forms.CheckBox _cbxReloadLocalOnChange;
        private System.Windows.Forms.Button _btnSaveAndClose;
        private System.Windows.Forms.OpenFileDialog _ofdLocalPage;
        private System.Windows.Forms.Button _btnPickLocalPage;
        private System.Windows.Forms.ImageList _ilFileTvIcons;
        private System.Windows.Forms.GroupBox _gbLocalProjectOptions;
        private System.Windows.Forms.TreeView _tvFilesToWatch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox _cbxStartWithWindows;
        private System.Windows.Forms.CheckBox _cbxHideOnBattery;
        private System.Windows.Forms.Button _btnRefreshFiles;
        private System.Windows.Forms.Button _btnDiscardAndClose;
    }
}

