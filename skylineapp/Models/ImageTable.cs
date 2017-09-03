using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skylineapp.Models
{
    public class ImageTable
    {
        private string pathToImage;
        private string user;
        private string category;
        private string desc;
        private string userProfilePhoto;
        private string title;
        private string id;

        public string User { get => user; set => user = value; }
        public string Category { get => category; set => category = value; }
        public string Id { get => id; set => id = value; }
        public string PathToImage { get => pathToImage; set => pathToImage = value; }
        public string Desc { get => desc; set => desc = value; }
        public string UserProfilePhoto { get => userProfilePhoto; set => userProfilePhoto = value; }
        public string Title { get => title; set => title = value; }
    }
}
