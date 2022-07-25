using System;
using System.Collections.Generic;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using ML_Appointments.Models;

namespace ML_Appointments.Services
{
    public class MtoServices : IMtoServices
    {
        private Container _container;
        private static readonly JsonSerializer Serializer = new JsonSerializer();
        public MtoServices(CosmosClient dbClient, string databaseName, string containerName)
        {
            this._container = dbClient.GetContainer(databaseName, containerName);
        }
        public async Task AddCompanyDataAsync(Mto mto)
        {
            // company.id= Guid.NewGuid().ToString();
            await this._container.CreateItemAsync<Mto>(mto, new PartitionKey(mto.M_Id));
        }
        public async Task DeleteAsync(string id)
        {
            await this._container.DeleteItemAsync<Mto>(id, new PartitionKey(id));
        }

        public async Task<Mto> GetAsync(string id)
        {
            try
            {
                ItemResponse<Mto> response = await this._container.ReadItemAsync<Mto>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

        }

        public async Task<IList<Mto>> GetAsyncQuery(string userName, string password)
        {
            var queryString = $@"SELECT *FROM c WHERE c.UserName ='{userName}' and c.Password='{password}'";
            var query = this._container.GetItemQueryIterator<Mto>(new QueryDefinition(queryString));
            List<Mto> results = new List<Mto>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task UpdateAsync(string id, Mto item)
        {
            await this._container.UpsertItemAsync<Mto>(item, new PartitionKey(id));
        }

        ////PostCompanySlots
        //public async Task AddCompanySlotsDataAsync(Companys company)
        //{
        //    await this._container.CreateItemAsync<Companys>(company, new PartitionKey(company.Id));
        //}

        //public async Task<IList<Company>> GetAsyncQuery(string queryString)
        //{
        //    var query = this._container.GetItemQueryIterator<Company>(new QueryDefinition(queryString));
        //    List<Company> results = new List<Company>();
        //    while (query.HasMoreResults)
        //    {
        //        var response = await query.ReadNextAsync();

        //        results.AddRange(response.ToList());
        //    }

        //    return results;
        //}


















    }

}

