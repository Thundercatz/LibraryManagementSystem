using LibraryAPI.Interfaces;
using LibraryAPI.Models;

namespace LibraryAPI.Repositories
{
    public class InMemoryLibraryRepository : ILibraryRepository
    {

        private readonly List<Book> _books = new();
        private readonly List<Member> _members = new();
        public IReadOnlyCollection<Book> GetAllBooks() => _books.AsReadOnly();

        public IReadOnlyCollection<Member> GetAllMembers() => _members.AsReadOnly();



        public void AddBook(Book book)
        {
            if (_books.Any(b => b.BookId == book.BookId))
                throw new ArgumentException("Book with the same Id Already exists");
            _books.Add(book);
        }

        public void RemoveBook(int bookId)
        {
            var book = GetBookById(bookId) ?? throw new ArgumentException("Book not found");
            _books.Remove(book);
        }



        public Book? GetBookById(int bookId) => _books.FirstOrDefault(b => b.BookId == bookId);

        public Member? GetMemberById(int memberId) => _members.FirstOrDefault(m => m.MemberId == memberId);



        public void RegisterMember(Member member)
        {
            if (_members.Any(m => m.MemberId == member.MemberId))
                throw new ArgumentException("Member already exists");
            _members.Add(member);

        }

        

        public void UnregisterMember(int memberId)
        {
            var member = GetMemberById(memberId) ?? throw new ArgumentException("Member doenst exist");
            _members.Remove(member);
        }

        public void UpdateBook(Book updatedBook)
        {
            var book = GetBookById(updatedBook.BookId) ?? throw new ArgumentException("Book not found");
            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.Price = updatedBook.Price;
            book.AvailableCopies = updatedBook.AvailableCopies;

        }

        public void UpdateMember(Member updateMember)
        {
            var member = GetMemberById(updateMember.MemberId) ?? throw new ArgumentException("Member not found");
            member.Name = updateMember.Name;
            member.BorrowedBooks = updateMember.BorrowedBooks;
        }
    }
}
