using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.Connectivity;
using XamarinTask.Constants;

namespace XamarinTask
{
    public class CloudDataStore : IDataStore<Item>
    {
        #region Fields
        // Back fields for properties
        HttpClient client;
        Item items;
        #endregion

        public CloudDataStore()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(AppConstants.JsonUrl);
            items = new Item();
        }

        #region Methods
        /// <summary>
        /// Method will return the download values from the Xamarin Task view
        /// </summary>
        /// <returns></returns>
        public async Task<Item> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh && CrossConnectivity.Current.IsConnected)
            {
                var json = await client.GetStringAsync($"");
                items = await Task.Run(() => JsonConvert.DeserializeObject<Item>(json));
            }
            else
                UserDialogs.Instance.Alert(AppConstants.AlertData, AppConstants.Alert, AppConstants.Ok);
            return items;
        }

        /// <summary>
        /// Method will return the download values from the to sort the Xamarin Task view
        /// </summary>
        /// <returns></returns>
        public async Task<Item> GetItemsAsync(string sortOrder)
        {
            if (CrossConnectivity.Current.IsConnected && sortOrder == AppConstants.SortAscending)
            {
                var json = await client.GetStringAsync($"");
                var resultString = json;
                var jsonResult = JObject.Parse(resultString);
                jsonResult["rows"] = new JArray(jsonResult["rows"].OrderBy(obj => obj["title"]));
                var jsonSorted = JsonConvert.SerializeObject(jsonResult);
                items = await Task.Run(() => JsonConvert.DeserializeObject<Item>(jsonSorted));
            }
            else
                UserDialogs.Instance.Alert(AppConstants.AlertData, AppConstants.Alert, AppConstants.Ok);
            return items;
        }
        #endregion
    }
}
