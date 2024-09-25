using LibraryAPI.Interfaces;
using LibraryAPI.Models;

namespace LibraryAPI.Services
{
    public class LibraryClient : ILibraryClient
    {

        private readonly ILibraryRepository _libraryRepository;

        public LibraryClient(ILibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }

        public IReadOnlyCollection<Book> AvailableBooks => _libraryRepository.GetAllBooks()
            .Where(b => b.AvailableCopies > 0)
            .ToList()
            .AsReadOnly();

        public void BorrowBook(int BookId, Member member)
        {
            var book = _libraryRepository.GetBookById(BookId) ?? throw new ArgumentException("Book not found");
            if (book.AvailableCopies <= 0)
                throw new ArgumentException("No available copies");

            book.AvailableCopies--;
            _libraryRepository.UpdateBook(book);

            member.BorrowedBooks?.Add(book);
            _libraryRepository.UpdateMember(member);
        }

        public IReadOnlyCollection<Book> BorrowedBooks(Member member)
        {
            return member.BorrowedBooks.ToList().AsReadOnly();
        }

        public void ReturnBook(int Bookid, Member member)
        {
            var book = member.BorrowedBooks.FirstOrDefault(b => b.BookId == Bookid);
            //var book = member.BorrowedBooks.FirstOrDefault(b => b.BookId == Bookid) ?? throw new ArgumentException("Book not borrowed by member");


            if (book == null)
                throw new ArgumentException("Book not borrowed by member");

            member.BorrowedBooks.Remove(book);
            book.AvailableCopies++;

            _libraryRepository.UpdateBook(book);
            _libraryRepository.UpdateMember(member);
        }
    }
}
