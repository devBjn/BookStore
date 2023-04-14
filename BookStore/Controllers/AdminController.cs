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
    public class AdminController : Controller
    {
        private readonly IAdminRepository _adminRepository;
        private readonly DBContext _ctx;

        public AdminController(DBContext ctx, IAdminRepository adminRepository)
        {
            _ctx = ctx;
            _adminRepository = adminRepository;
        }

        public async Task<IActionResult> Dashboard()
        {
            IEnumerable<Book> books = await _adminRepository.Dashboard();
            return View(books);
        }

        public IActionResult AddBook()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(Book book)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View();
            //}
            try
            {
                bool isSuccess = await _adminRepository.AddBook(book);
                if (isSuccess)
                {
                    TempData["msg"] = "Added Successfully!!!";
                    return RedirectToAction("AddBook");
                }
            } catch (Exception ex)
            {
                TempData["msg"] = "Added failed!!!";
                return View();
            }
            return View();
        }


        public async Task<IActionResult> RemoveBook(int id)
        {
            bool isSuccess = await _adminRepository.DeleteBook(id);
            if (isSuccess)
            {
                return RedirectToAction("Dashboard");
            }
            return View();
        }

        public IActionResult EditBook(int id)
        {
            var item = _ctx.Books.Find(id);
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> EditBook(Book book)
        {
            try
            {
                bool isSuccess = await _adminRepository.UpdateBook(book);
                if (isSuccess)
                {
                    return RedirectToAction("Dashboard");
                }
            } catch (Exception ex)
            {
                TempData["msg"] = "Updated failed!!!";
                return View();
            }
            return View();
        }
    }
}

