using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using UnitsNet;

namespace SRO_Management.Models
{
    /// <summary>
    /// Provides pressure and temperature unit conversions based on user selection on view.
    /// Uses Units.NET library from NuGet. Fully unit tested, PCL compatible, free license.
    /// </summary>
    public class UnitConverter
    {

        private double PressureConvert (double psiPres, PresUnitSelection targetUnit)
        {
            Pressure initialValue = Pressure.FromPsi(psiPres);
            double convertedValue;

            switch(targetUnit)
            {
                case PresUnitSelection.kPa:
                    convertedValue = initialValue.Kilopascals;
                    return convertedValue;

                case PresUnitSelection.bar:
                    convertedValue = initialValue.Bars;
                    return convertedValue;

                case PresUnitSelection.atm:
                    convertedValue = initialValue.Atmospheres;
                    return convertedValue;

                case PresUnitSelection.psia:
                    convertedValue = psiPres;
                    return convertedValue;
                
                default:
                    System.Diagnostics.Trace.Assert(false, "User selected unit conversion not implemented");
                    throw new NotImplementedException("User selected unit conversion not implemented");
                 
            }
        }


        private double TempConvert (double degcTemp, TempUnitSelection targetUnit)
        {
            Temperature initialValue = Temperature.FromDegreesCelsius(degcTemp);
            double convertedValue;

            switch(targetUnit)
            {
                case TempUnitSelection.degF:
                    convertedValue = initialValue.DegreesFahrenheit;
                    return convertedValue;

                case TempUnitSelection.degR:
                    convertedValue = initialValue.DegreesRankine;
                    return convertedValue;

                case TempUnitSelection.Kelvin:
                    convertedValue = initialValue.Kelvins;
                    return convertedValue;

                case TempUnitSelection.degC:
                    convertedValue = degcTemp;
                    return convertedValue;

                default:
                    System.Diagnostics.Trace.Assert(false, "User selected unit converted not implemented.");
                    throw new NotImplementedException("User selected unit conversion not implemented.");

            }

        }

        /// <summary>
        /// Accepts IEnum of raw data records from selected Csv files and converts them base on user selection on view
        /// </summary>
        /// <param name="rawRecords"></param>
        /// <param name="pressureUnit"></param>
        /// <param name="tempUnit"></param>
        /// <returns>IEnumerable<IDataRecord></returns>
        public IEnumerable<IDataRecord> ConvertUnits(IEnumerable<IDataRecord> rawRecords, PresUnitSelection pressureUnit, TempUnitSelection tempUnit)
        {
            foreach(var record in rawRecords)
            {
                if(record.Pressure != null)
                {
                    double pressure = Convert.ToDouble(record.Pressure);
                    record.Pressure = PressureConvert(pressure, pressureUnit);
                }

                if(record.Temperature != null)
                {
                    double temp = Convert.ToDouble(record.Temperature);
                    record.Temperature = TempConvert(temp, tempUnit);
                }
            }

            return rawRecords;
                
        }


    }
}
