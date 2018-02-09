using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DemoWithUsers.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public int OrderId { get; set; }

        public string UserIdentity { get; set; }
        public double OrderTotalPrice { get; set; }
    }
}