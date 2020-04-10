using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HTMLWallpaper
{
    static class Program
    {
        private const string MutexName = "MUTEX_HTMLWallpaper_a2127684-b4f1-4a42-a248-53022ce2da03";
        private static Mutex _mutexApplication;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // ensure one instance only
            bool isFirstInstance = true;
            if (_mutexApplication == null) _mutexApplication = new Mutex(true, MutexName, out isFirstInstance);
            if (!isFirstInstance)
            {
                MessageBox.Show("An instance of HTMLWallpaper is already running. Only one instance is allowed at any given time.", "Multiple instances detected.");
                if (_mutexApplication != null) _mutexApplication.Dispose();
                Application.Exit();
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new HTMLWallpaperApplicationContext());

            if (_mutexApplication != null) _mutexApplication.Dispose();
        }
    }
}
