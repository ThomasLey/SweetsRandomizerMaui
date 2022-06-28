using AppClient.Provider;
using Device = AppClient.Model.Device;

namespace AppClient.Pages
{
    public partial class OverviewPage : ContentPage
    {

        public OverviewPage()
        {
            InitializeComponent();

            // TODO: Change
            LoadDevices(new MockDeviceProvider());
        }

        private void LoadDevices(IDeviceProvider provider)
        {
            foreach (Device device in provider.GetDevices())
            {
                Button deviceButton = new Button
                {
                    Text = $"{device.Name} [{device.Host}]",
                    ContentLayout = new Button.ButtonContentLayout(
                        Button.ButtonContentLayout.ImagePosition.Right, 0
                    )
                };

                deviceButton.ImageSource = device.ConnectionStatus switch
                {
                    Model.ConnectionStatus.Online => (ImageSource)"status_online.svg",
                    Model.ConnectionStatus.CheckConnection => (ImageSource)"status_check.svg",
                    Model.ConnectionStatus.Offline => (ImageSource)"status_offline.svg",
                    _ => throw new NotImplementedException()
                };

                DeviceListLayout.Add(deviceButton);
            }
        }

    }
}
