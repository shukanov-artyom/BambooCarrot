using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using System.Windows;
using Carrot.Contracts;
using Microsoft.Win32;
using Timer = System.Timers.Timer;

namespace Carrot.Parts
{
    [Export(typeof(IExport))]
    public class ComponentsInteractionLogic : MarshalByRefObject, IComponentsInteraction, IDisposable
    {
        private readonly object sync = new object();
        private readonly Timer statusUpdateTimer = new Timer
        {
            Enabled = true,
            Interval = 1000 * 60 * 5,
        };

        private bool SessionLock
        {
            get;
            set;
        }

        private IMainView mainView;
        private ITrayComponent trayComponent;
        private ICiStatus ciStatus;
        private IExportLogger logger;

        public void Setup(params IExport[] exports)
        {
            SystemEvents.SessionSwitch += SessionSwitch;
            mainView = exports.OfType<IMainView>().Single();
            trayComponent = exports.OfType<ITrayComponent>().Single();
            ciStatus = exports.OfType<ICiStatus>().Single();
            logger = exports.OfType<IExportLogger>().Single();

            logger.Info("Application has started, all Parts activated.");

            trayComponent.AppShutdownRequested += ShutdownAction;
            trayComponent.MainViewShowHideRequested += MainViewShowHide;
            trayComponent.StatusRefreshRequested += StatusUpdate;
            statusUpdateTimer.Elapsed += StatusUpdate;
            statusUpdateTimer.Start();
            UpdateStatus();
        }

        public void TearDown()
        {
            trayComponent.AppShutdownRequested -= ShutdownAction;
            trayComponent.MainViewShowHideRequested -= MainViewShowHide;
            trayComponent.StatusRefreshRequested -= StatusUpdate;
            statusUpdateTimer.Stop();
        }

        private static void ShutdownAction(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MainViewShowHide(object sender, EventArgs eventArgs)
        {
            mainView.ShowHide();
        }

        private void StatusUpdate(object sender, EventArgs elapsedEventArgs)
        {
            UpdateStatus();
        }

        private void SessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            switch (e.Reason)
            {
                case SessionSwitchReason.SessionLock:
                    SessionLock = true;
                    break;
                case SessionSwitchReason.SessionUnlock:
                    UpdateStatus();
                    SessionLock = false;
                    break;
            }
        }

        private void UpdateStatus()
        {
            if (SessionLock)
            {
                return;
            }
            Monitor.Enter(sync);
            try
            {
                logger.Info("{operation} has started.", "Status Update");
                ciStatus.Refresh();
                mainView.SetMode(ciStatus);
                trayComponent.Update(ciStatus);
            }
            catch (Exception e)
            {
                logger.Error(e, "Was unable to {operation} CI status.", "Status Update");
                throw;
            }
            finally
            {
                Monitor.Exit(sync);
            }
        }

        public void Dispose()
        {
            TearDown();
        }
    }
}
