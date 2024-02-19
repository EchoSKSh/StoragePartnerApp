using StoragePartnerApp.Models;
using StoragePartnerApp.Services;

namespace StoragePartnerApp.Pages;

public partial class PropertyListPage : ContentPage
{
	public PropertyListPage(int categoryId, string categoryName)
	{
		InitializeComponent();

		Title = categoryName;

		GetCategoriesList(categoryId);

    }

	private async void GetCategoriesList(int categoryId)
	{
		var propertyList = await ApiService.GetPropertyByCategory(categoryId);
		CvPropertyList.ItemsSource = propertyList;
	}

    private async void CvPropertyList_SelectionChanged(object sender, SelectionChangedEventArgs e)
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