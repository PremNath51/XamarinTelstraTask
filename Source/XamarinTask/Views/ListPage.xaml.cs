using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace XamarinTask.Views
{
    public partial class ListPage : ContentPage
    {
        ItemsViewModel viewModel;

        //Initialize list page
        public ListPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //Do necessary data binding 
            BindingContext = viewModel = new ItemsViewModel();
            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}
