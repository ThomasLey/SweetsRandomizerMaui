using AppClient.DataStore;
using ModuleInfo = AppClient.DataStore.ModuleInfo;

namespace AppClient.Pages
{
    public partial class OverviewPage : ContentPage
    {

        public OverviewPage()
        {
            InitializeComponent();

            // ignore warning
            Task.WaitAll(ModuleStore.CheckConnectionStatusAsync(
                ModuleStore.Modules.ToArray()
            ));
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
                    //BindingContext = module,
                    Text = $"{module.Name} [{module.Host}]",
                    ContentLayout = new Button.ButtonContentLayout(
                        Button.ButtonContentLayout.ImagePosition.Right, 0
                    )
                };

                moduleButton.Clicked += (s, e) => ButtonModule_Clicked(module);
                //moduleButton.SetBinding(Button.ImageSourceProperty, nameof(module.StatusIcon));
                moduleButton.ImageSource = module.StatusIcon;
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
