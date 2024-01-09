using Appmeds.Services;
using Firebase.Database;
using Firebase.Database.Query;
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
	public partial class EditMedPage : ContentPage
	{
        private Medication _medication;
        private FirebaseClient _firebase;

        public EditMedPage(Medication medication)
        {
			InitializeComponent ();
            _medication = medication;
            _firebase = new FirebaseClient("https://msap-14332-default-rtdb.europe-west1.firebasedatabase.app/");
            PopulateFields();

        }

        private void PopulateFields()
        {
            MedicationNameEntry.Text = _medication.MedicationName;
            DosageEntry.Text = _medication.Dosage.ToString();
            NumberOfPillsEntry.Text = _medication.NumberOfPills.ToString();
            TimePicker.Time = new TimeSpan(_medication.Time.Hours, _medication.Time.Minutes, 0);
        }

        private async void OnEditButtonClicked(object sender, EventArgs e)
        {
            // Update medication details
            _medication.MedicationName = MedicationNameEntry.Text;
            _medication.Dosage = int.Parse(DosageEntry.Text);
            _medication.NumberOfPills = int.Parse(NumberOfPillsEntry.Text);
            _medication.Time = TimePicker.Time;

            // Update in Firebase
            var userId = Application.Current.Properties["UserId"] as string;
            DateTime notifyTime = DateTime.Today.Add(_medication.Time); // Combine today's date with the time picked
            DependencyService.Get<INotificationManager>().ScheduleNotification(_medication.MedicationName, "Time to take your medication.", notifyTime);

            await _firebase
                .Child("Users")
                .Child(userId)
                .Child("Medications")
                .Child(_medication.Key)
                .PutAsync(_medication);

            // Navigate back to ShowMedsPage
            await Navigation.PopAsync();
        }
    }
}