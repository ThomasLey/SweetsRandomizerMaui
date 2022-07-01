using AppClient.DataStore;

namespace AppClient.Pages.Content;

public partial class SegmentControlView : ContentView
{

    private readonly ModuleInfo module;
    public SegmentControlView(ModuleInfo module)
    {
        InitializeComponent();
        this.module = module;
    }

    private void CommandPicker_SelectedIndexChanged(object sender, EventArgs e)
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

    private async void SendButton_Clicked(object sender, EventArgs e)
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

        if (response != null)
            await Application.Current.MainPage.DisplayAlert("Error: Server responded", response, "OK");
    }

    private static async Task<string> SendCommand(ModuleInfo module, string command)
    { // TODO: Make one
        using HttpClient client = new HttpClient();

        try
        {
            using HttpResponseMessage message = await client.GetAsync(module.Host + command).ConfigureAwait(false);
            return message.IsSuccessStatusCode ? null : await message.Content.ReadAsStringAsync().ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

}