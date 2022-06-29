using AppClient.DataStore;

namespace AppClient.Pages;

public partial class AddModulePage : ContentPage
{
    public AddModulePage()
    {
        InitializeComponent();
    }

    private async void ButtonAbort_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void ButtonSaved_Clicked(object sender, EventArgs e)
    {
        string devicename = Devicename.Text;
        if (string.IsNullOrWhiteSpace(devicename))
        { await DisplayAlert("Speichern nicht möglich", "Bitte Gerätenamen eingeben", "OK"); return; }
        string url = URL.Text;
        if (string.IsNullOrWhiteSpace(url))
        { await DisplayAlert("Speichern nicht möglich", "Bitte URL eingeben", "OK"); return; }
        string picker = (string)Picker.SelectedItem;
        if (string.IsNullOrWhiteSpace(picker))
        { await DisplayAlert("Speichern nicht möglich", "Bitte Typ auswählen", "OK"); return; }
        string describtion = Describtion.Text;
        if (string.IsNullOrWhiteSpace(describtion))
        { await DisplayAlert("Speichern nicht möglich", "Bitte Beschreibung eingeben", "OK"); return; }

        ModuleInfo device = new ModuleInfo
        {
            Name = devicename,
            Description = describtion,
            Host = url
        };

        switch (picker)
        {
            case "SegmentedLights": device.Type = ModuleType.SegmentedLights; break;
            case "SpinningLights": device.Type = ModuleType.SegmentedLights; break;
            case "Webpage": device.Type = ModuleType.Webpage; break;
            case "Unkown": device.Type = ModuleType.Unknown; break;
        }

        ModuleStore.RegisterModule(device);
        await Navigation.PopAsync();
    }
}
