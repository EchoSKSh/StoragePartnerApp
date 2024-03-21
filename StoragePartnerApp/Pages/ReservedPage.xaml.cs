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
}