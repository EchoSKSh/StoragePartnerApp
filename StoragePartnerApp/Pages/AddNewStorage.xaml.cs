using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Maui.Maps;
using StoragePartnerApp.Helpers;
using StoragePartnerApp.Models;
using StoragePartnerApp.Services;
using System.Net.Http.Headers;

namespace StoragePartnerApp.Pages;

public partial class AddNewStorage : ContentPage
{
    private IFormFile UploadedFile;
    private double longtitude;
    private double latitude;

    public AddNewStorage()
	{
        InitializeComponent();

        GetCategories();
        entAddress.Completed += OnAddressEntered;
    }

	public async Task GetCategories()
	{
        await Task.Delay(100);
        var categories = await ApiService.GetCategories();
        await Task.Delay(100);
        pickerCategory.ItemsSource = categories;
		await Task.Delay(100);
    }

    private async void OnAddressEntered(object sender, EventArgs e)
    {
        var address = entAddress.Text;

        Location location = await ApiService.GetLocationFromAddress(address);
        if (location != null)
        {
            longtitude = location.Longitude;
            latitude = location.Latitude;
            map.MoveToRegion(MapSpan.FromCenterAndRadius(location, Distance.FromMiles(0.5)));
            map.IsVisible = true;
        }
    }

    private async void btnAddNewStorage_Clicked(object sender, EventArgs e)
    {
        var storage = new PropertyDetail
        {
            Name = entAddress.Text,
            Description = entDescription.Text,
            Price = Convert.ToDouble(entPrice.Text),
            Address = entAddress.Text,
            File = UploadedFile,
            Phone = entPhone.Text,
            Longitude = longtitude,
            Latitude = latitude,
            CategoryId = ((Category)pickerCategory.SelectedItem).Id
        };

        var isStorageAdded = await ApiService.AddNewStorage(storage);

        if (isStorageAdded)
        {
            await DisplayAlert("", "Succesfully storage added", "Ok");
        }
        else
        {
            await DisplayAlert("", "Something went wrong", "Cancel");
        }
    }

    private async void btnImageUpload_Clicked(object sender, EventArgs e)
    {
        var fileResult = await FilePicker.Default.PickAsync();

        if(fileResult != null)
        {
            if (fileResult.FileName.EndsWith(".jpg") || fileResult.FileName.EndsWith(".png"))
            {
                using var stream = await fileResult.OpenReadAsync();
                UploadedFile = new CustomFormFile(stream, fileResult.FileName, fileResult.ContentType);
            }

            else
            {
                await DisplayAlert("","Invalid file format", "Cancel");
            }
        }
    }
}