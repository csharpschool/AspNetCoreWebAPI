using AspNetCoreWebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreWebAPI.Controllers
{
    [Route("api/publishers")]
    public class BooksController : Controller
    {
        IBookstoreRepository _rep;

        public BooksController(IBookstoreRepository rep)
        {
            _rep = rep;
        }

        [HttpGet("{publisherId}/books")]
        public IActionResult Get(int publisherId)
        {
            var books = _rep.GetBooks(publisherId);

            return Ok(books);
        }

    }
}
