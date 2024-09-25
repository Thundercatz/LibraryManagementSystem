using LibraryAPI.Interfaces;
using LibraryAPI.Models;
using LibraryAPI.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Tests
{
    public class LibraryOperatorTests
    {
        private readonly Mock<ILibraryRepository> _mockRepo;
        private readonly LibraryOperator _operator;

        public LibraryOperatorTests()
        {
            _mockRepo = new Mock<ILibraryRepository>();
            _operator = new LibraryOperator(_mockRepo.Object);
        }

        [Fact]
        public void AddBook_ShouldCallRepositoryAddBook()
        {
            // Arrange
            var book = new Book { BookId = 1, Title = "New Book", AvailableCopies = 5 };

            // Act
            _operator.AddBook(book);

            // Assert
            _mockRepo.Verify(r => r.AddBook(book), Times.Once);
        }

        [Fact]
        public void RemoveBook_ShouldCallRepositoryRemoveBook()
        {
            // Arrange
            int bookId = 1;

            // Act
            _operator.RemoveBook(bookId);

            // Assert
            _mockRepo.Verify(r => r.RemoveBook(bookId), Times.Once);
        }

        [Fact]
        public void RegisterMember_ShouldCallRepositoryRegisterMember()
        {
            // Arrange
            var member = new Member { MemberId = 1, Name = "Jane Doe" };

            // Act
            _operator.RegisterMember(member);

            // Assert
            _mockRepo.Verify(r => r.RegisterMember(member), Times.Once);
        }

        [Fact]
        public void UnregisterMember_ShouldCallRepositoryUnregisterMember()
        {
            // Arrange
            int memberId = 1;

            // Act
            _operator.UnregisterMember(memberId);

            // Assert
            _mockRepo.Verify(r => r.UnregisterMember(memberId), Times.Once);
        }
    }
}
