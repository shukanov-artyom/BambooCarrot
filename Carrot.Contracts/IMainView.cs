using System;

namespace Carrot.Contracts
{
    /// <summary>
    /// Intferface for Main View component, also used asa contract for MEF part.
    /// </summary>
    public interface IMainView : IExport
    {
        /// <summary>
        /// Sets curren CI mode to display on a window.
        /// </summary>
        /// <param name="currentStatus">Current CI status.</param>
        void SetMode(ICiStatus currentStatus);

        /// <summary>
        /// Show or hide application main view.
        /// </summary>
        void ShowHide();
    }
}
