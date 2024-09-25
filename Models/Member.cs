namespace LibraryAPI.Models
{
    public class Member
    {
        public int MemberId { get; set; }
        public string? Name { get; set; }
        public ICollection<Book>? BorrowedBooks { get; set; } = new List<Book>();
    }
}
