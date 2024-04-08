namespace Mission_11.Models
{
    public class Cart
    {
        public List<CartLine> Lines { get; set; } = new List<CartLine>();

        public virtual void AddItem(Book b, int quantity)
        {
            CartLine? line = Lines
                .Where(x => x.Book.BookId == b.BookId).FirstOrDefault();
            if (line == null)
            {
                Lines.Add(new CartLine
                {
                    Book = b,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public virtual void RemoveLine(Book b) => Lines.RemoveAll(x => x.Book.BookId == b.BookId);

        public virtual void Clear() => Lines.Clear();
        public decimal CalculateTotal()
        {
            var total = Lines.Sum(x => x.Book.Price * x.Quantity);

            return (decimal)total;
        }
            public class CartLine
        {
            public int CartLineId { get; set; } 
            public Book Book { get; set; }
            public int Quantity { get; set; }

        }


    }
}
