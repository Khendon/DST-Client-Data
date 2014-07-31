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
        public List<SRORecord> SroRecords
        {
            get { return sroRecords; }
            private set { sroRecords = value; }
        }

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
                catch (CsvReaderException ex)
                {
                    System.Windows.MessageBox.Show("Please select a valid SRO/Historical data file");
                    System.Diagnostics.Trace.Assert(false, "User selected invalid file", ex.Message);
                }

            }
        }


        public IEnumerator<IDataRecord> GetEnumerator()
        {

            foreach (var record in sroRecords)
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

