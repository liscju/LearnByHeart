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
            MainContent.Content = new MainUIControl();
            UIControlSwitcher.setMainWindow(this);
            UIStatusBar.SetMainWindow(this);
        }

        public void NavigateTo(UserControl control)
        {
            MainContent.Content = control;
        }
    }
}
