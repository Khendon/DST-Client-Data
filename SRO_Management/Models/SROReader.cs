using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SRO_Management.Models
{
    public class SROReader : IEnumerable<IDataRecord>
    {

        private IEnumerable<SRORecord> SroRecords;

        public SROReader(string dirPath, List<string> fileNames, FileTypes userFileType)
        {
            ParseRecords(dirPath, fileNames, userFileType);
            SroRecords = new List<SRORecord>();
        }

        private void ParseRecords(string dirPath, List<string> fileNames, FileTypes userFileType)
        {
            Regex sroRegex = new Regex(@"^(?<number>\d+),(?<source>\d:\d),(?<timestamp>\d{1,2} \w{2,4} \d{4} \d{1,2}:\d{2}:\d{2}),((?<pressure>\d+\.\d+)?|(?<pError>Err:\w+)?)?,(?<pPrecision>\s±\d+.\d+)?,((?<temp>\d+\.\d+)?|(?<tError>Err:\w+)?)?");

            foreach (var fileName in fileNames)
            {
                try
                {
                    using ( StreamReader inputStream = new StreamReader(dirPath + "\\" + fileName))
                    {
                        string line;
                        while ((line = inputStream.ReadLine()) != null)
                        {
                            Match validRecord = sroRegex.Match(line);

                            if (validRecord.Success)
                            {
                                SRORecord record = new SRORecord();
                                record.Count = validRecord.Groups["number"].Value;
                                record.Source = validRecord.Groups["source"].Value;
                                record.TimeStamp = validRecord.Groups["timestamp"].Value;
                                record.Pressure = validRecord.Groups["pressure"].Value;
                            }
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
            return SroRecords.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}

