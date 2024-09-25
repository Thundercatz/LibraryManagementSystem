using LibraryAPI.Interfaces;
using LibraryAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    public class MembersController : ControllerBase
    {
        private readonly ILibraryOperator _operator;
        private readonly ILibraryClient _client;

        public MembersController(ILibraryOperator libraryOperator, ILibraryClient libraryClient)
        {
            _operator = libraryOperator;
            _client = libraryClient;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Member>> GetMembers()
        {
            // Assuming you have a method to get all members in the repository
            // This would require adding a method in ILibraryClient or ILibraryRepository
            return Ok(); // Placeholder
        }

        [HttpPost]
        public IActionResult RegisterMember([FromBody] Member member)
        {
            _operator.RegisterMember(member);
            return CreatedAtAction(nameof(GetMembers), new { id = member.MemberId }, member);
        }

        [HttpDelete("{id}")]
        public IActionResult UnregisterMember(int id)
        {
            _operator.UnregisterMember(id);
            return NoContent();
        }

        // Additional endpoints like Borrow and Return can be added here
    }
}
