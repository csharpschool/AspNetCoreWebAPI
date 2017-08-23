using AspNetCoreWebAPI.Models;
using System.Collections.Generic;

namespace AspNetCoreWebAPI.Services
{
    public interface IBookstoreRepository
    {
        IEnumerable<PublisherDTO> GetPublishers();
        PublisherDTO GetPublisher(int publisherId, bool includeBooks = false);
        void AddPublisher(PublisherDTO publisher);
        void UpdatePublisher(int id, PublisherUpdateDTO publisher);
        void DeletePublisher(PublisherDTO publisher);
        bool Save();
        bool PublisherExists(int publisherId);

        void DeleteBook(BookDTO book);
        IEnumerable<BookDTO> GetBooks(int publisherId);
        BookDTO GetBook(int publisherId, int bookId);
        void AddBook(BookDTO book);
        void UpdateBook(int publisherId, int bookId, BookUpdateDTO book);
    }
}
