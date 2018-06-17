using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnByHeart
{
    /// <summary>
    /// Main Window View interface.
    /// </summary>
    public interface IMainView
    {
        /// <summary>
        /// Show error with specified message.
        /// </summary>
        /// <param name="message">error message</param>
        void ShowError(string message);

        /// <summary>
        /// Navigates to exercise window.
        /// </summary>
        /// <param name="questions">exercise</param>
        void NavigateToExercise(Exercise exercise);
    }
}
