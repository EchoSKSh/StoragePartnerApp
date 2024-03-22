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
            lblReservedBy.Text = userDetails.Name;
            reservationSummary.IsVisible = true;
        }
        else
        {
            stackEditDelte.IsVisible = true;
        }

    }
}