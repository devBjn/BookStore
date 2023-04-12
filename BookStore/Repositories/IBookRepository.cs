using System;
namespace BookStore.Repositories
{
    //abstraction layer
	public interface IBookRepository
	{
        Task<IEnumerable<Book>> DisplayBook(string searchTerm = "", int categoryId = 0);
        Task<IEnumerable<BookCategory>> Categories();
        Task<Book> BookDetail(int id);
    }
}

