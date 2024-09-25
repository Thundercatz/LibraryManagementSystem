using LibraryAPI.Models;

namespace LibraryAPI.Interfaces
{
    public interface ILibraryOperator
    {
        void AddBook(Book book);
        void RemoveBook(int bookId);
        void RegisterMember(Member member);
        void UnregisterMember(int memberId);

    }
}
