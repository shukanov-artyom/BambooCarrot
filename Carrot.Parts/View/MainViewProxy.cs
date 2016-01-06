using System;
using System.ComponentModel.Composition;
using Carrot.Contracts;

namespace Carrot.Parts.View
{
    [Serializable]
    [Export(typeof(IExport))]
    public class MainViewProxy : MarshalByRefObject, IMainView
    {
        private readonly MainWindow view = new MainWindow();

        public void SetMode(ICiStatus currentStatus)
        {
            view.SetMode(currentStatus);
        }

        /// <summary>
        /// Show or hide application main view.
        /// </summary>
        public void ShowHide()
        {
            view.ShowHide();
        }
    }
}
