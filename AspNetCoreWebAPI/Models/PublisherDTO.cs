using System.Collections.Generic;

namespace AspNetCoreWebAPI.Models
{
    public class PublisherDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Established { get; set; }

        public int BookCount { get { return Books.Count; } }

        public ICollection<BookDTO> Books { get; set; } = new List<BookDTO>();
    }
}
