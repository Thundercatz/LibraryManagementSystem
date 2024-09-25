using LibraryAPI.Models;
using LibraryAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Tests
{
    public class InMemoryLibraryRepositoryTests
    {
        private readonly InMemoryLibraryRepository _repository;

        public InMemoryLibraryRepositoryTests()
        {
            _repository = new InMemoryLibraryRepository();
        }

        [Fact]
        public void AddBook_ShouldAddBookToRepository()
        {
            // Arrange
            var book = new Book { BookId = 1, Title = "Test Book", AvailableCopies = 3 };

            // Act
            _repository.AddBook(book);

            // Assert
            var retrievedBook = _repository.GetBookById(1);
            Assert.NotNull(retrievedBook);
            Assert.Equal("Test Book", retrievedBook.Title);
        }

        [Fact]
        public void AddBook_DuplicateId_ShouldThrowException()
        {
            // Arrange
            var book = new Book { BookId = 1, Title = "Test Book", AvailableCopies = 3 };
            _repository.AddBook(book);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _repository.AddBook(book));
        }

        [Fact]
        public void RemoveBook_ShouldRemoveBookFromRepository()
        {
            // Arrange
            var book = new Book { BookId = 1, Title = "Test Book", AvailableCopies = 3 };
            _repository.AddBook(book);

            // Act
            _repository.RemoveBook(1);

            // Assert
            var retrievedBook = _repository.GetBookById(1);
            Assert.Null(retrievedBook);
        }

        [Fact]
        public void RemoveBook_NonExistentId_ShouldThrowException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => _repository.RemoveBook(999));
        }

        [Fact]
        public void RegisterMember_ShouldAddMemberToRepository()
        {
            // Arrange
            var member = new Member { MemberId = 1, Name = "John Doe" };

            // Act
            _repository.RegisterMember(member);

            // Assert
            var retrievedMember = _repository.GetMemberById(1);
            Assert.NotNull(retrievedMember);
            Assert.Equal("John Doe", retrievedMember.Name);
        }

        [Fact]
        public void RegisterMember_DuplicateId_ShouldThrowException()
        {
            // Arrange
            var member = new Member { MemberId = 1, Name = "John Doe" };
            _repository.RegisterMember(member);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _repository.RegisterMember(member));
        }

        [Fact]
        public void UnregisterMember_ShouldRemoveMemberFromRepository()
        {
            // Arrange
            var member = new Member { MemberId = 1, Name = "John Doe" };
            _repository.RegisterMember(member);

            // Act
            _repository.UnregisterMember(1);

            // Assert
            var retrievedMember = _repository.GetMemberById(1);
            Assert.Null(retrievedMember);
        }

        [Fact]
        public void UnregisterMember_NonExistentId_ShouldThrowException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => _repository.UnregisterMember(999));
        }
    }
}
