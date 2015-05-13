using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;
using System.Collections;

namespace SRO_Management.Models
{
    public class CsvReader : IEnumerable<IDataRecord>
    {

        IEnumerable<HistRecord> histRecords;

        public CsvReader(string dirPath, List<string> fileNames, FileTypes userFileType)
        {
            ParseRecords(dirPath, fileNames, userFileType);
        }

        public void ParseRecords(string dirPath, List<string> fileNames, FileTypes userFileType)
        {
            FileHelperEngine parser = new FileHelperEngine(typeof(HistRecord));

            parser.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;

            foreach (var file in fileNames)
            {
                histRecords = parser.ReadFile(System.IO.Path.Combine(dirPath,file)) as HistRecord[];
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
