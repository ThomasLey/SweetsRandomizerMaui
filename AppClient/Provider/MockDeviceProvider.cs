using Device = AppClient.Model.Device;
using DeviceType = AppClient.Model.DeviceType;

namespace AppClient.Provider
{
    internal class MockDeviceProvider : IDeviceProvider
    {
        private static async Task FillConnectionStatus(Device[] devices)
        {
            HttpClient client = new HttpClient();
            foreach (Device device in devices)
            {
                try
                {
                    HttpResponseMessage message = await client.GetAsync(device.Host).ConfigureAwait(false);
                    if (!message.IsSuccessStatusCode)
                        device.ConnectionStatus = Model.ConnectionStatus.Offline;
                    else
                    {
                        string response = await message.Content.ReadAsStringAsync().ConfigureAwait(false);
                        device.ConnectionMessage = response;

                        switch (device.Type)
                        { // Connection check
                            case DeviceType.Webpage:
                                device.ConnectionStatus = response.StartsWith("<html>") ?
                                    Model.ConnectionStatus.Online : Model.ConnectionStatus.CheckConnection;
                                break;
                            case DeviceType.SegmentedLights:
                            case DeviceType.SpinningLights:
                                device.ConnectionStatus = response.StartsWith("{") ?
                                    Model.ConnectionStatus.Online : Model.ConnectionStatus.CheckConnection;
                                break;
                            case DeviceType.Unknown:
                                throw new NotImplementedException();
                        }
                    }
                }
                catch(Exception ex)
                {
                    device.ConnectionMessage = ex.Message;
                    device.ConnectionStatus = Model.ConnectionStatus.Offline;
                }
            }
        }

        public Device[] GetDevices()
        {
            Device[] devices = new Device[]
            {
                new Device()
                {
                    Name = "Router",
                    Host = "http://10.0.2.2:8080/router",
                    Type = DeviceType.Webpage,
                    Description = "Der Router"
                },
                new Device()
                {
                    Name = "RaspberryPI",
                    Host = "http://10.0.2.2:8080/pi",
                    Type = DeviceType.Webpage,
                    Description = "Der PI"
                },
                new Device()
                {
                    Name = "Schrank",
                    Host = "http://10.0.2.2:8080/schrank",
                    Type = DeviceType.SegmentedLights,
                    Description = "Der Schrank"
                },
                new Device()
                {
                    Name = "Leinwand",
                    Host = "http://10.0.2.2:8080/leinwand",
                    Type = DeviceType.SpinningLights,
                    Description = "Die Leinwand"
                },
                new Device()
                {
                    Name = "Lollipop",
                    Host = "http://10.0.2.2:8080/lollipop",
                    Type = DeviceType.SegmentedLights,
                    Description = "Die Lollipops"
                }
            };

            Task.WaitAll(FillConnectionStatus(devices));
            return devices;
        }
    }
}