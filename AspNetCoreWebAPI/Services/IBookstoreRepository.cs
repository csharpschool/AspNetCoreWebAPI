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
        bool Save();
        bool PublisherExists(int publisherId);

        void DeleteBook(BookDTO book);
    }
}
