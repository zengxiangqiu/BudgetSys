using Newtonsoft.Json;
using Npoi.Mapper.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BudgetSys.Models
{
    public abstract class MaterialBase: INotifyPropertyChanged, IEditableObject
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property.  
        // The CallerMemberName attribute that is applied to the optional propertyName  
        // parameter causes the property name of the caller to be substituted as an argument.  
        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void BeginEdit()
        {
        }

        public void EndEdit()
        {
        }

        public void CancelEdit()
        {
        }

        public int id { get; set; }

        [Ignore]
        public string batchNo { get; set; }

    }
}
