﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseSource.Model
{
    [Table("ORDER_DETAILS")]
    public class OrderDetail
    {
        public OrderDetail()
        {
            Id = Guid.NewGuid().ToString();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID", TypeName = "varchar"), MaxLength(36)]
        public string Id { get; set; }

        [Column("QUANTITY")]
        public int Quantity { get; set; } = 1;

        [Column("PRICE")]
        public int Price { get; set; } = 0;

        [Column("PRODUCT_ID", TypeName = "varchar"), MaxLength(36)]
        [ForeignKey("Product")]
        public string ProductId { get; set; }

        public Product Product { get; set; } = null!;

        [Column("ORDER_ID", TypeName = "varchar"), MaxLength(36)]
        [ForeignKey("Order")]
        public string OrderId { get; set; }

        public Order Order { get; set; } = null!;
    }
}
