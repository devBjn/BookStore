using System;
using Microsoft.EntityFrameworkCore;
namespace BookStore.Repositories
{
	public class BookRepository : IBookRepository
	{
		private readonly DBContext _ctx;

		public BookRepository(DBContext ctx)
		{
			_ctx = ctx;	
		}


        public async Task<IEnumerable<BookCategory>> Categories()
        {
            return await _ctx.BookCategories.ToListAsync();
        }

        public async Task<IEnumerable<Book>> DisplayBook(string searchTerm = "", int categoryId = 0)
		{
			searchTerm = searchTerm.ToLower();
			IEnumerable<Book> books = await (from book in _ctx.Books
						 join bookCategory in _ctx.BookCategories
						 on book.CategoryId equals bookCategory.CategoryId
						 where string.IsNullOrWhiteSpace(searchTerm) || (book != null && book.BookName.ToLower().StartsWith(searchTerm))
						 select new Book
						 {
							 Id = book.Id,
							 BookImage = book.BookImage,
							 BookName = book.BookName,
							 Author = book.Author,
							 BookPrice = book.BookPrice,
							 BookDescription = book.BookDescription,
							 CategoryId = book.CategoryId,
							 CategoryName = bookCategory.CategoryName
						 }
						 ).ToListAsync();

			if(categoryId > 0)
			{
				books = books.Where(book => book.CategoryId == categoryId).ToList();
			}

			return books;
		}

        public async Task<Book> BookDetail(int id)
        {
            Book bookDetail = await (from book in _ctx.Books
                                     join bookCategory in _ctx.BookCategories
                                     on book.CategoryId equals bookCategory.CategoryId
                                     where book.Id == id
                                     select new Book
                                     {
                                         Id = book.Id,
                                         BookImage = book.BookImage,
                                         BookName = book.BookName,
                                         Author = book.Author,
                                         BookPrice = book.BookPrice,
                                         BookDescription = book.BookDescription,
                                         CategoryId = book.CategoryId,
                                         CategoryName = bookCategory.CategoryName
                                     }
                            ).FirstOrDefaultAsync();
            return bookDetail;
        }
    }
}

