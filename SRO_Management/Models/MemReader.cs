using System;
using System.Collections.Generic;
using System.Collections;
using FileHelpers;

namespace SRO_Management.Models
{
    public class MemReader : IEnumerable<IDataRecord>
    {
        private IEnumerable<MemRecord> memRecords;

        public MemReader(string dirPath, string fileName)
        {
            ParseRecords(dirPath, fileName);
        }

        public void ParseRecords(string dirPath, string fileName)
        {
            FileHelperEngine parser = new FileHelperEngine(typeof(MemRecord));

            parser.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;

            var records = parser.ReadFile(System.IO.Path.Combine(dirPath, fileName)) as MemRecord[];

            memRecords = records;

            if (parser.ErrorManager.HasErrors)
            {
                parser.ErrorManager.SaveErrors(System.IO.Path.Combine(dirPath, "errors.txt"));
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
