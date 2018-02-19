using System;

using Xamarin.Forms;
using XamarinTask.Views;

namespace XamarinTask
{
    public partial class App : Application
    {
        /// <summary>
        /// Event invokes and Registers the Main page with Navigation
        /// </summary>

        public static bool UseMockDataStore = false;

        public App()
        {
            InitializeComponent();

            if (UseMockDataStore)
                DependencyService.Register<MockDataStore>();
            else
                DependencyService.Register<CloudDataStore>();

            //Invoke the Navigation page for both iOS and Android
                MainPage = new NavigationPage(new ListPage());
        }
    }
}
