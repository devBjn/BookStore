using System;
namespace BookStore.Models.DTOs
{
	public class BookDisplayModel
	{
        
            public IEnumerable<Book> Books { get; set; }
            public IEnumerable<BookCategory> BookCategories { get; set; }
            public string SearchTerm { get; set; } = "";
            public int CategoryId { get; set; } = 0;

    }
}

