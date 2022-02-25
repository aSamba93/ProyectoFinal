using Microsoft.AspNetCore.Mvc;
using ProyectoFinal_AndreRodriguez.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal_AndreRodriguez.Controllers
{
    public class ProductController : Controller
    {
        private readonly ICosmosDBServiceProducto _cosmosDbService;

        public ProductController(ICosmosDBServiceProducto cosmosDBService)
        {
            this._cosmosDbService = cosmosDBService;
        }
        public IActionResult Crear()
        {
            return View();
        }
        public async Task<ActionResult> Producto()
        {
            return View((await this._cosmosDbService.GetProductosAsync("SELECT * FROM producto")).ToList());
        }
        public async Task<ActionResult> CrearProducto(Producto producto)
        {
            producto.Id = Guid.NewGuid().ToString();
            await this._cosmosDbService.AddProductoAsync(producto);
            return RedirectToAction("Producto");
        }
        public IActionResult Edit(Producto producto)
        {
            return View(producto);
        }

        public async Task<ActionResult> EditProducto(Producto producto)
        {
            await this._cosmosDbService.UpdateProductoAsync(producto.Id, producto);
            return RedirectToAction("Producto");
        }
        public ActionResult Delete(Producto producto)
        {
            return View(producto);
        }

        public async Task<ActionResult> DeleteProduct(Producto producto)
        {
            await _cosmosDbService.DeleteProductoAsync(producto.Id);
            return RedirectToAction("Producto");
        }
    }
}
