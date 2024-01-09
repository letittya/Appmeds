using Appmeds.Services;
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
                ScheduleMedicationNotification(medication.Object);
            }
        }
        private void ScheduleMedicationNotification(Medication medication)
        {
            var notificationManager = DependencyService.Get<INotificationManager>();

            // Calculate the next occurrence of medication time
            DateTime now = DateTime.Now;
            DateTime medicationDateTime = new DateTime(now.Year, now.Month, now.Day, medication.Time.Hours, medication.Time.Minutes, medication.Time.Seconds);
            if (medicationDateTime < now)
            {
                // If the time has already passed today, schedule for the next day
                medicationDateTime = medicationDateTime.AddDays(1);
            }

            string title = "Medication Reminder";
            string message = $"Time to take your {medication.MedicationName}, {medication.Dosage} mg.";

            notificationManager.ScheduleNotification(title, message, medicationDateTime);
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
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Margin = new Thickness(10, 0, 0, 0)
            };

            // Create a CheckBox with adjusted margin and scale
            CheckBox checkbox = new CheckBox
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.End,
                Scale = 1.2, // Slightly increased size
                Margin = new Thickness(0, 0, 20, 0) // Adjusted margin to move it a bit to the left
            };

            // Create a horizontal layout to hold the label and the checkbox
            StackLayout horizontalLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = { medicationLabel, checkbox }
            };

            // Create a frame
            Frame frame = new Frame
            {
                Content = horizontalLayout,
                CornerRadius = 20,
                Margin = new Thickness(10),
                Padding = new Thickness(10),
                BackgroundColor = Color.FromHex("#FFC0CB"),
                BorderColor = Color.Gray
            };

            // Attach the CheckedChanged event handler to the checkbox
            checkbox.CheckedChanged += (sender, args) => {
                frame.BackgroundColor = args.Value ? Color.FromHex("#BFD8B8") : Color.FromHex("#FFC0CB");
            };

            medicationsLayout.Children.Add(frame);
        }



        private async void OnAddMedicationClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddMedPage());
        }
    }
}
