using System;
using System.Collections;
using System.Collections.Generic;
using FileHelpers;

namespace SRO_Management.Models
{
    /// <summary>
    /// Reader class for parsing Memory gauge files.
    /// </summary>
    public class MemReader : IEnumerable<IDataRecord>
    {
        private IEnumerable<MemRecord> memRecords;

        public MemReader(string dirPath, string fileName)
        {
            this.ParseRecords(dirPath, fileName);
        }

        public void ParseRecords(string dirPath, string fileName)
        {
            FileHelperEngine parser = new FileHelperEngine(typeof(MemRecord));

            parser.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;

            var records = parser.ReadFile(System.IO.Path.Combine(dirPath, fileName)) as MemRecord[];

            this.memRecords = records;

            if (parser.ErrorManager.HasErrors)
            {
                string file = string.Format("Mem_ErrorLog_{0:MM-yy_HHmm}.txt", DateTime.Now);
                parser.ErrorManager.SaveErrors(System.IO.Path.Combine(dirPath, file));
            }
        }

        public IEnumerator<IDataRecord> GetEnumerator()
        {
            return this.memRecords.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}