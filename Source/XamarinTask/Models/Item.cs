using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace XamarinTask
{
    /// <summary>
    /// Model class for the Xamarin Task
    /// </summary>
    /// 
    #region Properties
    public class Row
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageHref { get; set; }
    }

    public class Item
    {
        //Parse necessary data from response here
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageHref { get; set; }
        public ObservableCollection<Row> Rows { get; set; }

    }
    #endregion
}
