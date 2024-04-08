
namespace Mission_11.Models
{
    public class EFBookRepository : IBookInterface
    {
        private BookstoreContext _context;

        public EFBookRepository(BookstoreContext context)
        {
            _context = context;
        }
        public IQueryable<Book> Books => _context.Books;
    }
}
