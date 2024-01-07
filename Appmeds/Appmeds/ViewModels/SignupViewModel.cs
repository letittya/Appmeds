using Appmeds.Connections;
using Appmeds.Models;
using Firebase.Auth;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Appmeds.ViewModels
{
    public class SignupViewModel : BaseViewModel
    {
        private string email;
        private string password;
        private string confirmPassword;

        public string TxtSignUpEmail
        {
            get { return email; }
            set { SetValue(ref email, value); }
        }

        public string TxtSignUpPassword
        {
            get { return password; }
            set { SetValue(ref password, value); }
        }

        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set { SetValue(ref confirmPassword, value); }
        }

        public Command SignUpCommand { get; }

        public SignupViewModel(INavigation navigation)
        {
            Navigation = navigation;
            SignUpCommand = new Command(async () => await SignUpUser());
        }

        private async Task SignUpUser()
        {
            if (password != confirmPassword)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Passwords do not match", "OK");
                return;
            }

            try
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(DBConn.WebApyAuthentication));
                var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(email, password);
                // Optionally, you can store additional user data in the database here

                await App.Current.MainPage.DisplayAlert("Success", "User registered successfully", "OK");
                App.Current.MainPage = new NavigationPage(new MainPage());
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
