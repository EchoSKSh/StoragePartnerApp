using StoragePartnerApp.Services;

namespace StoragePartnerApp.Pages;

public partial class BookmarkPage : ContentPage
{
	public BookmarkPage()
	{
		InitializeComponent();
		GetReservedPropertyList();
	}

    private async void GetReservedPropertyList()
    {
		var reservedProperties = await ApiService.GetBookmarks();
		CVReservedProperties.ItemsSource = reservedProperties;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        GetReservedPropertyList();
    }
}