using AppClient.DataStore;

namespace AppClient.Pages.Content;

public partial class SpinningControlView : ContentView
{

    private readonly ModuleInfo module;
	public SpinningControlView(ModuleInfo module)
	{
		InitializeComponent();
        this.module = module;
	}

    private async void HighlightButton_Clicked(object sender, EventArgs e)
    {
        await ModuleControlPage.SendCommand(module, $"/highlight/{SpinningWidthField.Text}");
        SpinningWidthField.Text = "";
    }

    private async void HighlightSectionButton_Clicked(object sender, EventArgs e)
    {
        await ModuleControlPage.SendCommand(module, $"/section/{SpinningStartField.Text}/{SpinningEndField.Text}");
        SpinningEndField.Text = "";
    }

    private async void EnableButton_Clicked(object sender, EventArgs e)
    {
        await ModuleControlPage.SendCommand(module, $"/on/{SpinningColorField.Text}");
        SpinningColorField.Text = "";
    }

    private async void DisableButton_Clicked(object sender, EventArgs e)
    {
        await ModuleControlPage.SendCommand(module, "/off");
    }

    private async void UpdateButton_Clicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(SpinningSpeedField.Text))
        {
            await ModuleControlPage.SendCommand(module, $"/speed/{SpinningSpeedField.Text}");
            SpinningSpeedField.Text = "";
        }

        if (!string.IsNullOrWhiteSpace(SpinningDirectionField.Text))
        {
            await ModuleControlPage.SendCommand(module, $"/setDirection/{SpinningDirectionField.Text}");
            SpinningDirectionField.Text = "";
        }

        if (!string.IsNullOrWhiteSpace(SpinningBackgroundField.Text))
        {
            await ModuleControlPage.SendCommand(module, $"/setBackground/{SpinningBackgroundField.Text}");
            SpinningBackgroundField.Text = "";
        }

        if (!string.IsNullOrWhiteSpace(SpinningForegroundField.Text))
        {
            await ModuleControlPage.SendCommand(module, $"/setForeground/{SpinningForegroundField.Text}");
            SpinningForegroundField.Text = "";
        }

        if (!string.IsNullOrWhiteSpace(SpinningPixelPerSegmentField.Text))
        {
            await ModuleControlPage.SendCommand(module, $"/setPps/{SpinningPixelPerSegmentField.Text}");
            SpinningPixelPerSegmentField.Text = "";
        }

        if (!string.IsNullOrWhiteSpace(SpinningSegmentCountField.Text))
        {
            await ModuleControlPage.SendCommand(module, $"/setNos/{SpinningSegmentCountField.Text}");
            SpinningSegmentCountField.Text = "";
        }
    }

}