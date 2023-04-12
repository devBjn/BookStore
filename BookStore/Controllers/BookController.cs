using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookStore.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly DBContext _ctx;
        private readonly IBookRepository _bookRepository;

        public BookController(DBContext ctx, IBookRepository bookRepository)
        {
            _ctx = ctx;
            _bookRepository = bookRepository;
        }

        public async Task<IActionResult> DisplayBooks(string searchTerm = "", int categoryId = 0)
        {
            IEnumerable<Book> books = await _bookRepository.DisplayBook(searchTerm, categoryId);
            IEnumerable<BookCategory> category = await _bookRepository.Categories();
            BookDisplayModel bookModel = new BookDisplayModel
            {
                Books = books,
                BookCategories = category,
                SearchTerm = searchTerm,
                CategoryId = categoryId,
            };

            return View(bookModel);
        }

 
        public async Task<IActionResult> DetailBook(int id)
        {
            Book bookDetail = await _bookRepository.BookDetail(id);
            ViewBag.BookDetail = bookDetail;
            return View(bookDetail);
        }
    }
}

