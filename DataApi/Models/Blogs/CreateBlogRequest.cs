namespace DataApi.Models.Blogs
{
    public class CreateBlogRequest
    {
        public string Image { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string AuthorEmail { get; set; }
        public int CategoryID { get; set; }
    }
}
