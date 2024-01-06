using Appmeds.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Appmeds
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false); // Hide navigation bar for MainPage
            BindingContext = new LoginViewModel(Navigation);
        }

        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {

        }

        private async void SignUpLabel_Tapped(object sender, EventArgs e)
        {
            // Navigate to SignupPage.xaml
            await Navigation.PushAsync(new SignupPage());
        }

        private void ForgotPasswordButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}
