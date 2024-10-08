using API.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : ControllerBase
    {
        private readonly ECommerceDbContext _context;
        public BuggyController(ECommerceDbContext context)
        {
            _context = context;
        }
        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest()
        {
            if (_context.products.Find(105)==null) { 
                return NotFound(new APIResponse(404));
            }
            return Ok();
        }  
        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            var error = _context.products.Find(105)!.ToString();
          
            return Ok();
        }
        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new APIResponse(400));
        } 
        [HttpGet("badrequest/{id}")]
        public ActionResult GetNotFoundRequest(int id)
        {
            return Ok();
        }

    }
}
