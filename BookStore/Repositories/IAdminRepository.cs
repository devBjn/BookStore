using System;
namespace BookStore.Repositories
{
	public interface IAdminRepository
	{
        Task<IEnumerable<Book>> Dashboard();
        Task<bool> AddBook(Book book);
        Task<bool> UpdateBook(Book book);
        Task<bool> DeleteBook(int id);
    }
}

