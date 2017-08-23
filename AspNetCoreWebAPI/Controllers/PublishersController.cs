using AspNetCoreWebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreWebAPI.Controllers
{
    public class PublishersController : Controller
    {
        IBookstoreRepository _rep;
        public PublishersController(IBookstoreRepository rep)
        {
            _rep = rep;
        }

    }
}
