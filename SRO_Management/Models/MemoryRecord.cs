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


        DateTime? IDataRecord.TimeStamp
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


        /// <summary>
        /// Converts input string to Nullable<DateTime> format, or to null if invalid. Only used via IDataRecord interface.
        /// </summary>
        /// <param name="rawTimeStamp"></param>
        /// <returns>Nullable<DateTime></returns>
        private DateTime? FormatTimeStamp(string rawTimeStamp)
        {
            DateTime? convertInputFormat;

            try
            {
                convertInputFormat = DateTime.ParseExact(rawTimeStamp, "dd MMM yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None);

                return convertInputFormat;

            }
            catch (FormatException formatExc)
            {
                System.Diagnostics.Trace.Assert(false, formatExc.Message);
                return convertInputFormat = null;
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
