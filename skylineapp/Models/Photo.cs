using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skylineapp.Models
{
    public class Photo
    {
        string path;
        string category;
        int postedByUser;
        int id;

        public string Path { get => path; set => path = value; }
        public string Category { get => category; set => category = value; }
        public int PostedByUser { get => postedByUser; set => postedByUser = value; }
        public int Id { get => id; set => id = value; }
    }
}
