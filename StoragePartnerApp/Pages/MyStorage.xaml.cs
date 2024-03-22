using StoragePartnerApp.Models;
using StoragePartnerApp.Services;

namespace StoragePartnerApp.Pages;

public partial class MyStorage : ContentPage
{
	public MyStorage()
	{
		InitializeComponent();

		GetMyStorages();
	}

    private async void CvMyStorage_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var currentSelection = (Models.MyStorage)e.CurrentSelection.FirstOrDefault();
        if (currentSelection == null)
        {
            return;
        }

        await Navigation.PushModalAsync(new MyStorageDetailsPage(currentSelection.Id));

        ((CollectionView)sender).SelectedItem = null;
    }

	private async Task GetMyStorages()
	{
		var myStorageList = new List<Models.MyStorage>();

		var myStorages = await ApiService.GetMyStorage();
        var reservedStorages = await ApiService.GetReservedStorages();

		foreach(var storage in myStorages)
		{
            if (reservedStorages.Any(r => r.StorageId == storage.Id && r.EndDate > DateTime.Now.Date && r.StatusId == 1))
            {
                storage.Status = "Pending Aproval";
                //statusStack.IsVisible = true;
            }
            else if (reservedStorages.Any(r => r.StorageId == storage.Id && r.EndDate > DateTime.Now.Date && r.StatusId == 2))
            {
                storage.Status = "Booked on";
                //statusStack.IsVisible = true;
            }

            myStorageList.Add(storage);
        }
        CvMyStorage.ItemsSource = myStorageList;

	}

    private async void AddNewStorage_Clicked(object sender, EventArgs e)
    {
		await Navigation.PushAsync(new AddNewStorage());
    }
}