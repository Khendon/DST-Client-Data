using System;
using System.Collections;
using System.Collections.Generic;
using FileHelpers;

namespace SRO_Management.Models
{
    /// <summary>
    /// Reader class for parsing SRO/Historic log files.
    /// </summary>
    public class SROReader : IEnumerable<IDataRecord>
    {
        private List<SRORecord> histRecords;

        public SROReader(string dirPath, List<string> fileNames)
        {
            this.histRecords = new List<SRORecord>();
            this.ParseRecords(dirPath, fileNames);            
        }

        public void ParseRecords(string dirPath, List<string> fileNames)
        {
            FileHelperEngine parser = new FileHelperEngine(typeof(SRORecord));

            parser.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;

            foreach (var file in fileNames)
            {
                var records = parser.ReadFile(System.IO.Path.Combine(dirPath, file)) as SRORecord[];

                foreach (var record in records)
                {
                    this.histRecords.Add(record);
                }
            }

            if (parser.ErrorManager.HasErrors)
            {
                string file = string.Format("SRO_ErrorLog_{0:MM-yy_HHmm}.txt", DateTime.Now);
                parser.ErrorManager.SaveErrors(System.IO.Path.Combine(dirPath, file));
            }
        }

        public IEnumerator<IDataRecord> GetEnumerator()
        {
            return this.histRecords.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}