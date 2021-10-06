using Management.Domain.Log;
using Nest;

namespace Management.CrossCuttingConcerns.Logging.ElasticSearch.Concrete
{
    public static class QueryOperations
    {
        public static SearchRequest<LogModel> CreateQuery(int levelId,string index)
        {
            var sr = new SearchRequest<LogModel>(index)
            {
                TrackScores = true
            };
            
            QueryContainer queryContainer = null;
            queryContainer&= new BoolQuery { Must = new QueryContainer[] { new TermQuery { Field = "LogLevelId", Value = levelId, Boost = 0.0 } } };
            sr.Query = queryContainer;
            
            return sr;
        }
        
    }
}