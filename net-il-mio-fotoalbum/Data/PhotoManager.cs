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
        public static void InsertPhoto(Photo photo, List<string> selectedCategories)
        {
            using PhotoContext db = new PhotoContext();
            photo.Categories = new List<Category>();
            if(selectedCategories != null)
            {
                foreach(string category in selectedCategories)
                {
                    int id = int.Parse(category);
                    
                    var categoryFromDb = db.Categories.FirstOrDefault(c=>c.CategoryId == id);
                    if(categoryFromDb != null)
                    {
                        photo.Categories.Add(categoryFromDb);
                    }
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

        //public static bool EditPhoto(int id, Photo photo, List<string> selectedCategories)
        //{
        //    using PhotoContext db = new PhotoContext();
        //    Photo photoToEdit = db.Photos.Where(p => p.PhotoId == id).Include(p => p.Categories).FirstOrDefault();
        //    if (photoToEdit == null)
        //        return false;
        //    photoToEdit.Title = photo.Title;
        //    photoToEdit.Description = photo.Description;
        //    photoToEdit.IsVisible = photo.IsVisible;
        //    photoToEdit.Categories.Clear();
        //    if(selectedCategories != null)
        //    {
        //        foreach (var category in selectedCategories)
        //        {
        //            int categoryId = int.Parse(category);
        //            var categoryFromDb = db.Categories.FirstOrDefault(c=>c.CategoryId == categoryId);
        //            if(categoryFromDb == null)
        //                photoToEdit.Categories.Add(categoryFromDb);
        //        }
        //    }
        //    db.SaveChanges();
        //    return true;
        //}

        public static bool EditPhoto(int id, Photo photo, List<string> selectedCategories)
        {
            using PhotoContext db = new PhotoContext();
            Photo photoToEdit = db.Photos.Where(p => p.PhotoId == id).Include(p => p.Categories).FirstOrDefault();
            if (photoToEdit == null)
                return false;
            photoToEdit.Title = photo.Title;
            photoToEdit.Description = photo.Description;
            photoToEdit.IsVisible = photo.IsVisible;
            photoToEdit.Categories.Clear();
            if (selectedCategories != null)
            {
                foreach (var category in selectedCategories)
                {
                    int categoryId = int.Parse(category);
                    var categoryFromDb = db.Categories.FirstOrDefault(c => c.CategoryId == categoryId);
                    if (categoryFromDb != null)
                        photoToEdit.Categories.Add(categoryFromDb);
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
                return db.Photos.Where(p => p.Title.ToLower().Contains(searchName) && p.IsVisible).Include(p=>p.Categories).ToList();
            }
            else
            {
                return db.Photos.Where(p => p.IsVisible == true).Include(p => p.Categories).ToList();
            }
        }

        public static void InsertMessage(Message message)
        {
            using PhotoContext db = new PhotoContext();
            message.SentAt = DateTime.Now;
            db.Messages.Add(message);
            db.SaveChanges();
        }
    }
}
