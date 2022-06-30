namespace AppClient.DataStore
{
    public class ModuleInfo
    {

        public string Name { get; set; }
        public string Host { get; set; }
        public ModuleType Type { get; set; }
        public string Description { get; set; }

        public ConnectionStatus ConnectionStatus { get; set; }
        public string ConnectionMessage { get; set; }

    }

    public enum ConnectionStatus
    {
        Offline, CheckConnection, Online
    }

    public enum ModuleType
    {
        SegmentedLights, SpinningLights, AnimationLights, Webpage
    }
}
