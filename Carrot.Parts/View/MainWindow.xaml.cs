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

        public string FailedPlansMessage
        {
            get
            {
                return textBlockFailedPlan.Text;
            }
            set
            {
                textBlockFailedPlan.Text = value;
            }
        }

        /// <summary>
        /// Sets window mode.
        /// </summary>
        public void SetMode(ICiStatus currentStatus)
        {
            this.Dispatcher.Invoke(() =>
                {
                    if (currentStatus.Level == CiWarningLevel.Ok)
                    {
                        gridMain.Background = new SolidColorBrush(Colors.White);
                        imageShockedCat.Visibility = Visibility.Collapsed;
                        imageOk.Visibility = Visibility.Visible;
                    }
                    else if (currentStatus.Level == CiWarningLevel.Warn)
                    {
                        gridMain.Background = new SolidColorBrush(Colors.Black);
                        imageShockedCat.Visibility = Visibility.Visible;
                        imageOk.Visibility = Visibility.Collapsed;
                    }
                    else if (currentStatus.Level == CiWarningLevel.Error)
                    {
                        gridMain.Background = new SolidColorBrush(Colors.Black);
                        imageShockedCat.Visibility = Visibility.Visible;
                        imageOk.Visibility = Visibility.Collapsed;
                    }
                    string bottomLine = GetFailedPlansString(currentStatus);
                    textBlockFailedPlan.Text = bottomLine;
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

        private static string GetFailedPlansString(ICiStatus state)
        {
            string result;
            if (state.Level == CiWarningLevel.Ok)
            {
                result = "All is green.";
            }
            else if (state.Offenders.Length == 1)
            {
                result = String.Format("Failed plan: {0}", state.Offenders[0]);
            }
            else
            {
                result = state.Offenders.Aggregate(
                    (current, off) => String.Format("{0}, {1}", current, off));
            }
            return result;
        }
    }
}
