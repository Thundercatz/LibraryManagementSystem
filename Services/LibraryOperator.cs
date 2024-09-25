using LibraryAPI.Interfaces;
using LibraryAPI.Models;

namespace LibraryAPI.Services
{
    public class LibraryOperator : ILibraryOperator
    {
        private readonly ILibraryRepository _libraryRepository;

        public LibraryOperator(ILibraryRepository libraryRepository)
        {
        _libraryRepository = libraryRepository;
        }

        public void AddBook(Book book)
        {
            _libraryRepository.AddBook(book);
        }

        public void RegisterMember(Member member)
        {
            _libraryRepository.RegisterMember(member);
        }

        public void RemoveBook(int bookId)
        {
            _libraryRepository.RemoveBook(bookId);
        }

        public void UnregisterMember(int memberId)
        {
            _libraryRepository.UnregisterMember(memberId);
        }
    }
}
