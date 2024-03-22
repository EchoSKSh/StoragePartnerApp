using MetalPerformanceShaders;
using StoragePartnerApp.Services;

namespace StoragePartnerApp.Pages;

public partial class MyStorageDetailsPage : ContentPage
{
    private int storageId;
	public MyStorageDetailsPage(int storageId)
	{
		InitializeComponent();
        GetReservedStorageDetails(storageId);

    }

    private async void BackButtonImage_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    private async Task GetReservedStorageDetails(int storageId)
    {
        var storageDetail = await ApiService.GetStorageDetails(storageId);
        var ReservedStorages = await ApiService.GetReservedStorages();

        this.storageId = storageDetail.Id;
        ReservedDetailImage.Source = storageDetail.ImageUrl;
        PriceLabel.Text = storageDetail.Price.ToString();
        AddressLabel.Text = storageDetail.Address;
        DescriptionLabel.Text = storageDetail.Description;

        if(ReservedStorages.Any(r => r.StorageId == storageId && r.EndDate > DateTime.Now.Date && r.StatusId == 1))
        {
            var reservation = ReservedStorages.FirstOrDefault(r => r.StorageId == storageId && r.EndDate > DateTime.Now.Date && r.StatusId == 1);
            var userDetails = await ApiService.GetUserDetails(reservation.UserId);         
            lblStartDate.Text = reservation.StartDate.ToShortDateString();
            lblEndDate.Text = reservation.EndDate.ToShortDateString();
            costLabel.Text = reservation.Total.ToString();
            lblReserveText.Text = "Requested By - ";
            lblReservedBy.Text = userDetails.Name;
            reservationSummary.IsVisible = true;
            stackApproveReject.IsVisible = true;

        }
        else if(ReservedStorages.Any(r => r.StorageId == storageId && r.EndDate > DateTime.Now.Date && r.StatusId == 2))
        {
            var reservation = ReservedStorages.FirstOrDefault(r => r.StorageId == storageId && r.EndDate > DateTime.Now.Date && r.StatusId == 1);
            var userDetails = await ApiService.GetUserDetails(reservation.UserId);
            lblStartDate.Text = reservation.StartDate.ToShortDateString();
            lblEndDate.Text = reservation.EndDate.ToShortDateString();
            costLabel.Text = reservation.Total.ToString();
            lblReserveText.Text = "Reserved By - ";
            lblReservedBy.Text = userDetails.Name;
            reservationSummary.IsVisible = true;
        }
        else
        {
            stackEditDelte.IsVisible = true;
        }

    }

    private async void btnEdit_Clicked(object sender, EventArgs e)
    {
        await DisplayAlert("", "Something went wrong. Please try agian", "Ok");
    }

    private async void btnDelete_Clicked(object sender, EventArgs e)
    {
        var respone = await ApiService.DeleteStorage(storageId);

        if (respone)
        {
            await DisplayAlert("", "Storage deleted successfuly", "Ok");
            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("", "Cannot delete storage. please try again later", "Ok");
        }
    }
}