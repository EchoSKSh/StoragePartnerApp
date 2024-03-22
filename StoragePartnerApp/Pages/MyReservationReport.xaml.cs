using StoragePartnerApp.Models;
using StoragePartnerApp.Services;
using System.Collections.ObjectModel;

namespace StoragePartnerApp.Pages;

public partial class MyReservationReport : ContentPage
{
    private readonly HttpClient httpClient = new();

    public bool IsRefreshing { get; set; }
    public ObservableCollection<ReservationList> Reservations { get; set; } = new();
    public Command RefreshCommand { get; set; }
    public MyReservationReport()
	{
        RefreshCommand = new Command(async () =>
        {
            // Simulate delay
            await Task.Delay(2000);

            await LoadReservation();

            IsRefreshing = false;
            OnPropertyChanged(nameof(IsRefreshing));
        });

        BindingContext = this;

        InitializeComponent();
	}

    protected async override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        await LoadReservation();
    }

    private async void BackButtonImage_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    private async Task LoadReservation()
    {
        var reserVationList = new List<ReservationList>();
        var reservedStorages = await ApiService.GetReservedStorages();
        foreach (var storage in reservedStorages)
        {
            var storageDetails = await ApiService.GetStorageDetails(storage.StorageId);
            if (storage != null)
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

                if (storage.StatusId == 1 && storage.EndDate > DateTime.Now.Date)
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

        Reservations.Clear();

        foreach (var reservation in reserVationList)
        {
            Reservations.Add(reservation);
        }
    }
}