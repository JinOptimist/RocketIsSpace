using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SpaceWeb.EfStuff;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories;
using SpaceWeb.Models;
using SpaceWeb.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;
using Profile = SpaceWeb.EfStuff.Model.Profile;
namespace SpaceWeb
{
    public class Startup
    {
        public const string AuthMethod = "FunCookie";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetValue<string>("connectionString");
            services.AddDbContext<SpaceDbContext>(x => x.UseSqlServer(connectionString));

            services.AddAuthentication(AuthMethod)
                .AddCookie(AuthMethod, config =>
                {
                    config.Cookie.Name = "Smile";
                    config.LoginPath = "/User/Login";
                });

            services.AddScoped<UserRepository>(diContainer =>
                new UserRepository(diContainer.GetService<SpaceDbContext>()));

            services.AddScoped<RelicRepository>(diContainer =>
                new RelicRepository(diContainer.GetService<SpaceDbContext>()));

            services.AddScoped<ProfileRepository>(diContainer =>
                new ProfileRepository(diContainer.GetService<SpaceDbContext>()));

            services.AddScoped<AdvImageRepository>(diContainer =>
                new AdvImageRepository(diContainer.GetService<SpaceDbContext>()));

            services.AddScoped<BankAccountRepository>(diContainer =>
                new BankAccountRepository(diContainer.GetService<SpaceDbContext>()));

            services.AddScoped<DepartmentRepository>(diContainer =>
                new DepartmentRepository(diContainer.GetService<SpaceDbContext>()));

            RegisterMapper(services);

            services.AddScoped<ComfortRepository>(diContainer =>
                new ComfortRepository(diContainer.GetService<SpaceDbContext>()));

            services.AddScoped<RocketStageRepository>(diContainer => 
                new RocketStageRepository(diContainer.GetService<SpaceDbContext>()));

            services.AddScoped<RocketProfileRepository>(diContainer =>
                new RocketProfileRepository(diContainer.GetService<SpaceDbContext>()));

            services.AddScoped<UserService>(diContainer =>
                new UserService(
                    diContainer.GetService<UserRepository>(),
                    diContainer.GetService<IHttpContextAccessor>()
                ));


            services.AddControllersWithViews();

            services.AddHttpContextAccessor();
        }

        private void RegisterMapper(IServiceCollection services)
        {
            var configExpression = new MapperConfigurationExpression();

            configExpression.CreateMap<User, UserProfileViewModel>()
                .ForMember(nameof(UserProfileViewModel.FullName),
                    config => config
                        .MapFrom(dbModel => $"{dbModel.Name}, {dbModel.Surname} Mr"));

            configExpression.CreateMap<User, ProfileViewModel>();

            //configExpression.CreateMap<Relic, RelicViewModel>();
            //configExpression.CreateMap<RelicViewModel, Relic>();
            MapBoth<Relic, RelicViewModel>(configExpression);

            MapBoth<AdvImage, AdvImageViewModel>(configExpression);


            MapBoth<Profile, UserProfileViewModel>(configExpression);

            MapBoth<User,RegistrationViewModel>(configExpression);

            MapBoth<Department, DepartmentViewModel>(configExpression);

            MapBoth<BankAccount, BankAccountViewModel>(configExpression);


            var mapperConfiguration = new MapperConfiguration(configExpression);
            var mapper = new Mapper(mapperConfiguration);
            services.AddScoped<IMapper>(c => mapper);
        }

        public void MapBoth<Type1, Type2>(MapperConfigurationExpression configExpression)
        {
            configExpression.CreateMap<Type1, Type2>();
            configExpression.CreateMap<Type2, Type1>();
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

            //Êòî ÿ?
            app.UseAuthentication();

            //Êóäà ìíå ìîæíî
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
