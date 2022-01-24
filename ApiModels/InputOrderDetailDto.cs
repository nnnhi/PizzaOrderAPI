using System;
using System.ComponentModel.DataAnnotations;

namespace PizzaDelivery.ApiModels
{
    public class InputOrderDetailDto
    {
        [Required]
        public int PizzaId { get; set; }
        [Range(1, 1000)]
        public int Quantity { get; set; }
        [StringLength(100)]
        public string AdditionalRequirement { get; set; }
    }
}
