using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRO_Management.Models
{
    public class SortAndFilterData
    {
        
        public IEnumerable<IDataRecord> ChooseFilters(IEnumerable<IDataRecord> unfilteredRecords, bool allDataCheck, DateTime? filterStart, DateTime? filterEnd)
        {
            IEnumerable<IDataRecord> sortedRecords = SortRecords(unfilteredRecords);

            if(allDataCheck)
            {                
                return sortedRecords;
            }
            else
            {
                IEnumerable<IDataRecord> filteredRecords = TimeFilterRecords(unfilteredRecords, filterStart, filterEnd);
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

            List<IDataRecord> filteredRecords = unsortedRecords
                .GroupBy(record => record.TimeStamp)
                .Select(t => t.First())
                .ToList();

            var sortedRecords = from record in filteredRecords
                                orderby record.TimeStamp
                                select record;

            return sortedRecords;

        }
        
    }
}
