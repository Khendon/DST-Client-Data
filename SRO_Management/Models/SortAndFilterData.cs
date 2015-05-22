﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRO_Management.Models
{
    public class SortAndFilterData
    {
        
        public IEnumerable<IDataRecord> ChooseFilters(IEnumerable<IDataRecord> unfilteredRecords, bool allDataCheck, DateTime? filterStart, DateTime? filterEnd, string shiftDirection, TimeSpan linearShift)
        {
            IEnumerable<IDataRecord> sortedRecords = SortRecords(unfilteredRecords);

            if(allDataCheck)
            {
                var shiftedRecords = shiftTimeStamp(sortedRecords, shiftDirection, linearShift);
                return shiftedRecords;
            }
            else
            {
                var shiftedRecords = shiftTimeStamp(sortedRecords, shiftDirection, linearShift);
                var filteredRecords = TimeFilterRecords(shiftedRecords, filterStart, filterEnd);
                return filteredRecords;
            }
        }

        /// <summary>
        /// Called if time filtering is enable on mainwindow, selected records between start and end times.
        /// </summary>
        /// <param name="unfilteredRecords"></param>
        /// <param name="filterStart"></param>
        /// <param name="filterEnd"></param>
        /// <returns>IEnumerable<IDataRecord></returns>
        private IEnumerable<IDataRecord> TimeFilterRecords(IEnumerable<IDataRecord> unfilteredRecords, DateTime? filterStart, DateTime? filterEnd)
        {
            var filteredRecords = from record in unfilteredRecords
                                  where (record.TimeStamp > filterStart) && (record.TimeStamp < filterEnd)
                                  select record;

            return filteredRecords;
        }


        /// <summary>
        /// Groups IEnum data records to remove duplicate timestamps, sorts in date/time order and returns IEnum.
        /// </summary>
        /// <param name="unsortedRecords"></param>
        /// <returns>IEnumerable<IDataRecord></returns>
        private IEnumerable<IDataRecord> SortRecords(IEnumerable<IDataRecord> unsortedRecords)
        {
            var pressureOnly = from record in unsortedRecords
                               where (record.Pressure.HasValue && !record.Temperature.HasValue)
                               select record;

            var temperatureOnly = from record in unsortedRecords
                                  where (!record.Pressure.HasValue && record.Temperature.HasValue)
                                  select record;

            var mergedRecords = from tRecord in temperatureOnly
                                join pRecord in pressureOnly on tRecord.TimeStamp equals pRecord.TimeStamp
                                select new Models.SRORecord { TimeStamp = pRecord.TimeStamp, Pressure = pRecord.Pressure, Temperature = tRecord.Temperature };

            var finalRecords = mergedRecords.Union(unsortedRecords, new Models.DataRecordComparer());

            var sortedRecords = from record in finalRecords
                                orderby record.TimeStamp
                                select record;

            return sortedRecords;           

        }

        private IEnumerable<IDataRecord> shiftTimeStamp(IEnumerable<IDataRecord> unShiftedRecords, string selectedShift, TimeSpan linearShift)
        {
            if (selectedShift == "+")
            {
                foreach (var record in unShiftedRecords)
                {
                    record.TimeStamp = record.TimeStamp.Add(linearShift);
                }

                return unShiftedRecords;
            }
            else
            {
                foreach (var record in unShiftedRecords)
                {
                    record.TimeStamp = record.TimeStamp.Subtract(linearShift);
                }

                return unShiftedRecords;
            }
        }
        
    }
}
