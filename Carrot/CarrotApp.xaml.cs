using System;
using System.Windows;
using Carrot.ComposedApplication;
using Carrot.Contracts;
using Application = System.Windows.Application;

namespace Carrot
{
    /// <summary>
    /// Логика взаимодействия для CarrotApp.xaml
    /// </summary>
    public partial class CarrotApp : Application
    {
        private IComposedApplication composedApp;

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            composedApp = ApplicationCompositionHelper.GetInstance();
            InitializeComposedApplication(composedApp);
        }

        private void App_OnExit(object sender, ExitEventArgs e)
        {
            IDisposable app = composedApp as IDisposable;
            if (app != null)
            {
                app.Dispose();
            }
        }

        private void InitializeComposedApplication(IComposedApplication app)
        {
            app.Interaction.Setup(app.Exports);
        }
    }
}
