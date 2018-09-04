using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

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

            UIWindowTitle.SetSubtitle("New ");
        }

        public CreatorUIControl(string path, List<Question> questions)
        {
            InitializeComponent();
            controller = new CreatorController(this, path, questions);

            controller.Init();

            UIWindowTitle.SetSubtitle(path);
        }

        public Question GetCurrentQuestion()
        {
            return new Question(Content.Text, Answer.Text);
        }

        public string ChoosePathToSaveFile()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.AddExtension = true;
            dialog.Filter = "Question set|*.xml|Any|*.*";
            dialog.DefaultExt = "xml";
            if (dialog.ShowDialog() == false)
                return null;
            return dialog.FileName;
        }

        public void ShowQuestion(int idx, Question question, int count)
        {
            // It ensures that focus is set after window is initialized
            Dispatcher.BeginInvoke(
                DispatcherPriority.Input,
                new Action(delegate () {
                    Content.Focus();
                    Keyboard.Focus(Content);
                })
            );

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

        public void SwitchToMainWindow()
        {
            UIControlSwitcher.SwitchTo(new MainUIControl());
        }

        public void SaveAndSwitchToMainWindow()
        {
            MessageBoxResult decision = MessageBox.Show(
                "Do you want to save question set before closing?",
                "Question",
                MessageBoxButton.YesNo
            );

            if (decision == MessageBoxResult.Yes)
            {
                SaveQuestions.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }

            SwitchToMainWindow();
        }

        private void SaveQuestions_Click(object sender, RoutedEventArgs e)
        {
            controller.SaveQuestions();
        }

        private void SaveQuestionsAs_Click(object sender, RoutedEventArgs e)
        {
            string path = ChoosePathToSaveFile();
            if (path == null)
                return;

            controller.SaveQuestionsAs(path);
        }

        private void CloseCreator_Click(object sender, RoutedEventArgs e)
        {
            controller.Close();
        }

        private void NextQuestion_Click(object sender, RoutedEventArgs e)
        {
            controller.MoveToNextQuestion();
        }

        private void PreviousQuestion_Click(object sender, RoutedEventArgs e)
        {
            controller.MoveToPreviousQuestion();
        }

        private void RemoveQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (Content.Text.Length == 0 && Answer.Text.Length == 0)
            {
                controller.RemoveQuestion();
                return;
            }

            MessageBoxResult decision = MessageBox.Show(
                "Do you really want to delete question?",
                "Question",
                MessageBoxButton.YesNo
            );

            if (decision == MessageBoxResult.Yes)
                controller.RemoveQuestion();
        }

        private void AddQuestion_Click(object sender, RoutedEventArgs e)
        {
            controller.AddNextQuestion();
        }

        private void Content_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Answer.Focus();
            }
            else if (e.Key == Key.Escape)
            {
                Keyboard.ClearFocus();
            }

            controller.UpdateCurrentQuestion();
        }

        private void Answer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                controller.MoveToNextOrAddNew();
            }
            else if (e.Key == Key.Escape)
            {
                Keyboard.ClearFocus();
            }

            controller.UpdateCurrentQuestion();
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Alt
                && e.SystemKey == Key.Left)
            {
                controller.MoveToPreviousQuestion();
            }
            else if (e.KeyboardDevice.Modifiers == ModifierKeys.Alt
                     && e.SystemKey == Key.Right)
            {
                controller.MoveToNextQuestion();
            }
            else if (e.KeyboardDevice.Modifiers == ModifierKeys.Control
                    && e.Key == Key.D)
            {
                RemoveQuestion.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

                // Stop the character from being entered in textboxes
                e.Handled = true;
            }
            else if (e.KeyboardDevice.Modifiers == ModifierKeys.Control
                    && e.Key == Key.S)
            {
                SaveQuestions.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

                // Stop the character from being entered in textboxes
                e.Handled = true;
            }
            else if (e.Key == Key.Escape)
            {
                CloseCreator.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
        }

        public void ShowFileSavedSuccesfully(string path)
        {
            UIWindowTitle.SetSubtitle(path + " ");
            UIStatusBar.ShowText("File saved succesfully", 2000);
        }

        public void ShowFileIsDirty(string path)
        {
            if (path == null)
            {
                UIWindowTitle.SetSubtitle("New*");
            }
            else
            {
                UIWindowTitle.SetSubtitle(path + "*");
            }
        }
    }
}
