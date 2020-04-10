using HTMLWallpaper.Properties;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HTMLWallpaper
{
    public partial class FrmOptions : Form
    {
        private static readonly string StartupKey = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
        private static readonly string StartupName = "HTMLWallpaper";

        public FrmOptions()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            _cbxReloadLocalOnChange.Checked = Settings.Default.ReloadLocalOnChanges;
            _cbxHideOnBattery.Checked = Settings.Default.HideOnBattery;
            _tbxUrl.Text = Settings.Default.WallpaperURL;

            using (var hklm = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64))
            {
                using (var key = hklm.OpenSubKey(StartupKey, true))
                {
                    _cbxStartWithWindows.Checked = key.GetValue(StartupName, null) != null;
                }
            }
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            if (SystemColors.ActiveCaptionText.GetBrightness() > 0.5)
                Icon = Resources.AppIcon;
            else
                Icon = Resources.AppIconDark;
        }

        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            if (SystemColors.InactiveCaptionText.GetBrightness() > 0.5)
                Icon = Resources.AppIcon;
            else
                Icon = Resources.AppIconDark;
        }

        private void SaveOptions()
        {
            if (_cbxStartWithWindows.Checked) EnableStartWithWindows();
            else DisableStartWithWindows();

            Settings.Default.WallpaperURL = _tbxUrl.Text.Trim();
            Settings.Default.ReloadLocalOnChanges = _cbxReloadLocalOnChange.Checked;
            Settings.Default.HideOnBattery = _cbxHideOnBattery.Checked;

            Settings.Default.FilesToWatch = new StringCollection();
            Settings.Default.FilesToWatch.AddRange(GetCheckedChildNodes(_tvFilesToWatch.Nodes[0]).Select(n => ((FileInfo)n.Tag).FullName).ToArray());

            Settings.Default.Save();
        }

        private void UpdateUIForUrl()
        {
            TryLoadLocalProject();
        }

        private void TryLoadLocalProject()
        {
            var url = _tbxUrl.Text.Trim();
            if (!url.StartsWith("file:///"))
            {
                DisableLocalProjectUI();
                return;
            }

            var localPath = Utils.GetPathFromLocalUrl(url);
            if (!File.Exists(localPath)) { DisableLocalProjectUI(); return; }

            var projDirInfo = new DirectoryInfo(Path.GetDirectoryName(localPath));

            _tvFilesToWatch.Nodes.Clear();
            _tvFilesToWatch.Nodes.Add(CreateDirNode(projDirInfo));
            _tvFilesToWatch.Nodes[0].Expand();
            ExpandToShowChecked();

            _gbLocalProjectOptions.Enabled = true;

            _tvFilesToWatch.Enabled = _cbxReloadLocalOnChange.Checked;
            _btnRefreshFiles.Enabled = _cbxReloadLocalOnChange.Checked;
        }

        private void DisableLocalProjectUI()
        {
            _gbLocalProjectOptions.Enabled = false;
        }

        private void EnableStartWithWindows()
        {
            using (var hklm = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64))
            {
                using (var key = hklm.OpenSubKey(StartupKey, true))
                {
                    key.SetValue(StartupName, Application.ExecutablePath);
                }
            }
        }

        private void DisableStartWithWindows()
        {
            using (var hklm = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64))
            {
                using (var key = hklm.OpenSubKey(StartupKey, true))
                {
                    key.DeleteValue(StartupName, false);
                }
            }
        }

        #region treeview

        private TreeNode CreateDirNode(DirectoryInfo dir)
        {
            var node = new TreeNode($"[{dir.Name}]", 1, 1) { Tag = dir };

            foreach (var subdir in dir.GetDirectories())
                node.Nodes.Add(CreateDirNode(subdir));

            foreach (var file in dir.GetFiles())
                node.Nodes.Add(new TreeNode(file.Name) { Tag = file, Checked = Settings.Default.FilesToWatch != null && Settings.Default.FilesToWatch.Contains(file.FullName) });

            return node;
        }

        private List<TreeNode> GetCheckedChildNodes(TreeNode root)
        {
            var coll = new List<TreeNode>();
            if (root.Nodes.Count == 0 && root.Checked)
                coll.Add(root);
            else
                foreach (TreeNode subnode in root.Nodes)
                    coll.AddRange(GetCheckedChildNodes(subnode));
            return coll;
        }

        private void ExpandToShowChecked()
        {
            var nodes = GetCheckedChildNodes(_tvFilesToWatch.Nodes[0]);
            foreach (TreeNode child in nodes)
                ExpandUpwards(child);
        }

        private void ExpandUpwards(TreeNode child)
        {
            if (child.Parent != null)
            {
                child.Parent.Expand();
                ExpandUpwards(child.Parent);
            }
        }

        #endregion

        private void _btnSave_Click(object sender, EventArgs e)
        {
            SaveOptions();
        }

        private void _btnSaveAndClose_Click(object sender, EventArgs e)
        {
            SaveOptions();
            Close();
        }

        private void _btnPickLocalPage_Click(object sender, EventArgs e)
        {
            if (_ofdLocalPage.ShowDialog() == DialogResult.OK)
            {
                _tbxUrl.Text = "file:///" + _ofdLocalPage.FileName.Replace('\\', '/');
            }
        }

        private void _cbxReloadLocalOnChange_CheckedChanged(object sender, EventArgs e)
        {
            TryLoadLocalProject();
        }

        private void _tbxUrl_TextChanged(object sender, EventArgs e)
        {
            UpdateUIForUrl();
        }

        private void _tvFilesToWatch_AfterCheck(object sender, TreeViewEventArgs e)
        {
            foreach (TreeNode subnode in e.Node.Nodes)
                subnode.Checked = e.Node.Checked;
        }

        private void _btnRefreshFiles_Click(object sender, EventArgs e)
        {
            TryLoadLocalProject();
        }

        private void _btnDiscardAndClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
