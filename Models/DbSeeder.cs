using LibraryAPI.Interfaces;

namespace LibraryAPI.Models
{
    public class DbSeeder : IDbSeeder
    {
        private readonly ILibraryRepository _repository;

        public DbSeeder(ILibraryRepository repository)
        {
            _repository = repository;
        }

        public void Seed()
        {
            // Check if data already exists to prevent duplicate seeding
            if (!_repository.GetAllBooks().Any() && !_repository.GetAllMembers().Any())
            {
                // Seed books
                var books = new List<Book>
            {
                new Book { BookId = 1, Title = "C# in Depth", Author = "Jon Skeet", Price = 45.99m, AvailableCopies = 5 },
                new Book { BookId = 2, Title = "Clean Code", Author = "Robert C. Martin", Price = 40.00m, AvailableCopies = 0 },
                new Book { BookId = 3, Title = "Design Patterns", Author = "Erich Gamma", Price = 50.00m, AvailableCopies = 3 }
            };

                foreach (var book in books)
                {
                    _repository.AddBook(book);
                }

                // Seed members
                var members = new List<Member>
            {
                new Member { MemberId = 1, Name = "Alice Johnson", BorrowedBooks = new List<Book>() },
                new Member { MemberId = 2, Name = "Bob Smith", BorrowedBooks = new List<Book>() },
                new Member { MemberId = 3, Name = "Charlie Brown", BorrowedBooks = new List<Book>() }
            };

                foreach (var member in members)
                {
                    _repository.RegisterMember(member);
                }
            }
        }
    }
}
