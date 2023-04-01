using System.Net;
using DataApi;
using DataApi.Models.Blogs;
using JavaScriptCallsApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace JavaScriptCallsApi.Controllers
{
    [ApiController]
    [Route("blogs-v1")]
    public class BlogController : ControllerBase
    {
        private readonly IBlogManager _blogManager;

        public BlogController(IBlogManager blogManager) { _blogManager = blogManager; }

        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.Created)]
        [HttpPost("register-account")]
        public ActionResult<int> RegisterAccount([FromBody]User user)
        {
            var userDetails = new RegisterUserRequest()
            {
                Username = user.Username,
                Email = user.Email,
                Password = user.Password
            };

            return _blogManager.Register(userDetails);
        }
        
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [HttpPost("create-post")]
        public ActionResult<int> CreatePost([FromBody] CreateBlogRequest blogPostRequest)
        {
            return _blogManager.PostBlog(blogPostRequest);
        }
        
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [HttpDelete("delete-post")]
        public void DeletePost([FromQuery] int id)
        {
            _blogManager.DeletePost(id);
        } 
        
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [HttpPut("update-post")]
        public void UpdatePost([FromBody] UpdateBlogRequest updateRequest)
        {
            _blogManager.UpdatePost(updateRequest);
        }

        [ProducesResponseType((int)HttpStatusCode.OK)]
        [HttpPut("update-user-details")]
        public int UpdateAccount([FromBody] RegisterUserRequest updateRequest)
        {
            return _blogManager.UpdareUserAccount(updateRequest);
        }

        [ProducesResponseType(typeof(int), (int)HttpStatusCode.NoContent)]
        [HttpPost("user-login")]
        public ActionResult<int> UserLogin([FromBody] UserLogin userLogins)
        {
            var login = new Login()
            {
                Username = userLogins.Username,
                Password = userLogins.Password
            };

            return _blogManager.Login(login);
        }
        
        [ProducesResponseType(typeof(List<BlogPostResponse>), (int)HttpStatusCode.OK)]
        [HttpGet("get-posts")]
        public IEnumerable<BlogPostResponse> GetBlogPosts()
        {
            return _blogManager.GetBlogsList();
        }
        
        [ProducesResponseType(typeof(List<Category>), (int)HttpStatusCode.OK)]
        [HttpGet("get-categories")]
        public IEnumerable<Category> GetCategories()
        {
            return _blogManager.GetCategories();
        }
        
        [ProducesResponseType(typeof(List<Category>), (int)HttpStatusCode.OK)]
        [HttpGet("get-post")]
        public ActionResult<BlogPostResponse> GetCategories([FromQuery] int id)
        {
            return _blogManager.GetBlog(id);
        }
        
        [ProducesResponseType(typeof(UserDetailsResponse), (int)HttpStatusCode.OK)]
        [HttpGet("get-user-details")]
        public ActionResult<UserDetailsResponse> GetUser([FromQuery] int id)
        {
            return _blogManager.GetUserById(id);
        }
    }
}
