using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using XamarinTask.Constants;
using System.Linq;

namespace XamarinTask
{
    /// <summary>
    /// Class to generate the model values for XamarinTask views
    /// </summary>
    public class ItemsViewModel : BaseViewModel
    {
        #region Fields
        /// <summary>
        ///  Backfield for XamarinTask list
        /// </summary>
        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Command SortItemsCommand { get; set; }
        static int count = 0;
        #endregion
        public ItemsViewModel()
        {
            //Observable collectionf or the list view
            Items = new ObservableCollection<Item>();

            //Command for Load/refresh list view
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            //Command to sort list view
            SortItemsCommand = new Command(async () => await ExecuteSortItemsCommand());                
        }

        #region Methods
        /// <summary>
        /// Method will return the refersh list value for Xamrarin Task view
        /// </summary>
        /// <returns></returns>
        async Task ExecuteLoadItemsCommand()
        {          
            if (IsBusy)
                return;
            
            IsBusy = true;

            try
            {
                //Clear the observable collections before adding the fresh list
                Items.Clear();

                //Get data from server
                var items = await DataStore.GetItemsAsync(true);

                //Validate and Take all the valid datas from the response
                foreach (var row in items.Rows)
                {
                    var IndividualItem = new Item();
                    if (row.Title != null)
                    {
                        IndividualItem.Description = (row.Description == null) ? AppConstants.NoDescription : row.Description;
                        IndividualItem.Title = row.Title.ToUpper();
                        IndividualItem.ImageHref = (row.ImageHref == null)? AppConstants.NoImageURL : row.ImageHref;

                        //Add to the observable collections
                        Items.Add(IndividualItem);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// Method will return the Sort list value for Xamrarin Task view
        /// </summary>
        /// <returns></returns>
        async Task ExecuteSortItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                //Clear the observable collections before adding the fresh list
                Items.Clear();

                //Get data from server and create the sorted order
                var items = await DataStore.GetItemsAsync(SortOrder: AppConstants.SortAscending );
                count++;
                if (count % 2 == 0)
                    items.Rows = new ObservableCollection<Row>(items.Rows.OrderBy(x => x.Title));
                else
                    items.Rows = new ObservableCollection<Row>(items.Rows.OrderByDescending(x => x.Title));

                //Validate and Take all the valid datas from the response
                foreach (var row in items.Rows)
                {
                    var IndividualItem = new Item();
                    if (row.Title != null)
                    {
                        IndividualItem.Description = (row.Description == null) ? AppConstants.NoDescription : row.Description;
                        IndividualItem.Title = row.Title.ToUpper();
                        IndividualItem.ImageHref = (row.ImageHref == null) ? AppConstants.NoImageURL : row.ImageHref;

                        //Add sorted items to the observable collections
                        Items.Add(IndividualItem);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
        #endregion
    }
}
    