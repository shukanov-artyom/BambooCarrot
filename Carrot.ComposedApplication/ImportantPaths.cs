using System;
using System.IO;

namespace Carrot.ComposedApplication
{
    internal static class ImportantPaths
    {
        public static readonly string UpdateSourceFolder =
                "\\\\fs\\Project-TransVault$\\Internal\\Data\\BuildStatusTrayDemon\\Update";

        public static readonly string LocalComponentsAssemblyFolderName = "Components";

        public static readonly string ShadowCopyFolderName = "AssembliesShadowCopy";

        public static string LocalComponentsFolderPath => Path.Combine(
                    AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
                    LocalComponentsAssemblyFolderName);

        public static string ShadowCopyCacheDirectory => Path.Combine(
                AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
                ImportantPaths.ShadowCopyFolderName);

        public static string ComponentsDirectory => Path.Combine(
                AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
                LocalComponentsAssemblyFolderName);
    }
}
