using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaDelivery.Models
{
    public class Order : BaseEntity
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        [Required]
        public string CustomerName { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required, StringLength(50)]
        public string Address { get; set; }
        [Required, StringLength(7)]
        public string PostalCode { get; set; }
        [Required, StringLength(18)]
        public string Phone { get; set; }
        [Required, StringLength(20)]
        public string Cardnumber { get; set; }
        [StringLength(100)]
        public string NoteForShipper { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
