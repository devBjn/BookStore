using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace BookStore.Models
{
	[Table("Order")]
	public class Order
	{
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

		[Required]
		public string UserId { get; set; }

		public DateTime CreateData { get; set; } = DateTime.UtcNow;

		[Required]
		public int OrderStatusId { get; set; }

		public bool IsDeleted { get; set; }

		public virtual OrderStatus OrderStatus { get; set; }

		public List<OrderDetail> OrderDetail { get; set; }
	}
}

