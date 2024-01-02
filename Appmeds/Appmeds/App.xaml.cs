using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Appmeds
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage())
            {
                BarBackgroundColor = Color.DarkOliveGreen,
            };

            // Hide the navigation bar for the MainPage
            NavigationPage.SetHasNavigationBar(MainPage, false);

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
