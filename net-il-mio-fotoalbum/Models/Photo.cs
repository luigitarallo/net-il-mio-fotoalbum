using System.ComponentModel.DataAnnotations;

namespace net_il_mio_fotoalbum.Models
{
    public class Photo
    {
        [Key] public int PhotoId { get; set; }
        [Required(ErrorMessage = "The photo title is mandatory")]
        [MinWords(1, ErrorMessage = "The photo title has to contain at least 1 word.")]
        [StringLength(30,ErrorMessage = "Title has to contain max 30 letters")]
        public string Title { get; set; }

        [Required (ErrorMessage = "The photo description is mandatory")] 
        [MinWords(4, ErrorMessage = "Description has to contain at least 4 words.")]
        public string Description { get; set; }
        public byte[]? PhotoFile { get; set; }
        public bool IsVisible { get; set; }
        public string? PhotoSrc => PhotoFile != null ? $"data:image/png;base64,{Convert.ToBase64String(PhotoFile)}" : "";
        public List<Category>? Categories { get; set; }
        public Photo() { }

        public Photo(string title, string description, bool isVisible = false)
        {
            Title = title;
            Description = description;
            IsVisible = isVisible;
        }
    }
}
