using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using net_il_mio_fotoalbum.Data;

namespace net_il_mio_fotoalbum.Models
{
    public class PhotoFormModel
    {
        public Photo Photo { get; set; }
        public List<SelectListItem>? Categories { get; set; } // To show categories to select
        public List<string>? SelectedCategories { get; set; }
        public IFormFile? ImageFormFile { get; set; }
        public PhotoFormModel() { }

        public PhotoFormModel(Photo photo) 
        {
            this.Photo = photo;
            this.SelectedCategories = this.Photo.Categories.Select(c => c.CategoryId.ToString()).ToList();
        }

        public void CreateCategories()
        {
            this.Categories = new List<SelectListItem>();
            if (this.SelectedCategories == null) 
                this.SelectedCategories = new List<string>();
            var categoriesFromDb = PhotoManager.GetAllCategories();
            foreach (var category in categoriesFromDb)
            {
                bool isSelected = this.SelectedCategories.Contains(category.CategoryId.ToString());
                this.Categories.Add(new SelectListItem()
                {
                    Text = category.Name,
                    Value = category.CategoryId.ToString(),
                    Selected = isSelected
                });
            }
        }

        // function to convert Img to bytes array
        public byte[] SetImageFileFromFormFile()
        {
            if (ImageFormFile == null)
                return null;

            using var stream = new MemoryStream();
            this.ImageFormFile?.CopyTo(stream);
            Photo.PhotoFile = stream.ToArray();

            return Photo.PhotoFile;
        }
    }
}
