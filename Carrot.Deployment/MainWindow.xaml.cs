using System;
using System.Windows;

namespace Carrot.Deployment
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var settings = new DeploymentSettings();
            DataContext = new DeploymentSettingsViewModel(settings);
        }

        private void NextButtonClicked(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
