using StoragePartnerApp.Models;
using StoragePartnerApp.Services;

namespace StoragePartnerApp.Pages;

public partial class ReservedPage : ContentPage
{
	public ReservedPage()
	{
        InitializeComponent();
        GetReservedPropertyList();
    }

    private async void GetReservedPropertyList()
    {
        var reserVationList = new List<ReservationList>();
        var reservedStorages = await ApiService.GetReservedStorages();
        foreach (var storage in reservedStorages)
        {
            var storageDetails = await ApiService.GetStorageDetails(storage.StorageId);
            if(storage != null)
            {
                var reservationDetails = new ReservationList
                {
                    Id = storage.Id,
                    Name = storageDetails.Name,
                    Address = storageDetails.Address,
                    Total = storage.Total,
                    StartDate = storage.StartDate.ToShortDateString(),
                    EndDate = storage.EndDate.ToShortDateString(),
                    StatusId = storage.StatusId,
                    ImageUrl = storageDetails.ImageUrl
                };

                if(storage.StatusId == 1 && storage.EndDate > DateTime.Now.Date)
                {
                    reservationDetails.Status = "Active";
                }
                else
                {
                    reservationDetails.Status = "Expired";
                }
                reserVationList.Add(reservationDetails);
            }
        }
        CVReservedProperties.ItemsSource = reserVationList;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        GetReservedPropertyList();
    }

    private async void CVReservedProperties_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var currentSelection = (ReservationList)e.CurrentSelection.FirstOrDefault();
        if (currentSelection == null)
        {
            return;
        }

        await Navigation.PushModalAsync(new ReservedDetailPage(currentSelection.Id));

        ((CollectionView)sender).SelectedItem = null;
    }
}