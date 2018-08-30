using LearnByHeart.UIControls;
using System.Windows;
using System.Windows.Controls;

namespace LearnByHeart
{
    /// <summary>
    /// Interaction logic for main window.
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            UIControlSwitcher.setMainWindow(this);
            UIStatusBar.SetMainWindow(this);
            UIWindowTitle.SetMainWindow(this);

            MainContent.Content = new MainUIControl();
        }

        public void NavigateTo(UserControl control)
        {
            MainContent.Content = control;
        }
    }
}
