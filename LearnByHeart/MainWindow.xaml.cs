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
            Content = new MainUIControl();
            UIControlSwitcher.setMainWindow(this);
        }

        public void NavigateTo(UserControl control)
        {
            Content = control;
        }
    }
}
