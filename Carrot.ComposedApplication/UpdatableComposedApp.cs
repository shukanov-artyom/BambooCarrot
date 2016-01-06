using System;
using Carrot.Contracts;

namespace Carrot.ComposedApplication
{
    public class UpdatableComposedApp : IComposedApplication
    {
        private readonly IComposedApplication app;

        public UpdatableComposedApp()
        {
            try
            {
                UpdateDownloadHelper.DownloadUpdate();
            }
            catch (Exception e)
            {
                GetExport<IExportLogger>()?.Error(e, "Could not download an {operation}", "update");
                throw;
            }
            app = new ComposedApplication();
        }

        public IComponentsInteraction Interaction => app.Interaction;

        public IExport[] Exports => app.Exports;

        public TInterface GetExport<TInterface>() where TInterface : IExport
        {
            return app.GetExport<TInterface>();
        }
    }
}
