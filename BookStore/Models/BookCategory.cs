using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BookStore.Models
{
	[Table("BookCategory")]
	public class BookCategory
	{
		[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }

		[Required]
		public string CategoryName { get; set; }

		// one category can have multiple books
		public List<Book> Books { get; set; }
    }
}

