using AspNetCoreWebAPI.Entities;
using AspNetCoreWebAPI.Models;
using AspNetCoreWebAPI.Services;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
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

        [HttpGet("{publisherId}/books/{id}", Name = "GetGenericBook")]
        public IActionResult Get(int publisherId, int id, bool includeRelatedEntities = false)
        {
            var item = _rep.Get<Book>(id, includeRelatedEntities);

            if (item == null || !item.PublisherId.Equals(publisherId))
                return NotFound();

            var DTO = Mapper.Map<BookDTO>(item);
            return Ok(DTO);
        }

        [HttpPost("{publisherId}/books")]
        public IActionResult Post(int publisherId, [FromBody]BookDTO DTO)
        {
            if (DTO == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var itemToCreate = Mapper.Map<Book>(DTO);
            itemToCreate.PublisherId = publisherId;
            _rep.Add(itemToCreate);

            if (!_rep.Save()) return StatusCode(500,
                "A problem occurred while handling your request.");

            var createdDTO = Mapper.Map<BookDTO>(itemToCreate);

            return CreatedAtRoute("GetGenericBook",
                new { id = createdDTO.Id }, createdDTO);
        }

        [HttpPut("{publisherId}/books/{id}")]
        public IActionResult Put(int publisherId, int id, [FromBody]BookUpdateDTO DTO)
        {
            if (DTO == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var entity = _rep.Get<Book>(id);
            if (entity == null) return NotFound();

            Mapper.Map(DTO, entity);

            if (!_rep.Save()) return StatusCode(500,
                "A problem happened while handling your request.");

            return NoContent();
        }

        [HttpPatch("{publisherId}/books/{id}")]
        public IActionResult Patch(int publisherId, int id, [FromBody]JsonPatchDocument<BookUpdateDTO> DTO)
        {
            if (DTO == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var entity = _rep.Get<Book>(id);
            if (entity == null) return NotFound();

            var entityToPatch = Mapper.Map<BookUpdateDTO>(entity);
            DTO.ApplyTo(entityToPatch, ModelState);
            TryValidateModel(entityToPatch);
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Mapper.Map(entityToPatch, entity);

            if (!_rep.Save()) return StatusCode(500,
                "A problem happened while handling your request.");

            return NoContent();
        }

    }
}
