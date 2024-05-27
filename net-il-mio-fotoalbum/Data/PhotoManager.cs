using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Data
{
    public class PhotoManager
    {
        public static List<Photo> GetAllPhotos()
        {
            using PhotoContext db = new PhotoContext();
            return db.Photos.ToList();
        }
        public static List<Category> GetAllCategories()
        {
            using PhotoContext db = new PhotoContext();
            return db.Categories.ToList();
        }
        public static void IsertPhoto(Photo photo, List<string> SelectedCategories = null)
        {
            using PhotoContext db = new PhotoContext();
            if(SelectedCategories != null)
            {
                photo.Categories = new List<Category>();
                foreach(string categoryId in SelectedCategories)
                {
                    int id = int.Parse(categoryId);
                    var category = db.Categories.FirstOrDefault(c=>c.CategoryId == id);
                    photo.Categories.Add(category);
                }
            }
            db.Photos.Add(photo);
            db.SaveChanges();
        }
    }
}
