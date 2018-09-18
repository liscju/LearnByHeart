using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace LearnByHeart.UIControls
{
    /// <summary>
    /// Miscelannous utilities for User Controls.
    /// </summary>
    class UIUserControlUtil
    {

        private static MainWindow window;

        /// <summary>
        /// Sets main window of the application.
        /// </summary>
        /// <param name="window">main window</param>
        public static void setMainWindow(MainWindow window)
        {
            UIUserControlUtil.window = window;
        }

        /// <summary>
        /// Sets focus on specified element.
        /// </summary>
        public static void SetFocus(Control control)
        {
            window.Dispatcher.BeginInvoke(
                DispatcherPriority.Input,
                new Action(delegate () {
                    control.Focus();
                    Keyboard.Focus(control);
                })
            );
        }

    }
}
