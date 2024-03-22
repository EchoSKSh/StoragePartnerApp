namespace StoragePartnerApp.Pages;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();
	}

    private async void SignOut_Clicked(object sender, EventArgs e)
    {
        Preferences.Set("accesstoken", string.Empty);
        Application.Current.MainPage  = new NavigationPage(new RegisterPage());
    }

    private async void myStorageReport_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new MyStorageReport());
    }

    private async void reservationDetailReport_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new MyReservationReport());
    }
}