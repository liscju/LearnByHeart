using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace LearnByHeart.UIControls
{
    /// <summary>
    /// Interaction logic for ExerciseUIControl.xaml
    /// </summary>
    public partial class ExerciseUIControl : UserControl, IExerciseView
    {
        private ExerciseController controller;
        private int durationSeconds;
        private DispatcherTimer timer;

        public ExerciseUIControl(string path, Exercise exercise)
        {
            InitializeComponent();
            controller = new ExerciseController(exercise, this);
            controller.Initialize();

            durationSeconds = 0;

            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += ExerciseDuration_Tick;

            timer.Start();

            UIWindowTitle.SetSubtitle(path);
        }

        // Actions

        public void BackToMain()
        {
            timer.Stop();
            UIControlSwitcher.SwitchTo(new MainUIControl());
        }

        public void ShowQuestion(Question question)
        {
            // It ensures that focus is set after window is initialized
            Dispatcher.BeginInvoke(
                DispatcherPriority.Input,
                new Action(delegate () {
                    AnswerToTry.Focus();
                    Keyboard.Focus(AnswerToTry);
                })
            );

            QuestionContent.Text = question.Content;

            AnswerToTry.Text = "";

            ResultContent.Text = "";
            
            ActionButton.Content = "Check";
            ActionButton.Click -= ActionButton_NextClick;
            ActionButton.Click += ActionButton_CheckClick;
        }

        public void ShowStatistics(ExerciseStatistics statistics)
        {
            AnsweredCount.Text = statistics.AnsweredCount.ToString();
            AllCount.Text = statistics.AllCount.ToString();
            ExerciseProgress.Value = ((double)statistics.AnsweredCount / statistics.AllCount) * 100;
        }

        public void ShowAnswerIsCorrect()
        {
            ActionButton.Focus();
            ActionButton.Content = "Next";
            ActionButton.Click -= ActionButton_CheckClick;
            ActionButton.Click += ActionButton_NextClick;
            
            ResultContent.Text = "Correct!!!";
        }

        public void ShowAnswerIsIncorrect(string correctAnswer)
        {
            ActionButton.Focus();
            ActionButton.Content = "Next";
            ActionButton.Click -= ActionButton_CheckClick;
            ActionButton.Click += ActionButton_NextClick;
            
            ResultContent.Text = "Wrong! Correct answer is: " + correctAnswer;
        }

        public void ShowExerciseHasEnded()
        {
            MessageBox.Show("Congratulation, you finished your exercise!");
        }

        // Event handlers

        private void CloseExercise_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult decision = MessageBox.Show(
                "Do you really want to close exercise?",
                "Exercise",
                MessageBoxButton.YesNo
            );

            if (decision == MessageBoxResult.Yes)
            {
                controller.Close();
            }
        }

        private void ExerciseDuration_Tick(object sender, EventArgs e)
        {
            durationSeconds++;
            TimeSpan ts = TimeSpan.FromSeconds(durationSeconds);
            ExerciseDuration.Content = ts.ToString(@"mm\:ss");
        }

        private void ActionButton_NextClick(object sender, EventArgs e)
        {
            controller.SetNewQuestion();
        }

        private void ActionButton_CheckClick(object sender, EventArgs e)
        {
            if (AnswerToTry.Text == "")
                return;
            controller.Answer(AnswerToTry.Text);
        }

        private void AnswerToTry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                ActionButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
        }

        private void Exercise_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                CloseExercise.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
        }
    }
}
