using Microsoft.AspNetCore.Mvc;
using CarpoolService.Models;
using CarpoolService.Data;
using System.Linq;

namespace CarpoolService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarpoolController : ControllerBase
    {
        private readonly CarpoolContext _context;

        public CarpoolController(CarpoolContext context)
        {
            _context = context;
        }

        // GET: api/carpool/search
        [HttpGet("search")]
        public IActionResult Search(string origin, string destination)
        {
            var carpoolUsers = _context.CarpoolUsers
                .Where(u => u.Origin == origin && u.Destination == destination)
                .ToList();

            return Ok(carpoolUsers);
        }

        // POST: api/carpool/add
        [HttpPost("add")]
        public IActionResult Add(CarpoolUser user)
        {
            _context.CarpoolUsers.Add(user);
            _context.SaveChanges();

            return Ok();
        }

        // DELETE: api/carpool/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _context.CarpoolUsers.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.CarpoolUsers.Remove(user);
            _context.SaveChanges();

            return Ok();
        }
    }
}
