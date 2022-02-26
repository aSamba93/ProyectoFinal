using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal_AndreRodriguez.Models
{
    public class Machine
    {
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }

        [JsonProperty(PropertyName = "description_name")]
        public string description_name { get; set; }

        [JsonProperty(PropertyName = "qty_products_hour")]
        public int qty_products_hour { get; set; }

        [JsonProperty(PropertyName = "cost_operating_hour")]
        public double cost_operating_hour { get; set; }

        [JsonProperty(PropertyName = "prob_fail")]
        public double prob_fail { get; set; }

        [JsonProperty(PropertyName = "repair_hours")]
        public double repair_hours { get; set; }

        [JsonProperty(PropertyName = "date_purchase")]
        public DateTime date_purchase { get; set; }

        [JsonProperty(PropertyName = "condition")]
        public Boolean condition { get; set; }
    }
}
