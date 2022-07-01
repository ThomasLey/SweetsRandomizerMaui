using AppClient.DataStore;
using AppClient.Pages.Content;
using Microsoft.Maui.Controls;

namespace AppClient.Pages;

public partial class ModuleControlPage : ContentPage
{

	private readonly ModuleInfo module;
	public ModuleControlPage(ModuleInfo module)
	{
		InitializeComponent();
		this.module = module;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Set generic information
        ModuleNameField.Text = module.Name;
        ModuleDescriptionField.Text = module.Description;

        // Set online status and control status
        if (module.ConnectionStatus != ConnectionStatus.Online)
        {
            ConnectionStatusLayout.IsVisible = true;
            ModuleConnectionMessageField.Text = module.ConnectionMessage;
            return;
        }

        // Select ControlView
        ContentView controlView;
        switch (module.Type)
        {
            case ModuleType.Webpage:
                controlView = new WebpageControlView(module);
                break;

            case ModuleType.SegmentedLights:
                controlView = new SegmentControlView(module);
                break;

            case ModuleType.SpinningLights:
                controlView = new SpinningControlView(module);
                break;

            case ModuleType.AnimationLights:
                controlView = new AnimationControlView(module);
                break;

            default:
                throw new NotImplementedException();
        }

        ControlLayout.Add(controlView);
    }

    private async void EditButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddModulePage(module));
    }

    private async void ButtonRemove_Clicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Bestätigung", "Möchtest du das Gerät wirklich entfernen?", "Ja", "Nein");
        if (answer)
        {
            ModuleStore.UnregisterModule(module);
            await Navigation.PopAsync();
        }
    }

    public static async Task SendCommand(ModuleInfo module, string command)
    {
        using HttpClient client = new HttpClient();

        string response;
        try
        {
            using HttpResponseMessage message = await client.GetAsync(module.Host + command).ConfigureAwait(false);
            response = message.IsSuccessStatusCode ? null : await message.Content.ReadAsStringAsync().ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            response = ex.Message;
        }

        if (response != null)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
                await Application.Current.MainPage.DisplayAlert("Fehler!", response, "OK"));
        }
    }

}