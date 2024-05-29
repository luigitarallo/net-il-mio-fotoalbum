using System.ComponentModel.DataAnnotations;

namespace net_il_mio_fotoalbum.Models
{
    public class Category
    {
        [Key] public int CategoryId { get; set; }
        [Required(ErrorMessage ="Category name is mandatory")]
        [StringLength(20, ErrorMessage = "Category name has to contain max 20 letters")]
        [MinWords(1, ErrorMessage = "Description has to contain at least 1 word.")]
        public string Name { get; set; }

        public List<Photo> Photos { get; set; }

        public Category() { }
    }
}
