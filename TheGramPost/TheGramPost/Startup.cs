using AspNetCore.Firebase.Authentication.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NLog;
using TheGramPost.Helpers;
using TheGramPost.Repository;

namespace TheGramPost
{
    public class Startup
    {private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddFirebaseAuthentication(Configuration["JWT:Issuer"], Configuration["JWT:Audience"]);
            services.AddMemoryCache();

            services.AddScoped<IFirebaseService, FirebaseService>();
            services.AddScoped<IStorageUtility, StorageUtility>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IUserContextHelper, UserContextHelper>();
            services.AddHttpContextAccessor();

            services.AddDbContext<PostContext>(builder =>
            {
                builder.UseInMemoryDatabase(Configuration.GetConnectionString("PostContext"));
            });
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}