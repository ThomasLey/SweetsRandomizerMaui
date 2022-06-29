using AppClient.DataStore;
using ModuleInfo = AppClient.DataStore.ModuleInfo;

namespace AppClient.Pages
{
    public partial class OverviewPage : ContentPage
    {

        public OverviewPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Load modules
            ModuleListLayout.Clear();
            foreach (ModuleInfo module in ModuleStore.Modules)
            {
                Button moduleButton = new Button
                {
                    Text = $"{module.Name} [{module.Host}]",
                    ContentLayout = new Button.ButtonContentLayout(
                        Button.ButtonContentLayout.ImagePosition.Right, 0
                    )
                };

                moduleButton.Clicked += (s, e) => ButtonModule_Clicked(module);
                moduleButton.ImageSource = module.ConnectionStatus switch
                {
                    ConnectionStatus.Online => (ImageSource)"status_online.svg",
                    ConnectionStatus.CheckConnection => (ImageSource)"status_check.svg",
                    ConnectionStatus.Offline => (ImageSource)"status_offline.svg",
                    _ => throw new NotImplementedException()
                };

                ModuleListLayout.Add(moduleButton);
            }
        }

        private async void ButtonModule_Clicked(ModuleInfo module)
        { // Handler for module button
            await Navigation.PushAsync(new ModuleControlPage(module));
        }

        private async void ButtonAddModule_Clicked(object sender, EventArgs e)
        { // Handler for add module button
            await Navigation.PushAsync(new AddModulePage());
        }
    }
}
