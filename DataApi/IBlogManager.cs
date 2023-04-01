using System.Collections.Generic;
using DataApi.Models.Blogs;

namespace DataApi
{
    public interface IBlogManager
    {
        string GetConnString(string connString);

        // Get user Id to Authenticate after registering
        int Register(RegisterUserRequest user);

        // Return true when user is successfully Authenticated
        int Login(Login userLogins);
        
        UserDetailsResponse GetUserById(int id);

        List<BlogPostResponse> GetBlogsList();

        BlogPostResponse GetBlog(int id);

        //Getting the ID to display in the single page after creating the Blog
        int PostBlog(CreateBlogRequest blogDetails);

        void UpdatePost(UpdateBlogRequest id);

        void DeletePost(int id);

        int UpdareUserAccount(RegisterUserRequest userRequest);

        List<Category> GetCategories();
    }
}
