using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace skylineapp.Models
{
    public class Category : ViewCell
    {
        public string Image { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }

        public Category()
        {
        }

        public Category(string image, string title, string subtitle)
        {
            this.Image = image;
            this.Title = title;
            this.Subtitle = subtitle;
        }
    }
}
