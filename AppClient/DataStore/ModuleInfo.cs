using System.ComponentModel;
using System.Text.Json.Serialization;

namespace AppClient.DataStore
{
    public class ModuleInfo : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string host;
        public string Host
        {
            get => host;
            set
            {
                host = value;
                OnPropertyChanged(nameof(Host));
            }
        }

        private ModuleType type;
        public ModuleType Type
        {
            get => type;
            set
            {
                type = value;
                OnPropertyChanged(nameof(Type));
            }
        }

        private string description;
        public string Description
        {
            get => description;
            set
            {
                description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        [JsonIgnore]
        private ConnectionStatus connectionStatus;

        [JsonIgnore]
        public ConnectionStatus ConnectionStatus
        {
            get => connectionStatus;
            set
            {
                connectionStatus = value;
                OnPropertyChanged(nameof(ConnectionStatus));
                OnPropertyChanged(nameof(StatusIcon));
            }
        }

        [JsonIgnore]
        private string connectionMessage;

        [JsonIgnore]
        public string ConnectionMessage
        {
            get => connectionMessage;
            set
            {
                connectionMessage = value;
                OnPropertyChanged(nameof(ConnectionMessage));
            }
        }

        [JsonIgnore]
        public ImageSource StatusIcon
        {
            get => ConnectionStatus switch
            {
                ConnectionStatus.Pending => (ImageSource)"status_pending.svg",
                ConnectionStatus.Offline => (ImageSource)"status_offline.svg",
                ConnectionStatus.CheckConnection => (ImageSource)"status_check.svg",
                ConnectionStatus.Online => (ImageSource)"status_online.svg",
                _ => throw new NotImplementedException()
            };
        }

        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }

    public enum ConnectionStatus
    {
        Pending, Offline, CheckConnection, Online
    }

    public enum ModuleType
    {
        SegmentedLights, SpinningLights, AnimationLights, Webpage
    }
}
