﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BudgetSys.Models
{
    public class MetalViewModel
    {
        public ObservableCollection<Metal> Details { get; set; }

        public List<MetalBatch> Batches { get; set; }
    }
}