using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using DataApi.Models.Blogs;

namespace DataApi
{
    public class BlogManager : IBlogManager
    {
        public string GetConnString(string connString)
        {
            return GlobalConfig.GetConnectionString(connString);
        }

        public int Register(RegisterUserRequest user)
        {
            string conn = GetConnString("ContactsDB");
            int userId;

            using (SqlConnection connection = new SqlConnection(conn))
            {
                var param = new DynamicParameters();

                param.Add("@Username", user.Username);
                param.Add("@Password", user.Password);
                param.Add("@Email", user.Email);
                param.Add("@Id", 0, dbType:DbType.Int32, direction:ParameterDirection.Output);

                connection.Execute("dbo.CreateAccount", param, commandType: CommandType.StoredProcedure);

                userId = param.Get<int>("@Id");
            }

            return userId;
        }

        public int Login(Login userLogins)
        {
            string conn = GetConnString("ContactsDB");
            int userId;

            using (SqlConnection connection = new SqlConnection(conn))
            {
                var param = new DynamicParameters();

                param.Add("@Username", userLogins.Username);
                param.Add("@Password", userLogins.Password);
                param.Add("@Id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.UserLogin", param, commandType: CommandType.StoredProcedure);

                userId = param.Get<int>("@Id");
            }

            return userId;
        }

        public UserDetailsResponse GetUserById(int id)
        {
            string conn = GetConnString("ContactsDB");

            using (SqlConnection connection = new SqlConnection(conn))
            {
                var param = new DynamicParameters();

                param.Add("@Id", id);

                var userDetails = connection.QuerySingle<UserDetailsResponse>("dbo.GetUserDetails", param, commandType: CommandType.StoredProcedure);

                return userDetails;
            }
        }

        public List<BlogPostResponse> GetBlogsList()
        {
            string conn = GetConnString("ContactsDB");

            using (SqlConnection connection = new SqlConnection(conn))
            {
                var blogs = connection.Query<BlogPostResponse>("dbo.GetPosts", commandType: CommandType.StoredProcedure).ToList();

                return blogs;
            }
        }

        public BlogPostResponse GetBlog(int id)
        {
            string conn = GetConnString("ContactsDB");

            using (SqlConnection connection = new SqlConnection(conn))
            {
                var param = new DynamicParameters();

                param.Add("@Id", id);

                return connection.QuerySingle<BlogPostResponse>("dbo.GetPost", param, commandType: CommandType.StoredProcedure);
            }
        }

        public int PostBlog(CreateBlogRequest blogDetails)
        {
            string conn = GetConnString("ContactsDB");

            using (SqlConnection connection = new SqlConnection(conn))
            {
                var param = new DynamicParameters();
                string error = "";

                var image =
                    "data:image/jpeg;base64,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";

                param.Add("@Image", image);
                param.Add("@Description", blogDetails.Description);
                param.Add("@Title", blogDetails.Title);
                param.Add("@UserEmail", blogDetails.AuthorEmail);
                param.Add("@CategoryId", 7); // blogDetails.CategoryID);
                param.Add("@BlogId", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.CreatePost", param, commandType: CommandType.StoredProcedure);

                var blogId = param.Get<int>("@BlogId");

                return blogId;
            }
        }

        public List<Category> GetCategories()
        {
            string conn = GetConnString("ContactsDB");

            using (SqlConnection connection = new SqlConnection(conn))
            {
                var test = connection.Query<Category>("dbo.GetCategories", commandType: CommandType.StoredProcedure).ToList();

                return test;
            }
        }

        public void UpdatePost(UpdateBlogRequest updatePost)
        {
            string conn = GetConnString("ContactsDB");

            using (SqlConnection connection = new SqlConnection(conn))
            {
                var param = new DynamicParameters();

                param.Add("@Id", updatePost.Id);
                //param.Add("@Image", id);
                param.Add("@Description", updatePost.Description);
                param.Add("@Title", updatePost.Title);
                //param.Add("@Date", updatePost.Date);
                param.Add("@Author", updatePost.AuthorEmail);
                param.Add("@Category", updatePost.Category);

                connection.Execute("dbo.UpdatePost", param, commandType: CommandType.StoredProcedure);
            }
        }

        public void DeletePost(int id)
        {
            string conn = GetConnString("ContactsDB");

            using (SqlConnection connection = new SqlConnection(conn))
            {
                var param = new DynamicParameters();

                param.Add("@Id", id);
                connection.Execute("dbo.DeletePost", param, commandType: CommandType.StoredProcedure);
            }
        }

        public int UpdareUserAccount(RegisterUserRequest userRequest)
        {
            string conn = GetConnString("ContactsDB");

            using (SqlConnection connection = new SqlConnection(conn))
            {
                var param = new DynamicParameters();

                param.Add("@UserEmail", userRequest.Email);
                param.Add("@Password", userRequest.Password);
                param.Add("UserId", dbType:DbType.Int32, direction:ParameterDirection.Output);

                connection.Execute("dbo.UpdateUserDetails", param, commandType: CommandType.StoredProcedure);

                var userId = param.Get<int>("UserId");

                return userId;
            }
        }
    }
}
