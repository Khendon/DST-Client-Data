using System;
using FileHelpers;
using System.Collections;
using System.Collections.Generic;

namespace SRO_Management.Models
{
    public class SROReader : IEnumerable<IDataRecord>
    {
        private List<SRORecord> histRecords;

        public SROReader(string dirPath, List<string> fileNames)
        {
            histRecords = new List<SRORecord>();
            ParseRecords(dirPath, fileNames);            
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
                    histRecords.Add(record);
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
            return histRecords.GetEnumerator();
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}