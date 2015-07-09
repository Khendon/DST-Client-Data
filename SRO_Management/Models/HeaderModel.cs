using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRO_Management.Models
{
    /// <summary>
    /// Class to hold Client File Header Data, as input by user on view.
    /// </summary>
    public class HeaderModel
    {
        public HeaderModel()
        {
            this.ClientLabel = "Client";

            this.WellLabel = "Well";

            this.DstLabel = "DST No.";

            this.SerialLabel = "Serial No.";

            this.DepthLabel = "Depth";

            this.PositionLabel = "Position";

            this.DateTimeLabel = "Date Time";

            this.PressureLabel = "Pressure";

            this.TempLabel = "Temperature";

            this.DateStringFormat = "DD/MM/YYYY HH:MM:SS";
        }

        // Public properties - header labels
        public string ClientLabel { get; set; }

        public string WellLabel { get; set; }

        public string DstLabel { get; set; }

        public string SerialLabel { get; set; }

        public string DepthLabel { get; set; }

        public string PositionLabel { get; set; }
        
        // Public properties - header user input values
        public string ClientInput { get; set; }

        public string WellInput { get; set; }

        public string DstInput { get; set; }

        public string SerialInput { get; set; }

        public string DepthInput { get; set; }

        public string PositionInput { get; set; }
        
        // Column header labels
        public string DateTimeLabel { get; set; }

        public string PressureLabel { get; set; }

        public string TempLabel { get; set; }

        public string DateStringFormat { get; set; }

        public string PresStringFormat { get; set; }

        public string TempStringFormat { get; set; }        
    }    
}