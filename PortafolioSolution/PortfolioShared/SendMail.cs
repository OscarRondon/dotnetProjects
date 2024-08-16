using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioShared
{
    public class SendMail
    {
        [Required]
        public string SenderName { get; set; }
        [Required]
        [EmailAddress]
        public string SenderEmail { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
