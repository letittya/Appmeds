﻿using Firebase.Database;
using Firebase.Database.Query;
using System;
using Xamarin.Forms;

namespace Appmeds
{
    public partial class AddMedPage : ContentPage
    {
        private readonly FirebaseClient firebase = new FirebaseClient("https://msap-14332-default-rtdb.europe-west1.firebasedatabase.app/");

        public AddMedPage()
        {
            InitializeComponent();

          
        }

        

        private async void OnAddButtonClicked(object sender, EventArgs e)
        {
            // Retrieve values from the form
            string medicationName = MedicationNameEntry.Text;
            double dosage = Convert.ToDouble(DosageEntry.Text);
            double numberOfPills = Convert.ToDouble(NumberOfPillsEntry.Text);
            TimeSpan time = TimePicker.Time;

            // Create a new Medication object
            Medication newMedication = new Medication
            {
                MedicationName = medicationName,
                Dosage = dosage,
                NumberOfPills = numberOfPills,
                Time = time
            };
            var userId = Application.Current.Properties["UserId"] as string;

            // Save to Firebase
            await firebase
                .Child("Users")
                .Child(userId)
                .Child("Medications")
                .PostAsync(newMedication);

            // navigate back or show a confirmation message
            await DisplayAlert("Success", "Medication added successfully", "OK");
            await Navigation.PopAsync(); // Goes back to the previous page
        }
    }
}
