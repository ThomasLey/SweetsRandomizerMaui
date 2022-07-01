using AppClient.DataStore;

namespace AppClient.Pages.Content;

public partial class WebpageControlView : ContentView
{

	private readonly ModuleInfo module;
	public WebpageControlView(ModuleInfo module)
	{
		InitializeComponent();
		this.module = module;
	}

	private async void OpenBrowserButton_Clicked(object sender, EventArgs e)
    {
		try
        {
            await Browser.Default.OpenAsync(
                new Uri(module.Host),
                BrowserLaunchMode.SystemPreferred
            );
        }
		catch (Exception ex)
		{
			await Shell.Current.DisplayAlert(
				"Browser konnte nicht geöffnet werden", ex.Message, "OK"
			);
		}
    }

}