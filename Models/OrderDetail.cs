using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DemoWithUsers.Models
{
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Ordernummer")]
        public int OrderId { get; set; }

        [Required]
        [Display(Name = "Namn")]
        public string Firstname { get; set; }

        [Required]
        [Display(Name = "Efternamn")]
        public string Lastname { get; set; }

        [Required]
        [Display(Name = "Adress")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Postkod")]
        public int PostalCode { get; set; }

        [Required]
        [Display(Name = "Ort")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Mobilnummer")]
        public int PhoneNumber { get; set; }
    }
}