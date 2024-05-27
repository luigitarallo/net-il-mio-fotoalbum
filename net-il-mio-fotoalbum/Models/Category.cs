using System.ComponentModel.DataAnnotations;

namespace net_il_mio_fotoalbum.Models
{
    public class Category
    {
        [Key] public int CategoryId { get; set; }
        [Required] public string Name { get; set; }

        public List<Photo> Photos { get; set; }

        public Category() { }
    }
}
