using System;
using System.Configuration;

namespace Carrot.Configuration
{
    public class CarrotConfiguration
    {
        public string BambooBuildBunnyUrl { get; set; }

        public string BambooBuildBunnyLogin { get; set; }

        public string BambooBuildBunnyPassword { get; set; }

        public bool UseSeqLogging { get; set; }

        public string SeqUrl { get; set; }

        public bool UseUpdateFolder { get; set; }

        public string UpdateFolderPath { get; set; }

        public static CarrotConfiguration GetFromCommandLine(CarrotCommandLine line)
        {
            line.Validate();
            bool useSeq = line.ContainsKey(CommandLineArguments.Seq);
            bool useUpdateFolder = line.ContainsKey(CommandLineArguments.UpdateFolder);
            return new CarrotConfiguration()
                   {
                       BambooBuildBunnyUrl = line[CommandLineArguments.Bunny],
                       BambooBuildBunnyLogin = line[CommandLineArguments.BunnyUser],
                       BambooBuildBunnyPassword = line[CommandLineArguments.BunnyPwd],
                       UseSeqLogging = useSeq,
                       SeqUrl = useSeq ? line[CommandLineArguments.Seq] : String.Empty,
                       UseUpdateFolder = useUpdateFolder,
                       UpdateFolderPath = useUpdateFolder ? line[CommandLineArguments.UpdateFolder] : String.Empty,
                   };
        }

        public void Save()
        {
            UpdateConfigurationKey(AllowedConfigurationKeys.BambooBuildBunnyUrl, BambooBuildBunnyUrl);
            UpdateConfigurationKey(AllowedConfigurationKeys.BambooBuildBunnyUser, BambooBuildBunnyLogin);
            UpdateConfigurationKey(AllowedConfigurationKeys.BambooBuildBunnyPassword, BambooBuildBunnyPassword);
            UpdateConfigurationKey(AllowedConfigurationKeys.UseSeqService, UseSeqLogging.ToString());
            UpdateConfigurationKey(AllowedConfigurationKeys.SeqServiceUrl, SeqUrl);
            UpdateConfigurationKey(AllowedConfigurationKeys.UseUpdateFolder, UseUpdateFolder.ToString());
            UpdateConfigurationKey(AllowedConfigurationKeys.UpdateFolder, UpdateFolderPath);
        }

        private void UpdateConfigurationKey(string key, string newValue)
        {
            System.Configuration.Configuration config = 
                ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings[key].Value = newValue;
            config.Save(ConfigurationSaveMode.Modified);
        }
    }
}
