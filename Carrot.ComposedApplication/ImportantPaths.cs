using System;
using System.Configuration;
using System.IO;

namespace Carrot.ComposedApplication
{
    internal static class ImportantPaths
    {
        public static string UpdateSourceFolder => 
            ConfigurationManager.AppSettings["UpdateFolder"]; 

        public static readonly string LocalComponentsAssemblyFolderName = "Components";

        public static string LocalComponentsFolderPath => Path.Combine(
                    AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
                    LocalComponentsAssemblyFolderName);
    }
}
