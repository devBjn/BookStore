using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStore.Controllers
{
    [Authorize]
    public class CartController : Controller
    {

        private readonly ICartRepository _cartRepository;


        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<IActionResult> AddItem(int bookId, int quantity = 1, int redirect = 0)
        {
            var cartCount = await _cartRepository.AddItem(bookId, quantity);
            if (redirect == 0)
                return Ok(cartCount);
            return RedirectToAction("GetUserCart");
        }

        public async Task<IActionResult> RemoveItem(int bookId)
        {
            var cartCount = await _cartRepository.RemoveItem(bookId);
            return RedirectToAction("GetUserCart");
        }

        public async Task<IActionResult> GetUserCart()
        {
            var cart = await _cartRepository.GetUserCart();

            return View(cart);
        }

        public async Task<IActionResult> GetTotalItemInCart()
        {
            int count = await _cartRepository.GetCartItemCount();
            return Ok(count);
        }

        public async Task<IActionResult> DoCheckOut()
        {
            bool isCheckedOut = await _cartRepository.DoCheckOut();
            if (!isCheckedOut)
                throw new Exception("Something is happened");
            return RedirectToAction("Index", "Home");
        }
    }
}

