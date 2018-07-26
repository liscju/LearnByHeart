using LearnByHeart.Utils;
using System;
using System.Collections.Generic;
using System.IO;

namespace LearnByHeart
{
    /// <summary>
    /// Controller for the Main Window.
    /// </summary>
    class MainController
    {
        private IMainView view;

        /// <summary>
        /// Creates controller with specified view.
        /// </summary>
        /// <param name="view"></param>
        public MainController(IMainView view)
        {
            this.view = view;
        }

        /// <summary>
        /// Loads exercise from specified file path.
        /// </summary>
        /// <param name="fileName">path to exercise</param>
        public void LoadExercise(string path)
        {
            try
            {
                List<Question> questions = QuestionLoader.Load(path);
                if (questions.Count == 0)
                {
                    view.ShowError("Selected exercise is empty");
                    return;
                }
                view.NavigateToExercise(new Exercise(questions, DateTime.Now));
            } catch (IOException ex)
            {
                view.ShowError(ex.Message);
            } catch (FileFormatException ex)
            {
                view.ShowError(ex.Message);
            }
        }
    }
}
