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
        private readonly FirebaseClient firebase = new FirebaseClient("https://msap-14332-default-rtdb.europe-west1.firebasedatabase.app/");

        public ShowMedsPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadMedications();
        }

        private async Task LoadMedications()
        {
            medicationsLayout.Children.Clear();

            var userId = Application.Current.Properties["UserId"] as string;
            var medications = await firebase
                                        .Child("Users")
                                        .Child(userId)
                                        .Child("Medications")
                                        .OnceAsync<Medication>();

            foreach (var medication in medications)
            {
                var medicationWithKey = medication.Object;
                medicationWithKey.Key = medication.Key; // Set the Firebase key
                DisplayMedicationDetails(medicationWithKey);
            }
        }

        private void DisplayMedicationDetails(Medication medication)
        {
            var formattedString = new FormattedString();

            formattedString.Spans.Add(new Span
            {
                Text = $"{medication.MedicationName} {medication.Dosage} mg\n",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)) * 1.5,
                FontAttributes = FontAttributes.Bold
            });

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

          

            CheckBox checkbox = new CheckBox
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Start,
                Scale = 1.2,
                Margin = new Thickness(0, 0, 10, 0) // Adjust as needed
            };

            Image editIcon = CreateIcon("edit_green.png", () => EditMedication(medication));
            Image deleteIcon = CreateIcon("delete_green.png", () => DeleteMedication(medication));

            StackLayout iconLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Children = { editIcon, deleteIcon },
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center
            };

            // Create the horizontal layout
            StackLayout horizontalLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = { checkbox, medicationLabel, iconLayout },
                Spacing = 10 // Adjust spacing as needed
            };

            Frame frame = new Frame
            {
                Content = horizontalLayout,
                CornerRadius = 20,
                Margin = new Thickness(10),
                Padding = new Thickness(10),
                BackgroundColor = Color.FromHex("#FFC0CB"),
                BorderColor = Color.Gray
            };

            checkbox.CheckedChanged += (sender, args) => {
                frame.BackgroundColor = args.Value ? Color.FromHex("#BFD8B8") : Color.FromHex("#FFC0CB");
            };

            medicationsLayout.Children.Add(frame);
        }

        private Image CreateIcon(string imageName, Action tapAction)
        {
            Image icon = new Image
            {
                Source = imageName,
                HeightRequest = 30,
                WidthRequest = 30,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.End
            };
            var tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += (s, e) => tapAction();
            icon.GestureRecognizers.Add(tapGesture);
            return icon;
        }

        private void EditMedication(Medication medication)
        {
            // Logic for editing medication
        }

        private async void DeleteMedication(Medication medication)
        {
            var isUserSure = await DisplayAlert("Confirm Delete", $"Are you sure you want to delete {medication.MedicationName}?", "Yes", "No");
            if (isUserSure)
            {
                var userId = Application.Current.Properties["UserId"] as string;
                await firebase
                    .Child("Users")
                    .Child(userId)
                    .Child("Medications")
                    .Child(medication.Key) // Use the key to reference the medication
                    .DeleteAsync();

                await LoadMedications(); // Refresh the list to update the UI
            }
        }

        private async void OnAddMedicationClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddMedPage());
        }
    }
}
