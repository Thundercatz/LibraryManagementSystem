using LibraryAPI.Models;

namespace LibraryAPI.Interfaces
{
    public interface ILibraryRepository
    {
        IReadOnlyCollection<Book> GetAllBooks();
        Book? GetBookById(int bookId);
        void AddBook(Book book);
        void RemoveBook(int bookId);
        void UpdateBook(Book book);

        IReadOnlyCollection<Member> GetAllMembers();
        Member? GetMemberById(int memberId);
        void RegisterMember(Member member);
        void UnregisterMember(int memberId);
        void UpdateMember(Member member);
    }
}
