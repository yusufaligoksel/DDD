using System;
using System.Threading.Tasks;
using Management.Domain.Log;
using Management.CrossCuttingConcerns.Logging.ElasticSearch.Mapping;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace Management.CrossCuttingConcerns.Logging.ElasticSearch.Extensions
{
    public static class ElasticSearchExtensions
    {
        public async static Task AddElasticsearch(
            this IServiceCollection services, IConfiguration configuration)
        {
            var url = configuration["ElasticSearch:Url"];
            var defaultIndex = configuration["ElasticSearch:Index"];

            var settings = new ConnectionSettings(new Uri(url))
                .DefaultIndex(defaultIndex);

            //AddDefaultMappings(settings);

            var client = new ElasticClient(settings);

            services.AddSingleton<IElasticClient>(client);

            await CreateIndex(client, defaultIndex);
        }
        
        // create index by name
        public static async Task CreateIndex(IElasticClient client, string indexName)
        {
            var any = await client.Indices.ExistsAsync(indexName);
            if (any.Exists)
                return;
            
            var createIndexResponse = await client.Indices.CreateAsync(indexName,
                ci => ci
                    .Index(indexName)
                    .LogMapping()
                    .Settings(s => s.NumberOfShards(3).NumberOfReplicas(1))
            );
        }
        
        //default mapping
        private static void AddDefaultMappings(ConnectionSettings settings)
        {
            settings
                .DefaultMappingFor<LogModel>(m => m
                    //.Ignore(p => p.Price)
                    //.Ignore(p => p.Quantity)
                    //.Ignore(p => p.Rating)
                );
        }
    }
}