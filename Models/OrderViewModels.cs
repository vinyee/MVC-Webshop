using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoWithUsers.Models
{
    public class OrderViewModels
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public long OrderDate { get; set; }
        public string UserIdentity { get; set; }
        public double OrderTotalPrice { get; set; }
        public List<int> Apps { get; set; }
    }
}