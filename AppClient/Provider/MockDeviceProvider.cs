using AppClient.DataStore;

namespace AppClient.Provider
{
    public class MockDeviceProvider : IDeviceProvider
    {
        private static async Task FillConnectionStatus(ModuleInfo[] devices)
        {
            using HttpClient client = new HttpClient();

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

        public ModuleInfo[] GetModules()
        {
            ModuleInfo[] devices = new ModuleInfo[]
            {
                new ModuleInfo()
                {
                    Name = "Router",
                    Host = "http://10.0.2.2:8080/router",
                    Type = ModuleType.Webpage,
                    Description = "Der Router"
                },
                new ModuleInfo()
                {
                    Name = "RaspberryPI",
                    Host = "http://10.0.2.2:8080/pi",
                    Type = ModuleType.Webpage,
                    Description = "Der PI"
                },
                new ModuleInfo()
                {
                    Name = "Schrank",
                    Host = "http://10.0.2.2:8080/schrank",
                    Type = ModuleType.SegmentedLights,
                    Description = "Der Schrank"
                },
                new ModuleInfo()
                {
                    Name = "Leinwand",
                    Host = "http://10.0.2.2:8080/leinwand",
                    Type = ModuleType.SpinningLights,
                    Description = "Die Leinwand"
                },
                new ModuleInfo()
                {
                    Name = "Lollipop",
                    Host = "http://10.0.2.2:8080/lollipop",
                    Type = ModuleType.SegmentedLights,
                    Description = "Die Lollipops"
                }
            };

            Task.WaitAll(FillConnectionStatus(devices));
            return devices;
        }
    }
}