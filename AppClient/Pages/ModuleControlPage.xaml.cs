using AppClient.DataStore;

namespace AppClient.Pages;

public partial class ModuleControlPage : ContentPage
{

	private ModuleInfo module;
	public ModuleControlPage(ModuleInfo module)
	{
		InitializeComponent();
		this.module = module;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ModuleNameField.Text = module.Name;
        ModuleDescriptionField.Text = module.Description;

        if (module.ConnectionStatus != ConnectionStatus.Online)
        {
            ConnectionStatusLayout.IsVisible = true;
            ModuleConnectionMessageField.Text = module.ConnectionMessage;
            return;
        }

        switch (module.Type)
        {
            case ModuleType.Webpage:
                NoControlLabel.IsVisible = true;
                break;

            case ModuleType.SegmentedLights:
                SegmentedLightsLayout.IsVisible = true;
                break;

            case ModuleType.SpinningLights:
                SpinningLightsLayout.IsVisible = true;
                break;

            default:
                throw new NotImplementedException();
        }
    }

    #region Segmented Lights
    private void SegmentedCommandPicker_SelectedIndexChanged(object sender, EventArgs e)
    { // TODO: Bitte irgendwie lösen
        object selectedItem = SegmentedCommandPicker.SelectedItem;
        if (selectedItem == null) return;

        bool segmentFieldEnabled;
        bool colorFieldEnabled;
        switch ((string)selectedItem)
        {
            case "Segment einschalten":
                segmentFieldEnabled = true;
                colorFieldEnabled = true;
                break;
            case "Segment exklusiv einschalten":
                segmentFieldEnabled = true;
                colorFieldEnabled = true;
                break;
            case "Auf Hintergrundfarbe setzen":
                segmentFieldEnabled = false;
                colorFieldEnabled = true;
                break;
            case "Hintergrundfarbe ändern":
                segmentFieldEnabled = false;
                colorFieldEnabled = true;
                break;
            case "Alle Segmente einschalten":
                segmentFieldEnabled = false;
                colorFieldEnabled = true;
                break;
            case "Alle Segmente ausschalten":
                segmentFieldEnabled = false;
                colorFieldEnabled = false;
                break;

            default:
                throw new NotImplementedException();
        }

        SegmentedSegmentIdField.IsEnabled = segmentFieldEnabled;
        SegmentedColorIdField.IsEnabled = colorFieldEnabled;
    }

    private async void SegmentedSendButton_Clicked(object sender, EventArgs e)
    { // TODO: Bitte irgendwie lösen
        object selectedItem = SegmentedCommandPicker.SelectedItem;
        if (selectedItem == null) return;

        string response;
        switch ((string)selectedItem)
        {
            case "Segment einschalten":
                response = await SendCommand(module, $"/segment/{SegmentedSegmentIdField.Text}/color/{SegmentedColorIdField.Text}");
                SegmentedSegmentIdField.Text = "";
                SegmentedColorIdField.Text = "";
                break;
            case "Segment exklusiv einschalten":
                response = await SendCommand(module, $"/xsegment/{SegmentedSegmentIdField.Text}/color/{SegmentedColorIdField.Text}");
                SegmentedSegmentIdField.Text = "";
                SegmentedColorIdField.Text = "";
                break;
            case "Auf Hintergrundfarbe setzen":
                response = await SendCommand(module, "/clear");
                break;
            case "Hintergrundfarbe ändern":
                response = await SendCommand(module, $"/setBackground/{SegmentedColorIdField.Text}");
                SegmentedColorIdField.Text = "";
                break;
            case "Alle Segmente einschalten":
                response = await SendCommand(module, $"/on/{SegmentedColorIdField.Text}");
                SegmentedColorIdField.Text = "";
                break;
            case "Alle Segmente ausschalten":
                response = await SendCommand(module, "/off");
                break;
            default:
                throw new NotImplementedException();
        }

        if(response != null)
            await DisplayAlert("Error: Server responded", response, "OK");
    }
    #endregion

