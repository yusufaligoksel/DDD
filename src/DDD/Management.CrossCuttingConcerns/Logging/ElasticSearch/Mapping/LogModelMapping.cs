﻿using Management.Domain.Log;
using Nest;

namespace Management.CrossCuttingConcerns.Logging.ElasticSearch.Mapping
{
    public static class LogModelMapping
    {
        public static CreateIndexDescriptor LogMapping(this CreateIndexDescriptor descriptor)
        {
            return descriptor.Map<LogModel>(m => m.Properties(p => p
                    .Keyword(k => k.Name(n => n.ShortMessage))
                    .Text(t => t.Name(n => n.ShortMessage))
                    .Text(t => t.Name(n => n.FullMessage))
                )
            );
        }
    }
}