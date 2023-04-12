using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStore.Controllers
{
    public class UserOrderController : Controller
    {

        private readonly IUserOrderRepository _userOrderRepository;

        public UserOrderController(IUserOrderRepository userOrderRepository)
        {
            _userOrderRepository = userOrderRepository;
        }


        public async Task<IActionResult> UserOrders()
        {
            var orders = await _userOrderRepository.UserOrders();
            return View(orders);
        }
    }
}

