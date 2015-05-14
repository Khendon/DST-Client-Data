using System;
using FileHelpers;


namespace SRO_Management.Models
{
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

        public string status;


        // IDataRecord interface implementation
        DateTime IDataRecord.TimeStamp
        {
            get { return TimeStamp; }
            set { TimeStamp = value; }
        }

        double? IDataRecord.Pressure
        {
            get { return Pressure; }
            set { Pressure = value; }
        }

        double? IDataRecord.Temperature
        {
            get { return Temperature; }
            set { Temperature = value; }
        }
    }
}
