using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Forms;
using Carrot.Contracts;

namespace Carrot.Parts
{
    /// <summary>
    /// Tray component implementation
    /// </summary>
    [Serializable]
    [Export(typeof(IExport))]
    public class TrayComponent : MarshalByRefObject, ITrayComponent, IDisposable
    {
        public event EventHandler SettingsViewRequested;

        public event EventHandler AppShutdownRequested;

        public event EventHandler StatusRefreshRequested;

        /// <summary>
        /// Updates current state if a tray component.
        /// </summary>
        public void Update(ICiStatus currentStatus)
        {
            if (IconHelper.TrayIcon == null)
            {
                IconHelper.TrayIcon = new NotifyIcon
                {
                    Icon = IconHelper.GetStatusIcon(CiWarningLevel.Ok),
                    Visible = true
                };
                IconHelper.TrayIcon.DoubleClick += OnHideShowActivated;
                IconHelper.TrayIcon.ContextMenu = new ContextMenu(
                    new []
                    {
                        new MenuItem("Update Status", OnUpdateStatusActivated), 
                        new MenuItem("Hide/Show", OnHideShowActivated),
                        new MenuItem("Exit", OnAppShutdownActivated)
                    });
            }
            IconHelper.UpdateIcon(currentStatus.Level);
            if (currentStatus.Level == CiWarningLevel.Warn)
            {
                IconHelper.TrayIcon.ShowBalloonTip(8000,
                    "Build broken!",
                    currentStatus.Offenders.Aggregate((s, s1) =>
                        String.Format("{0}, {1}", s, s1)),
                    ToolTipIcon.Warning);
            }
            else if (currentStatus.Level == CiWarningLevel.Error)
            {
                IconHelper.TrayIcon.ShowBalloonTip(10000,
                    "Build broken!",
                    currentStatus.Offenders.Aggregate((s, s1) =>
                        String.Format("{0}, {1}", s, s1)),
                    ToolTipIcon.Error);
            }
            if (!IconHelper.TrayIcon.Visible)
            {
                IconHelper.TrayIcon.Visible = true;
            }
        }

        private void OnHideShowActivated(Object s, EventArgs e)
        {
            EventHandler h = SettingsViewRequested;
            h?.Invoke(s, e);
        }

        private void OnAppShutdownActivated(Object s, EventArgs e)
        {
            EventHandler h = AppShutdownRequested;
            h?.Invoke(s, e);
        }

        private void OnUpdateStatusActivated(Object s, EventArgs e)
        {
            EventHandler h = StatusRefreshRequested;
            h?.Invoke(s, e);
        }

        public void Dispose()
        {
            IconHelper.TrayIcon.Dispose();
        }
    }
}
