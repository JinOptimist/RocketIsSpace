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
using Profile = SpaceWeb.EfStuff.Model.Profile;
using SpaceWeb.EfStuff.Repositories.IRepository;
using SpaceWeb.Presentation;
using SpaceWeb.Models.Human;
using System.Reflection;

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
                    config.AccessDeniedPath = "/User/AccessDenied";
                });

            services.AddScoped<IRelicPresentation>(container =>
                new RelicPresentation(
                    container.GetService<IRelicRepository>(),
                    container.GetService<IMapper>()));

            services.AddScoped<IHumanPresentation>(container =>
                new HumanPresentation(
                    container.GetService<IUserRepository>(),
                    container.GetService<IDepartmentRepository>(),
                    container.GetService<IMapper>()));

            services.AddScoped<IUserRepository>(diContainer =>
                new UserRepository(
                    diContainer.GetService<SpaceDbContext>(),
                    diContainer.GetService<IBankAccountRepository>()
                    ));

            //services.AddScoped<IRelicRepository>(diContainer =>
            //    new RelicRepository(diContainer.GetService<SpaceDbContext>()));

            //services.AddScoped<IDepartmentRepository>(diContainer =>
            //    new DepartmentRepository(diContainer.GetService<SpaceDbContext>()));

            //services.AddScoped<IBankAccountRepository>(diContainer =>
            //    new BankAccountRepository(diContainer.GetService<SpaceDbContext>()));

            services.AddScoped<ProfileRepository>(diContainer =>
                new ProfileRepository(diContainer.GetService<SpaceDbContext>()));

            services.AddScoped<AdvImageRepository>(diContainer =>
                new AdvImageRepository(diContainer.GetService<SpaceDbContext>()));

            services.AddScoped<BanksCardRepository>(diContainer =>
                new BanksCardRepository(diContainer.GetService<SpaceDbContext>()));

            services.AddScoped<ComfortRepository>(diContainer =>
                new ComfortRepository(diContainer.GetService<SpaceDbContext>()));

            services.AddScoped<RocketStageRepository>(diContainer =>
                new RocketStageRepository(diContainer.GetService<SpaceDbContext>()));

            services.AddScoped<OrderRepository>(diContainer =>
                new OrderRepository(diContainer.GetService<SpaceDbContext>()));
            services.AddControllersWithViews();

            services.AddScoped<AdditionRepository>(diContainer =>
                new AdditionRepository(diContainer.GetService<SpaceDbContext>()));

            services.AddScoped<ShopRocketRepository>(diContainer =>
                new ShopRocketRepository(diContainer.GetService<SpaceDbContext>()));

            RegisterMapper(services);
            services.AddScoped<UserService>(diContainer =>
               new UserService(
                   diContainer.GetService<IUserRepository>(),
                   diContainer.GetService<IHttpContextAccessor>()
               ));

            services.AddControllersWithViews();

            services.AddHttpContextAccessor();

            RegistrationRepositories(services);
        }

        private void RegistrationRepositories(IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var types = assembly.GetTypes();

            foreach (var iRepo in types.Where(type =>
                type.IsInterface
                && type.GetInterfaces()
                    .Any(x =>
                        x.IsGenericType
                        && x.GetGenericTypeDefinition() == typeof(IBaseRepository<>))
                ))
            {
                var realization = types.Single(x => x.GetInterfaces().Contains(iRepo));
                services.AddScoped(
                    iRepo,
                    diContainer =>
                    {
                        var constructor = realization.GetConstructors()[0];
                        var paramInfoes = constructor.GetParameters();

                        var paramValues = new object[paramInfoes.Length];
                        for (int i = 0; i < paramInfoes.Length; i++)
                        {
                            var paramInfo = paramInfoes[i];
                            var paramValue = diContainer.GetService(paramInfo.ParameterType);
                            paramValues[i] = paramValue;
                        }

                        var answer = constructor.Invoke(paramValues);
                        return answer;
                    });
            }
        }

        private void RegisterMapper(IServiceCollection services)
        {
            var configExpression = new MapperConfigurationExpression();

            //configExpression.CreateMap<User, UserProfileViewModel>()
            //    .ForMember(nameof(UserProfileViewModel.FullName),
            //        config => config
            //            .MapFrom(dbModel => $"{dbModel.Name}, {dbModel.SurName} Mr"));

            configExpression.CreateMap<User, ProfileViewModel>();

            //configExpression.CreateMap<Relic, RelicViewModel>();
            //configExpression.CreateMap<RelicViewModel, Relic>();

            MapBoth<Relic, RelicViewModel>(configExpression);
            MapBoth<User, UserProfileViewModel>(configExpression);
            MapBoth<User, BanksCardViewModel>(configExpression);

            MapBoth<Profile, UserProfileViewModel>(configExpression);

            MapBoth<Profile, ProfileViewModel>(configExpression);

            MapBoth<AdvImage, AdvImageViewModel>(configExpression);

            MapBoth<User, RegistrationViewModel>(configExpression);

            MapBoth<Order, OrderViewModel>(configExpression);

            MapBoth<BankAccount, BankAccountViewModel>(configExpression);


            MapBoth<User, RocketProfileViewModel>(configExpression);

            MapBoth<Comfort, ComfortFormViewModel>(configExpression);

            MapBoth<AddShopRocket, AddShopRocketViewModel>(configExpression);

            MapBoth<AddShopRocketViewModel, AddShopRocket>(configExpression);

            
            MapBoth<Rocket, AddShopRocketViewModel>(configExpression);


            MapBoth<ShortUserViewModel, User>(configExpression);

            MapBoth<ClientViewModel, Client>(configExpression);

            MapBoth<HumanOrderViewModel, Order>(configExpression);

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
