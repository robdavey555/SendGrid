using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SendGrid.Service.Models
{
    public class SendGridSettings
    {
        public string Key { get; set; }
        public string FromAddress { get; set; }
        public string FromName { get; set; }
    }
}
