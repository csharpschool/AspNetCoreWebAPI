using AspNetCoreWebAPI.Models;
using AspNetCoreWebAPI.Services;
using Microsoft.AspNetCore.JsonPatch;
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

        [HttpGet("{publisherId}/books/{id}", Name = "GetBook")]
        public IActionResult Get(int publisherId, int id)
        {
            var book = _rep.GetBook(publisherId, id);

            if (book == null) return NotFound();

            return Ok(book);
        }

        [HttpPost("{publisherId}/books")]
        public IActionResult Post(int publisherId, [FromBody]BookCreateDTO book)
        {
            if (book == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var publisherExists = _rep.PublisherExists(publisherId);
            if (!publisherExists) return NotFound();

            var bookToAdd = new BookDTO
            {
                PublisherId = publisherId,
                Title = book.Title
            };

            _rep.AddBook(bookToAdd);
            _rep.Save();

            return CreatedAtRoute("GetBook", new
            {
                publisherId = publisherId,
                id = bookToAdd.Id
            }, bookToAdd);
        }

        [HttpPut("{publisherId}/books/{id}")]
        public IActionResult Put(int publisherId, int id, [FromBody]BookUpdateDTO book)
        {
            if (book == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var bookToUpdate = _rep.GetBook(publisherId, id);
            if (bookToUpdate == null) return NotFound();

            _rep.UpdateBook(publisherId, id, book);
            _rep.Save();

            return NoContent();
        }

        [HttpPatch("{publisherId}/books/{id}")]
        public IActionResult Patch(int publisherId, int id, [FromBody]JsonPatchDocument<BookUpdateDTO> book)
        {
            if (book == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var bookToUpdate = _rep.GetBook(publisherId, id);
            if (bookToUpdate == null) return NotFound();

            var bookToPatch =
                new BookUpdateDTO()
                {
                    PublisherId = bookToUpdate.PublisherId,
                    Title = bookToUpdate.Title
                };

            book.ApplyTo(bookToPatch, ModelState);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            _rep.UpdateBook(publisherId, id, bookToPatch);
            _rep.Save();

            return NoContent();
        }

        [HttpDelete("{publisherId}/books/{id}")]
        public IActionResult Delete(int publisherId, int id)
        {
            var book = _rep.GetBook(publisherId, id);

            if (book == null) return NotFound();

            _rep.DeleteBook(book);
            _rep.Save();

            return NoContent();
        }

    }
}