    #region Spinning Lights
    private async void SpinningHighlightButton_Clicked(object sender, EventArgs e)
    {
        string response = await SendCommand(module, $"/highlight/{SpinningWidthField.Text}");
        if(response != null) await DisplayAlert("Error: Server responded", response, "OK");
        SpinningWidthField.Text = "";
    }

    private async void SpinningHighlightSectionButton_Clicked(object sender, EventArgs e)
    {
        string response = await SendCommand(module, $"/section/{SpinningStartField.Text}/{SpinningEndField.Text}");
        if (response != null) await DisplayAlert("Error: Server responded", response, "OK");
        SpinningEndField.Text = "";
    }

    private async void SpinningEnableButton_Clicked(object sender, EventArgs e)
    {
        string response = await SendCommand(module, $"/on/{SpinningColorField.Text}");
        if (response != null) await DisplayAlert("Error: Server responded", response, "OK");
        SpinningColorField.Text = "";
    }

    private async void SpinningDisableButton_Clicked(object sender, EventArgs e)
    {
        string response = await SendCommand(module, "/off");
        if (response != null) await DisplayAlert("Error: Server responded", response, "OK");
    }

    private async void SpinningUpdateButton_Clicked(object sender, EventArgs e)
    {
        string response;
        if (!string.IsNullOrWhiteSpace(SpinningSpeedField.Text))
        {
            response = await SendCommand(module, $"/speed/{SpinningSpeedField.Text}");
            if (response != null) await DisplayAlert("Error: Server responded", response, "OK");
            SpinningSpeedField.Text = "";
        }

        if (!string.IsNullOrWhiteSpace(SpinningDirectionField.Text))
        {
            response = await SendCommand(module, $"/setDirection/{SpinningDirectionField.Text}");
            if (response != null) await DisplayAlert("Error: Server responded", response, "OK");
            SpinningDirectionField.Text = "";
        }

        if (!string.IsNullOrWhiteSpace(SpinningBackgroundField.Text))
        {
            response = await SendCommand(module, $"/setBackground/{SpinningBackgroundField.Text}");
            if (response != null) await DisplayAlert("Error: Server responded", response, "OK");
            SpinningBackgroundField.Text = "";
        }

        if (!string.IsNullOrWhiteSpace(SpinningForegroundField.Text))
        {
            response = await SendCommand(module, $"/setForeground/{SpinningForegroundField.Text}");
            if (response != null) await DisplayAlert("Error: Server responded", response, "OK");
            SpinningForegroundField.Text = "";
        }

        if (!string.IsNullOrWhiteSpace(SpinningPixelPerSegmentField.Text))
        {
            response = await SendCommand(module, $"/setPps/{SpinningPixelPerSegmentField.Text}");
            if (response != null) await DisplayAlert("Error: Server responded", response, "OK");
            SpinningPixelPerSegmentField.Text = "";
        }

        if (!string.IsNullOrWhiteSpace(SpinningSegmentCountField.Text))
        {
            response = await SendCommand(module, $"/setNos/{SpinningSegmentCountField.Text}");
            if (response != null) await DisplayAlert("Error: Server responded", response, "OK");
            SpinningSegmentCountField.Text = "";
        }
    }
    #endregion

    private async void EditButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddModulePage(module));
    }

    private async void ButtonRemove_Clicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Question?", "Möchtest du das Gerät wirklich entfernen?", "Ja", "Nein");
        if (answer)
        {
            ModuleStore.UnregisterModule(module);
            await Navigation.PopAsync();
        }
    }

    private static async Task<string> SendCommand(ModuleInfo module, string command)
    {
        using HttpClient client = new HttpClient();

        try
        {
            using HttpResponseMessage message = await client.GetAsync(module.Host + command).ConfigureAwait(false);
            return message.IsSuccessStatusCode ? null : await message.Content.ReadAsStringAsync().ConfigureAwait(false);
        }
        catch(Exception ex)
        {
            return ex.Message;
        }
    }

}