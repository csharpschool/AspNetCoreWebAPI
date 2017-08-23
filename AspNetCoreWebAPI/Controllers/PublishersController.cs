using AspNetCoreWebAPI.Models;
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

        [HttpPost]
        public IActionResult Post([FromBody] PublisherCreateDTO publisher)
        {
            if (publisher == null) return BadRequest();

            if (publisher.Established < 1534)
                ModelState.AddModelError("Established", "The first publishing house was founded in 1534.");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var publisherToAdd = new PublisherDTO
            {
                Established = publisher.Established,
                Name = publisher.Name
            };

            _rep.AddPublisher(publisherToAdd);
            _rep.Save();

            return CreatedAtRoute("GetPublisher", new { id = publisherToAdd.Id }, publisherToAdd);
        }

    }
}
