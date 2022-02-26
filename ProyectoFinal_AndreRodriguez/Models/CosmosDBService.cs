using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

namespace ProyectoFinal_AndreRodriguez.Models
{
    public interface ICosmosDBServiceProduct
    {
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(string id, Product producto);
        Task DeleteProductAsync(string id);
        Task<Product> GetProductAsync(string id);
        Task<IEnumerable<Product>> GetProductsAsync(string query);
    }
    public interface ICosmosDBServiceMachine
    {
        Task AddMachineAsync(Machine item);
        Task UpdateMachineAsync(string id, Machine machine);
        Task DeleteMachineAsync(string id);
        Task<Machine> GetMachineAsync(string id);
        Task<IEnumerable<Machine>> GetMachinesAsync(string query);
    }
    public interface ICosmosDBServiceSimulation
    {
        Task AddSimulationAsync(Simulation item, string id);
        Task<IEnumerable<Simulation>> GetSimilationsAsync(string query);
    }

    public class CosmosDBServiceProduct : ICosmosDBServiceProduct
    {
        private Container _container;

        public CosmosDBServiceProduct(CosmosClient client, string databaseName, string containerName)
        {
            this._container = client.GetContainer(databaseName, containerName);
        }
        public async Task AddProductAsync(Product product)
        {
            await this._container.CreateItemAsync<Product>(product, new PartitionKey(product.id));
        }
        public async Task UpdateProductAsync(string id, Product product)
        {
            await this._container.UpsertItemAsync<Product>(product, new PartitionKey(id));
        }

        public async Task DeleteProductAsync(string id)
        {
            await this._container.DeleteItemAsync<Product>(id, new PartitionKey(id));
        }

        public async Task<Product> GetProductAsync(string id)
        {
            try
            {
                ItemResponse<Product> response = await this._container.ReadItemAsync<Product>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<Product>(new QueryDefinition(queryString));
            List<Product> results = new List<Product>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

    }
    public class CosmosDBServiceMachine : ICosmosDBServiceMachine
    {
        private Container _container;
        public CosmosDBServiceMachine(CosmosClient dbClient, string dataBaseName, string containerName)
        {
            this._container = dbClient.GetContainer(dataBaseName, containerName);
        }

        public async Task AddMachineAsync(Machine machine)
        {            
            await this._container.CreateItemAsync<Machine>(machine, new PartitionKey(machine.id));
        }
        public async Task UpdateMachineAsync(string id, Machine machine)
        {
            var aux = this.GetMachineAsync(id).Result;
            aux.description_name = machine.description_name;
            aux.condition = machine.condition;
            aux.repair_hours = machine.repair_hours;
            aux.qty_products_hour = machine.qty_products_hour;
            aux.cost_operating_hour = machine.cost_operating_hour;

            await this._container.UpsertItemAsync<Machine>(aux, new PartitionKey(id));
        }
        public async Task DeleteMachineAsync(string id)
        {
            await this._container.DeleteItemAsync<Machine>(id, new PartitionKey(id));
        }
        public async Task<Machine> GetMachineAsync(string id)
        {
            try
            {
                ItemResponse<Machine> response = await this._container.ReadItemAsync<Machine>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) 
            {
                return null;
            }
        }
        public async Task<IEnumerable<Machine>> GetMachinesAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<Machine>(new QueryDefinition(queryString));
            List<Machine> results = new List<Machine>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

    }
    public class CosmosDBServiceSimulation : ICosmosDBServiceSimulation
    {
        private Container _container;
        public CosmosDBServiceSimulation(CosmosClient dbClient, string dataBaseName, string containerName)
        {
            this._container = dbClient.GetContainer(dataBaseName, containerName);
        }

        public async Task<IEnumerable<Simulation>> GetSimilationsAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<Simulation>(new QueryDefinition(queryString));
            List<Simulation> results = new List<Simulation>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }
        public async Task AddSimulationAsync(Simulation simulation, string id)
        {
            await this._container.CreateItemAsync<Simulation>(simulation, new PartitionKey(id));
        }        
    }

}
