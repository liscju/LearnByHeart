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
            this.currentQuestion = 0;
            this.questions = new List<Question>();
        }

        public void SaveQuestions(String path)
        {
            view.TriggerCurrentQuestionUpdate();

            try
            {
                QuestionLoader.Save(path, questions);
            }
            catch (ArgumentException ex)
            {
                view.ShowError(ex.Message);
            }
            catch (IOException ex)
            {
                view.ShowError(ex.Message);
            }
        }

        public void UpdateCurrentQuestion(string content, string answer)
        {
            if (currentQuestion == questions.Count)
            {
                questions.Add(new Question(content, answer));
            }
            else
            {
                questions[currentQuestion] = new Question(content, answer);
            }
        }
    }
}