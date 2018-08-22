using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;

namespace LearnByHeart.UIControls
{
    /// <summary>
    /// Interaction logic for CreatorUIControl.xaml
    /// </summary>
    public partial class CreatorUIControl : UserControl, ICreatorView
    {
        private CreatorController controller;

        public CreatorUIControl()
        {
            InitializeComponent();
            controller = new CreatorController(this);
        }

        public void ShowError(string message)
        {
            MessageBox.Show(
                message,
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error
            );
        }

        public void TriggerCurrentQuestionUpdate()
        {
            controller.UpdateCurrentQuestion(Question.Text, Answer.Text);
        }

        private void SaveQuestions_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            if (dialog.ShowDialog() != true)
                return;

            controller.SaveQuestions(dialog.FileName);
        }

        private void CloseCreator_Click(object sender, RoutedEventArgs e)
        {
            UIControlSwitcher.SwitchTo(new MainUIControl());
        }

    }
}
