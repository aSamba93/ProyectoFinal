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
        private readonly ICosmosDBServiceMaquina _cosmosDbService;

        public MachineController(ICosmosDBServiceMaquina cosmosDBService)
        {
            this._cosmosDbService = cosmosDBService;
        }
        public IActionResult Crear()
        {
            return View();
        }
        public async Task<ActionResult> Maquina()
        {
            return View((await this._cosmosDbService.GetMaquinasAsync("SELECT * FROM maquina")).ToList());
        }
        public async Task<ActionResult> CrearMaquina(Maquina Maquina)
        {
            Maquina.id = Guid.NewGuid().ToString();
            await this._cosmosDbService.AddMaquinaAsync(Maquina);
            return RedirectToAction("Maquina");
        }
        public IActionResult Edit(Maquina Maquina)
        {
            return View(Maquina);
        }

        public async Task<ActionResult> EditMaquina(Maquina Maquina)
        {
            await this._cosmosDbService.UpdateMaquinaAsync(Maquina.id, Maquina);
            return RedirectToAction("Maquina");
        }
        public ActionResult Delete(Maquina Maquina)
        {
            return View(Maquina);
        }

        public async Task<ActionResult> DeleteMaquina(Maquina Maquina)
        {
            await _cosmosDbService.DeleteMaquinaAsync(Maquina.id);
            return RedirectToAction("Maquina");
        }

    }
}
