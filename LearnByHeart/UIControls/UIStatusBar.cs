using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace LearnByHeart.UIControls
{
    /// <summary>
    /// Status bar in main application window.
    /// </summary>
    class UIStatusBar
    {
        private static MainWindow window;
        private static Timer timer;

        /// <summary>
        /// Sets main window of the application.
        /// </summary>
        /// <param name="window">main window</param>
        public static void SetMainWindow(MainWindow window)
        {
            UIStatusBar.window = window;
        }

        /// <summary>
        /// Show text on status bar.
        /// </summary>
        /// <param name="text">text to show</param>
        public static void ShowText(string text)
        {
            if (timer != null)
                timer.Stop();

            window.StatusBar.Text = text;
        }

        /// <summary>
        /// Show text on status bar for specified number of seconds.
        /// </summary>
        /// <param name="text">text to show</param>
        /// <param name="timeMs">maximum time in seconds</param>
        public static void ShowText(string text, int timeMs)
        {
            if (timer != null)
                timer.Stop();

            window.StatusBar.Text = text;

            timer = new Timer
            {
                AutoReset = false,
                Interval = timeMs,
                Enabled = true,
            };
            timer.Elapsed += ClearStatusBar;
        }

        private static void ClearStatusBar(object sender, ElapsedEventArgs e)
        {
            window.Dispatcher.Invoke(() =>
            {
                ShowText("");
            });
        }
    }
}
