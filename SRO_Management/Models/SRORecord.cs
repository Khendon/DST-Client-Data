using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;


namespace SRO_Management.Models
{   
    /// <summary>
    /// Class to mirror SRO/Historical data CSV files for reading in all records.
    /// Implements IDataRecord interface which filters just the required information for client file ouput, with conversion from string for manipulation, sorting and unit conversion.
    /// </summary>
    public class SRORecord : IDataRecord
    {   
        // Public properties to match columns/records in Csv files.
        public string Count { get; set; }
        public string Source { get; set; }
        public string TimeStamp { get; set; }
        public string Pressure { get; set; }
        public string pPrecision { get; set; }
        public string Temperature { get; set; }
        public string tPrecision { get; set; }
        public string logIndex { get; set; }
        public string infoColumn { get; set; }

        // Explicit implementation of IDataRecord interface.
        DateTime IDataRecord.TimeStamp 
        { 
            get { return FormatTimeStamp(TimeStamp); }
            set { TimeStamp = value.ToString(); }
        }

        double? IDataRecord.Pressure 
        { 
            get { return Pressure.ToNullable<double>(); }
            set { Pressure = value.ToString(); }
        }

        double? IDataRecord.Temperature 
        { 
            get { return Temperature.ToNullable<double>(); }
            set { Temperature = value.ToString(); }
        }


        private DateTime FormatTimeStamp(string rawTimeStamp)
        {
            DateTime convertInputFormat;

            try
            {
                if (rawTimeStamp.Contains("End"))
                {
                    return DateTime.MinValue;
                }
                else
                {
                    //convertInputFormat = DateTime.ParseExact(rawTimeStamp.Trim('\"'), "dd MMM yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None);
                    DateTime.TryParse(rawTimeStamp.Trim('\"'), out convertInputFormat);

                    return convertInputFormat;
                }
            }
            catch (FormatException formatEx)
            {
                System.Diagnostics.Trace.WriteLine(DateTime.Now + formatEx.ToString(), "Timestamp not a recognised DateTime format");
                throw;              
            }


        }
    }


    /// <summary>
    /// Provides a map for the CsvHelper library to retrieve records from Csv file based on index and map to my SRORecord class.
    /// </summary>
    public sealed class SRORecordClassMap : CsvClassMap<SRORecord>
    {
        public SRORecordClassMap()
        {
            Map(m => m.Count).Index(0);
            Map(m => m.Source).Index(1);
            Map(m => m.TimeStamp).Index(2);
            Map(m => m.Pressure).Index(3);
            Map(m => m.pPrecision).Index(4);
            Map(m => m.Temperature).Index(5);
            Map(m => m.tPrecision).Index(6);
            Map(m => m.logIndex).Index(7);
            Map(m => m.infoColumn).Index(8);
        }
    }
}

