using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;

namespace SRO_Management.Models
{
    public class SROReader : IEnumerable<IDataRecord>
    {
        private List<SRORecord> sroRecords = new List<SRORecord>();

        public SROReader(string dirPath, List<string> fileNames, FileTypes userFileType)
        {
            ParseRecords(dirPath, fileNames, userFileType);
        }

        private void ParseRecords(string dirPath, List<string> fileNames, FileTypes userFileType)
        {
            CsvStreamCreator csvStreamCreate = new CsvStreamCreator();

            foreach (var fileName in fileNames)
            {
                try
                {
                    TextReader inputStream = new StreamReader(dirPath + "\\" + fileName);
                    inputStream.ReadLine();
                    inputStream.ReadLine();
                    inputStream.ReadLine();

                    using (var csv = csvStreamCreate.CsvStream(inputStream, userFileType))
                    {
                        var records = csv.GetRecords<SRORecord>();

                        foreach (var record in records)
                        {
                            sroRecords.Add(record);
                        }
                    }

                }
                catch (Exception sroEx)
                {
                    System.Diagnostics.Trace.WriteLine(DateTime.Now + sroEx.ToString(), "Error parsing SRO log file");
                    throw;
                }

            }
        }


        public IEnumerator<IDataRecord> GetEnumerator()
        {
            return sroRecords.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}

