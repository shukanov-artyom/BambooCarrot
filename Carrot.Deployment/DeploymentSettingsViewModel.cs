using System;
using System.Net.Configuration;

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
    }
}
