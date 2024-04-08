using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Mission_11.Infra;
using Mission_11.Models;

namespace Mission_11.Pages
{
    public class BuyModel : PageModel
    {
        private IBookInterface _repo;
        public Cart Cart { get; set; }
        public BuyModel(IBookInterface temp, Cart cartService) 
        {
            _repo = temp;
            Cart = cartService;
        }
        
        public string ReturnUrl { get; set; } = "/";

        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";            

        }

        public IActionResult OnPost(int bookId, string returnUrl) 
        { 
            Book book = _repo.Books
                .FirstOrDefault(x => x.BookId == bookId);
            
            if (book != null) 
            {
               
                Cart.AddItem(book, 1);

            }

           return RedirectToPage (new { returnUrl = returnUrl});
        }

        public IActionResult OnPostRemove(int bookId, string returnUrl)
        { 
            Cart.RemoveLine(Cart.Lines.First(x => x.Book.BookId == bookId).Book);
            return RedirectToPage (new {returnUrl = returnUrl});
        }
    }
}
