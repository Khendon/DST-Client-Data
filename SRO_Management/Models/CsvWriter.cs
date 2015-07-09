using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRO_Management.Models
{
    /// <summary>
    /// Writes client data file using data passed from viewmodel. Due to the required layout not being "strict" CSV, does not use FileHelper library.
    /// </summary>
    public class CsvWriter
    {
        public void CreateFileWriterStreams(string fileName, IEnumerable<IDataRecord> clientRecords, HeaderModel headerData)
        {          
            using (TextWriter clientDataFile = new StreamWriter(fileName))
            {
                clientDataFile.WriteLine("{0},{1}", headerData.ClientLabel, headerData.ClientInput);
                clientDataFile.WriteLine("{0},{1}", headerData.WellLabel, headerData.WellInput);
                clientDataFile.WriteLine("{0},{1}", headerData.DstLabel, headerData.DstInput);
                clientDataFile.WriteLine("{0},{1}", headerData.SerialLabel, headerData.SerialInput);
                clientDataFile.WriteLine("{0},{1}", headerData.DepthLabel, headerData.DepthInput);
                clientDataFile.WriteLine("{0},{1}", headerData.PositionLabel, headerData.PositionInput);
                clientDataFile.WriteLine();
                clientDataFile.WriteLine("{0},{1},{2}", headerData.DateTimeLabel, headerData.PressureLabel, headerData.TempLabel);
                clientDataFile.WriteLine("{0},{1},{2}", headerData.DateStringFormat, headerData.PresStringFormat, headerData.TempStringFormat);

                foreach (var record in clientRecords)
                {
                    if (record.TimeStamp != null)
                    {
                        clientDataFile.WriteLine("{0:dd/MM/yyyy HH:mm:ss},{1:f2},{2:f2}", record.TimeStamp, record.Pressure, record.Temperature);
                    }                    
                }
            }
        }       
    }
}