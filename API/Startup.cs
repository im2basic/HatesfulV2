
using API.Extensions;
using API.Helpers;
using API.MiddleWare;
using Infastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace API
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            //get connection to database using connection string
            services.AddDbContext<StoreContext>(x => x.UseSqlite(_config.GetConnectionString("DefaultConnection")));
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddControllers().AddJsonOptions(options =>
                { options.JsonSerializerOptions.PropertyNameCaseInsensitive = false; });
            services.AddApplicationServices();
            services.AddSwaggerDocumentation();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();
            app.UseSwaggerDocumentation();
            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
