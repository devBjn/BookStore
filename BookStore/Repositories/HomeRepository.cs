using System;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repositories
{
	public class HomeRepository : IHomeRepository
	{
        private readonly DBContext _ctx;
        public HomeRepository(DBContext ctx)
		{
			_ctx = ctx;
		}

		public async Task<IEnumerable<Book>> DisplayBook()
		{
			IEnumerable<Book> books = await (from book in _ctx.Books
											 join bookCategory in _ctx.BookCategories
											 on book.CategoryId equals			bookCategory.CategoryId
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
			return books;
		}
    }
}

