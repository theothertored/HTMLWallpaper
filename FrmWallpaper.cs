using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HTMLWallpaper
{
    public partial class FrmWallpaper : Form
    {
        private readonly ChromiumWebBrowser _browser;

        private string _address;

        public FrmWallpaper()
        {
            InitializeComponent();
        }

        public FrmWallpaper(string url) : this()
        {
            _address = url;
            _browser = new ChromiumWebBrowser(url);
            Controls.Add(_browser);
        }

        public void ReloadWallpaper()
        {
            if (_browser.Address == _address)
                _browser.Reload(true);
            else
                _browser.Load(_address);
        }

        public void SetWallpaperUrl(string url)
        {
            if (_address == url)
            {
                _browser.Reload(true);
            }
            else
            {
                _address = url;
                _browser.Load(url);
            }
        }
    }
}
