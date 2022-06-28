namespace AppClient.Model
{
    internal class Device
    {

        public string Name { get; set; }
        public string Host { get; set; }
        public DeviceType Type { get; set; }
        public string Description { get; set; }

        public ConnectionStatus ConnectionStatus { get; set; }
        public string ConnectionMessage { get; set; }

    }

    internal enum ConnectionStatus
    {
        Offline, CheckConnection, Online
    }

    internal enum DeviceType
    {
        SegmentedLights, SpinningLights,
        Webpage, Unknown
    }
}
