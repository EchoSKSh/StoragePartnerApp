using StoragePartnerApp.Models;
using StoragePartnerApp.Services;

namespace StoragePartnerApp.Pages;

public partial class SearchPage : ContentPage
{
	public SearchPage()
	{
		InitializeComponent();
	}

    private void ImgBackButton_Clicked(object sender, EventArgs e)
    {
		Navigation.PopModalAsync();
    }

    private async void SearchBarProperty_TextChanged(object sender, TextChangedEventArgs e)
    {
        var searchText = e.NewTextValue;

        if (searchText == null)
            return;

        var propertyResults = await ApiService.SearchStorage(searchText);

        if(propertyResults != null)
        {
            CVSearchProperty.ItemsSource = propertyResults;
        }
    }

    private async void CVSearchProperty_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var currentSelection = (PropertyByCategory)e.CurrentSelection.FirstOrDefault();

        if (currentSelection == null)
        {
            return;
        }

        await Navigation.PushModalAsync(new PropertyDetailPage(currentSelection.Id));

        ((CollectionView)sender).SelectedItem = null;
    }
}