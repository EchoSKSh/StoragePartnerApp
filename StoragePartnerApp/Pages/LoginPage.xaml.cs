using StoragePartnerApp.Services;

namespace StoragePartnerApp.Pages;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

    private async void TapJoinNow_Tapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new RegisterPage());
    }

    private async void BtnSignIn_Clicked(object sender, EventArgs e)
    {

        if (string.IsNullOrEmpty(EntEmail.Text))
        {
            await DisplayAlert("", "Please enter the email to login", "OK");
        }
        else if (string.IsNullOrEmpty(EntPassword.Text))
        {
            await DisplayAlert("", "Please enter the password login", "OK");
        }
        else
        {
            var response = await ApiService.Login(EntEmail.Text, EntPassword.Text);

            if (response)
            {
                Application.Current.MainPage = new CustomTabbedPage();
            }
            else
            {
                await DisplayAlert("", "Please enter valid email or password", "Cancel");
            }
        }

    }
}