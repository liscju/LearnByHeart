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

            controller.Init();
        }

        public Question getCurrentQuestion()
        {
            return new Question(Content.Text, Answer.Text);
        }

        public void ShowQuestion(int idx, Question question, int count)
        {
            Content.Text = question.Content;
            Answer.Text = question.Answer;

            Index.Content = idx.ToString();
            Count.Content = count.ToString();
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

        private void NextQuestion_Click(object sender, RoutedEventArgs e)
        {
            controller.MoveToNextQuestion();
        }
    }
}
