using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal_AndreRodriguez.Models
{
    public class ModelFinal
    {
        //este object es para mandarlo por parametros a la vista para poder obtener la lista de maquinas y productos y podr escogerlos
        public Simulation Simulation { get; set; }
        public IEnumerable<Product> ProductList { get; set; }
        public IEnumerable<Machine> MachineList { get; set; }
    }
}
