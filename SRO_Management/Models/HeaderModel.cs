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
        // Private members - header labels
        private string clientLabel = "Client";
        private string wellLabel = "Well";
        private string dstLabel = "DST No.";
        private string serialLabel = "Serial No.";
        private string depthLabel = "Depth";
        private string positionLabel = "Position";

        // Private members - header user input values
        private string clientInput;
        private string wellInput;
        private string dstInput;
        private string serialInput;
        private string depthInput;
        private string positionInput;


        //Public properties - header labels
        public string ClientLabel 
        { 
            get { return clientLabel; } 
            set { clientLabel = value;} 
        }

        public string WellLabel 
        {
            get { return wellLabel; }
            set { wellLabel = value; } 
        }

        public string DstLabel 
        {
            get { return dstLabel; }
            set { dstLabel = value; } 
        }

        public string SerialLabel 
        {
            get { return serialLabel; }
            set { serialLabel = value; } 
        }

        public string DepthLabel 
        {
            get { return depthLabel; }
            set { depthLabel = value; } 
        }
        public string PositionLabel 
        {
            get { return positionLabel; }
            set { positionLabel = value; } 
        }

        // Public accessors - header user input values
        public string ClientInput
        {
            get { return clientInput; }
            set { clientInput = value; }
        }

        public string WellInput
        {
            get { return wellInput; }
            set { wellInput = value; }
        }

        public string DstInput
        {
            get { return dstInput; }
            set { dstInput = value; }
        }

        public string SerialInput
        {
            get { return serialInput; }
            set { serialInput = value; }
        }

        public string DepthInput
        {
            get { return depthInput; }
            set { depthInput = value; }
        }

        public string PositionInput
        {
            get { return positionInput; }
            set { positionInput = value; }
        }


        //Column header labels
        private string dateTimeLabel = "Date Time";
        public string DateTimeLabel
        {
            get { return dateTimeLabel; }
            set { dateTimeLabel = value; }
        }

        private string pressureLabel = "Pressure";
        public string PressureLabel
        {
            get { return pressureLabel; }
            set { pressureLabel = value; }
        }

        private string tempLabel = "Temperature";
        public string TempLabel
        {
            get { return tempLabel; }
            set { tempLabel = value; }
        }


        // Column unit header data
        private string dateStringFormat = "DD/MM/YYYY HH:MM:SS";

        public string DateStringFormat
        {
            get { return dateStringFormat; }
            set { dateStringFormat = value; }
        }

        private string presStringFormat;

        public string PresStringFormat
        {
            get { return presStringFormat; }
            set { presStringFormat = value; }
        }

        private string tempStringFormat;

        public string TempStringFormat
        {
            get { return tempStringFormat; }
            set { tempStringFormat = value; }
        }
        
        
        

    }
}
