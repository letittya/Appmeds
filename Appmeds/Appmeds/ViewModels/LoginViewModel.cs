using Appmeds.Connections;
using Appmeds.Models;
using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Appmeds.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region attributes
        private string email;
        private string password;
        #endregion

        #region Properties
        public string txtemail
        {
            get { return email; }
            set { SetValue(ref email, value); }
        }
        public string txtpassword
        {
            get { return password; }
            set { SetValue(ref password, value); }
        }

        #endregion

        #region Command
        public Command LoginCommand { get; }
        #endregion

        #region Method
        public async Task LoginUser()
        {
            var objuser = new UserModel()
            {
                EmailField = email,
                PasswordField = password,
            };
            try
            {

                var authentication = new FirebaseAuthProvider(new FirebaseConfig(DBConn.WebApyAuthentication));
                var authuser = await authentication.SignInWithEmailAndPasswordAsync(objuser.EmailField.ToString(), objuser.PasswordField.ToString());
                string obternertoken = authuser.FirebaseToken;

                var Properties_NavigationPage = new NavigationPage(new ShowMedsPage());

                Properties_NavigationPage.BarBackgroundColor = Color.DarkOliveGreen;
                App.Current.MainPage = Properties_NavigationPage;


            }
            catch (Exception)
            {

                await App.Current.MainPage.DisplayAlert("warning", "wrong credentials", "accept");
            }
        }
        #endregion

        #region Constructor
        public LoginViewModel(INavigation navegar)
        {
            Navigation = navegar;
            LoginCommand = new Command(async () => await LoginUser());

        }
        #endregion
    }
}
