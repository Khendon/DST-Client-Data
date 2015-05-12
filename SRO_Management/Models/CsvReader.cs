using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;

namespace SRO_Management.Models
{
    public class CsvReader
    {

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
                HistRecord[] records = parser.ReadFile(dirPath + "\\" + file) as HistRecord[];

                foreach (var record in records)
                {
                    System.Diagnostics.Debug.WriteLine(record.TimeStamp.ToString());
                }
            }

            if (parser.ErrorManager.HasErrors)
            {
                parser.ErrorManager.SaveErrors("errors.txt");
            }

        }

    }
}
