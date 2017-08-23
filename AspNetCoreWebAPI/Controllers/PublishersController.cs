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

    }
}
