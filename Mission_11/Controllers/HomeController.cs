using Microsoft.AspNetCore.Mvc;
using Mission_11.Models;
using Mission_11.Models.ViewModels;
using System.Diagnostics;

namespace Mission_11.Controllers
{
    public class HomeController : Controller
    {
        private IBookInterface _repo;

        public HomeController(IBookInterface repo)
        {
            _repo = repo;
        }


        public IActionResult Index(string? bookType, int pageNum = 1)
        {
            int pageSize = 2;
            var blah = new BooksListViewModel
            {
                Books = _repo.Books
                    .Where(x => x.Category == bookType || bookType == null)
                    .OrderBy(x => x.Title)
                    .Skip((pageNum - 1) * pageSize)
                    .Take(pageSize),
                PaginationInfo = new PaginationInfo
                {
                    currentPage = pageNum,
                    itemsPerPage = pageSize,
                    totalItems = bookType == null ? _repo.Books.Count() : _repo.Books.Where(x => x.Category == bookType).Count()
                },
                CurrentBookType = bookType
            };

            return View(blah);
        }



    }
}
