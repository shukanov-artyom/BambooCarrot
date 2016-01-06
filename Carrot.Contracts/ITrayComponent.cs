using System;

namespace Carrot.Contracts
{
    /// <summary>
    /// Tray component Part contract.
    /// </summary>
    public interface ITrayComponent : IExport
    {
        /// <summary>
        /// Updates tray component.
        /// </summary>
        /// <param name="currentStatus">Current CI status.</param>
        void Update(ICiStatus currentStatus);

        event EventHandler MainViewShowHideRequested;

        event EventHandler AppShutdownRequested;

        event EventHandler StatusRefreshRequested;
    }
}
