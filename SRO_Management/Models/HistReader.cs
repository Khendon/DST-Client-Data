using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;
using System.Collections;

namespace SRO_Management.Models
{
    public class HistReader : IEnumerable<IDataRecord>
    {

        private List<HistRecord> histRecords { get; set; }

        public HistReader(string dirPath, List<string> fileNames)
        {
            histRecords = new List<HistRecord>();
            ParseRecords(dirPath, fileNames);
        }

        public void ParseRecords(string dirPath, List<string> fileNames)
        {
            FileHelperEngine parser = new FileHelperEngine(typeof(HistRecord));

            parser.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;

            foreach (var file in fileNames)
            {
                var records = parser.ReadFile(System.IO.Path.Combine(dirPath, file)) as HistRecord[];

                foreach (var record in records)
                {
                    histRecords.Add(record);
                }
            }

            if (parser.ErrorManager.HasErrors)
            {
                parser.ErrorManager.SaveErrors(System.IO.Path.Combine(dirPath,"errors.txt"));
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
