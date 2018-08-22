using LearnByHeart.Utils;
using System;
using System.Collections.Generic;
using System.IO;

namespace LearnByHeart
{
    /// <summary>
    /// Controller for the Creator Window.
    /// </summary>
    public class CreatorController
    {
        private ICreatorView view;
        private int currentQuestion;
        private List<Question> questions;

        public CreatorController(ICreatorView view)
        {
            this.view = view;
        }

        public void Init()
        {
            this.currentQuestion = 0;
            this.questions = new List<Question>();
            questions.Add(new Question("", ""));

            ShowCurrentQuestion();
        }

        public void SaveQuestions(String path)
        {
            UpdateCurrentQuestion();

            try
            {
                QuestionLoader.Save(path, questions);
            }
            catch (ArgumentException ex)
            {
                view.ShowError("Invalid question set: " + ex.Message);
            }
            catch (IOException ex)
            {
                view.ShowError("Problem while saving question set: " + ex.Message);
            }
        }

        private void ShowCurrentQuestion()
        {
            view.ShowQuestion(
                currentQuestion + 1,
                questions[currentQuestion],
                questions.Count
            );
        }

        public void UpdateCurrentQuestion()
        {
            questions[currentQuestion] = view.getCurrentQuestion();
        }

        public void MoveToNextQuestion()
        {
            UpdateCurrentQuestion();

            if (!IsCurrentQuestionFilled())
            {
                view.ShowError(
                    "Fill question content and answer before moving to next question"
                );
                return;
            }
        }

        private bool IsCurrentQuestionFilled()
        {
            Question current = questions[currentQuestion];
            return current.Content.Length > 0 && current.Answer.Length > 0;
        }
    }
}