namespace Mission_11.Models
{
    public interface IBookInterface
    {
        public IQueryable<Book> Books { get; }
    }
}
