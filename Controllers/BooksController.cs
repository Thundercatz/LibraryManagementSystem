using LibraryAPI.Interfaces;
using LibraryAPI.Models;
using LibraryAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly ILibraryOperator _operator;
        private readonly ILibraryClient _client;
        private readonly ILibraryRepository _repository;

        public BooksController(ILibraryOperator libraryOperator, ILibraryClient libraryClient, ILibraryRepository repository)
        {
            _operator = libraryOperator;
            _client = libraryClient;
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetAvailableBooks()
        {
            return Ok(_client.AvailableBooks);
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] Book book)
        {
            _operator.AddBook(book);
            return CreatedAtAction(nameof(GetAvailableBooks), new { id = book.BookId }, book);
        }

        [HttpDelete("{id}")]
        public IActionResult RemoveBook(int id)
        {
            _operator.RemoveBook(id);
            return NoContent();
        }

        [HttpPost("{bookId}/borrow")]
        public IActionResult BorrowBook(int bookId, [FromBody] BorrowRequest request)
        {
            if (request == null)
                return BadRequest("Borrow request data is null.");

            try
            {
                // Assuming you have a method to get a member by ID
                var member = _repository.GetMemberById(request.MemberId);
                var book = _repository.GetBookById(bookId);
                if (member == null)
                    return NotFound($"Member with ID {request.MemberId} does not exist.");

                _client.BorrowBook(bookId, member);
                return Ok($"Book with ID {bookId} and title {book?.Title} has been borrowed by Member ID {request.MemberId} and name {member.Name}.");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Books/{bookId}/return
        [HttpPost("{bookId}/return")]
        public IActionResult ReturnBook(int bookId, [FromBody] ReturnRequest request)
        {
            if (request == null)
                return BadRequest("Return request data is null.");

            try
            {
                // Assuming you have a method to get a member by ID
                var member = _repository.GetMemberById(request.MemberId);
                if (member == null)
                    return NotFound($"Member with ID {request.MemberId} does not exist.");

                _client.ReturnBook(bookId, member);
                return Ok($"Book with ID {bookId} has been returned by Member ID {request.MemberId}.");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }


        // GET: api/Books/search?name={bookName}
        [HttpGet("search")]
        public ActionResult<IEnumerable<Book>> SearchBooksByName([FromQuery] string name)
        {

            if (string.IsNullOrEmpty(name))
                return BadRequest("Search parameter 'name' is required.");

            var books = _repository.GetAllBooks();

            

            var matchedBooks = _client.AvailableBooks
                .Where(b => b.Title.Contains(name, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (matchedBooks.Count < 0)
                return NotFound($"No books found with name '{name}'.");

            return Ok(matchedBooks);

        }

        
    }
}
