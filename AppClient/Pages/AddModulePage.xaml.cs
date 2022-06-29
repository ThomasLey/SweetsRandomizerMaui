using AppClient.DataStore;

namespace AppClient.Pages;

public partial class AddModulePage : ContentPage
{

    private ModuleInfo module;
    public AddModulePage(ModuleInfo module)
    {
        InitializeComponent();
        this.module = module;
    }

    public AddModulePage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (module == null) return;

        ModuleNameField.Text = module.Name;
        Host.Text = module.Host;
        Picker.SelectedIndex = (int)module.Type;
        Description.Text = module.Description;
    }

    private async void ButtonAbort_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void ButtonSaved_Clicked(object sender, EventArgs e)
    {
        string moduleName = ModuleNameField.Text;
        if (string.IsNullOrWhiteSpace(moduleName))
        { 
            await DisplayAlert("Speichern nicht möglich", "Bitte Gerätenamen eingeben", "OK");
            return; 
        }

        string host = Host.Text;
        if (string.IsNullOrWhiteSpace(host))
        { 
            await DisplayAlert("Speichern nicht möglich", "Bitte URL eingeben", "OK"); 
            return; 
        }

        if (Picker.SelectedIndex == -1)
        { 
            await DisplayAlert("Speichern nicht möglich", "Bitte Typ auswählen", "OK");
            return; 
        }

        string description = Description.Text;
        if (string.IsNullOrWhiteSpace(description))
        { 
            await DisplayAlert("Speichern nicht möglich", "Bitte Beschreibung eingeben", "OK"); 
            return; 
        }

        bool notRegistered = module == null;
        if(module == null)
            module = new ModuleInfo();

        module.Name = moduleName;
        module.Description = description;
        module.Host = host;
        module.Type = (ModuleType)Picker.SelectedIndex;

        if (notRegistered)
            ModuleStore.RegisterModule(module);
        else
            Task.WaitAll(ModuleStore.CheckConnectionStatusAsync(module));

        await Navigation.PopAsync();
    }
}
