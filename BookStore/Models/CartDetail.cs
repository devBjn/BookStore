using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace BookStore.Models
{
	[Table("CartDetail")]
	public class CartDetail
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required]
		public int ShoppingCartId { get; set; }

		[Required]
		public int BookId { get; set; }

		[Required]
		public int Quantity { get; set; }

		public double UnitPrice { get; set; }

		public virtual ShoppingCart ShoppingCart { get; set; }
		public virtual Book Book { get; set; }
    }
}

