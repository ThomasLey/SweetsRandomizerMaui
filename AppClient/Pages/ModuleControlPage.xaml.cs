using AppClient.DataStore;
using static Microsoft.Maui.ApplicationModel.Permissions;

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

            case ModuleType.AnimationLights:
                AnimationLightsLayout.IsVisible = true;
                break;

            default:
                throw new NotImplementedException();
        }
    }

    #region Animated Lights
    private async void AnimationRedButton_Clicked(object sender, EventArgs e)
    {
        string response = await SendCommand(module, "/colorCode/red");
        if (response != null) await DisplayAlert("Error: Server responded", response, "OK");
    }

    private async void AnimationGreenButton_Clicked(object sender, EventArgs e)
    {
        string response = await SendCommand(module, "/colorCode/green");
        if (response != null) await DisplayAlert("Error: Server responded", response, "OK");
    }

    private async void AnimationBlueButton_Clicked(object sender, EventArgs e)
    {
        string response = await SendCommand(module, "/colorCode/blue");
        if (response != null) await DisplayAlert("Error: Server responded", response, "OK");
    }

    private async void AnimationYellowButton_Clicked(object sender, EventArgs e)
    {
        string response = await SendCommand(module, "/colorCode/yellow");
        if (response != null) await DisplayAlert("Error: Server responded", response, "OK");
    }

    private async void AnimationCyanButton_Clicked(object sender, EventArgs e)
    {
        string response = await SendCommand(module, "/colorCode/cyan");
        if (response != null) await DisplayAlert("Error: Server responded", response, "OK");
    }

    private async void AnimationMagentaButton_Clicked(object sender, EventArgs e)
    {
        string response = await SendCommand(module, "/colorCode/magenta");
        if (response != null) await DisplayAlert("Error: Server responded", response, "OK");
    }

    private async void AnimationOffButton_Clicked(object sender, EventArgs e)
    {
        string response = await SendCommand(module, "/colorCode/black");
        if (response != null) await DisplayAlert("Error: Server responded", response, "OK");
    }

    private async void AnimationOnButton_Clicked(object sender, EventArgs e)
    {
        string response = await SendCommand(module, "/colorCode/white");
        if (response != null) await DisplayAlert("Error: Server responded", response, "OK");
    }

    private async void AnimationPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (AnimationPicker.SelectedIndex < 0) return;

        string animation;
        switch((string)AnimationPicker.SelectedItem)
        {
            case "Statisch":
                animation = "all";
                break;
            case "Einzeln":
                animation = "single";
                break;
            case "Invertiert":
                animation = "invert";
                break;
            case "Nachziehend":
                animation = "kitt";
                break;
            default:
                throw new NotImplementedException();
        }

        string response = await SendCommand(module, "/animation/" + animation);
        if (response != null) await DisplayAlert("Error: Server responded", response, "OK");
    }

    private void AnimationSpeedSlider_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        switch((int)Math.Round(AnimationSpeedSlider.Value))
        {
            case 0:
                AnimationSpeedLabel.Text = "Schildkröte";
                break;
            case 1:
                AnimationSpeedLabel.Text = "Sehr langsam";
                break;
            case 2:
                AnimationSpeedLabel.Text = "Langsam";
                break;
            case 3:
                AnimationSpeedLabel.Text = "Medium";
                break;
            case 4:
                AnimationSpeedLabel.Text = "Schnell";
                break;
            case 5:
                AnimationSpeedLabel.Text = "Sehr schnell";
                break;
            case 6:
                AnimationSpeedLabel.Text = "Maximum";
                break;

            default:
                throw new NotImplementedException();
        }
    }

    private async void AnimationSpeedSlider_DragCompleted(object sender, EventArgs e)
    {
        string speed;
        switch ((int)Math.Round(AnimationSpeedSlider.Value))
        {
            case 0:
                speed = "turtle";
                break;
            case 1:
                speed = "veryslow";
                break;
            case 2:
                speed = "slow";
                break;
            case 3:
                speed = "medium";
                break;
            case 4:
                speed = "fast";
                break;
            case 5:
                speed = "veryfast";
                break;
            case 6:
                speed = "max";
                break;

            default:
                throw new NotImplementedException();
        }

        string response = await SendCommand(module, "/speedCode/" + speed);
        if (response != null) await DisplayAlert("Error: Server responded", response, "OK");
    }

    private void AnimationMoveSlider_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        AnimationMoveLabel.Text = (int)Math.Round(AnimationMoveSlider.Value) + " Sekunden";
    }

    private async void AnimationMoveSlider_DragCompleted(object sender, EventArgs e)
    {
        int seconds = (int)Math.Round(AnimationMoveSlider.Value);
        string response = await SendCommand(module, "/move/" + (seconds * 1000));
        if (response != null) await DisplayAlert("Error: Server responded", response, "OK");
    }

    private async void AnimationColorButton_Clicked(object sender, EventArgs e)
    {
        string response = await SendCommand(module, "/color/" + AnimationColorField.Text);
        if (response != null) await DisplayAlert("Error: Server responded", response, "OK");
    }
    #endregion

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