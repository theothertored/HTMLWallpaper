using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HTMLWallpaper
{
    public static class Utils
    {
        public static bool IsLocalUrl(string url) => url.StartsWith("file:///");

        public static string GetPathFromLocalUrl(string url) => url.Substring("file:///".Length).Replace('/', '\\');

        public static bool OnBattery => SystemInformation.PowerStatus.PowerLineStatus == PowerLineStatus.Offline;
    }
}
