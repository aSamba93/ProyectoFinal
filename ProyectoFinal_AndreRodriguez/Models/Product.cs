using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal_AndreRodriguez.Models
{
    public class Product
    {        
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }

        [JsonProperty(PropertyName = "descripction")]
        public string descripction { get; set; }

        [JsonProperty(PropertyName = "price")]
        public int price { get; set; }
    }
}