using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SRO_Management.ViewModels
{

    /// <summary>
    /// Base view model for INotifyPropertyChanged. To be inherited by Viewmodels.
    /// </summary>
    public class INotifyViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;

            if (handler != null)
            {
                PropertyChangedEventArgs args = new PropertyChangedEventArgs(propertyName);

                handler(this, args);
            }
        }

    }
}
