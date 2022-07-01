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
                OnPropertyChanged(nameof(DisplayName));
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
                OnPropertyChanged(nameof(DisplayName));
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
                OnPropertyChanged(nameof(StatusColor));
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
        public Color StatusColor
        {
            get => ConnectionStatus switch
            {
                ConnectionStatus.Pending => null,
                ConnectionStatus.Offline => Colors.Red,
                ConnectionStatus.CheckConnection => Colors.Orange,
                ConnectionStatus.Online => Colors.Green,
                _ => throw new NotImplementedException()
            };
        }

        [JsonIgnore]
        public string DisplayName
        {
            get => $"{Name} [{Host}]";
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
