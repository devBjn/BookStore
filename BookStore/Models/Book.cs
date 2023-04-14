using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
namespace BookStore.Models
{
	[Table("Book")]
  
    public class Book
	{
		[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

		[Required]
		public string BookName { get; set; }

		[Required]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter a valid price")]
        public double BookPrice { get; set; }

        [Required]
        public string BookDescription { get; set; }

		[Required]
		public string Author { get; set; }

		[Required]
        [DataType(DataType.ImageUrl)]
        [RegularExpression(@"(http(s?):)([/|.|\w|\s|-])*\.(?:jpg|gif|png)",
ErrorMessage = "Please enter a valid image URL (JPG, GIF, or PNG)")]
        public string BookImage { get; set; }

		public int? CategoryId { get; set; }
		[ForeignKey("CategoryId")]

		public virtual BookCategory BookCategory { get; set; }

		//The relationship
		public List<CartDetail> CartDetail { get; set; }
		public List<OrderDetail> OrderDetail { get; set; }


		[NotMapped]
		public string CategoryName { get; set; }
    }
}

