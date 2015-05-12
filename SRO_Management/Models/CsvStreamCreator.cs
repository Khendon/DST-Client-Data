using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using CsvHelper;

namespace SRO_Management.Models
{
    public class CsvStreamCreator
    {

        //private CsvConfiguration MemReaderConfig()
        //{
        //    CsvConfiguration config = new CsvConfiguration();
        //    config.Delimiter = ",";
        //    config.IgnoreQuotes = true;
        //    config.QuoteNoFields = true;
        //    config.WillThrowOnMissingField = false;
        //    config.TrimFields = true;
        //    config.RegisterClassMap<MemRecordClassMap>();

        //    return config;
        //}

        private CsvConfiguration SROReaderConfig()
        {

            CsvConfiguration config = new CsvConfiguration();
            config.Delimiter = ",";
            config.IgnoreQuotes = true;
            config.QuoteNoFields = true;
            config.WillThrowOnMissingField = false;
            config.TrimFields = true;
            config.RegisterClassMap<SRORecordClassMap>();

            return config;
        }

        public CsvReader CsvStream(TextReader inputStream, FileTypes userSelectedFileType)
        {
            CsvConfiguration config;

            switch (userSelectedFileType)
            {
                case FileTypes.Memory:
                    //config = MemReaderConfig();
                    config = SROReaderConfig();
                    break;
                case FileTypes.SRO:
                    config = SROReaderConfig();
                    break;
                default:
                    System.Diagnostics.Debug.Assert(false, "User file type selection not implemented");
                    throw new NotImplementedException("User file type selection not implemented");
            }

            CsvReader reader = new CsvReader(inputStream, config);

            return reader;

        }
    }
}
