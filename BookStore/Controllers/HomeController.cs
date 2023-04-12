using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BookStore.Models;
using BookStore.Data;

namespace BookStore.Controllers;

public class HomeController : Controller
{
    //private readonly ILogger<HomeController> _logger;

    //public HomeController(ILogger<HomeController> logger)
    //{
    //    _logger = logger;
    //}

    private readonly IHomeRepository _homeRepository;
    private readonly DBContext _ctx;

    public HomeController(DBContext ctx,IHomeRepository homeRepository )
    {
        _ctx = ctx;
        _homeRepository = homeRepository;
    }

    public async Task<IActionResult> Index()
    {
        IEnumerable<Book> books = await _homeRepository.DisplayBook();
        return View(books);
    }

  


}

