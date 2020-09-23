using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetSys.Models
{
    public class Sys
    {
        public  BatchType BatchType { get; set; }

        public readonly static string configPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "/Config/";
        private static List<MetalMaterial> _metalMaterials;
        public static List<MetalMaterial> metalMaterials
        {
            get
            {
                if (_metalMaterials == null)
                {
                    _metalMaterials = Newtonsoft.Json.JsonConvert.DeserializeObject<List<MetalMaterial>>(File.ReadAllText(configPath + "Materials.json"));
                }
                return _metalMaterials;
            }
        }


        private static List<MetalTonnage> _metalTonnages;
        public static List<MetalTonnage> metalTonnages
        {
            get
            {
                if (_metalTonnages == null)
                {
                    _metalTonnages = Newtonsoft.Json.JsonConvert.DeserializeObject<List<MetalTonnage>>(File.ReadAllText(configPath + "Tonnages.json"));
                }
                return _metalTonnages;
            }
        }

        private static Columns _columns;
        public static Columns Columns
        {
            get
            {
                if (_columns == null)
                {
                    _columns = Newtonsoft.Json.JsonConvert.DeserializeObject<Columns>(File.ReadAllText(configPath + "Columns.json"));
                }
                return _columns;
            }
        }

        private static ExchangeRate _exchangeRate;
        public static ExchangeRate ExchangeRate
        {
            get
            {
                if (_exchangeRate == null)
                {
                    _exchangeRate = Newtonsoft.Json.JsonConvert.DeserializeObject<ExchangeRate>(File.ReadAllText(configPath + "ExchangeRate.json"));
                }
                return _exchangeRate;
            }
        }

        private static List<PlasticMaterial> _plasticMaterials;
        public static List<PlasticMaterial> plasticMaterials
        {
            get
            {
                if (_plasticMaterials == null)
                {
                    _plasticMaterials = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PlasticMaterial>>(File.ReadAllText(configPath + "PlasticMaterials.json"));
                }
                return _plasticMaterials;
            }
        }


        private static List<PlasticTonnage> _plasticTonnages;
        public static List<PlasticTonnage> plasticTonnages
        {
            get
            {
                if (_plasticTonnages == null)
                {
                    _plasticTonnages = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PlasticTonnage>>(File.ReadAllText(configPath + "PlasticTonnages.json"));
                }
                return _plasticTonnages;
            }
        }

        private static List<PlasticSalivaId> _plasticSalivaId;
        public static List<PlasticSalivaId> plasticSalivaId
        {
            get
            {
                if (_plasticSalivaId == null)
                {
                    _plasticSalivaId = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PlasticSalivaId>>(File.ReadAllText(configPath + "PlasticSalivaId.json"));
                }
                return _plasticSalivaId.OrderBy(x=>x.scope).ToList();
            }
        }
    }
}
