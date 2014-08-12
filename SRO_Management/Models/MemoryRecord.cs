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
    /// Class to mirror Memory data CSV files for reading in all records.
    /// Implements IDataRecord interface which filters just the required information for client file ouput, with conversion from string for manipulation, sorting and unit conversion.
    /// </summary>
    public class MemoryRecord : IDataRecord
    {
        public string Number { get; set; }
        public string Ref { get; set; }
        public string TimeStamp { get; set; }
        public string Pressure { get; set; }
        public string Temperature { get; set; }


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
                    //convertInputFormat = DateTime.Parse(rawTimeStamp, "dd MMM yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None);
                    DateTime.TryParse(rawTimeStamp, out convertInputFormat);

                    return convertInputFormat;
                }
            }
            catch (FormatException formatExc)
            {
                System.Diagnostics.Trace.WriteLine(DateTime.Now + formatExc.ToString(), "Timestamp not a recognised DateTime format");
                throw;
            }


        }
    }
    
    /// <summary>
    /// Provides a map for the CsvHelper library to retrieve records from Csv file based on index and map to my SRORecord class.
    /// </summary>
    public sealed class MemRecordClassMap : CsvClassMap<MemoryRecord>
    {
        public MemRecordClassMap()
        {
            Map(m => m.Number).Index(0);
            Map(m => m.Ref).Index(1);
            Map(m => m.TimeStamp).Index(2);
            Map(m => m.Pressure).Index(3);
            Map(m => m.Temperature).Index(4);
        }
    }
}
