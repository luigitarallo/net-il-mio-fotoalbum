﻿using Microsoft.AspNetCore.Mvc.Rendering;
using net_il_mio_fotoalbum.Data;

namespace net_il_mio_fotoalbum.Models
{
    public class PhotoFormModel
    {
        public Photo Photo { get; set; }
        public List<SelectListItem>? Categories { get; set; } // To show categories to select
        public List<string> SelectedCategories { get; set; }

        public PhotoFormModel() { }

        public PhotoFormModel(Photo photo) 
        {
            this.Photo = photo;
        }

        public void CreateCategories()
        {
            this.Categories = new List<SelectListItem>();
            if (this.SelectedCategories == null) 
            {
                this.SelectedCategories = new List<string>();
            }
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
    }
}
