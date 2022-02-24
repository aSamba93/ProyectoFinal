using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
namespace ProyectoFinal_AndreRodriguez.Models
{
    public interface ICosmosDBServiceProducto
    {
        Task<IEnumerable<Producto>> GetProductosAsync(string query);
        Task<Producto> GetProductoAsync(string id);
        Task AddProductoAsync(Producto producto);
        Task UpdateProductoAsync(string id, Producto producto);
        Task DeleteProductoAsync(string id);

    }
    public class CosmosDBServiceProducto : ICosmosDBServiceProducto
    {
        private Container _container;

        public CosmosDBServiceProducto(CosmosClient client, string databaseName, string containerName)
        {
            this._container = client.GetContainer(databaseName, containerName);
        }
        public async Task AddProductoAsync(Producto item)
        {
            await this._container.CreateItemAsync<Producto>(item, new PartitionKey(item.Id));
        }

        public async Task DeleteProductoAsync(string id)
        {
            await this._container.DeleteItemAsync<Producto>(id, new PartitionKey(id));
        }

        public async Task<Producto> GetProductoAsync(string id)
        {
            try
            {
                ItemResponse<Producto> response = await this._container.ReadItemAsync<Producto>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Producto>> GetProductosAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<Producto>(new QueryDefinition(queryString));
            List<Producto> results = new List<Producto>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task UpdateProductoAsync(string id, Producto item)
        {
            await this._container.UpsertItemAsync<Producto>(item, new PartitionKey(id));
        }
    }
}
