using System;
using System.ComponentModel.DataAnnotations;

namespace PizzaDelivery.Models
{
    public class OrderDetail
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int PizzaId { get; set; }
        public Pizza Pizza { get; set; }

        public int Quantity { get; set; }
        [StringLength(100)]
        public string AdditionalRequirement { get; set; }
    }
}
