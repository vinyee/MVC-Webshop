using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DemoWithUsers.Models
{
    public class Wish
    {
        [Key]
        public int Id { get; set; }

        public string UserIdentity { get; set; }
        public int AppId { get; set; }
    }
}