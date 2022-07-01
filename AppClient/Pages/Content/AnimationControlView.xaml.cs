using AppClient.DataStore;

namespace AppClient.Pages.Content;

public partial class AnimationControlView : ContentView
{

    private readonly ModuleInfo module;
	public AnimationControlView(ModuleInfo module)
	{
		InitializeComponent();
        this.module = module;
	}

    #region Colors
    private async void RedButton_Clicked(object sender, EventArgs e)
    {
        await ModuleControlPage.SendCommand(module, "/colorCode/red");
    }

    private async void GreenButton_Clicked(object sender, EventArgs e)
    {
        await ModuleControlPage.SendCommand(module, "/colorCode/green");
    }

    private async void BlueButton_Clicked(object sender, EventArgs e)
    {
        await ModuleControlPage.SendCommand(module, "/colorCode/blue");
    }

    private async void YellowButton_Clicked(object sender, EventArgs e)
    {
        await ModuleControlPage.SendCommand(module, "/colorCode/yellow");
    }

    private async void CyanButton_Clicked(object sender, EventArgs e)
    {
        await ModuleControlPage.SendCommand(module, "/colorCode/cyan");
    }

    private async void MagentaButton_Clicked(object sender, EventArgs e)
    {
        await ModuleControlPage.SendCommand(module, "/colorCode/magenta");
    }

    private async void OffButton_Clicked(object sender, EventArgs e)
    {
        await ModuleControlPage.SendCommand(module, "/colorCode/black");
    }

    private async void OnButton_Clicked(object sender, EventArgs e)
    {
        await ModuleControlPage.SendCommand(module, "/colorCode/white");
    }

    private async void SetColorButton_Clicked(object sender, EventArgs e)
    {
        await ModuleControlPage.SendCommand(module, "/color/" + AnimationColorField.Text);
    }
    #endregion

    #region Animation
    private async void AnimationPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (AnimationPicker.SelectedIndex < 0) return;

        string animation;
        switch ((string)AnimationPicker.SelectedItem)
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

        await ModuleControlPage.SendCommand(module, "/animation/" + animation);
    }

    private void AnimationSpeedSlider_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        switch ((int)Math.Round(AnimationSpeedSlider.Value))
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

        await ModuleControlPage.SendCommand(module, "/speedCode/" + speed);
    }
    #endregion

    #region Sensor
    private void AnimationMoveSlider_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        AnimationMoveLabel.Text = (int)Math.Round(AnimationMoveSlider.Value) + " Sekunden";
    }

    private async void AnimationMoveSlider_DragCompleted(object sender, EventArgs e)
    {
        int seconds = (int)Math.Round(AnimationMoveSlider.Value);
        await ModuleControlPage.SendCommand(module, $"/move/{seconds * 1000}");
    }
    #endregion

}