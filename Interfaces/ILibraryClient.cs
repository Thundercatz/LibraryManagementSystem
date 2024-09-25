using LibraryAPI.Models;

namespace LibraryAPI.Interfaces
{
    public interface ILibraryClient
    {
        IReadOnlyCollection<Book> AvailableBooks { get; }
        IReadOnlyCollection<Book> BorrowedBooks(Member member);

        void BorrowBook(int BookId, Member member);
        void ReturnBook(int Bookid, Member member);
    }
}
