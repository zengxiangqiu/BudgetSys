﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using BudgetSys.Models;

namespace BudgetSys.Repositories
{
    class PlasticRepository : MatetiralRepository, IMatetrialRepository
    {
        public PlasticRepository() : base(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "/Batches/Plastic/")
        {

        }

        public void AutoGenColumn(DataGridAutoGeneratingColumnEventArgs e)
        {
            base.AutoGenColumns<Plastic>(e);
        }

        public override void Calculate(RawMaterial material)
        {
            var plastic = material as Plastic;

            if (plastic != null)
            {
                var cost = Sys.plasticMaterials.Find(x => x.id == plastic.materialId)?.cost ?? 0;
                var t_cost = Sys.plasticTonnages.Find(x => x.id == plastic.tonnageId)?.cost ?? 0;

                PlasticSalivaId salivaId= null;

                foreach (PlasticSalivaId item in Sys.plasticSalivaId)
                {
                    if (item.scope > plastic.p_netWeigth)
                    {
                        salivaId = item;
                        break;
                    }
                }

                //口水
                plastic.p_salivaId = Math.Round(plastic.p_netWeigth * salivaId.percent,2);
                //毛重量
                plastic.p_grossWeight = Math.Round(plastic.qty *( plastic.p_netWeigth+plastic.p_salivaId),2);

                //损耗
                plastic.loss = Math.Round(plastic.p_grossWeight * 0.03f,2);

                // 加工成本 成本公式*周期s/60/模穴*数量
                plastic.finishedCost1 = Math.Round(t_cost *plastic.p_cycle/60 /plastic.p_cavity* plastic.qty,2) ;

                if (double.IsNaN(plastic.finishedCost1) || double.IsInfinity(plastic.finishedCost1))
                    plastic.finishedCost1 = 0;

                //材料成本 单价公式*（毛重量+损耗）/1000*数量
                plastic.costOfMeterial1 = Math.Round((plastic.p_grossWeight + plastic.loss) /1000 * cost * plastic.qty,2);

                //ME
                plastic.p_ME1 = Math.Round(plastic.finishedCost1 + plastic.costOfMeterial1, 2);

                //total（ME + CMF）/ 良率
                plastic.total1 = Math.Round((plastic.p_ME1 + plastic.CMF1) / (plastic.yield/100),2);

                if (double.IsNaN(plastic.total1) || double.IsInfinity(plastic.total1))
                    plastic.total1 = 0;

                // 加工成本2
                plastic.finishedCost2 = Math.Round(plastic.finishedCost1 / Sys.ExchangeRate.First().Current,2);

                //材料成本2
                plastic.costOfMeterial2 = Math.Round(plastic.costOfMeterial1 / Sys.ExchangeRate.First().Current,2);

                plastic.p_ME2 = Math.Round(plastic.p_ME1 / Sys.ExchangeRate.First().Current,2);

                //total2
                plastic.total2 = Math.Round(plastic.total1 / Sys.ExchangeRate.First().Current,2);

                if (double.IsNaN(plastic.total2) || double.IsInfinity(plastic.total2))
                    plastic.total2 = 0;
            }
        }

        public object CreateViewModel(MetalBatch batch = null)
        {
            return base.CreateViewModel<Plastic>(batch);
        }

        public void DeleteRecord(object dataContext, object material)
        {
            base.DeleteRecord<Plastic>(dataContext, material as Plastic);
        }

        public void DeleteBatch(object dataContext, MetalBatch batch)
        {
            base.DeleteBatch<Plastic>(dataContext, batch);
        }

        public ObservableCollection<MetalBatch> GetBatches()
        {
            return base.GetBatches<Plastic>();
        }

        public void RenameColumn(DataGridColumn column)
        {
            var config = Sys.Columns.Plastic.Where(x => x.Key == column.Header.ToString()).First();
            column.DisplayIndex = config.Value.order;
            column.Header = config.Value.description;
        }

        public void Save(object dataContext)
        {
            base.Save<Plastic>(dataContext);
        }

        public object AddNewItem(object dataContext)
        {
            return base.AddNewItem<Plastic>(dataContext);
        }
    }
}
