using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaDelivery.Models
{
    public class Pizza : BaseEntity
    {
        public Pizza()
        {
        }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public double Price { get; set; }
    }
}
