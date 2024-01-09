﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Appmeds
{
    public class Medication
    {
        public string MedicationName { get; set; }
        public double Dosage { get; set; }
        public double NumberOfPills { get; set; }
        public TimeSpan Time { get; set; }
    }
}
