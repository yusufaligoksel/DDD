using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Domain.Settings
{
    public class RedisConnectionSetting
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public int Database { get; set; }
    }
}
