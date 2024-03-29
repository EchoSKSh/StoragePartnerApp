using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using StoragePartnerApp.Models;
using StoragePartnerApp.Services;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;

namespace StoragePartnerApp.Pages;

public partial class AddNewStorage : ContentPage
{
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

            var pin = new Pin
            {
                Label = address, // Optional label for the pin
                Location = location
            };

            // Add the pin to the map's pins collection
            map.Pins.Add(pin);

            map.MoveToRegion(MapSpan.FromCenterAndRadius(location, Distance.FromMiles(0.5)));
            map.IsVisible = true;
        }
    }

    private async void btnAddNewStorage_Clicked(object sender, EventArgs e)
    {

        if (string.IsNullOrEmpty(entName.Text))
        {
            await DisplayAlert("", "Please enter the name to proceed further", "OK");
        }
        else if (string.IsNullOrEmpty(entDescription.Text))
        {
            await DisplayAlert("", "Please enter the description to proceed further", "OK");
        }
        else if (string.IsNullOrEmpty(entAddress.Text))
        {
            await DisplayAlert("", "Please enter the address to proceed further", "OK");
        }
        else if (string.IsNullOrEmpty(entPrice.Text))
        {
            await DisplayAlert("", "Please enter the price to proceed further", "OK");
        }
        else if (string.IsNullOrEmpty(entPhone.Text))
        {
            await DisplayAlert("", "Please enter the password to proceed further", "OK");
        }
        else if (!Regex.IsMatch(entPhone.Text, @"^\d{10}$"))
        {
            await DisplayAlert("", "Please enter a valid 10 digit phone number to continue with the registration", "OK");
        }
        else 
        {
            var storage = new PropertyDetail
            {
                Name = entName.Text,
                Description = entDescription.Text,
                Price = Convert.ToDouble(entPrice.Text),
                Address = entAddress.Text,
                Phone = entPhone.Text,
                Longitude = longtitude,
                Latitude = latitude,
                CategoryId = ((Category)pickerCategory.SelectedItem).Id
            };

            var isStorageAdded = await ApiService.AddNewStorage(storage);

            if (isStorageAdded)
            {
                await DisplayAlert("", "Succesfully storage added. Please upload primary image", "Ok");
                await Task.Delay(1000);
                await UploadImage();
            }
            else
            {
                await DisplayAlert("", "Something went wrong", "Cancel");
            }
        }

    }

    private async Task UploadImage()
    {
        var fileResult = await FilePicker.Default.PickAsync();

        if (fileResult != null)
        {
            if (fileResult.FileName.EndsWith(".jpg") || fileResult.FileName.EndsWith(".png"))
            {
                var imageData = new ImageData();

                using (var stream = await fileResult.OpenReadAsync())
                {
                    imageData.ImageArray = new byte[stream.Length];
                    await stream.ReadAsync(imageData.ImageArray, 0, (int)stream.Length);
                }
                imageData.FileName = fileResult.FileName;

                await Navigation.PopAsync();
                
            }
            else
            {
                await DisplayAlert("", "Invalid file format", "Cancel");
            }
        }
    }
}