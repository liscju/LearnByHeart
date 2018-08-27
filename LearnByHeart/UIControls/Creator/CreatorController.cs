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

            currentQuestion = -1;
            questions = new List<Question>();
        }

        public void Init()
        {
            AddNewEmptyQuestion();
            ShowCurrentQuestion();
        }

        private void AddNewEmptyQuestion()
        {
            currentQuestion++;
            questions.Add(new Question("", ""));
        }

        public void AddNextQuestion()
        {
            UpdateCurrentQuestion();
            if (!IsCurrentQuestionFilled())
            {
                view.ShowError(
                    "Fill question content and answer before moving to next " +
                    "question or remove question"
                );
                return;
            }

            AddNewEmptyQuestion();
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
            bool isLastQuestion = currentQuestion + 1 == questions.Count;
            if (isLastQuestion)
                return;

            UpdateCurrentQuestion();
            if (!IsCurrentQuestionFilled())
            {
                view.ShowError(
                    "Fill question content and answer before moving to next " +
                    "question or remove question"
                );
                return;
            }

            currentQuestion++;
            ShowCurrentQuestion();
        }

        public void MoveToPreviousQuestion()
        {
            if (currentQuestion <= 0)
                return;

            UpdateCurrentQuestion();
            if (!IsCurrentQuestionFilled())
            {
                view.ShowError(
                    "Fill question content and answer before moving to " +
                    "previous question or remove question"
                );
                return;
            }

            currentQuestion--;
            ShowCurrentQuestion();
        }

        public void RemoveQuestion()
        {
            bool isLastQuestion = currentQuestion + 1 == questions.Count;

            if (questions.Count == 1)
            {
                currentQuestion = -1;
                questions.RemoveAt(0);
                AddNewEmptyQuestion();
                ShowCurrentQuestion();
            }
            else if (currentQuestion == 0)
            {
                questions.RemoveAt(0);
                ShowCurrentQuestion();
            }
            else if (isLastQuestion)
            {
                questions.RemoveAt(currentQuestion);
                currentQuestion--;
                ShowCurrentQuestion();
            }
            else {
                questions.RemoveAt(currentQuestion);
                ShowCurrentQuestion();
            }
        }

        private bool IsCurrentQuestionFilled()
        {
            Question current = questions[currentQuestion];
            return current.Content.Length > 0 && current.Answer.Length > 0;
        }
    }
}