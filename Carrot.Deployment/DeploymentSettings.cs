using System;

namespace Carrot.Deployment
{
    internal class DeploymentSettings
    {
        public bool UseSeqLogging { get; set; }

        public string SeqUrl { get; set; }

        public bool UseUpdateFolder { get; set; }

        public string UpdateFolderPath { get; set; }

        public string BunnyUrl { get; set; }

        public string BunnyUsername { get; set; }

        public string BunnyPassword { get; set; }
    }
}
