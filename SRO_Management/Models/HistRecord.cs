﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;

namespace SRO_Management.Models
{
    [DelimitedRecord(",")]
    [IgnoreFirst(4)]
    public class HistRecord
    {
        public string Count;

        public string Source;

        [FieldConverter(typeof(DateFieldConverter))]
        public DateTime TimeStamp;

        [FieldConverter(ConverterKind.Double, ".")]
        [FieldNullValue(typeof(double), "-1")]
        public double Pressure;

        public string pPrecision;

        [FieldConverter(ConverterKind.Double, ".")]
        [FieldNullValue(typeof(double), "-1")]
        public double Temperature;

        public string tPrecision;

        public string logIndex;

        public string status;
    }
}