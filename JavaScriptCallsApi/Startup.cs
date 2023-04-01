using DataApi;
using Microsoft.AspNetCore.Builder;

namespace JavaScriptCallsApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddMvc();
            //services.AddTransient<IBlogManager, BlogManager>();
        }

        public void Configure(IApplicationBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        }
    }
}
