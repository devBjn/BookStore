using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repositories
{
	public class CartRepository : ICartRepository
	{
		private readonly DBContext _ctx;
		private readonly UserManager<IdentityUser> _userManager;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public CartRepository(DBContext ctx, UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor)
		{
			_ctx = ctx;
			_userManager = userManager;
			_httpContextAccessor = httpContextAccessor;
		}

		public async Task<int> AddItem(int bookId, int quantity)
		{
            string userId = GetUserId();
            //entities in every table will be reflected to Database
            using var transaction = _ctx.Database.BeginTransaction();
			try
			{
				var cart = await GetCart(userId);

				if (string.IsNullOrEmpty(userId))
					throw new Exception("user is not logged-in");

				if (cart == null)
				{
					cart = new ShoppingCart
					{
						UserId = userId
					};
					_ctx.ShoppingCarts.Add(cart);
				}
				_ctx.SaveChanges();

				//Cart Item
				var cartItem = _ctx.CartDetails.FirstOrDefault(x => x.ShoppingCartId == cart.Id && x.BookId == bookId);
				if (cartItem != null)
				{
					cartItem.Quantity += quantity;
				} else
				{
					var book = _ctx.Books.Find(bookId);
					cartItem = new CartDetail
					{
						BookId = bookId,
						ShoppingCartId = cart.Id,
						Quantity = quantity,
						UnitPrice = book.BookPrice,
					};
					_ctx.CartDetails.Add(cartItem);
				}
				_ctx.SaveChanges();
				transaction.Commit();
			}
			catch (Exception ex)
			{		
			}

			var cartItemCount = await GetCartItemCount(userId);
			return cartItemCount;
		}


		public async Task<int> RemoveItem(int bookId)
		{
            string userId = GetUserId();
            try
			{		

				if (string.IsNullOrEmpty(userId))
					throw new Exception("User is not logged-in");

                var cart = await GetCart(userId);

                if (cart == null)
				{
					throw new Exception("Invalid Cart");
                }
                _ctx.SaveChanges();

				//Cart Item
				var cartItem = _ctx.CartDetails.FirstOrDefault(x => x.ShoppingCartId == cart.Id && x.BookId == bookId);

				if (cartItem == null)
					throw new Exception("Not item in cart");
                else if (cartItem.Quantity == 1)
				{
					_ctx.CartDetails.Remove(cartItem);
				}
				else
				{
					cartItem.Quantity -= 1;
				}
				_ctx.SaveChanges();

			
			}
			catch (Exception ex)
			{
			
			}
            var cartItemCount = await GetCartItemCount(userId);
            return cartItemCount;
        }


		public async Task<ShoppingCart> GetUserCart()
		{
			var userId = GetUserId();
			if (userId == null)
				throw new Exception("Invalid userId");
			var shoppingCart = await _ctx.ShoppingCarts
									.Include(a => a.CartDetails)
									.ThenInclude(a => a.Book)
									.ThenInclude(a => a.BookCategory)
									.Where(a => a.UserId == userId).FirstOrDefaultAsync();
			return shoppingCart;
		}

		public async Task<int> GetCartItemCount(string userId = "")
		{
			if (!string.IsNullOrEmpty(userId))
			{
				userId = GetUserId();
			}
			var data = await (from cart in _ctx.ShoppingCarts
							  join cartDetail in _ctx.CartDetails
							  on cart.Id equals cartDetail.ShoppingCartId
							  select new { cartDetail.Id }
							  ).ToListAsync();
			return data.Count();
		}


		public async Task<bool> DoCheckOut()
		{
			using var transaction = _ctx.Database.BeginTransaction();
			try
			{
				//Check login
				var userId = GetUserId();
				if (string.IsNullOrEmpty(userId))
					throw new Exception("User is not logged-in");

				var cart = await GetCart(userId);
				if (cart == null)
					throw new Exception("Invalid cart");

				var cartDetail = _ctx.CartDetails.Where(a => a.ShoppingCartId == cart.Id).ToList();
				if (cartDetail.Count == 0)
					throw new Exception("Cart is an empty");

				var order = new Order
				{
					UserId = userId,
					CreateData = DateTime.UtcNow,
					OrderStatusId = 1, //pending
				};
				_ctx.Orders.Add(order);
				_ctx.SaveChanges();

				foreach (var item in cartDetail)
				{
					var orderDetail = new OrderDetail
					{
						BookId = item.BookId,
						OrderId = order.Id,
						Quantity = item.Quantity,
						UnitPrice = item.UnitPrice,
					};
					_ctx.OrderDetails.Add(orderDetail);
				}
				_ctx.SaveChanges();


				// Remove cart detail
				_ctx.CartDetails.RemoveRange(cartDetail);
				_ctx.SaveChanges();
				transaction.Commit();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}


        // Check user has cart or not
        public async Task<ShoppingCart> GetCart(string userId)
		{
			var cart = await _ctx.ShoppingCarts.FirstOrDefaultAsync(x => x.UserId == userId);
			return cart;
		}


		private string GetUserId()
		{
			var principal = _httpContextAccessor.HttpContext.User;
			string userId = _userManager.GetUserId(principal);
			return userId;
		}
	}
}

