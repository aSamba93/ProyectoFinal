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
        private readonly ICosmosDBServiceProduct _cosmosDBService;
        public ProductController(ICosmosDBServiceProduct cosmosDBService)
        {
            this._cosmosDBService = cosmosDBService;
        }
        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Products()
        {
            return View((await this._cosmosDBService.GetProductsAsync("SELECT * FROM producto")).ToList());
        }
        public async Task<ActionResult> CreateProduct(Product product)
        {
            //method for generate id
            product.id = Guid.NewGuid().ToString();
            await this._cosmosDBService.AddProductAsync(product);

            return RedirectToAction("Products");

        }

        public IActionResult Edit(Product product)
        {
            return View(product);
        }

        public async Task<ActionResult> EditProduct(Product product)
        {
            await this._cosmosDBService.UpdateProductAsync(product.id, product);

            return RedirectToAction("Products");

        }

        public IActionResult Delete(Product product)
        {
            return View(product);
        }

        public async Task<ActionResult> DeleteProduct(Product product)
        {
            await this._cosmosDBService.DeleteProductAsync(product.id);

            return RedirectToAction("Products");

        }

    }
}
