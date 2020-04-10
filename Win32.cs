using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HTMLWallpaper
{
    public class Win32
    {
        [DllImport("User32.dll")]
        public static extern IntPtr GetShellWindow();

        [DllImport("User32.dll")]
        public static extern IntPtr SetParent(IntPtr child, IntPtr newParent);
    }
}
