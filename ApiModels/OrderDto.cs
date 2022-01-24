using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaDelivery.ApiModels
{
    public class OrderDto
    {
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public string NoteForShipper { get; set; }
        public IEnumerable<OrderDetailDto> OrderDetails { get; set; }
    }
}
