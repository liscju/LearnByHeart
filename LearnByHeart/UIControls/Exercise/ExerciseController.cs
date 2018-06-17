using System;

namespace LearnByHeart.UIControls
{
    public class ExerciseController
    {
        private Exercise exercise;
        private IExerciseView view;
        private Question currentQuestion;

        public ExerciseController(Exercise exercise, IExerciseView view)
        {
            this.exercise = exercise;
            this.view = view;
        }

        public void Close()
        {
            view.BackToMain();
        }

        public void Initialize()
        {
            SetNewQuestion();
        }

        public void SetNewQuestion()
        {
            if (exercise.HasEnded())
            {
                view.ShowExerciseHasEnded();
                view.BackToMain();
                return;
            }

            currentQuestion = exercise.NextQuestion();
            view.ShowQuestion(currentQuestion);
            view.ShowStatistics(exercise.CalculateStatistics());
        }

        public void Answer(string guess)
        {
            if (exercise.Answer(currentQuestion, guess))
                view.ShowAnswerIsCorrect();
            else
                view.ShowAnswerIsIncorrect(currentQuestion.Answer);
            view.ShowStatistics(exercise.CalculateStatistics());
        }
    }
}