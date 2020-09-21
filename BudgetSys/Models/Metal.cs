using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BudgetSys.Models
{
    public class Metal : INotifyPropertyChanged
	{

		public event PropertyChangedEventHandler PropertyChanged;

		// This method is called by the Set accessor of each property.  
		// The CallerMemberName attribute that is applied to the optional propertyName  
		// parameter causes the property name of the caller to be substituted as an argument.  
		private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public int id { get; set; }
		public int batchId { get; set; }
		public string m_name { get; set; }
		public string m_picture { get; set; }
		public int m_materialId { get; set; }
		public int m_qty { get; set; }
		public float m_length { get; set; }
		public float m_width { get; set; }
		public float m_thick { get; set; }
		public float m_volume { get; set; }
		public float m_weigth { get; set; }
		public float m_loss { get; set; }
		public int m_tonnage { get; set; }
		public int m_workStation { get; set; }
		public float m_yield { get; set; }
		private float _costOfMetetial1;
		public float m_costOfMeterial1 { 
			get
			{
				return _costOfMetetial1;
			}
			set
			{
				_costOfMetetial1 = value;
				NotifyPropertyChanged();
			} 
		}
		public float m_finishedCost1 { get; set; }
		public float m_CMF1 { get; set; }
		public float m_total1 { get; set; }
		public string m_costOfMeterial2 { get; set; }
		public string m_finishedCost2 { get; set; }
		public string m_CMF2 { get; set; }
		public string m_total2 { get; set; }
	}
}
