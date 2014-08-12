using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace SRO_Management.Models
{
    /// <summary>
    /// Enumerators to populate and utilise comboboxes and radio buttons on view.
    /// Created in namespace to allow selections to be used by other classes.
    /// </summary>

    public enum PresUnitSelection
    {
        psia,
        bar,
        kPa,
        atm
    };

    public enum TempUnitSelection
    {
        degC,
        degF,
        degR,
        Kelvin
    };

    public enum FileTypes
    {
        [Description("SRO/Historical")]
        SRO,
        [Description("Memory")]
        Memory
    };

    
}
