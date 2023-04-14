using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace BookStore.Repositories
{
	public class AdminRepository : IAdminRepository
	{
		private readonly DBContext _ctx;

		public AdminRepository(DBContext ctx)
		{
			_ctx = ctx;
		}

		public async Task<IEnumerable<Book>> Dashboard()
		{
			IEnumerable<Book> books = await (from book in _ctx.Books
											 join bookCategory in _ctx.BookCategories
											 on book.CategoryId equals bookCategory.CategoryId
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
											 }).ToListAsync();
			return books;
		}

		public async Task<bool> AddBook(Book book)
		{
			Book newBook = new Book
			{
				Id = book.Id,
				BookImage = book.BookImage,
				BookName = book.BookName,
				Author = book.Author,
				BookPrice = book.BookPrice,
				BookDescription = book.BookDescription,
				CategoryId = book.CategoryId,
			};
			_ctx.Books.Add(newBook);
			_ctx.SaveChanges();
			return true;
		}

		public async Task<bool> UpdateBook(Book book)
		{
            Book item = new Book
            {
                Id = book.Id,
                BookImage = book.BookImage,
                BookName = book.BookName,
                Author = book.Author,
                BookPrice = book.BookPrice,
                BookDescription = book.BookDescription,
                CategoryId = book.CategoryId,
            };
			_ctx.Books.Update(item);
			_ctx.SaveChanges();
            return true;
        }

		public async Task<bool> DeleteBook(int id)
		{
			var item = _ctx.Books.Find(id);
			if(item != null)
			{
				_ctx.Books.Remove(item);
				_ctx.SaveChanges();
				return true;
			}

			return false;
		}
	}
}

