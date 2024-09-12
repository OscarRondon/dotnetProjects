using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OARCapiRestToolsShared
{
    public class EmailConfiguration
    {
        public string smtpServer {  get; set; }
        public int smtpPort { get; set; }
        public string smtpUser { get; set; }
        public string smtpPassword { get; set; }
    }
}
