using AspNetCoreWebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreWebAPI.Controllers
{
    [Route("api/genpublishers")]
    public class GenBooksController : Controller
    {
        IGenericEFRepository _rep;

        public GenBooksController(IGenericEFRepository rep)
        {
            _rep = rep;
        }
    }

}
