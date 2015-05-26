using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRO_Management.Models
{
    public class DataRecordComparer : IEqualityComparer<IDataRecord>
    {
        public bool Equals(IDataRecord x, IDataRecord y)
        {
            return x.TimeStamp == y.TimeStamp;
        }

        public int GetHashCode(IDataRecord obj)
        {

            if (obj == null)
            {
                return 0;
            }

            return obj.TimeStamp.GetHashCode();
        }
    }
}