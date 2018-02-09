using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DemoWithUsers.Models
{
    public class Sessions
    {
        [Key]
        public string UserEmail { get; set; }
        public int TotalPrice { get; set; }
        public Dictionary<int, Product> Apps { get; set; } 
    }
}