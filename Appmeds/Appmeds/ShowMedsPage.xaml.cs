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
    public partial class ShowMedsPage : ContentPage
    {
        public ShowMedsPage()
        {
            InitializeComponent();

            // Handle the case where no medication details are provided
            DisplayAlert("Hi!", "No medication details provided yet.", "OK");
        }

        public ShowMedsPage(Medication newMedication) : this()
        {
            // Process the newMedication details here
            if (newMedication != null)
            {
                DisplayMedicationDetails(newMedication);
            }
        }

        private async void OnAddMedicationClicked(object sender, EventArgs e)
        {
            // You can add validation logic here if needed

            // Navigate to ShowMedsPage
            await Navigation.PushAsync(new AddMedPage());
        }

        private void DisplayMedicationDetails(Medication medication)
        {
            // Add your logic to display the medication details on the page
            // For example, you might use labels to display the data
            Label medicationLabel = new Label
            {
                Text = $"Medication Name: {medication.MedicationName}\n" +
                       $"Dosage: {medication.Dosage} mg\n" +
                       $"Number of Pills: {medication.NumberOfPills}\n" +
                       $"Time: {medication.Time.ToString("hh\\:mm")}",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };

            // Add the label to your layout (e.g., StackLayout)
            // stackLayout.Children.Add(medicationLabel);
        }
    }


}


