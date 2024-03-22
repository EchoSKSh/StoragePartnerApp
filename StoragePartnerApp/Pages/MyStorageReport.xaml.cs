using StoragePartnerApp.Models;
using StoragePartnerApp.Services;
using System.Collections.ObjectModel;
using System.Net.Http.Json;

namespace StoragePartnerApp.Pages;

public partial class MyStorageReport : ContentPage
{
    private readonly HttpClient httpClient = new();

    public bool IsRefreshing { get; set; }
    public ObservableCollection<Models.MyStorage> Storages { get; set; } = new();
    public Command RefreshCommand { get; set; }
    public MyStorageReport()
	{
        RefreshCommand = new Command(async () =>
        {
            // Simulate delay
            await Task.Delay(2000);

            await LoadStorage();

            IsRefreshing = false;
            OnPropertyChanged(nameof(IsRefreshing));
        });

        BindingContext = this;

        InitializeComponent();
	}

    private async void BackButtonImage_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    protected async override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        await LoadStorage();
    }

    private async Task LoadStorage()
    {
        var storages = await ApiService.GetMyStorage();

        Storages.Clear();

        foreach (var storage in storages)
        {
            Storages.Add(storage);
        }
    }
}