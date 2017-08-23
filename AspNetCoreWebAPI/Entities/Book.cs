using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreWebAPI.Entities
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [ForeignKey("PublisherId")]
        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }
    }
}
