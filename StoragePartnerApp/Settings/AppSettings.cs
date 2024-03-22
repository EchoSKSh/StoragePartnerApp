using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoragePartnerApp.Settings
{
    public static class AppSettings
    {
        public static string BaseApiUrl = "https://storagepartnerapi.azurewebsites.net/";

        public static string RegisterUserUrl = BaseApiUrl + "api/user/register";
        public static string LoginUrl =  BaseApiUrl + "api/user/login";
        public static string CategoriesUrl =  BaseApiUrl + "api/categories";
        public static string StorageListUrl =  BaseApiUrl + "api/properties/storagelist/";
        public static string StorageDetailUrl =  BaseApiUrl + "api/properties/storagedetail/";
        public static string TrendingStorageUrl =  BaseApiUrl + "api/properties/trendingstorages";
        public static string SearchStorageUrl =  BaseApiUrl + "api/properties/search/";
        public static string MyStorageUrl = BaseApiUrl + "api/properties/mystorage/";
        public static string AddNewStorage = BaseApiUrl + "api/properties/";
        public static string UploadImage = BaseApiUrl + "api/properties/uploadimage";
        public static string AddReservationUrl = BaseApiUrl + "api/reservations";
        public static string GetReservedStorages = BaseApiUrl + "api/reservations";
        public static string ReservedStorageDetailUrl = BaseApiUrl + "api/reservations/";

        //Implement
        public static string DeleteBookmarkUrl =  BaseApiUrl + "api/bookmarks/";

        //Google map
        public const string GoogleMapapiKey = "AIzaSyCApYlmf5stSZ-BoqLmLVexPvkRE8CAFxU";
        public static string GoogleMapAPI = $"https://maps.googleapis.com/maps/api/geocode/json?address=";
    }
}
