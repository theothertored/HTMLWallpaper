using HTMLWallpaper.Properties;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HTMLWallpaper
{
    class HTMLWallpaperApplicationContext : ApplicationContext
    {
        private readonly ToolStripMenuItem _miExit;
        private readonly ToolStripMenuItem _miOptions;
        private readonly ToolStripMenuItem _miReload;
        private readonly ToolStripMenuItem _miHideWallpaper;
        private readonly NotifyIcon _trayIcon;

        private readonly FileSystemWatcher _fswWatcher;

        private FrmWallpaper _wall = null;
        private FrmOptions _opts = null;

        private bool _wallAutoClosed = false;

        public HTMLWallpaperApplicationContext()
        {

            // menu items
            _miExit = new ToolStripMenuItem("Exit");
            _miOptions = new ToolStripMenuItem("Options");
            _miReload = new ToolStripMenuItem("Reload");
            _miHideWallpaper = new ToolStripMenuItem("Hide HTML wallpaper");
            _miExit.Click += _miExit_Clicked;
            _miOptions.Click += _miOptions_Clicked;
            _miReload.Click += _miReload_Clicked;
            _miHideWallpaper.Click += _miHideWallpaper_Clicked;

            // tray icon
            _trayIcon = new NotifyIcon();
            _trayIcon.Icon = Resources.AppIcon;
            _trayIcon.Click += _trayIcon_Click;
            _trayIcon.DoubleClick += _trayIcon_DoubleClick;

            _trayIcon.ContextMenuStrip = new ContextMenuStrip();
            _trayIcon.ContextMenuStrip.Items.AddRange(new ToolStripItem[]
            {
                _miReload,
                _miHideWallpaper,
                new ToolStripSeparator(),
                _miOptions,
                _miExit
            });
            _trayIcon.ContextMenuStrip.AutoClose = true;
            _trayIcon.ContextMenuStrip.Closing += ContextMenuStrip_Closing;

            // show the tray icon
            _trayIcon.Visible = true;

            // settings
            Settings.Default.PropertyChanged += Settings_PropertyChanged;

            // file system watcher
            _fswWatcher = new FileSystemWatcher();
            _fswWatcher.IncludeSubdirectories = true;
            _fswWatcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;
            _fswWatcher.Created += _fswWatcher_Changed;
            _fswWatcher.Changed += _fswWatcher_Changed;
            _fswWatcher.Deleted += _fswWatcher_Changed;
            _fswWatcher.Renamed += _fswWatcher_Changed;

            if (Settings.Default.ReloadLocalOnChanges && Utils.IsLocalUrl(Settings.Default.WallpaperURL))
                StartWatcher();

            SystemEvents.PowerModeChanged += SystemEvents_PowerModeChanged;

            // wallpaper
            if (!Utils.OnBattery || !Settings.Default.HideOnBattery)
                LaunchWallpaper(Settings.Default.WallpaperURL);

            UpdateMenuItems();
        }

        private void Settings_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Settings.Default.WallpaperURL):
                    ChangeWallpaperUrl(Settings.Default.WallpaperURL);
                    break;
                case nameof(Settings.Default.ReloadLocalOnChanges):
                case nameof(Settings.Default.FilesToWatch):
                    if (Settings.Default.ReloadLocalOnChanges && Utils.IsLocalUrl(Settings.Default.WallpaperURL))
                        StartWatcher();
                    else
                        StopWatcher();
                    break;
                case nameof(Settings.Default.HideOnBattery):
                    if (Utils.OnBattery && Settings.Default.HideOnBattery) CloseWallpaper();
                    break;
            }
        }

        private Rectangle GetDisplayBounds()
        {
            var screens = Screen.AllScreens;
            var rect = new Rectangle(0, 0, 0, 0);

            foreach (var s in screens)
            {
                if (s.Bounds.Left < rect.Left)
                {
                    rect.Width += rect.Left - s.Bounds.Left;
                    rect.X = s.Bounds.Left;
                }

                if (s.Bounds.Top < rect.Top)
                {
                    rect.Height += rect.Top - s.Bounds.Top;
                    rect.Y = s.Bounds.Y;
                }

                if (s.Bounds.Left + s.Bounds.Width > rect.Left + rect.Width)
                    rect.Width = s.Bounds.Left + s.Bounds.Width - rect.Left;

                if (s.Bounds.Top + s.Bounds.Height > rect.Top + rect.Height)
                    rect.Height = s.Bounds.Top + s.Bounds.Height - rect.Top;
            }

            return rect;
        }

        private void UpdateMenuItems()
        {
            _miHideWallpaper.Checked = _wall == null;
            _miReload.Enabled = _wall != null;
        }

        private void Exit()
        {
            CloseWallpaper();
            CloseOptions();

            // hide the tray icon so it doesn't stay visible until the user mouses over it
            _trayIcon.Visible = false;

            Application.Exit();
        }

        #region file system watcher

        private void StartWatcher()
        {
            StopWatcher();
            _fswWatcher.Path = Path.GetDirectoryName(Utils.GetPathFromLocalUrl(Settings.Default.WallpaperURL));
            _fswWatcher.EnableRaisingEvents = true;
        }

        private void StopWatcher()
        {
            _fswWatcher.EnableRaisingEvents = false;
        }

        private void _fswWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            if (Settings.Default.FilesToWatch.Contains(e.FullPath)) ReloadWallpaper();
        }

        #endregion

        #region wallpaper actions

        private void LaunchWallpaper(string url)
        {
            if (_wall != null) CloseWallpaper();

            _wall = new FrmWallpaper(url);
            _wall.FormClosed += _wall_FormClosed;

            _wall.Bounds = GetDisplayBounds();
            // display form at wallpaper
            Win32.SetParent(_wall.Handle, Win32.GetShellWindow());

            _wall.Show();

            UpdateMenuItems();
        }

        private void ChangeWallpaperUrl(string url)
        {
            if (_wall == null) return;
            _wall.SetWallpaperUrl(url);
        }

        private void ReloadWallpaper()
        {
            if (_wall == null) return;
            _wall.ReloadWallpaper();
        }

        private void CloseWallpaper()
        {
            if (_wall != null)
            {
                _wall.Close();
                _wall = null;
            }

            UpdateMenuItems();
        }

        #endregion

        #region options actions

        private void OpenOptions()
        {
            if (_opts != null)
            {
                _opts.BringToFront();
                return;
            }

            _opts = new FrmOptions();
            _opts.FormClosed += _opts_FormClosed;
            _opts.Show();
            _opts.Activate();
        }

        private void CloseOptions()
        {
            if (_opts == null) return;
            _opts.Close();
            _opts = null;
        }

        #endregion

        #region event handlers

        // tray icon
        private void _trayIcon_Click(object sender, EventArgs e)
        {

        }

        private void _trayIcon_DoubleClick(object sender, EventArgs e)
        {
            OpenOptions();
        }

        // forms closing
        private void _opts_FormClosed(object sender, FormClosedEventArgs e)
        {
            _opts = null;
        }

        private void _wall_FormClosed(object sender, FormClosedEventArgs e)
        {
            _wall = null;
        }


        // context menu
        private void _miExit_Clicked(object sender, EventArgs e)
        {
            _trayIcon.ContextMenuStrip.Close();
            Exit();
        }

        private void _miOptions_Clicked(object sender, EventArgs e)
        {
            _trayIcon.ContextMenuStrip.Close();
            OpenOptions();
        }

        private void _miReload_Clicked(object sender, EventArgs e)
        {
            ReloadWallpaper();
        }

        private void _miHideWallpaper_Clicked(object sender, EventArgs e)
        {
            if (_wall == null)
                LaunchWallpaper(Settings.Default.WallpaperURL);
            else
            {
                CloseWallpaper();
                _wallAutoClosed = false;
            }

            _miHideWallpaper.Checked = _wall == null;
            
        }

        private void ContextMenuStrip_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            e.Cancel = e.CloseReason == ToolStripDropDownCloseReason.ItemClicked;
        }


        // system
        private void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            if (Utils.OnBattery && Settings.Default.HideOnBattery && _wall != null)
            {
                CloseWallpaper();
                _wallAutoClosed = true;
            }
            else if (_wallAutoClosed)
            {
                LaunchWallpaper(Settings.Default.WallpaperURL);
                _wallAutoClosed = false;
            }
        }

        #endregion
    }
}
