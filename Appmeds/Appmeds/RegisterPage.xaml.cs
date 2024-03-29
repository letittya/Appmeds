﻿using Appmeds.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Appmeds
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
            BindingContext = new SignupViewModel(Navigation);

         

            ShowPasswordCheckBox.CheckedChanged += OnShowPasswordCheckBoxChanged;
        }

        private void OnShowPasswordCheckBoxChanged(object sender, CheckedChangedEventArgs e)
        {
            txtSignUpPassword.IsPassword = !e.Value;
            txtConfirmPassword.IsPassword = !e.Value;
        }



    }


}