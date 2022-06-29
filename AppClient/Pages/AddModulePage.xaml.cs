using Java.Lang;

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

	private async void ButtonSaved_Clicked(object sender, EventArgs e)
	{
		string devicename = Devicename.Text;
		if (string.IsNullOrWhiteSpace(devicename))
		{ await DisplayAlert("Speichern nicht m�glich", "Bitte Ger�tenamen eingeben", "OK");  }
        string url = URL.Text;
        if (string.IsNullOrWhiteSpace(url))
        { await DisplayAlert("Speichern nicht m�glich", "Bitte URL eingeben", "OK"); }
        string picker = (string)Picker.SelectedItem;
        if (string.IsNullOrWhiteSpace(picker))
        { await DisplayAlert("Speichern nicht m�glich", "Bitte Typ ausw�hlen", "OK"); }
        string describtion = Describtion.Text;
        if (string.IsNullOrWhiteSpace(describtion))
        { await DisplayAlert("Speichern nicht m�glich", "Bitte Beschreibung eingeben", "OK"); }


    }
}