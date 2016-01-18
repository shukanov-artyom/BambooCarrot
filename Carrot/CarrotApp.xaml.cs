using System;
using System.Collections.Generic;
using System.Configuration;
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

        protected override void OnStartup(StartupEventArgs e)
        {
            //e.Args is the string[] of command line argruments
            composedApp = ApplicationCompositionHelper.GetInstance();
            InitializeComposedApplication(composedApp);
            if (e.Args.Length != 0)
            {
                UpdateConfiguration(new Carrot.Configuration.CarrotCommandLine(e.Args));
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

        private void UpdateConfiguration(Carrot.Configuration.CarrotCommandLine parameters)
        {
            foreach (KeyValuePair<string, string> pair in Carrot.Configuration.CmdLineToConfigKeyMapping.Mapping)
            {
                string cmdParam = pair.Key;
                string configKey = pair.Value;
                if (!parameters.ContainsKey(cmdParam))
                {
                    throw new ArgumentException(
                        String.Format("Argument {0} not provided, cannot complete configuration.",
                        cmdParam));
                }
                UpdateConfigurationKey(configKey, parameters[cmdParam]);
            }
        }

        private void UpdateConfigurationKey(string key, string newValue)
        {
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(
                System.Windows.Forms.Application.ExecutablePath);
            config.AppSettings.Settings[key].Value = newValue;
            config.Save(ConfigurationSaveMode.Modified);
        }
    }
}
