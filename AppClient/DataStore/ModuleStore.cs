using AppClient.Provider;

namespace AppClient.DataStore
{
    public static class ModuleStore
    {

        public static List<ModuleInfo> Modules { get; } = new List<ModuleInfo>();

        static ModuleStore()
        {
            // TODO: Change Provider
            IDeviceProvider provider = new MockDeviceProvider();
            ModuleInfo[] devices = provider.GetDevices();
            LoadModules(devices);
        }

        public static void LoadModules(ModuleInfo[] devices)
        {
            Modules.Clear();
            Task.WaitAll(CheckConnectionStatus(devices));
            Modules.AddRange(devices);
        }

        public static void RegisterModule(ModuleInfo device)
        {
            Task.WaitAll(CheckConnectionStatus(device));
            Modules.Add(device);
        }

        public static void UnregisterModule(ModuleInfo device)
        {
            Modules.Remove(device);
        }

        public static async Task CheckConnectionStatus(params ModuleInfo[] devices)
        {
            using HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(10);

            foreach (ModuleInfo device in devices)
            {
                try
                {
                    using HttpResponseMessage message = await client.GetAsync(device.Host).ConfigureAwait(false);
                    string response = await message.Content.ReadAsStringAsync().ConfigureAwait(false);

                    if (!message.IsSuccessStatusCode)
                        throw new Exception(response);
                    else
                    {
                        device.ConnectionMessage = response;

                        switch (device.Type)
                        { // Connection check
                            case ModuleType.Webpage:
                                device.ConnectionStatus = response.StartsWith("<html>") ?
                                    ConnectionStatus.Online : ConnectionStatus.CheckConnection;
                                break;
                            case ModuleType.SegmentedLights:
                            case ModuleType.SpinningLights:
                                device.ConnectionStatus = response.StartsWith("{") ?
                                    ConnectionStatus.Online : ConnectionStatus.CheckConnection;
                                break;
                            case ModuleType.Unknown:
                                throw new NotImplementedException();
                        }
                    }
                }
                catch (Exception ex)
                {
                    device.ConnectionMessage = string.IsNullOrWhiteSpace(ex.Message) ?
                        "No Message!" : ex.Message;
                    device.ConnectionStatus = ConnectionStatus.Offline;
                }
            }
        }

    }
}
