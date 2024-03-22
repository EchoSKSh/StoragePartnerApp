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

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        var address = AddressLabel.Text;

        if (string.IsNullOrEmpty(address))
        {
            await DisplayAlert("Empty Address", "Please enter an address to open in Maps.", "OK");
            return;
        }

        // Construct platform-specific URIs
        string uri = $"geo:0,0?q={Uri.EscapeDataString(address)}";

        if (DeviceInfo.Platform == DevicePlatform.iOS)
        {
            uri = $"http://maps.apple.com/?daddr={Uri.EscapeDataString(address)}";
        }

        // Launch the maps app on the device
        try
        {
            await Launcher.OpenAsync(uri);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to launch Maps app: {ex.Message}", "OK");
        }
    }
}