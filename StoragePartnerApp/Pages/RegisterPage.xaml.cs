using StoragePartnerApp.Services;
using System.Text.RegularExpressions;

namespace StoragePartnerApp.Pages;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
	}

    private async void BtnRegister_Clicked(object sender, EventArgs e)
    {
		if (string.IsNullOrEmpty(entFullName.Text))
		{
			await DisplayAlert("", "Please enter the name to proceed further", "OK");
		}
		else if (string.IsNullOrEmpty(entEmail.Text))
		{
            await DisplayAlert("", "Please enter the email to proceed further", "OK");
        }
        else if (string.IsNullOrEmpty(entPassword.Text))
        {
            await DisplayAlert("", "Please enter the password to proceed further", "OK");
        }
        else if (string.IsNullOrEmpty(entPhone.Text))
        {
            await DisplayAlert("", "Please enter the password to proceed further", "OK");
        }
        else if (!entEmail.Text.Contains("@") || !entEmail.Text.Contains("."))
        {
            await DisplayAlert("", "Email is invalid, Please proceed with a valid email", "OK");
        }
        else if (!Regex.IsMatch(entPhone.Text, @"^\d{10}$"))
        {
            await DisplayAlert("", "Please enter a valid 10 digit phone number to continue with the registration", "OK");
        }
        else
        {
            var response = await ApiService.RegisterUser(entFullName.Text, entEmail.Text, entPassword.Text, entPhone.Text);

            if (response)
            {
                await DisplayAlert("", "Your account has been created", "ok");
                await Navigation.PushAsync(new LoginPage());
            }
            else
            {
                await DisplayAlert("", "User already exists, Please proceed with a diferent email", "cancel");
            }
        }
    }

    private async void TapSignIn_Tapped(object sender, TappedEventArgs e)
    {
		await Navigation.PushAsync(new LoginPage());
    }
}