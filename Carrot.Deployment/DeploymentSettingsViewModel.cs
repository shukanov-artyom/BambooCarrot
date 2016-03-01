using System;

namespace Carrot.Deployment
{
    internal class DeploymentSettingsViewModel : ViewModelBase
    {
        private readonly DeploymentSettings model;

        public DeploymentSettingsViewModel(DeploymentSettings model)
        {
            this.model = model;
        }

        public bool UseSeqLogging
        {
            get
            {
                return model.UseSeqLogging;
            }
            set
            {
                model.UseSeqLogging = value;
                OnPropertyChanged("UseSeqLogging");
            }
        }

        public string SeqUrl
        {
            get
            {
                return model.SeqUrl;
            }
            set
            {
                model.SeqUrl = value;
                OnPropertyChanged("SeqUrl");
            }
        }

        public bool UseUpdateFolder
        {
            get
            {
                return model.UseUpdateFolder;
            }
            set
            {
                OnPropertyChanged("UseUpdateFolder");
                model.UseUpdateFolder = value;
            }
        }

        public string UpdateFolderPath {
            get
            {
                return model.UpdateFolderPath;
            }
            set
            {
                model.UpdateFolderPath = value;
                OnPropertyChanged("UpdateFolderPath");
            }
        }

        public string BunnyUrl
        {
            get
            {
                return model.BunnyUrl;
            }
            set
            {
                model.UpdateFolderPath = value;
                OnPropertyChanged("BunnyUrl");
            }
        }

        public string BunnyUsername
        {
            get
            {
                return model.BunnyUsername;
            }
            set
            {
                model.BunnyUsername = value;
                OnPropertyChanged("BunnyUsername");
            }
        }

        public string BunnyPassword
        {
            get
            {
                return model.BunnyPassword;
            }
            set
            {
                model.BunnyPassword = value;
                OnPropertyChanged("BunnyPassword");
            }
        }
    }
}
