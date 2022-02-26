using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal_AndreRodriguez.Models
{
    public class Simulation
    {
        public Simulation()
        {
            machine1 = "";
            machine2 = "";
            product = "";
            winner_machine = "";
            id = "";
        }
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }

        /*  Datos de la produccion diaria  */
        [JsonProperty(PropertyName = "qty_weekly_production_day")]
        public int qty_weekly_production_day { get; set; }

        [JsonProperty(PropertyName = "qty_diary_production_hours")]
        public int qty_diary_production_hours { get; set; }

        /*  Datos de la duracion de la simulacion   */

        [JsonProperty(PropertyName = "qty_months_simulating")]
        public int qty_months_simulating { get; set; }
        [JsonProperty(PropertyName = "qty_days_simulating")]
        public int qty_days_simulating { get; set; }
        [JsonProperty(PropertyName = "qty_hours_simulating")]
        public int qty_hours_simulating { get; set; }

        /*  Datos de las maquinas para la simulacion  */

        [JsonProperty(PropertyName = "manufacturer_price")]
        public int manufacturer_price { get; set; }
        [JsonProperty(PropertyName = "machine1")]
        public string machine1 { get; set; }
        [JsonProperty(PropertyName = "machine2")]
        public string machine2 { get; set; }
        [JsonProperty(PropertyName = "product")]
        public string product { get; set; }

        /*  Datos de resultado  */

        [JsonProperty(PropertyName = "qty_produced_M1")]
        public int qty_produced_M1 { get; set; }

        [JsonProperty(PropertyName = "qty_produced_M2")]
        public int qty_produced_M2 { get; set; }

        [JsonProperty(PropertyName = "gross_profit_M1")]
        public int gross_profit_M1 { get; set; }

        [JsonProperty(PropertyName = "gross_profit_M2")]
        public int gross_profit_M2 { get; set; }

        [JsonProperty(PropertyName = "net_profit_M1")]
        public int net_profit_M1 { get; set; }

        [JsonProperty(PropertyName = "net_profit_M2")]
        public int net_profit_M2 { get; set; }

        [JsonProperty(PropertyName = "winner_machine")]
        public string winner_machine { get; set; }

    }
}
