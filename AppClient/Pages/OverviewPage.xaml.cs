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
            ModuleStore.CheckConnectionStatusAsync(
                ModuleStore.Modules.ToArray()
            );
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // (Re-)Load modules
            ModuleListLayout.Clear();
            foreach (ModuleInfo module in ModuleStore.Modules)
            {
                Button moduleButton = new Button
                {
                    BindingContext = module,
                    ContentLayout = new Button.ButtonContentLayout(
                        Button.ButtonContentLayout.ImagePosition.Right, 0
                    )
                };

                moduleButton.Clicked += (s, e) => ButtonModule_Clicked(module);
                moduleButton.SetBinding(Button.BackgroundColorProperty, nameof(module.StatusColor));
                moduleButton.SetBinding(Button.TextProperty, nameof(module.DisplayName));
                ModuleListLayout.Add(moduleButton);
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            // Remove bindings
            foreach(Button moduleButton in ModuleListLayout)
            {
                moduleButton.RemoveBinding(Button.BackgroundColorProperty);
                moduleButton.RemoveBinding(Button.TextProperty);
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
