using System;

namespace DataApi.Models.Blogs
{
    public class UserDetailsResponse
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime DateCreated { get; set; }
        public string ProfilePic { get; set; }
    }
}
