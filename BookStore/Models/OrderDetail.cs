using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace BookStore.Models
{
	[Table("OrderDetail")]
	public class OrderDetail
	{
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
		public int BookId { get; set; }

		public int Quantity { get; set; }

		public double UnitPrice { get; set; }

		public virtual Order Order { get; set; }
		public virtual Book Book { get; set; }
    }
}

