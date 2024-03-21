using StoragePartnerApp.Models;
using StoragePartnerApp.Services;

namespace StoragePartnerApp.Pages;

public partial class PropertyListPage : ContentPage
{
	public PropertyListPage(int categoryId, string categoryName)
	{
		InitializeComponent();

		Title = categoryName;

        GetStorageList(categoryId);

    }

	private async void GetStorageList(int categoryId)
	{
		var storageResult = new List<PropertyByCategory>();
		var propertyList = await ApiService.GetStorageByCategory(categoryId);
		var reservedStorageList = await ApiService.GetReservedStorages();
		foreach (var storage in propertyList)
		{
			if(reservedStorageList.Any(r => r.StorageId == storage.Id && r.EndDate > DateTime.Now.Date && r.StatusId == 1))
			{
				storage.Status = "Unavailable";
            }
			else if (reservedStorageList.Any(r => r.StorageId == storage.Id && r.EndDate > DateTime.Now.Date && r.StatusId == 2))
			{
				storage.Status = "Booked on";
			}
			else
			{
                storage.Status = "Available";
            }
			storageResult.Add(storage);
        }
		CvPropertyList.ItemsSource = storageResult;
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