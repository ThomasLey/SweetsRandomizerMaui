using AppClient.DataStore;

namespace AppClient.Pages;

public partial class ModuleControlPage : ContentPage
{

	private ModuleInfo device;
	public ModuleControlPage(ModuleInfo device)
	{
		InitializeComponent();
		this.device = device;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ModuleNameField.Text = device.Name;
        ModuleDescriptionField.Text = device.Description;

        if (device.ConnectionStatus != ConnectionStatus.Online)
        {
            ConnectionStatusLayout.IsVisible = true;
            ModuleConnectionMessageField.Text = device.ConnectionMessage;
            return;
        }

        switch (device.Type)
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

            case ModuleType.Unknown:
            default:
                throw new NotImplementedException();
        }
    }

    #region Segmented Lights
    private void SegmentedCommandPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
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
    {
        object selectedItem = SegmentedCommandPicker.SelectedItem;
        if (selectedItem == null) return;

        string response;
        switch ((string)selectedItem)
        {
            case "Segment einschalten":
                response = await SendCommand(device, $"/segment/{SegmentedSegmentIdField.Text}/color/{SegmentedColorIdField.Text}");
                SegmentedSegmentIdField.Text = "";
                SegmentedColorIdField.Text = "";
                break;
            case "Segment exklusiv einschalten":
                response = await SendCommand(device, $"/xsegment/{SegmentedSegmentIdField.Text}/color/{SegmentedColorIdField.Text}");
                SegmentedSegmentIdField.Text = "";
                SegmentedColorIdField.Text = "";
                break;
            case "Auf Hintergrundfarbe setzen":
                response = await SendCommand(device, "/clear");
                break;
            case "Hintergrundfarbe ändern":
                response = await SendCommand(device, $"/setBackground/{SegmentedColorIdField.Text}"); SegmentedSegmentIdField.Text = "";
                SegmentedColorIdField.Text = "";
                break;
            case "Alle Segmente einschalten":
                response = await SendCommand(device, $"/on/{SegmentedColorIdField.Text}"); SegmentedSegmentIdField.Text = "";
                SegmentedColorIdField.Text = "";
                break;
            case "Alle Segmente ausschalten":
                response = await SendCommand(device, "/off");
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
        string response = await SendCommand(device, $"/highlight/{SpinningWidthField.Text}");
        if(response != null) await DisplayAlert("Error: Server responded", response, "OK");
    }

    private async void SpinningHighlightSectionButton_Clicked(object sender, EventArgs e)
    {
        string response = await SendCommand(device, $"/section/{SpinningStartField.Text}/{SpinningEndField.Text}");
        if (response != null) await DisplayAlert("Error: Server responded", response, "OK");
    }

    private async void SpinningEnableButton_Clicked(object sender, EventArgs e)
    {
        string response = await SendCommand(device, $"/on/{SpinningColorField.Text}");
        if (response != null) await DisplayAlert("Error: Server responded", response, "OK");
    }

    private async void SpinningDisableButton_Clicked(object sender, EventArgs e)
    {
        string response = await SendCommand(device, "/off");
        if (response != null) await DisplayAlert("Error: Server responded", response, "OK");
    }

    private async void SpinningUpdateButton_Clicked(object sender, EventArgs e)
    {
        string response;
        if (!string.IsNullOrWhiteSpace(SpinningSpeedField.Text))
        {
            response = await SendCommand(device, $"/speed/{SpinningSpeedField.Text}");
            if (response != null) await DisplayAlert("Error: Server responded", response, "OK");
            SpinningSpeedField.Text = "";
        }

        if (!string.IsNullOrWhiteSpace(SpinningDirectionField.Text))
        {
            response = await SendCommand(device, $"/setDirection/{SpinningDirectionField.Text}");
            if (response != null) await DisplayAlert("Error: Server responded", response, "OK");
            SpinningDirectionField.Text = "";
        }

        if (!string.IsNullOrWhiteSpace(SpinningBackgroundField.Text))
        {
            response = await SendCommand(device, $"/setBackground/{SpinningBackgroundField.Text}");
            if (response != null) await DisplayAlert("Error: Server responded", response, "OK");
            SpinningBackgroundField.Text = "";
        }

        if (!string.IsNullOrWhiteSpace(SpinningForegroundField.Text))
        {
            response = await SendCommand(device, $"/setForeground/{SpinningForegroundField.Text}");
            if (response != null) await DisplayAlert("Error: Server responded", response, "OK");
            SpinningForegroundField.Text = "";
        }

        if (!string.IsNullOrWhiteSpace(SpinningPixelPerSegmentField.Text))
        {
            response = await SendCommand(device, $"/setPps/{SpinningPixelPerSegmentField.Text}");
            if (response != null) await DisplayAlert("Error: Server responded", response, "OK");
            SpinningPixelPerSegmentField.Text = "";
        }

        if (!string.IsNullOrWhiteSpace(SpinningSegmentCountField.Text))
        {
            response = await SendCommand(device, $"/setNos/{SpinningSegmentCountField.Text}");
            if (response != null) await DisplayAlert("Error: Server responded", response, "OK");
            SpinningSegmentCountField.Text = "";
        }
    }
    #endregion

    private async void EditButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddModulePage(device));
    }

    private static async Task<string> SendCommand(ModuleInfo device, string command)
    {
        using HttpClient client = new HttpClient();

        try
        {
            using HttpResponseMessage message = await client.GetAsync(device.Host + command).ConfigureAwait(false);
            return message.IsSuccessStatusCode ? null : await message.Content.ReadAsStringAsync().ConfigureAwait(false);
        }
        catch(Exception ex)
        {
            return ex.Message;
        }
    }

}