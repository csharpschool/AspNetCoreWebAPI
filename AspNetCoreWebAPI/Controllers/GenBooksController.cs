using AspNetCoreWebAPI.Entities;
using AspNetCoreWebAPI.Models;
using AspNetCoreWebAPI.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

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

        [HttpGet("{publisherId}/books")]
        public IActionResult Get(int publisherId)
        {
            var items = _rep.Get<Book>().Where(b =>
                b.PublisherId.Equals(publisherId));

            var DTOs = Mapper.Map<IEnumerable<BookDTO>>(items);
            return Ok(DTOs);
        }

    }
}
