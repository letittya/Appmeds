using System;
using System.Collections.Generic;
using System.Text;

namespace Appmeds
{
    public class Medication
    {
        public string MedicationName { get; set; }
        public int Dosage { get; set; }
        public int NumberOfPills { get; set; }
        public TimeSpan Time { get; set; }
    }
}
