using StoragePartnerApp.Services;

namespace StoragePartnerApp.Pages;

public partial class ReservedDetailPage : ContentPage
{
    private int reservationId;

    public ReservedDetailPage(int reservationId)
	{
		InitializeComponent();
        GetReservedStorageDetails(reservationId);

    }

    private async void BackButtonImage_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    private async Task GetReservedStorageDetails(int reservationId)
    {
        var reservationDetail = await ApiService.GetReservedStorageDetails(reservationId);
        var storageDetail = await ApiService.GetStorageDetails(reservationDetail.StorageId);

        this.reservationId = reservationDetail.Id;
        ReservedDetailImage.Source = storageDetail.ImageUrl;
        PriceLabel.Text = storageDetail.Price.ToString();
        AddressLabel.Text = storageDetail.Address;
        DescriptionLabel.Text = storageDetail.Description;
        lblStartDate.Text = reservationDetail.StartDate.ToShortDateString();
        lblEndDate.Text = reservationDetail.EndDate.ToShortDateString();
        costLabel.Text = reservationDetail.Total.ToString();
    }
}