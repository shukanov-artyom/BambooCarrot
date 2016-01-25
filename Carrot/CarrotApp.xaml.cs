using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows;
using Carrot.ComposedApplication;
using Carrot.Configuration;
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

        protected override void OnStartup(StartupEventArgs e)
        {
            //e.Args is the string[] of command line argruments
            composedApp = ApplicationCompositionHelper.GetInstance();
            InitializeComposedApplication(composedApp);
            if (e.Args.Length != 0)
            {
                CarrotConfiguration config = CarrotConfiguration.
                    GetFromCommandLine(new CarrotCommandLine(e.Args));
                config.Save();
                Shutdown();
            }
        }

        private void App_OnExit(object sender, ExitEventArgs e)
        {
            (composedApp as IDisposable)?.Dispose();
        }

        private void InitializeComposedApplication(IComposedApplication app)
        {
            app.Interaction.Setup(app.Exports);
        }
    }
}
