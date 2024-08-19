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
        [Required(ErrorMessage = "This field is required.")]
        public string SenderName { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        [EmailAddress]
        public string SenderEmail { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string Message { get; set; }
    }
}
