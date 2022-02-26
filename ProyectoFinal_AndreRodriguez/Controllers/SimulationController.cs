using Microsoft.AspNetCore.Mvc;
using ProyectoFinal_AndreRodriguez.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal_AndreRodriguez.Controllers
{
    public class SimulationController : Controller
    {
        private readonly ICosmosDBServiceMachine _cosmosServiceMachine;
        private readonly ICosmosDBServiceProduct _cosmosServiceProduct;
        private readonly ICosmosDBServiceSimulation _cosmosServiceSimulation;

        public SimulationController(ICosmosDBServiceMachine cosmosDBServiceMachine, ICosmosDBServiceSimulation cosmosDBServiceSimulation, ICosmosDBServiceProduct cosmosDBServiceProduct)
        {
            this._cosmosServiceMachine = cosmosDBServiceMachine;
            this._cosmosServiceSimulation = cosmosDBServiceSimulation;
            this._cosmosServiceProduct = cosmosDBServiceProduct;
        }
        public IActionResult Create()
        {
            
            IEnumerable<Machine> list_machine = this._cosmosServiceMachine.GetMachinesAsync("SELECT * FROM machine").Result;
            ModelFinal modelFinal = new ModelFinal();
            modelFinal.MachineList = list_machine;

            IEnumerable<Product> list_product = this._cosmosServiceProduct.GetProductsAsync("SELECT * FROM producto").Result;
            modelFinal.ProductList = list_product;
            //como enviar por parametros el model al view
            return View(modelFinal);
        }
        public ActionResult Details()
        {
            IEnumerable<Simulation> simulaciones = this._cosmosServiceSimulation.GetSimilationsAsync("SELECT * FROM simulaciones").Result;
            var simulacionResult = simulaciones.ToList();
            int totalSimulation = simulaciones.ToList().Count();
            var LastSimulation = simulacionResult[(totalSimulation - 1)];
            return View(LastSimulation);
        }
        public async Task<ActionResult> StartSimulation(ModelFinal model)
        {
            model.Simulation.id = Guid.NewGuid().ToString();

            int Contador;

            //recorrer la cantidad de dias
            for (int j = 1; j <= model.Simulation.qty_days_simulating; j++)
            { 

            }

                await this._cosmosServiceSimulation.AddSimulationAsync(model.Simulation, model.Simulation.id);
            return RedirectToAction("Details");

        }
    }
}
