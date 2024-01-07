using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Appmeds
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowMedsPage : ContentPage
    {
        private readonly FirebaseClient firebase = new FirebaseClient("https://msap-14332-default-rtdb.europe-west1.firebasedatabase.app/"); // Replace with your Firebase URL

        public ShowMedsPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadMedications(); // Refresh medication list every time the page appears
        }

        private async Task LoadMedications()
        {
            medicationsLayout.Children.Clear(); // Clear existing medication entries

            var userId = Application.Current.Properties["UserId"] as string;
            var medications = await firebase
                                    .Child("Users")
                                    .Child(userId)
                                    .Child("Medications")
                                    .OnceAsync<Medication>();

            foreach (var medication in medications)
            {
                DisplayMedicationDetails(medication.Object);
            }
        }

        private void DisplayMedicationDetails(Medication medication)
        {
            Label medicationLabel = new Label
            {
                Text = $"Medication Name: {medication.MedicationName}\n" +
                       $"Dosage: {medication.Dosage} mg\n" +
                       $"Number of Pills: {medication.NumberOfPills}\n" +
                       $"Time: {medication.Time.ToString("hh\\:mm")}",
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Margin = new Thickness(10)
            };

            // Assuming you have a StackLayout named 'medicationsLayout' for displaying medication details
            medicationsLayout.Children.Add(medicationLabel);
        }

        private async void OnAddMedicationClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddMedPage());
        }
    }
}
