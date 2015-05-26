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
                string file = string.Format("Mem_ErrorLog_{0:MM-yy_HHmm}.txt", DateTime.Now);
                parser.ErrorManager.SaveErrors(System.IO.Path.Combine(dirPath, file));
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