using Microsoft.EntityFrameworkCore;
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
        public static void InsertPhoto(Photo photo, List<string> SelectedCategories = null)
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

        public static Photo GetPhotoById(int id, bool includeReferences = true) 
        {
            using PhotoContext db = new PhotoContext();
            if(includeReferences)
            {
                return db.Photos
                    .Where(p => p.PhotoId == id)
                    .Include(p => p.Categories)
                    .FirstOrDefault();
            }
            return db.Photos.FirstOrDefault(p => p.PhotoId == id);
        }

        public static void InsertCategory(Category category)
        {
            using PhotoContext db = new PhotoContext();
            db.Categories.Add(category);
            db.SaveChanges();
        }

        public static bool DeleteCategory(int id)
        {
            using PhotoContext db = new PhotoContext();
            Category category = db.Categories.FirstOrDefault(c=> c.CategoryId == id);
            if (category == null)
                return false;
            db.Categories.Remove(category);
            db.SaveChanges();
            return true;
        }

        public static bool EditPhoto(int id, Action<Photo, List<Category>> edit, List<string> categories)
        {
            using PhotoContext db = new PhotoContext();
            Photo photo = db.Photos
                .Include(p => p.Categories)
                .FirstOrDefault(p => p.PhotoId == id);

            if (photo == null)
                return false;
            List<Category> categoriesFromDb = new List<Category>();
            if(categories != null)
            {
                foreach (var category in categories)
                {
                    int categoryId = int.Parse(category);
                    var categoryFromDb = db.Categories.FirstOrDefault(c=>c.CategoryId == categoryId);
                    categoriesFromDb.Add(categoryFromDb);
                }
            }
            edit(photo, categoriesFromDb);
            db.SaveChanges();
            return true;
        }

        public static bool EditPhoto(int id, string title, string description, bool isVisible, List<string> categories)
        {
            using PhotoContext db = new PhotoContext();
            var photo = db.Photos.Where(p=> p.PhotoId== id).Include(p=> p.Categories).FirstOrDefault();
            if (photo == null) 
                return false;
            photo.Title = title;
            photo.Description = description;
            photo.IsVisible = isVisible;
            photo.Categories.Clear();
            if(categories != null)
            {
                foreach (var category in categories)
                {
                    int categoryId = int.Parse(category);
                    var categoryFromDb = db.Categories.FirstOrDefault(c=> c.CategoryId == categoryId);
                    photo.Categories.Add(categoryFromDb);
                }
            }
            db.SaveChanges();
            return true;
        }

        public static bool DeletePhoto(int id)
        {
            using PhotoContext db = new PhotoContext();
            Photo post = db.Photos.FirstOrDefault(p=> p.PhotoId == id);
            if (post == null)
                return false;
            db.Photos.Remove(post);
            db.SaveChanges();
            return true;
        }

        // api methods

        public static List<Photo> GetAllPublicPhotos(string? name = null)
        {
            using PhotoContext db = new PhotoContext();
            if (name != null)
            {
                string searchName = name.ToLower();
                return db.Photos.Where(p => p.Title.ToLower().Contains(searchName) && p.IsVisible).ToList();
            }
            else
            {
                return db.Photos.Where(p => p.IsVisible == true).ToList();
            }
        }

        public static void InsertMessage(Message message)
        {
            using PhotoContext db = new PhotoContext();
            db.Messages.Add(message);
            db.SaveChanges();
        }
    }
}
