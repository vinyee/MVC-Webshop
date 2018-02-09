using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DemoWithUsers.Models
{
    public class Carts
    {
        [Key]
        public int UserId { get; set; } //Vem vagnen tillhör :D
        public List<Product> Apps { get; set; } //Antal appar du har i din vagn
    }
}