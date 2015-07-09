using System;
using FileHelpers;

namespace SRO_Management.Models
{
    /// <summary>
    /// Class representing Memory file structure for use with MemReader.
    /// </summary>
    [DelimitedRecord(",")]
    [IgnoreFirst(3)]
    public class MemRecord : IDataRecord
    {
        public string Count;

        public string PnTRef;

        [FieldConverter(typeof(DateFieldConverter))]
        public DateTime TimeStamp;

        [FieldConverter(ConverterKind.Double, ".")]
        public double? Pressure;

        [FieldConverter(ConverterKind.Double, ".")]
        public double? Temperature;

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