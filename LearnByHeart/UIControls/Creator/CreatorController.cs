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
        private string filePath;
        private int currentQuestion;
        private List<Question> questions;
        private bool isDataDirty;

        public CreatorController(ICreatorView view)
        {
            this.view = view;

            currentQuestion = -1;
            questions = new List<Question>();
            isDataDirty = false;
        }

        public CreatorController(ICreatorView view, string path, List<Question> questions) : this(view)
        {
            this.view = view;
            this.filePath = path;

            currentQuestion = 0;
            this.questions = questions;
            isDataDirty = false;
        }

        public void Init()
        {
            if (questions.Count == 0)
                AddNewEmptyQuestion();
            ShowCurrentQuestion();
        }

        private void AddNewEmptyQuestion()
        {
            currentQuestion++;
            questions.Insert(currentQuestion, new Question("", ""));
        }

        public void AddNextQuestion()
        {
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

        public void SaveQuestions()
        {
            if (filePath == null)
            {
                string path = view.ChoosePathToSaveFile();
                if (path == null)
                    return;
                SaveQuestionsAs(path);
            }
            else
            {
                SaveQuestionDataToFile(filePath);
            }
        }

        public void SaveQuestionsAs(String path)
        {
            SaveQuestionDataToFile(path);
        }

        public void SaveQuestionDataToFile(String path)
        {
            try
            {
                QuestionLoader.Save(path, questions);
                isDataDirty = false;
                view.ShowFileSavedSuccesfully(path);
                filePath = path;
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

        public void Close()
        {
            if (!isDataDirty)
            {
                view.SwitchToMainWindow();
            }
            else
            {
                view.SaveAndSwitchToMainWindow();
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
            Question updatedQuestion = view.GetCurrentQuestion();
            Question question = questions[currentQuestion];

            if (!String.Equals(updatedQuestion.Content, question.Content) ||
                !String.Equals(updatedQuestion.Answer, question.Answer)) {
                questions[currentQuestion] = view.GetCurrentQuestion();

                if (!isDataDirty)
                {
                    isDataDirty = true;
                    view.ShowFileIsDirty(filePath);
                }
            }
        }

        public void MoveToNextQuestion()
        {
            bool isLastQuestion = currentQuestion + 1 == questions.Count;
            if (isLastQuestion)
                return;

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

        public void MoveToNextOrAddNew()
        {
            bool isLastQuestion = currentQuestion + 1 == questions.Count;
            if (isLastQuestion)
            {
                AddNextQuestion();
            }
            else
            {
                MoveToNextQuestion();
            }
        }

        public void MoveToPreviousQuestion()
        {
            if (currentQuestion <= 0)
                return;

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