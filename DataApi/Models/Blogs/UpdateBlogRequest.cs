﻿using System;

namespace DataApi.Models.Blogs
{
    public class UpdateBlogRequest
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public int Category { get; set; }
        public DateTime Date { get; set; }
        public string AuthorEmail { get; set; }
    }
}
