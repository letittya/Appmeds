using System;
using Xamarin.Forms;

namespace Appmeds
{
    public partial class AddMedPage : ContentPage
    {
        public AddMedPage()
        {
            InitializeComponent();
        }

        private async void OnAddButtonClicked(object sender, EventArgs e)
        {
            // Retrieve values from the form
            string medicationName = MedicationNameEntry.Text;
            int dosage = Convert.ToInt32(DosageEntry.Text);
            int numberOfPills = Convert.ToInt32(NumberOfPillsEntry.Text);
            TimeSpan time = TimePicker.Time;

            // Create a new Medication object or any suitable data structure to hold the data
            Medication newMedication = new Medication
            {
                MedicationName = medicationName,
                Dosage = dosage,
                NumberOfPills = numberOfPills,
                Time = time
            };

            // Pass the data to the ShowMedsPage
            await Navigation.PushAsync(new ShowMedsPage(newMedication));
        }
    }
}
