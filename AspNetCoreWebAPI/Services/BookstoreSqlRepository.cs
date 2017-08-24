using AspNetCoreWebAPI.Entities;
using AspNetCoreWebAPI.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreWebAPI.Services
{
    public class BookstoreSqlRepository : IBookstoreRepository
    {
        private SqlDbContext _db;

        public BookstoreSqlRepository(SqlDbContext db)
        {
            _db = db;
        }

        public void AddBook(BookDTO book)
        {
            var bookToAdd = Mapper.Map<Book>(book);
            _db.Books.Add(bookToAdd);
        }

        public void AddPublisher(PublisherDTO publisher)
        {
            var publisherToAdd = Mapper.Map<Publisher>(publisher);
            _db.Publishers.Add(publisherToAdd);
        }

        public void UpdateBook(int publisherId, int bookId, BookUpdateDTO book)
        {
            var bookToUpdate = _db.Books.FirstOrDefault(b =>
                b.Id.Equals(bookId) && b.PublisherId.Equals(publisherId));

            if (bookToUpdate == null) return;

            bookToUpdate.Title = book.Title;
            bookToUpdate.PublisherId = book.PublisherId;
        }

        public void DeleteBook(BookDTO book)
        {
            var bookToDelete = _db.Books.FirstOrDefault(b =>
                b.Id.Equals(book.Id) &&
                b.PublisherId.Equals(book.PublisherId));

            if (bookToDelete != null) _db.Books.Remove(bookToDelete);
        }

        public void UpdatePublisher(int id, PublisherUpdateDTO publisher)
        {
            var publisherToUpdate = _db.Publishers.FirstOrDefault(p =>
                p.Id.Equals(id));

            if (publisherToUpdate == null) return;

            publisherToUpdate.Name = publisher.Name;
            publisherToUpdate.Established = publisher.Established;
        }

        public void DeletePublisher(PublisherDTO publisher)
        {
            var publisherToDelete = _db.Publishers.FirstOrDefault(p =>
                p.Id.Equals(publisher.Id));

            if (publisherToDelete != null)
                _db.Publishers.Remove(publisherToDelete);
        }

        public BookDTO GetBook(int publisherId, int bookId)
        {
            var book = _db.Books.FirstOrDefault(b => b.Id.Equals(bookId)
                && b.PublisherId.Equals(publisherId));

            var bookDTO = Mapper.Map<BookDTO>(book);

            return bookDTO;
        }

        public PublisherDTO GetPublisher(int publisherId, bool includeBooks = false)
        {
            var publisher = _db.Publishers.FirstOrDefault(p =>
                p.Id.Equals(publisherId));

            if (includeBooks && publisher != null)
            {
                _db.Entry(publisher).Collection(c => c.Books).Load();
            }

            var publisherDTO = Mapper.Map<PublisherDTO>(publisher);

            return publisherDTO;
        }

        public IEnumerable<BookDTO> GetBooks(int publisherId)
        {
            var books = _db.Books.Where(b =>
                b.PublisherId.Equals(publisherId));
            var bookDTOs = Mapper.Map<IEnumerable<BookDTO>>(books);

            return bookDTOs;
        }

        public IEnumerable<PublisherDTO> GetPublishers()
        {
            return Mapper.Map<IEnumerable<PublisherDTO>>(_db.Publishers);
        }

        public bool PublisherExists(int publisherId)
        {
            return _db.Publishers.Count(p => p.Id.Equals(publisherId)) == 1;
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0;
        }

    }
}
