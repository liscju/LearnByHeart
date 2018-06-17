using System.Windows.Controls;

namespace LearnByHeart.UIControls
{
    /// <summary>
    /// Switcher for UI control in main application window.
    /// </summary>
    public class UIControlSwitcher
    {
        private static MainWindow window;

        /// <summary>
        /// Sets main window of the application.
        /// </summary>
        /// <param name="window">main window</param>
        public static void setMainWindow(MainWindow window)
        {
            UIControlSwitcher.window = window;
        }

        /// <summary>
        /// Switch UI control in main application window. 
        /// </summary>
        /// <param name="control">UI control to switch</param>
        public static void SwitchTo(UserControl control)
        {
            window.NavigateTo(control);
        }
    }
}
