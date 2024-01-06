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
        #region Attributes
        private string email;
        private string password;
        private string confirmPassword;
        #endregion

        #region Properties
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
        #endregion

        #region Command
        public Command SignUpCommand { get; }
        #endregion

        #region Method
        public async Task SignUpUser()
        {
            if (password != confirmPassword)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Passwords do not match", "OK");
                return;
            }

            var newUser = new UserModel()
            {
                EmailField = email,
                PasswordField = password,
            };

            try
            {
                var authentication = new FirebaseAuthProvider(new FirebaseConfig(DBConn.WebApyAuthentication));
                var authResult = await authentication.CreateUserWithEmailAndPasswordAsync(newUser.EmailField, newUser.PasswordField);

                // Additional logic after successful signup if needed

                await App.Current.MainPage.DisplayAlert("Success", "Account created successfully", "OK");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
        #endregion

        #region Constructor
        public SignupViewModel()
        {
            SignUpCommand = new Command(async () => await SignUpUser());
        }
        #endregion
    }
}
