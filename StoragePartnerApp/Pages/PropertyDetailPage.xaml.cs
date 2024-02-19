using StoragePartnerApp.Services;
using System.Runtime.CompilerServices;

namespace StoragePartnerApp.Pages;

public partial class PropertyDetailPage : ContentPage
{
    private string phoneNumber;
    private bool isBookmarkEnabled;
    private int propertyId;
    private int bookmarkId;

    public PropertyDetailPage(int propertyId)
    {
        InitializeComponent();
        GetPropertyDetail(propertyId);
    }

    private async void GetPropertyDetail(int propertyId)
    {
        var propertyDetail = await ApiService.GetPropertyDetails(propertyId);
        PriceLabel.Text = propertyDetail.Price.ToString();
        AddressLabel.Text = propertyDetail.Address;
        DescriptionLabel.Text = propertyDetail.Detail;
        PropertyDetailImage.Source = propertyDetail.FullImageUrl;
        this.propertyId = propertyDetail.Id;

        phoneNumber = propertyDetail.Phone;

        if (propertyDetail.Bookmark != null && propertyDetail.Bookmark.Status)
        {
            ReserveButtonImage.Source =  "bookmark_fill_icon.svg";
            this.bookmarkId = propertyDetail.Bookmark.Id;
            isBookmarkEnabled = true;
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
        if (!isBookmarkEnabled)
        {
            //Add bookmark
            var userId = Preferences.Get("userId", 0);
            var response = await ApiService.AddBookmark(userId, propertyId);
            if (response)
            {
                isBookmarkEnabled = true;
                ReserveButtonImage.Source = "bookmark_fill_icon.svg";
                await DisplayAlert("", "Bookmark Added", "Ok");
            }
            else
            {
                await DisplayAlert("", "Oops something went wrong", "Cancel");
            }
        }
        else
        {
            //Delete bookmark
            var response = await ApiService.DeleteBookmark(bookmarkId);
            if (response)
            {
                isBookmarkEnabled = false;
                ReserveButtonImage.Source = "bookmark_empty_icon.svg";
            }
        }
    }
}