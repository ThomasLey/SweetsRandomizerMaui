using AppClient.DataStore;

namespace AppClient.Pages;

public partial class ModuleControlPage : ContentPage
{

	private ModuleInfo device;
	public ModuleControlPage(ModuleInfo device)
	{
		InitializeComponent();
		this.device = device;

		ModuleNameField.Text = device.Name;
		ModuleDescriptionField.Text = device.Description;

		if(device.ConnectionStatus != ConnectionStatus.Online)
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
        bool segmentFieldEnabled;
        bool colorFieldEnabled;
        switch ((string)SegmentedCommandPicker.SelectedItem)
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
        switch ((string)SegmentedCommandPicker.SelectedItem)
        {
            case "Segment einschalten":
                await SendCommand(device, $"/segment/{SegmentedSegmentIdField.Text}/color/{SegmentedColorIdField.Text}");
                break;
            case "Segment exklusiv einschalten":
                await SendCommand(device, $"/xsegment/{SegmentedSegmentIdField.Text}/color/{SegmentedColorIdField.Text}");
                break;
            case "Auf Hintergrundfarbe setzen":
                await SendCommand(device, "/clear");
                break;
            case "Hintergrundfarbe ändern":
                await SendCommand(device, $"/setBackground/{SegmentedColorIdField.Text}");
                break;
            case "Alle Segmente einschalten":
                await SendCommand(device, $"/on/{SegmentedColorIdField.Text}");
                break;
            case "Alle Segmente ausschalten":
                await SendCommand(device, "/off");
                break;

            default:
                throw new NotImplementedException();
        }
    }
    #endregion

    private async Task SendCommand(ModuleInfo device, string command)
    {
        using HttpClient client = new HttpClient();

        try
        {
            using HttpResponseMessage message = await client.GetAsync(device.Host + command).ConfigureAwait(false);
            if (!message.IsSuccessStatusCode)
            {
                string response = await message.Content.ReadAsStringAsync().ConfigureAwait(false);
                await DisplayAlert("Error: Server responded:", response, "OK");
            }
        }
        catch(Exception ex)
        {
            await DisplayAlert("Error: ", ex.Message, "OK");
        }
    }

}