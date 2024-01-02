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
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            // You can add validation logic here if needed

            // Navigate to ShowMedsPage
            await Navigation.PushAsync(new ShowMedsPage());
        }

        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {

        }

        private void ForgotPasswordButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}
