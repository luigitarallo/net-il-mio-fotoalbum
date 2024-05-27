using System.ComponentModel.DataAnnotations;

namespace net_il_mio_fotoalbum.Models
{
    public class Photo
    {
        [Key] public int PhotoId { get; set; }
        [Required] public string Title { get; set; }
        [Required] public string Description { get; set; }
        [Required] public byte[] PhotoFile { get; set; }
        public bool IsVisible { get; set; }
        public string PhotoSrc => $"data:image/jpeg;base64,{Convert.ToBase64String(PhotoFile)}";
        public Photo() { }

        public Photo(string title, string description, byte[] photoFile, bool isVisible) 
        {
            Title = title;
            Description = description;
            PhotoFile = photoFile;
            IsVisible = isVisible;
        }
    }
}
