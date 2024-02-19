using StoragePartnerApp.Models;
using StoragePartnerApp.Services;

namespace StoragePartnerApp.Pages;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();

		SetupGreetingsMessage();

		GetCategories();

		GetTrendingProperties();
    }

	private void SetupGreetingsMessage()
	{
		var userName = Preferences.Get("username", string.Empty);
		var greetingName = userName[0].ToString().ToUpper() + userName.Substring(1);

		LblUserName.Text = "Hi " + greetingName;
	}

	private async void GetCategories()
	{
		var categories = await ApiService.GetCategories();
		CvCategories.ItemsSource = categories;
    }

	private async void GetTrendingProperties()
	{
		var trendingProperties = await ApiService.GetTrendingProperties();
		CvTopPicks.ItemsSource = trendingProperties;
	}

    private void CvCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
		var selectedItem = (Category)e.CurrentSelection.FirstOrDefault();

		if(selectedItem == null) {
			return;
		}

		Navigation.PushAsync(new PropertyListPage(selectedItem.Id, selectedItem.Name));

		((CollectionView)sender).SelectedItem = null;
    }

    private async void CvTopPicks_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
		var currentSelection = (TrendingProperty)e.CurrentSelection.FirstOrDefault();

		if (currentSelection == null) { return; }

		await Navigation.PushModalAsync(new PropertyDetailPage(currentSelection.Id));

		((CollectionView)sender).SelectedItem = null;
    }

    private async void TapSearch_Tapped(object sender, TappedEventArgs e)
    {
		await Navigation.PushModalAsync(new SearchPage());
    }
}