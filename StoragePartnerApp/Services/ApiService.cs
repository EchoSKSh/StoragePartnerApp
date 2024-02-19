﻿using Newtonsoft.Json;
using StoragePartnerApp.Models;
using StoragePartnerApp.Settings;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace StoragePartnerApp.Services
{
    public static class ApiService
    {
        public static async Task<bool> RegisterUser(string Name, string Email, string Password, string Phone)
        {
            var registerRequest = new Register
            {
                Name = Name,
                Email = Email,
                Password = Password,
                Phone = Phone
            };
            var httpClient = new HttpClient();
            var jsonRequest = JsonConvert.SerializeObject(registerRequest);    
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(AppSettings.RegisterUserUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            return true;
        }

        public static async Task<bool> Login(string email, string password)
        {
            var loginRequest = new Login 
            { 
                Email = email, 
                Password = password 
            };

            var jsonRequest = JsonConvert.SerializeObject(loginRequest);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            var httpClient = new HttpClient();
            var response = await httpClient.PostAsync(AppSettings.LoginUrl, content);

            if(!response.IsSuccessStatusCode) 
            {
                return false;
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<Token>(jsonResponse);

            Preferences.Set("accesstoken", results.AccessToken);
            Preferences.Set("userId", results.UserId);
            Preferences.Set("username", results.UserName);

            return true;
        }

        public static async Task<List<Category>> GetCategories()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.CategoriesUrl);
            var results = JsonConvert.DeserializeObject<List<Category>>(response);

            return results;
        }

        public static async Task<List<PropertyByCategory>> GetPropertyByCategory(int categoryId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.StorageListUrl + categoryId);
            var results = JsonConvert.DeserializeObject<List<PropertyByCategory>>(response);

            return results;
        }

        public static async Task<PropertyDetail> GetPropertyDetails(int propertyId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.StorageDetailUrl + propertyId);
            var results = JsonConvert.DeserializeObject<PropertyDetail>(response);

            return results;
        }

        public static async Task<List<TrendingProperty>> GetTrendingProperties()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.TrendingStorageUrl);
            var results = JsonConvert.DeserializeObject<List<TrendingProperty>>(response);

            return results; 
        }

        public static async Task<List<PropertyByCategory>> SearchProperty(string address)
        {
            var httpClient = new HttpClient(); ;
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
            var response = await httpClient.GetStringAsync (AppSettings.SearchStorageUrl + address);
            var results = JsonConvert.DeserializeObject<List<PropertyByCategory>>(response);

            return results;
        }

        public static async Task<List<BookmarkList>> GetBookmarks()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.BookmarksUrl);
            var results = JsonConvert.DeserializeObject<List<BookmarkList>>(response);

            return results;
        }

        public static async Task<bool> AddBookmark(int userId, int propertyId)
        {
            var request = new AddBookmark 
            {
                UserId = userId,
                PropertyId = propertyId
            };

            var jsonRequest = JsonConvert.SerializeObject(request);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
            var response = await httpClient.PostAsync(AppSettings.AddBookmarksUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            return true;

        }

        public static async Task<bool> DeleteBookmark(int bookmarkId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
            var response = await httpClient.DeleteAsync(AppSettings.DeleteBookmarkUrl + bookmarkId);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            return true;
        }
    }
}
