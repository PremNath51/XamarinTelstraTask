﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using XamarinTask.Views;

namespace XamarinTask
{
    public class MockDataStore : IDataStore<Item>
    {
        #region Fields
        /// <summary>
        ///  Backfield for Xamarin Task list
        /// </summary>
        Item items;
        #endregion

        public MockDataStore()
        {
            items = new Item();
            var mockItems = new List<Item>
            {
                //Do optionals here
            };
        }

        #region Methods

        /// <summary>
        /// Method will return the value and  Data store for Xamarin Task
        /// </summary>
        /// <returns></returns>
        public async Task<Item> GetItemsAsync(bool forceRefresh = false)
        {
            //Load json from the local file
            var assembly = typeof(ListPage).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("XamarinProficiencyExercise.Constants.Proficiency.json");

            //Serialize the json string
            var jsonSorted = JsonConvert.SerializeObject(stream);

            //Parse the json and return
            items = await Task.Run(() => JsonConvert.DeserializeObject<Item>(jsonSorted));
            return await Task.FromResult(items);
        }

        /// <summary>
        /// Method will return the value and reterive Data store for Xamarin Task
        /// </summary>
        /// <returns></returns>
        public async Task<Item> GetItemsAsync(string sortOrder)
        {
            //Load json from the local file
            var assembly = typeof(ListPage).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("XamarinProficiencyExercise.Constants.Proficiency.json");

            //Serialize the json string
            var jsonSorted = JsonConvert.SerializeObject(stream);

            //Parse the json and return
            items = await Task.Run(() => JsonConvert.DeserializeObject<Item>(jsonSorted));
            return await Task.FromResult(items);
        }
        #endregion

    }
}
