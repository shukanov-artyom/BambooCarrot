using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Carrot.Contracts;

namespace Carrot.Parts
{
    internal static class IconHelper
    {
        private static readonly IDictionary<CiWarningLevel, System.Drawing.Icon> iconsCache =
            new Dictionary<CiWarningLevel, System.Drawing.Icon>();

        static IconHelper()
        {
            iconsCache[CiWarningLevel.Ok] = new System.Drawing.Icon(GetIconByName("circle_green.ico"));
            iconsCache[CiWarningLevel.Warn] = new System.Drawing.Icon(GetIconByName("circle_yellow.ico"));
            iconsCache[CiWarningLevel.Error] = new System.Drawing.Icon(GetIconByName("circle_red.ico"));
        }

        public static NotifyIcon TrayIcon { get; set; }

        public static void UpdateIcon(CiWarningLevel currentBunnyStatus)
        {
            if (TrayIcon == null)
            {
                throw new InvalidOperationException("Cannot set tray ivon when it's not initialized yet.");
            }
            TrayIcon.Icon = GetStatusIcon(currentBunnyStatus);
        }

        public static System.Drawing.Icon GetStatusIcon(CiWarningLevel level)
        {
            return iconsCache[level];
        }

        private static Stream GetIconByName(string name)
        {
            string assemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            return System.Reflection.Assembly.GetExecutingAssembly().
                GetManifestResourceStream(String.Format("{1}.{0}", name, assemblyName));
        }

        public static void DisplayTooltip(string title, string text, ToolTipIcon icon)
        {
            if (TrayIcon == null)
            {
                throw new InvalidOperationException("Tray icon not initialized, but an attempt was made to display ");
            }
            TrayIcon.ShowBalloonTip(10000, title, text, icon);
        }
    }
}
