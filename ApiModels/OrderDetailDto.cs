using System;
using System.ComponentModel.DataAnnotations;

namespace PizzaDelivery.ApiModels
{
    public class OrderDetailDto
    {
        public string PizzaName { get; set; }
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string AdditionalRequirement { get; set; }
    }
}
