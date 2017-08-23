using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net.Http;


namespace skylineapp.Models
{
    public class User
    {
        private string firstName;
        private string lastName;
        private string gender;
        private string email;
        private string password;
        private int age;
        private string username;
        private string id;
        private string profilePhoto;

        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Gender { get => gender; set => gender = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public int Age { get => age; set => age = value; }
        public string Username { get => username; set => username = value; }
        public string Id { get => id; set => id = value; }
        public string ProfilePhoto { get => profilePhoto; set => profilePhoto = value; }
    }
}
