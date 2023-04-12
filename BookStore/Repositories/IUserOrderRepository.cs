using System;
namespace BookStore.Repositories
{
	public interface IUserOrderRepository
	{
        Task<IEnumerable<Order>> UserOrders();

    }
}

