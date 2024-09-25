using LibraryAPI.Interfaces;
using LibraryAPI.Services;
using LibraryAPI.Models;
using Moq;

namespace LibraryManagementSystem.Tests
{
    public class LibraryClientTests
    {

        private readonly Mock<ILibraryRepository> _libraryRepositoryMock;
        private readonly LibraryClient _libraryClient;

        public LibraryClientTests()
        {
            _libraryRepositoryMock = new Mock<ILibraryRepository>();
            _libraryClient = new LibraryClient(_libraryRepositoryMock.Object);
        }

        [Fact]
        public void AvailableBooks_ShouldReturnOnlyBooksWithAvailableCopies()
        {

            //Arrange
            var books = new List<Book>
            {
            new Book {BookId = 1, Title = "Book One", AvailableCopies = 2},
            new Book {BookId = 2, Title = "Book Two", AvailableCopies = 0},
            new Book {BookId = 3, Title = "Book Three", AvailableCopies = 5},
            new Book {BookId = 4, Title = "Book Four", AvailableCopies = 5}
            };



            _libraryRepositoryMock.Setup(r => r.GetAllBooks()).Returns(books);

            //Act
            var availableBooks = _libraryClient.AvailableBooks;

            // Assert
            Assert.Equal(3, availableBooks.Count);
            Assert.DoesNotContain(availableBooks, b => b.BookId == 2);
        }
        
        //[MethodUnderTest]_[Condition]_[ExpectedResult]
        [Fact]
        public void BorrowBook_ShouldDecreaseAvailableCopies_AndAddBookToMember()
        {
            // Arrange
            var book = new Book {BookId = 1,Author = "John Doe", Title = "Book One", AvailableCopies= 3};
            var member = new Member { MemberId = 1, Name = "John Doe" };

            _libraryRepositoryMock.Setup(r => r.GetBookById(1)).Returns(book);
            _libraryRepositoryMock.Setup(r => r.UpdateBook(It.IsAny<Book>()));
            _libraryRepositoryMock.Setup(r => r.UpdateMember(It.IsAny<Member>()));

            // Act
            _libraryClient.BorrowBook(1, member);

            //Assert
            Assert.Equal(2,book.AvailableCopies);
            Assert.Contains(member.BorrowedBooks, b=> b.BookId == 1);
            _libraryRepositoryMock.Verify(r => r.UpdateBook(book),Times.Once);
            _libraryRepositoryMock.Verify(r => r.UpdateMember(member),Times.Once);


        }

        [Fact]
        public void BorrowBook_NoAvailableCopies_ShouldThrowException()
        {
            // Arrange
            var book = new Book { BookId = 1, Title = "Book One", AvailableCopies = 0 };
            var member = new Member { MemberId = 1, Name = "John Doe" };
            _libraryRepositoryMock.Setup(r => r.GetBookById(1)).Returns(book);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _libraryClient.BorrowBook(1, member));
        }

        [Fact]
        public void ReturnBook_ShouldIncreaseAvailableCopies_AndRemoveBookFromMember()
        {
            // Arrange
            var book = new Book { BookId = 1, Title = "Book One", AvailableCopies = 2 };
            var member = new Member { MemberId = 1, Name = "John Doe", BorrowedBooks = new List<Book> { book } };
            _libraryRepositoryMock.Setup(r => r.UpdateBook(It.IsAny<Book>()));
            _libraryRepositoryMock.Setup(r => r.UpdateMember(It.IsAny<Member>()));

            // Act
            _libraryClient.ReturnBook(1, member);

            // Assert
            Assert.Equal(3, book.AvailableCopies);
            Assert.DoesNotContain(member.BorrowedBooks, b => b.BookId == 1);
            _libraryRepositoryMock.Verify(r => r.UpdateBook(book), Times.Once);
            _libraryRepositoryMock.Verify(r => r.UpdateMember(member), Times.Once);
        }

        [Fact]
        public void ReturnBook_NotBorrowedByMember_ShouldThrowException()
        {
            // Arrange
            var member = new Member { MemberId = 1, Name = "John Doe", BorrowedBooks = new List<Book>() };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _libraryClient.ReturnBook(1, member));
        }
    }
}