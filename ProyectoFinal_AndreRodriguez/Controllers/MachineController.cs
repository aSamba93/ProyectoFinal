using Microsoft.AspNetCore.Mvc;
using ProyectoFinal_AndreRodriguez.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal_AndreRodriguez.Controllers
{
    public class MachineController : Controller
    {
        private readonly ICosmosDBServiceMachine _cosmosDBService;
        public MachineController(ICosmosDBServiceMachine cosmosDBService)
        {
            this._cosmosDBService = cosmosDBService;
        }
        public async Task<IActionResult> Machines()
        {
            return View((await this._cosmosDBService.GetMachinesAsync("SELECT * FROM machine")).ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        public async Task<ActionResult> CreateMachine(Machine machine)
        {
            //method for generate id
            machine.id = Guid.NewGuid().ToString();
            machine.prob_fail = probFail();
            await this._cosmosDBService.AddMachineAsync(machine);
            
            return RedirectToAction("Machines");

        }

        public IActionResult Edit(Machine machine)
        {
            return View(machine);
        }

        public async Task<ActionResult> EditMachine(Machine machine)
        {
            await this._cosmosDBService.UpdateMachineAsync(machine.id, machine);

            return RedirectToAction("Machines");

        }

        public IActionResult Delete(Machine machine)
        {
            return View(machine);
        }

        public async Task<ActionResult> DeleteMachine(Machine machine)
        {
            await this._cosmosDBService.DeleteMachineAsync(machine.id);

            return RedirectToAction("Machines");

        }
        public double probFail()
        {
            return (new Random().Next(1,11)/10.00);
        }
    }
}
