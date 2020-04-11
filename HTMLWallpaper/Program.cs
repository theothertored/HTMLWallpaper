using System;
using System.Collections.Generic;
using System.IO;
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
        private static HTMLWallpaperApplicationContext _appContext;

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

            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            _appContext = new HTMLWallpaperApplicationContext();

            Application.Run(_appContext);

            if (_mutexApplication != null) _mutexApplication.Dispose();
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            OnUnhandledException((Exception)e.ExceptionObject);
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            OnUnhandledException(e.Exception);
        }

        private static void OnUnhandledException(Exception ex)
        {
            // an unhandled exception occured

            // save exception data
            File.WriteAllText(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "exception.txt"), ex.ToString());

            // display a message to inform the user
            MessageBox.Show("An unhandled exception has occurred, sorry for that.\n" +
                "Full exception info was saved to a txt file in the app's installation folder (" + Path.GetDirectoryName(Application.ExecutablePath) + ").\n" +
                "Exception message:\n" + ex.Message + "\n"
                + "HTMLWallpaper will now close.", "Shit's fucked, yo", MessageBoxButtons.OK, MessageBoxIcon.Error);

            if (_appContext != null) _appContext.Exit();
        }
    }
}
