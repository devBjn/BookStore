using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace BookStore.Models
{
	[Table("ShoppingCart")]
	public class ShoppingCart
	{
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

		[Required]
		public string UserId { get; set; }

		public bool IsDeleted { get; set; } = false;


		public ICollection<CartDetail> CartDetails { get; set; }
	}
}

