using System;
using System.IO;

namespace Carrot.ComposedApplication
{
    internal static class UpdateDownloadHelper
    {
        public static void RemoveOldPlugin()
        {
            if (Directory.Exists(ImportantPaths.LocalComponentsFolderPath))
            {
                foreach (FileInfo file in new DirectoryInfo(ImportantPaths.LocalComponentsFolderPath).EnumerateFiles())
                {
                    File.Delete(file.FullName);
                }
            }
            else
            {
                Directory.CreateDirectory(ImportantPaths.LocalComponentsFolderPath);
            }
        }

        public static void DownloadUpdate()
        {
            if (!Directory.Exists(ImportantPaths.UpdateSourceFolder))
            {
                throw new DirectoryNotFoundException(
                    String.Format("Cannot get to {0} directory", ImportantPaths.UpdateSourceFolder));
            }
            if (!Directory.Exists(ImportantPaths.UpdateSourceFolder))
            {
                throw new FileNotFoundException("Updates component is absent by expected path.");
            }
            RemoveOldPlugin();
            foreach (FileInfo fileInfo in new DirectoryInfo(ImportantPaths.UpdateSourceFolder).EnumerateFiles())
            {
                File.Copy(fileInfo.FullName, Path.Combine(ImportantPaths.LocalComponentsFolderPath, fileInfo.Name), true);
            }
        }
    }
}
