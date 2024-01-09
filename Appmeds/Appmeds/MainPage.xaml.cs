using Appmeds.ViewModels;
using Firebase.Auth;
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
            await Navigation.PushAsync(new RegisterPage());
        }

        private async void ForgotPasswordButton_Clicked(object sender, EventArgs e)
        {
            string email = txtemail.Text;

            if (string.IsNullOrWhiteSpace(email))
            {
                await DisplayAlert("Error", "Please enter your email address.", "OK");
                return;
            }

            try
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyCCn4DOB_aqTThGEaXldUfQ64-sr2NgXSs"));
                await authProvider.SendPasswordResetEmailAsync(email);
                await DisplayAlert("Password Reset", "A password reset email has been sent.", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Failed to send password reset email: " + ex.Message, "OK");
            }
        }

    }
}
