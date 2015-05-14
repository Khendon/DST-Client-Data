using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;

namespace SRO_Management.Models
{
    public class MemoryReader : IEnumerable<IDataRecord>
    {
        List<MemoryRecord> memRecords = new List<MemoryRecord>();

        public MemoryReader(string dir, string file, FileTypes userSelectedFileType)
        {
            ParseRecords(dir, file, userSelectedFileType);
        }


        private void ParseRecords(string dirPath, string fileName, FileTypes userSelectedFileType)
        {
            CsvStreamCreator csvStreamCreate = new CsvStreamCreator();

            try
            {
                TextReader inputStream = new StreamReader(dirPath + "\\" + fileName);
                inputStream.ReadLine();
                inputStream.ReadLine();
                inputStream.ReadLine();

                using (var csv = csvStreamCreate.CsvStream(inputStream, userSelectedFileType))
                {
                    IEnumerable<MemoryRecord> records = csv.GetRecords<MemoryRecord>();

                    foreach (var record in records)
                    {
                        memRecords.Add(record);
                    }
                }

            }
            catch (Exception memEx)
            {
                System.Diagnostics.Trace.WriteLine(DateTime.Now + memEx.ToString(), "Error parsing memory log file");
                throw;
            }            
        }
        

        public IEnumerator<IDataRecord> GetEnumerator()
        {
            return memRecords.GetEnumerator();            
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    
}
