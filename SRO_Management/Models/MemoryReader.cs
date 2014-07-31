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

            TextReader inputStream = new StreamReader(dirPath + "\\" + fileName);
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
        

        public IEnumerator<IDataRecord> GetEnumerator()
        {         

            foreach(var record in memRecords)
            {
                if (record == null)
                {
                    break;
                }

                yield return record;
            }
            
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    
}
