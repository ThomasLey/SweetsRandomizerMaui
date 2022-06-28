using AppClient.DataStore;
using ModuleInfo = AppClient.DataStore.ModuleInfo;

namespace AppClient.Pages
{
    public partial class OverviewPage : ContentPage
    {

        public OverviewPage()
        {
            InitializeComponent();

            // Load devices
            foreach (ModuleInfo device in ModuleStore.Modules)
            {
                Button deviceButton = new Button
                {
                    Text = $"{device.Name} [{device.Host}]",
                    ContentLayout = new Button.ButtonContentLayout(
                        Button.ButtonContentLayout.ImagePosition.Right, 0
                    )
                };

                deviceButton.Clicked += (s, e) => ButtonDevice_Clicked(device);
                deviceButton.ImageSource = device.ConnectionStatus switch
                {
                    ConnectionStatus.Online => (ImageSource)"status_online.svg",
                    ConnectionStatus.CheckConnection => (ImageSource)"status_check.svg",
                    ConnectionStatus.Offline => (ImageSource)"status_offline.svg",
                    _ => throw new NotImplementedException()
                };

                DeviceListLayout.Add(deviceButton);
            }
        }

        private async void ButtonDevice_Clicked(ModuleInfo device)
        { // Handler for device button
            await Navigation.PushAsync(new ModuleControlPage(device));
        }

        private async void ButtonAddDevice_Clicked(object sender, EventArgs e)
        { // Handler for add device button
            await Navigation.PushAsync(new AddModulePage());
        }
    }
}
