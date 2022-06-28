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

}