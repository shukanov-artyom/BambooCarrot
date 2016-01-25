using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Carrot.Contracts;

namespace Carrot.Parts.View
{
    /// <summary>
    /// MainWindow interaction logic.
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Hide();
            this.WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// Sets window mode.
        /// </summary>
        public void SetMode(ICiStatus currentStatus)
        {
            this.Dispatcher.Invoke(() =>
                {
                    
                });
        }

        /// <summary>
        /// Shows or hides main view.
        /// </summary>
        public void ShowHide()
        {
            this.Dispatcher.Invoke(() =>
                {
                    if (this.Visibility == Visibility.Visible)
                    {
                        this.Visibility = Visibility.Hidden;
                        this.WindowState = WindowState.Minimized;
                    }
                    else
                    {
                        this.Visibility = Visibility.Visible;
                        this.WindowState = WindowState.Normal;
                    }
                });
            
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
                {
                    e.Cancel = true;
                    this.Hide();
                });
        }
    }
}
