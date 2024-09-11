using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Common.Configuration
{
    public class ProviderConfiguration
    {
        public required string ConnectionString { get; set; }
        public int CommandTimeOut { get; set; } = 30;
    }
}
