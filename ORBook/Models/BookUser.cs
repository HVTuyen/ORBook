namespace ORBook.Models
{
    public class BookUser
    {
        public int BookId { get; set; }
        public Book Book { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
