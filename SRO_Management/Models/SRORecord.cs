using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;

namespace SRO_Management.Models
{
    /// <summary>
    /// Class representing SRO data file structure for use with SRO Reader.
    /// </summary>
    [DelimitedRecord(",")]
    [IgnoreFirst(4)]
    public class SRORecord : IDataRecord
    {
        public string Count;

        public string Source;

        [FieldConverter(typeof(DateFieldConverter))]
        public DateTime TimeStamp;

        [FieldConverter(ConverterKind.Double, ".")]
        public double? Pressure;

        public string PPrecision;

        [FieldConverter(ConverterKind.Double, ".")]
        public double? Temperature;

        public string TPrecision;

        public string LogIndex;

        public string Status;

        // IDataRecord interface implementation
        DateTime IDataRecord.TimeStamp
        {
            get { return this.TimeStamp; }
            set { this.TimeStamp = value; }
        }

        double? IDataRecord.Pressure
        {
            get { return this.Pressure; }
            set { this.Pressure = value; }
        }

        double? IDataRecord.Temperature
        {
            get { return this.Temperature; }
            set { this.Temperature = value; }
        }
    }
}
