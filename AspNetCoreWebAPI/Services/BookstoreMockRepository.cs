using System.Collections.Generic;
using AspNetCoreWebAPI.Models;
using AspNetCoreWebAPI.Data;

namespace AspNetCoreWebAPI.Services
{
    public class BookstoreMockRepository : IBookstoreRepository
    {
        public IEnumerable<PublisherDTO> GetPublishers()
        {
            return MockData.Current.Publishers;
        }
    }
}
