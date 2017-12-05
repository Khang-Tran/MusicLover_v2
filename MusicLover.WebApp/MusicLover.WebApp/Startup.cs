using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MusicLover.WebApp.Server.Extensions;
using MusicLover.WebApp.Server.Persistent;
using MusicLover.WebApp.Server.Persistent.Repositories.Commons;
using MusicLover.WebApp.Server.Persistent.Repositories.Contracts;
using MusicLover.WebApp.Server.Persistent.UnitOfWork.Contracts;

namespace MusicLover.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IAttendanceRepository, AttendanceRepository>();
            services.AddScoped<IFollowingRepository, FollowingRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IGigRepository, GigRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IPhotoRepository, PhotoRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<AppInitializer>();
            services.AddCustomIdentity();
            services.AddAutoMapper();

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, AppInitializer appInitializer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseMvcWithDefaultRoute();
            appInitializer.Seed().Wait();
        }
    }
}

