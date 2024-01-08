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
            var formattedString = new FormattedString();

            // First line with larger font and bold
            formattedString.Spans.Add(new Span
            {
                Text = $"{medication.MedicationName} {medication.Dosage} mg\n",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)) * 1.5, // Double the default medium size
                FontAttributes = FontAttributes.Bold
            });

            // Remaining lines with normal font
            formattedString.Spans.Add(new Span
            {
                Text = $"{medication.Time.ToString("hh\\:mm")}\n" +
                       $"{medication.NumberOfPills} pills left",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
            });

            Label medicationLabel = new Label
            {
                FormattedText = formattedString,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.Start, // Align text to the left
                Margin = new Thickness(10, 0, 0, 0) // Left margin for text
            };

            Frame frame = new Frame
            {
                Content = medicationLabel,
                CornerRadius = 20,
                Margin = new Thickness(10),
                Padding = new Thickness(10),
                BackgroundColor = Color.FromHex("#FFC0CB"), // Pale red background
                BorderColor = Color.Gray
            };

            medicationsLayout.Children.Add(frame);
        }



        private async void OnAddMedicationClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddMedPage());
        }
    }
}
