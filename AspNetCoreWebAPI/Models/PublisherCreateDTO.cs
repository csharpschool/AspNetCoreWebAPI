using System.ComponentModel.DataAnnotations;

namespace AspNetCoreWebAPI.Models
{
    public class PublisherCreateDTO
    {
        [Required(ErrorMessage = "You must enter a name.")]
        [MaxLength(50)]
        public string Name { get; set; }
        public int Established { get; set; }
    }
}
