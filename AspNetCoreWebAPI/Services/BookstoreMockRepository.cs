using System.Collections.Generic;
using System.Linq;
using AspNetCoreWebAPI.Models;
using AspNetCoreWebAPI.Data;
using System;

namespace AspNetCoreWebAPI.Services
{
    public class BookstoreMockRepository : IBookstoreRepository
    {
        public IEnumerable<PublisherDTO> GetPublishers()
        {
            return MockData.Current.Publishers;
        }

        public PublisherDTO GetPublisher(int publisherId, bool includeBooks = false)
        {
            var publisher = MockData.Current.Publishers.FirstOrDefault(p =>
                p.Id.Equals(publisherId));

            if (includeBooks && publisher != null)
            {
                publisher.Books = MockData.Current.Books.Where(b =>
                    b.PublisherId.Equals(publisherId)).ToList();
            }

            return publisher;
        }

        public void AddPublisher(PublisherDTO publisher)
        {
            // For Demo purposes only: Get next id
            var id = GetPublishers().Max(m => m.Id) + 1;
            publisher.Id = id;
            MockData.Current.Publishers.Add(publisher);
        }

        public bool Save()
        {
            return true;
        }

        public void UpdatePublisher(int id, PublisherUpdateDTO publisher)
        {
            var publisherToUpdate = GetPublisher(id);
            publisherToUpdate.Name = publisher.Name;
            publisherToUpdate.Established = publisher.Established;
        }

        public bool PublisherExists(int publisherId)
        {
            return MockData.Current.Publishers.Count(p => p.Id.Equals(publisherId)).Equals(1);
        }
    }
}
