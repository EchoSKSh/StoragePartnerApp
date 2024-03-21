using StoragePartnerApp.Models;
using StoragePartnerApp.Services;

namespace StoragePartnerApp.Pages;

public partial class PropertyDetailPage : ContentPage
{
    private string phoneNumber;
    private bool isReserved;
    private int storageId;
    private double price;
    private double totalCost;

    public PropertyDetailPage(int storageId)
    {
        InitializeComponent();
        GetPropertyDetail(storageId);
    }

    private async Task GetPropertyDetail(int storageId)
    {
        var propertyDetail = await ApiService.GetStorageDetails(storageId);
        var reservedStorages = await ApiService.GetReservedStorages();
        PriceLabel.Text = propertyDetail.Price.ToString();
        price = propertyDetail.Price;
        AddressLabel.Text = propertyDetail.Address;
        DescriptionLabel.Text = propertyDetail.Description;
        PropertyDetailImage.Source = propertyDetail.ImageUrl;
        this.storageId = propertyDetail.Id;
        phoneNumber = propertyDetail.Phone;

        if (reservedStorages.Any(r => r.StorageId == propertyDetail.Id && r.EndDate > DateTime.Now.Date && (r.StatusId == 1 || r.StatusId == 2)))
        {
            ReserveButtonImage.Source =  "bookmark_fill_icon.svg";
            isReserved = true;
        }
        else
        {
            ReserveButtonImage.Source = "bookmark_empty_icon.svg" ;
        }
    }

    private void TapMessage_Tapped(object sender, TappedEventArgs e)
    {
        var message = new SmsMessage("Hi!, I am interested in your property", phoneNumber);
        Sms.ComposeAsync(message);
    }

    private void TapCall_Tapped(object sender, TappedEventArgs e)
    {
        PhoneDialer.Open(phoneNumber);
    }

    private async void BackButtonImage_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    private async void ReserveButtonImage_Clicked(object sender, EventArgs e)
    {
        if (!isReserved)
        {
            var reservation = new Reservation
            {
                StorageId = storageId,
                StartDate = DateTime.Now.Date,
                EndDate = DateTime.Now.Date.AddDays(1),
                StatusId = 1
            };

            startDateStack.IsVisible = true; 
            endDateStack.IsVisible = true;
        }
        else
        {
            await DisplayAlert("", "Storage is already booked on. Please try again later", "Ok");
        }

    }

    private async void OnDateSelected(object sender, DateChangedEventArgs args)
    {
        if (startDatePicker.Date != null && endDatePicker.Date != null)
        {
            if (startDatePicker.Date < DateTime.Now.Date)
            {
                await DisplayAlert("", "Please select valid start date", "Ok");
            }
            else if(startDatePicker.Date > endDatePicker.Date)
            {
                await DisplayAlert("", "Start date cannot be greater than end date", "Ok");
            }
            else if(startDatePicker.Date == endDatePicker.Date)
            {
                await DisplayAlert("", "Invalid end date", "Ok");
            }
            else if (endDatePicker.Date > DateTime.Now.Date.AddDays(30))
            {
                await DisplayAlert("", "Storage cannot be reserved for more than 30 days", "Ok");
            }
            else
            {
                TimeSpan timeSpan = endDatePicker.Date - startDatePicker.Date;
                int days = timeSpan.Days;
                double cost = CalculateCost(days, price);
                costLabel.Text = cost.ToString();
                totalCost = cost;

                costLabelStack.IsVisible = true;
                btnReserve.IsVisible = true;
            }
        }
    }

    private double CalculateCost(int days, double perDayCost)
    {
        return days * perDayCost;
    }

    private async void btnReserve_Clicked(object sender, EventArgs e)
    {
        var reservation = new Reservation
        {
            StorageId = storageId,
            StartDate = startDatePicker.Date,
            EndDate = endDatePicker.Date,
            StatusId = 1,
            Total = totalCost
        };

        var response = await ApiService.AddReservation(reservation);
        if (response)
        {
            isReserved = true;
            ReserveButtonImage.Source = "bookmark_fill_icon.svg";
            await DisplayAlert("", "Successfully reserved", "Ok");
        }
        else
        {
            await DisplayAlert("", "Oops something went wrong", "Cancel");
        }
    }
}