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
}