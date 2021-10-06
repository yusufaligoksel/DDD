using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Management.Domain.Log;

namespace Management.CrossCuttingConcerns.Logging.ElasticSearch.Abstract
{
    public interface ILogService
    {
        Task<IEnumerable<LogModel>> GetLogsAsync();
        Task<LogModel> Find(string id);
        Task<IEnumerable<LogModel>> GetLogsByLevelId(int levelId);
        Task DeleteAsync(LogModel logModel);
        Task InsertLog(LogModel logModel);
        Task InsertLogRange(LogModel[] logModels);
        Task UpdateLog(LogModel logModel);
        Task SaveBulkAsync(LogModel[] logModels);
    }
}