using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaDelivery.ApiModels
{
    public class InputOrderDto
    {
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required, RegularExpression(@"^([a-zA-Z]\d[a-zA-Z])\ {0,1}(\d[a-zA-Z]\d)$",
         ErrorMessage = "Characters are not allowed.")]
        public string PostalCode { get; set; }
        [RegularExpression(@"^(\+\d{1,2}\s)?\(?\d{3}\)?\d{7}$",
          ErrorMessage = "Phone is invalid.")]
        public string Phone { get; set; }
        [Required, RegularExpression(@"^\d{10,}$",
          ErrorMessage = "Cardnumber is invalid.")]
        public string Cardnumber { get; set; }
        public string NoteForShipper { get; set; }
        [Required]
        public List<InputOrderDetailDto> OrderDetails { get; set; }
    }
}
