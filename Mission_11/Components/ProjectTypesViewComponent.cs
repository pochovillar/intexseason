using Microsoft.AspNetCore.Mvc;
using Mission_11.Models;

namespace Mission_11.Components
{
    public class ProjectTypesViewComponent : ViewComponent
    {
        private IBookInterface _bookRepo;
        public ProjectTypesViewComponent(IBookInterface temp) 
        { 
            _bookRepo = temp;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedBookType = RouteData?.Values["bookType"];

            var bookTypes = _bookRepo.Books
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x);

            return View(bookTypes);
        }
    }
}
