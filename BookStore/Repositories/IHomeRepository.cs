using System;
namespace BookStore.Repositories
{
	public interface IHomeRepository
	{
        Task<IEnumerable<Book>> DisplayBook();
    }
}

