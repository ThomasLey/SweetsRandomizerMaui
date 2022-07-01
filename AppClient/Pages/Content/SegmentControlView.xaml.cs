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

        switch ((string)selectedItem)
        {
            case "Segment einschalten":
                await ModuleControlPage.SendCommand(module, $"/segment/{SegmentedSegmentIdField.Text}/color/{SegmentedColorIdField.Text}");
                SegmentedSegmentIdField.Text = "";
                SegmentedColorIdField.Text = "";
                break;
            case "Segment exklusiv einschalten":
                await ModuleControlPage.SendCommand(module, $"/xsegment/{SegmentedSegmentIdField.Text}/color/{SegmentedColorIdField.Text}");
                SegmentedSegmentIdField.Text = "";
                SegmentedColorIdField.Text = "";
                break;
            case "Auf Hintergrundfarbe setzen":
                await ModuleControlPage.SendCommand(module, "/clear");
                break;
            case "Hintergrundfarbe ändern":
                await ModuleControlPage.SendCommand(module, $"/setBackground/{SegmentedColorIdField.Text}");
                SegmentedColorIdField.Text = "";
                break;
            case "Alle Segmente einschalten":
                await ModuleControlPage.SendCommand(module, $"/on/{SegmentedColorIdField.Text}");
                SegmentedColorIdField.Text = "";
                break;
            case "Alle Segmente ausschalten":
                await ModuleControlPage.SendCommand(module, "/off");
                break;
            default:
                throw new NotImplementedException();
        }
    }

}