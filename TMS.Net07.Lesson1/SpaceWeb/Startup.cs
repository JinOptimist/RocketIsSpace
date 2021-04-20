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
using SpaceWeb.Models.RocketModels;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

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
                    config.LoginPath = "/Rocket/Login";
                    config.AccessDeniedPath = "/User/AccessDenied";
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

            RegisterMapper(services);

            services.AddScoped<ComfortRepository>(diContainer =>
                new ComfortRepository(diContainer.GetService<SpaceDbContext>()));

            services.AddScoped<RocketStageRepository>(diContainer =>
                new RocketStageRepository(diContainer.GetService<SpaceDbContext>()));

            services.AddScoped<UserService>(diContainer =>
                new UserService(
                    diContainer.GetService<UserRepository>(),
                    diContainer.GetService<IHttpContextAccessor>()
                ));
            services.AddScoped<RocketService>(diContainer =>
                new RocketService(
                    diContainer.GetService<UserRepository>(),
                    diContainer.GetService<IHttpContextAccessor>()
                ));

            services.AddControllersWithViews();

            services.AddHttpContextAccessor();
 
            services.AddScoped<OrderRepository>(diContainer =>
                new OrderRepository(diContainer.GetService<SpaceDbContext>()));
            services.AddControllersWithViews();

            services.AddScoped<AdditionRepository>(diContainer =>
                new AdditionRepository(diContainer.GetService<SpaceDbContext>()));
            
            services.AddScoped<ShopRocketRepository>(diContainer =>
                new ShopRocketRepository(diContainer.GetService<SpaceDbContext>()));
        }

        private void RegisterMapper(IServiceCollection services)
        {
            var configExpression = new MapperConfigurationExpression();

            configExpression.CreateMap<User, UserProfileViewModel>()
                .ForMember(nameof(UserProfileViewModel.FullName),
                    config => config
                        .MapFrom(dbModel => $"{dbModel.Name}, {dbModel.SurName} Mr"));

            configExpression.CreateMap<User, ProfileViewModel>();

            //configExpression.CreateMap<Relic, RelicViewModel>();
            //configExpression.CreateMap<RelicViewModel, Relic>();
            MapBoth<Relic, RelicViewModel>(configExpression);

            MapBoth<AdvImage, AdvImageViewModel>(configExpression);
            
            MapBoth<User,RocketRegistrationViewModel>(configExpression);
            
            MapBoth<Order,OrderViewModel>(configExpression);

            MapBoth<BankAccount, BankAccountViewModel>(configExpression);
            
            MapBoth<User,RocketProfileViewModel>(configExpression);

            MapBoth<Comfort, ComfortFormViewModel>(configExpression);
            
            MapBoth<AddShopRocket, AdminAddRocketViewModel>(configExpression);

            var mapperConfiguration = new MapperConfiguration(configExpression);
            var mapper = new Mapper(mapperConfiguration);
            services.AddScoped<IMapper>(c => mapper);
        }

        public void MapBoth<Type1, Type2>(MapperConfigurationExpression configExpression)
        {
            configExpression.CreateMap<Type1, Type2>();
            configExpression.CreateMap<Type2, Type1>();
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

            //��� �?
            app.UseAuthentication();

            //���� ��� �����
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
