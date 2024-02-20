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

        //Implement
        public static string BookmarksUrl =  BaseApiUrl + "api/bookmarks";
        public static string AddBookmarksUrl =  BaseApiUrl + "api/bookmarks";
        public static string DeleteBookmarkUrl =  BaseApiUrl + "api/bookmarks/";
    }
}
