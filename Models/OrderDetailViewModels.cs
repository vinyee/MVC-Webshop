using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DemoWithUsers.Models
{
    public class OrderDetailViewModels
    {
        //så vi kan få med totalprice :D

        public int Id { get; set; }

        [Display(Name = "Ordernummer")]
        public int OrderId { get; set; }

        [Display(Name = "Namn")]
        public string Firstname { get; set; }

        [Display(Name = "Efternamn")]
        public string Lastname { get; set; }

        [Display(Name = "Adress")]
        public string Address { get; set; }

        [Display(Name = "Postkod")]
        public int PostalCode { get; set; }

        [Display(Name = "Ort")]
        public string City { get; set; }

        [Display(Name = "Mobilnummer")]
        public int PhoneNumber { get; set; }

        [Display(Name = "Totalt pris")]
        public double TotalPrice { get; set; }

        [Display(Name = "Konto")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "App")]
        public List<Product> Apps { get; set; }
    }
}