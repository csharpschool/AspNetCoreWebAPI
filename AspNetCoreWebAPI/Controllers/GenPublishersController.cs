using AspNetCoreWebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreWebAPI.Controllers
{
    [Route("api/genpublishers")]
    public class GenPublishersController : Controller
    {
        IGenericEFRepository _rep;

        public GenPublishersController(IGenericEFRepository rep)
        {
            _rep = rep;
        }

    }
}
