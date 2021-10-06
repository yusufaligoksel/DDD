using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Management.CrossCuttingConcerns.Logging.ElasticSearch.Abstract;
using Management.Domain.Log;
using Management.Domain.Settings;
using Microsoft.Extensions.Options;
using Nest;

namespace Management.CrossCuttingConcerns.Logging.ElasticSearch.Concrete
{
    public class ElasticSearchLogService : ILogService
    {
        private readonly IElasticClient _client;
        private readonly ElasticSearchOption _elasticSearchOption;

        public ElasticSearchLogService(IElasticClient client,
            IOptions<ElasticSearchOption> elasticSearchOption)
        {
            _client = client;
            _elasticSearchOption = elasticSearchOption.Value;
        }

        public async Task<IEnumerable<LogModel>> GetLogsAsync()
        {
            var logs = await _client.SearchAsync<LogModel>(q => q.Index(_elasticSearchOption.Index)); //.Scroll("5m")
            return logs.Documents.ToList();
        }

        public async Task<LogModel> Find(string id)
        {
            var result = await _client.GetAsync<LogModel>(id, q => q.Index(_elasticSearchOption.Index));
            return result.Source;
        }

        public async Task<IEnumerable<LogModel>> GetLogsByLevelId(int levelId)
        {
            var query = QueryOperations.CreateQuery(levelId, _elasticSearchOption.Index);
            var result = await _client.SearchAsync<LogModel>(query);
            return result.Documents.ToList();
        }

        public async Task DeleteAsync(LogModel logModel)
        {
            var result = await _client.DeleteAsync<LogModel>(logModel);
        }

        public async Task InsertLog(LogModel logModel)
        {
            var result = await _client.IndexDocumentAsync<LogModel>(logModel);
        }

        public async Task InsertLogRange(LogModel[] logModels)
        {
            var result = await _client.IndexManyAsync(logModels);
        }

        public async Task UpdateLog(LogModel logModel)
        {
            var result = await _client.UpdateAsync<LogModel>(logModel, u => u.Doc(logModel));
        }

        public async Task SaveBulkAsync(LogModel[] logModels)
        {
            var result = await _client.BulkAsync(b => b.Index(_elasticSearchOption.Index).IndexMany(logModels));
        }
    }
}