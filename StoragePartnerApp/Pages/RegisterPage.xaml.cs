using StoragePartnerApp.Services;

namespace StoragePartnerApp.Pages;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
	}

    private async void BtnRegister_Clicked(object sender, EventArgs e)
    {
		var response = await ApiService.RegisterUser(entFullName.Text, entEmail.Text, entPassword.Text, entPhone.Text);

		if (response)
		{
			await DisplayAlert("", "Your account has been created", "ok");
			await Navigation.PushAsync(new LoginPage());
		}
		else
		{
			await DisplayAlert("", "Oops something went wrong", "cancel");
		}
    }

    private async void TapSignIn_Tapped(object sender, TappedEventArgs e)
    {
		await Navigation.PushAsync(new LoginPage());
    }
}