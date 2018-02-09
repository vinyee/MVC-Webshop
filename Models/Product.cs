using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DemoWithUsers.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Namn")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Pris")]
        public int Price { get; set; }

        [Required]
        [Display(Name = "Bild")]
        public string ImageUrl { get; set; }

        [Required]
        [Display(Name = "Beskrivning")]
        [MaxLength(35, ErrorMessage = "Beskrivning kan max innehålla {0} tecken!")]
        public string Desc { get; set; }
    }
}