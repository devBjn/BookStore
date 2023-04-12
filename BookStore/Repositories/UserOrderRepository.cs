using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace BookStore.Repositories
{
	public class UserOrderRepository : IUserOrderRepository
	{
		private readonly DBContext _ctx;
        private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly UserManager<IdentityUser> _userManager;

        public UserOrderRepository(DBContext ctx, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager)
		{
			_ctx = ctx;
			_httpContextAccessor = httpContextAccessor;
			_userManager = userManager;
		}

		public async Task<IEnumerable<Order>> UserOrders()
		{
			var userId = GetUserId();
			if (string.IsNullOrEmpty(userId))
				throw new Exception("User is not logged-in");

			var orders = await _ctx.Orders
                            .Include(x => x.OrderStatus)
                            .Include(x => x.OrderDetail)
							.ThenInclude(x => x.Book)
							.ThenInclude(x => x.BookCategory)
							.Where(a => a.UserId == userId)
							.ToListAsync();

			return orders;
		}

        private string GetUserId()
        {
            var principal = _httpContextAccessor.HttpContext.User;
            string userId = _userManager.GetUserId(principal);
            return userId;
        }
    }
}

