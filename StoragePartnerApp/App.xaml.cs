using StoragePartnerApp.Pages;

namespace StoragePartnerApp;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		var accessToken = Preferences.Get("accesstoken", string.Empty);

		if(!string.IsNullOrEmpty(accessToken))
		{
            MainPage = new NavigationPage(new CustomTabbedPage());
        }
		else
        {
            MainPage = new NavigationPage(new RegisterPage());
        }

	}
}
