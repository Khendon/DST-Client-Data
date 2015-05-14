using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;

namespace SRO_Management.Models
{
    public class DateFieldConverter : ConverterBase
    {

        public override object StringToField(string from)
        {
            DateTime convertedDate;

            convertedDate = DateTime.ParseExact(from, "d MMM yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None);

            return convertedDate;
        }

        public override string FieldToString(object from)
        {
            return base.FieldToString(from);
        }
    }
}
