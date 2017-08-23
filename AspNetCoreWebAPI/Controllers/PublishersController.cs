using AspNetCoreWebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreWebAPI.Controllers
{
    [Route("api/publishers")]
    public class PublishersController : Controller
    {
        IBookstoreRepository _rep;
        public PublishersController(IBookstoreRepository rep)
        {
            _rep = rep;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_rep.GetPublishers());
        }

        [HttpGet("{id}", Name = "GetPublisher")]
        public IActionResult Get(int id, bool includeBooks = false)
        {
            var publisher = _rep.GetPublisher(id, includeBooks);

            if (publisher == null) return NotFound();

            return Ok(publisher);
        }

    }
}
