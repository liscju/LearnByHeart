using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnByHeart.UIControls
{
    /// <summary>
    /// Utility for setting window title.
    /// </summary>
    class UIWindowTitle
    {
        private const string APPLICATION_NAME = "LearnByHeart";
        private static MainWindow window;

        /// <summary>
        /// Sets main window of the application.
        /// </summary>
        /// <param name="window">main window</param>
        public static void SetMainWindow(MainWindow window)
        {
            UIWindowTitle.window = window;
        }

        /// <summary>
        /// Sets original application window.
        /// </summary>
        public static void SetTitle()
        {
            window.Title = APPLICATION_NAME;
        }

        /// <summary>
        /// Set subtitle (text next to application name) of the window.
        /// </summary>
        /// <param name="subtitle">subtitle to set</param>
        public static void SetSubtitle(string subtitle)
        {
            window.Title = subtitle + " - " + APPLICATION_NAME;
        }
    }
}
