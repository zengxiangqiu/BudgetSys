using BudgetSys.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace BudgetSys.Repositories
{
    public abstract class MatetiralRepository<T> where T: MaterialBase, new()
    {
        public MatetiralRepository(string basePath)
        {
            this.metalBasePath = basePath;
        }

        protected readonly string metalBasePath = "";
        public virtual void Save(object dataContext) 
        {
            var viewModel = dataContext as MetalViewModel<T>;
            var metals = Newtonsoft.Json.JsonConvert.SerializeObject(viewModel.Details);

            var batchNo = viewModel.BatchInput.Trim();
            if (viewModel.CurrentBatch.batchNo.Equals(String.Empty))
            {
                //文件名不能为空
                if (batchNo.Equals(string.Empty))
                {
                    throw new Exception("文件名不能为空");
                }
                //文件已存在
                if (File.Exists(metalBasePath + batchNo + ".json"))
                {
                    throw new Exception("批次已存在");
                }
            }
            else
            {
                batchNo = viewModel.CurrentBatch.batchNo;
            }
            //保存
            File.WriteAllText(metalBasePath + batchNo + ".json", metals);
            var batch = new MetalBatch { batchNo = batchNo, batchType  =GetBatchType()};
            if (viewModel.Batches.Where(x => x.batchNo == batch.batchNo).Count() == 0)
            {
                viewModel.Batches.Add(batch);
                viewModel.CurrentBatch = batch;
            }
        }

        protected virtual void DeleteBatch(object dataContext, MetalBatch batch)
        {
            File.Delete(metalBasePath + batch.batchNo + ".json");
            (dataContext as MetalViewModel<T>).Batches.Remove(batch);
        }

        protected virtual void DeleteRecord(object dataContext, T material)
        {
            (dataContext as MetalViewModel<T>).Details.Remove(material);
        }

        public virtual ObservableCollection<MetalBatch> GetBatches() 
        {
            var batches = new DirectoryInfo(metalBasePath).GetFiles("*.json").Where(x=>!x.Name.Contains("default")).ToList();
            var batchesList = new ObservableCollection<MetalBatch>(batches.Select(x => new MetalBatch
            {
                batchNo = x.Name.Replace(".json", ""),
                batchType =GetBatchType()
            }));

            return batchesList;
        }

        protected object CreateViewModel(MetalBatch batch)
        {
            ObservableCollection<T> details;
            try
            {
                //if(batch!=null &&batch?.batchNo == "default")
                //    details = Newtonsoft.Json.JsonConvert.DeserializeObject<ObservableCollection<T>>(File.ReadAllText(metalBasePath + "default.json"));
                //else if(batch==null)
                //    details = new ObservableCollection<T>();
                //else
                    details = Newtonsoft.Json.JsonConvert.DeserializeObject<ObservableCollection<T>>(File.ReadAllText(metalBasePath + batch.batchNo + ".json"));
            }
            catch (Exception)
            {
                details = new ObservableCollection<T>();
            }

            if (batch?.batchNo == "default")
                batch = null;

            return new MetalViewModel<T>
            {
                Batches = GetBatches(),
                Details = details,
                CurrentBatch = batch??new MetalBatch { batchType = GetBatchType() }
            };
        }

        protected virtual void AutoGenColumns(DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "id")
            {
                e.Column.IsReadOnly = true;
            }
            if (e.PropertyName == "batchNo")
            {
                e.Cancel = true;
                return;
            }
            else
            {
                if (e.PropertyName == "materialId")
                {
                    var templateColumn = new DataGridComboBoxColumn();

                    templateColumn.ItemsSource = Materials;

                    templateColumn.DisplayMemberPath = "name";
                    templateColumn.SelectedValuePath = "id";
                    var binding = new Binding("materialId");
                    binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                    templateColumn.SelectedValueBinding = binding;
                    e.Column = templateColumn;
                }
                else if (e.PropertyName == "tonnageId")
                {
                    var templateColumn = new DataGridComboBoxColumn();
                    templateColumn.ItemsSource = Tonnages;
                    templateColumn.DisplayMemberPath = "tonnage";
                    templateColumn.SelectedValuePath = "id";
                    var binding = new Binding("tonnageId");
                    binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                    templateColumn.SelectedValueBinding = binding;
                    e.Column = templateColumn;
                }
                e.Column.Header = e.PropertyName;
                //e.Column.Header = data["Metal"][e.PropertyName];
            }
        }

        public abstract void Calculate(MaterialBase material);

        protected virtual object AddNewItem(object dataContext) 
        {
            var batchNo = (dataContext as MetalViewModel<T>).CurrentBatch?.batchNo;
            var metal = new T
            {
                id = (dataContext as MetalViewModel<T>).Details.Count,
                batchNo = batchNo
            };
            return metal;
        }

        public abstract IEnumerable<object> Materials { get; }

        public abstract IEnumerable<object> Tonnages { get; }

        public abstract BatchType GetBatchType();

        public virtual void Calculate(MaterialBase materialBase, DataGridColumn column, Object value, Dictionary<string, ColumnProp> props)
        {
            try
            {
                var metal = materialBase as T;
                var propName = props.Where(x => x.Value.description == column.Header.ToString()).First();
                System.Reflection.PropertyInfo prop = materialBase.GetType().GetProperty(propName.Key, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                var val =  Convert.ChangeType(value, prop.PropertyType);
                prop.SetValue(metal, val, null);

                Calculate(materialBase);

                metal.NotifyAllProps();
            }
            catch (Exception ex)
            {
                //log
            }
        }

    }
}
