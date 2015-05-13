using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRO_Management.Models
{
    /// <summary>
    /// Interface implemented by SRORecord and MemoryRecord classes. Used to "filter" only pertinent info required for client data files
    /// </summary>
    public interface IDataRecord
    {
        DateTime TimeStamp { get; set; }
        double Pressure { get; set; }
        double Temperature { get; set; }
    }
}
