using StoragePartnerApp.Services;

namespace StoragePartnerApp.Pages;

public partial class MyStorage : ContentPage
{
	public MyStorage()
	{
		InitializeComponent();

		GetMyStorages();
	}

    private void CvMyStorage_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }

	private async Task GetMyStorages()
	{
		var myStorage = await ApiService.GetMyStorage();
		CvMyStorage.ItemsSource = myStorage;
	}

    private async void AddNewStorage_Clicked(object sender, EventArgs e)
    {
		await Navigation.PushAsync(new AddNewStorage());
    }
}