using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using Carrot.Contracts;

namespace Carrot.ComposedApplication
{
    /// <summary>
    /// MEF logic is encapsulated here.
    /// </summary>
    public class ComposedApplication : MarshalByRefObject, IComposedApplication, IDisposable
    {
        private CompositionContainer container;
        private AggregateCatalog catalog = new AggregateCatalog();
        private DirectoryCatalog directoryCatalog;

        public ComposedApplication()
        {
            catalog = new AggregateCatalog();
            if (!Directory.Exists(ImportantPaths.LocalComponentsFolderPath))
            {
                Directory.CreateDirectory(ImportantPaths.LocalComponentsFolderPath);
            }
            directoryCatalog = new DirectoryCatalog(
                ImportantPaths.LocalComponentsFolderPath);
            catalog.Catalogs.Add(directoryCatalog);
            container = new CompositionContainer(catalog);
            Recompose();
        }

        public IComponentsInteraction Interaction => GetExport<IComponentsInteraction>();

        public IExport[] Exports => container.GetExportedValues<IExport>().ToArray();

        public TInterface GetExport<TInterface>() where TInterface : IExport
        {
            return container.GetExportedValues<IExport>().OfType<TInterface>().Single();
        }

        public void Recompose()
        {
            try
            {
                directoryCatalog.Refresh();
                container.ComposeParts(directoryCatalog.Parts);
            }
            catch (CompositionException compositionException)
            {
                // TODO : add processing and logging here
                throw;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void Dispose()
        {
            foreach (IExport export in Exports)
            {
                IDisposable dispExport = export as IDisposable;
                dispExport?.Dispose();
            }
            catalog.Dispose();
        }
    }
}
