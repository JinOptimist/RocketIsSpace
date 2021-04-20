using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SpaceWeb.EfStuff;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories;
using SpaceWeb.Models;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace SpaceWeb
{
    public class Startup
    {
        public Startup(Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
               var connectionString = Configuration.GetValue<string>("connectionString");
            services.AddDbContext<SpaceDbContext>(x => x.UseSqlServer(connectionString));

            services.AddScoped<UserRepository>(diContainer => 
                new UserRepository(diContainer.GetService<SpaceDbContext>()));

            services.AddScoped<RelicRepository>(diContainer =>
                new RelicRepository(diContainer.GetService<SpaceDbContext>()));

            services.AddScoped<ComfortRepository>(diContainer =>
                new ComfortRepository(diContainer.GetService<SpaceDbContext>()));

            services.AddScoped<RocketStageRepository>(diContainer => 
                new RocketStageRepository(diContainer.GetService<SpaceDbContext>()));

            services.AddScoped<RocketProfileRepository>(diContainer =>
                new RocketProfileRepository(diContainer.GetService<SpaceDbContext>()));

            
            RegisterMapper(services);

            services.AddControllersWithViews();
        }

        private void RegisterMapper(IServiceCollection services)
        {
            var congfigExpression = new MapperConfigurationExpression();

            MapBoth<RocketStage, RocketStageAddViewModel>(congfigExpression);

            var mapperConfiguration = new MapperConfiguration(congfigExpression);
            var mapper = new Mapper(mapperConfiguration);
            services.AddScoped<IMapper>(c => mapper);
        }

        public void MapBoth<Type1, Type2>(MapperConfigurationExpression configurationExpression) 
        {
            configurationExpression.CreateMap<Type1, Type2>();
            configurationExpression.CreateMap<Type2, Type1>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
