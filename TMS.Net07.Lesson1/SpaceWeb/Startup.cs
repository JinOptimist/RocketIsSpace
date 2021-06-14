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
using System.Numerics;
using System.Threading.Tasks;
using SpaceWeb.Models.RocketModels;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;
using Questionary = SpaceWeb.EfStuff.Model.Questionary;
using SpaceWeb.EfStuff.Repositories.IRepository;
using SpaceWeb.Presentation;
using SpaceWeb.Models.Human;
using SpaceWeb.Models.Bank;
using SpaceWeb.Extensions;
using System.Reflection;
using SpaceWeb.Migrations;
using Microsoft.Extensions.Logging;
using AdvImage = SpaceWeb.EfStuff.Model.AdvImage;

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
                    container.GetService<IMapper>(),
                    container.GetService<IEmployeRepository>(),
                    container.GetService<UserService>()));

            services.AddScoped<IUserRepository>(diContainer =>
                new UserRepository(
                    diContainer.GetService<SpaceDbContext>(),
                    diContainer.GetService<IBankAccountRepository>()
                    ));

            services.AddScoped<IRocketShopPresentation>(container =>
                new RocketShopPresentation(
                    container.GetService<IMapper>(),
                    container.GetService<IOrderRepository>(),
                    container.GetService<IShopRocketRepository>(),
                    container.GetService<UserService>()));

            //services.AddScoped<IRelicRepository>(diContainer =>
            //    new RelicRepository(diContainer.GetService<SpaceDbContext>()));

            //services.AddScoped<IDepartmentRepository>(diContainer =>
            //    new DepartmentRepository(diContainer.GetService<SpaceDbContext>()));

            //services.AddScoped<IBankAccountRepository>(diContainer =>
            //    new BankAccountRepository(diContainer.GetService<SpaceDbContext>()));

            services.AddScoped<QuestionaryRepository>(diContainer =>
                new QuestionaryRepository(diContainer.GetService<SpaceDbContext>()));

            services.AddScoped<AdvImageRepository>(diContainer =>
                new AdvImageRepository(diContainer.GetService<SpaceDbContext>()));

            services.AddScoped<BanksCardRepository>(diContainer =>
                new BanksCardRepository(diContainer.GetService<SpaceDbContext>()));

            services.AddScoped<ComfortRepository>(diContainer =>
                new ComfortRepository(diContainer.GetService<SpaceDbContext>()));

            services.AddScoped<RocketStageRepository>(diContainer =>
                new RocketStageRepository(diContainer.GetService<SpaceDbContext>()));

            services.AddScoped<InsuranceTypeRepository>(diContainer =>
                new InsuranceTypeRepository(diContainer.GetService<SpaceDbContext>()));

            services.AddScoped<InsuranceRepository>(diContainer =>
                new InsuranceRepository(diContainer.GetService<SpaceDbContext>()));

            services.AddScoped<ExchangeRateToUsdCurrentRepository>(diContainer =>
                new ExchangeRateToUsdCurrentRepository(diContainer.GetService<SpaceDbContext>()));

            services.AddScoped<ExchangeRateToUsdHistoryRepository>(diContainer =>
                new ExchangeRateToUsdHistoryRepository(diContainer.GetService<SpaceDbContext>()));

            services.AddScoped<ExchangeAccountHistoryRepository>(diContainer =>
                new ExchangeAccountHistoryRepository(diContainer.GetService<SpaceDbContext>()));

            services.AddScoped<ICurrencyService>(diContainer =>
                new CurrencyService(
                    diContainer.GetService<UserService>(),
                    diContainer.GetService<ExchangeRateToUsdCurrentRepository>(),
                    diContainer.GetService<ExchangeAccountHistoryRepository>(),
                    diContainer.GetService<ExchangeRateToUsdHistoryRepository>(),
                    diContainer.GetService<Mapper>()
                ));

            services.AddScoped<UserService>(diContainer =>
                new UserService(
                    diContainer.GetService<IUserRepository>(),
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

            //services.AddScoped<ICurrencyService>(diContainer =>
            //    new CurrencyService(diContainer.GetService<UserService>(),
            //        diContainer.GetService<ExchangeRateToUsdCurrentRepository>(),
            //        diContainer.GetService<ExchangeAccountHistoryRepository>()));

            services.AddScoped<IBankPresentation>(diContainer =>
                new BankPresentation(diContainer.GetService<IProfileRepository>(), diContainer.GetService<IMapper>()));

            services.AddScoped<BankPresentation>(diContainer =>
                new BankPresentation(diContainer.GetService<IProfileRepository>(), diContainer.GetService<IMapper>()));

            //services.AddScoped<IEmployeRepository>(diContainer =>
            //    new EmployeRepository(diContainer.GetService<SpaceDbContext>()));

            services.AddScoped<ICurrencyService>(diContainer =>
                new CurrencyService(
                    diContainer.GetService<UserService>(),
                    diContainer.GetService<ExchangeRateToUsdCurrentRepository>(),
                    diContainer.GetService<ExchangeAccountHistoryRepository>(),
                    diContainer.GetService<ExchangeRateToUsdHistoryRepository>(),
                    diContainer.GetService<IMapper>()));
            
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

            //configExpression.CreateMap<User, QuestionaryViewModel>()
            //    .ForMember(nameof(QuestionaryViewModel.FullName),
            //        config => config
            //            .MapFrom(dbModel => $"{dbModel.Name}, {dbModel.SurName} Mr"));

            configExpression.CreateMap<Employe, ShortEmployeViewModel>()
                .ForMember(nameof(ShortEmployeViewModel.Name), config => config.MapFrom(x => x.User.Name))
                .ForMember(nameof(ShortEmployeViewModel.Surname), config => config.MapFrom(x => x.User.SurName))
                .ForMember(nameof(ShortEmployeViewModel.SalaryPerHour), config => config.MapFrom(x => x.SalaryPerHour))
                .ForMember(nameof(ShortEmployeViewModel.Position), config => config.MapFrom(x => x.Position.GetDisplayableName()));

            configExpression.CreateMap<User, RequestViewModel>()
                .ForMember(nameof(RequestViewModel.Id), config => config.MapFrom(x => x.Employe.Id))
                .ForMember(nameof(RequestViewModel.ForeignKeyUser), config => config.MapFrom(x => x.Employe.ForeignKeyUser))
                .ForMember(nameof(RequestViewModel.Position), config => config.MapFrom(x => x.Employe.Position))
                .ForMember(nameof(RequestViewModel.SalaryPerHour), config => config.MapFrom(x => x.Employe.SalaryPerHour))
                .ForMember(nameof(RequestViewModel.EmployeStatus), config => config.MapFrom(x => x.Employe.EmployeStatus));

            configExpression.CreateMap<RequestViewModel, Employe>();


            configExpression.CreateMap<User, ProfileViewModel>();

            configExpression.CreateMap<User, EmployeeProfileViewModel>()
                .ForMember(nameof(EmployeeProfileViewModel.DepartmentName),
                    config => config.MapFrom(user =>
                    user.Employe.Department == null
                        ? "N/A"
                        : user.Employe.Department.DepartmentName))
                .ForMember(nameof(EmployeeProfileViewModel.Salary),
                    config => config.MapFrom(user =>
                        user.Employe.SalaryPerHour));


            //configExpression.CreateMap<Relic, RelicViewModel>();
            //configExpression.CreateMap<RelicViewModel, Relic>();

            MapBoth<Relic, RelicViewModel>(configExpression);
            MapBoth<User, QuestionaryViewModel>(configExpression);
            MapBoth<User, BanksCardViewModel>(configExpression);

            MapBoth<Questionary, QuestionaryViewModel>(configExpression);

            MapBoth<Questionary, ProfileViewModel>(configExpression);

            MapBoth<AdvImage, AdvImageViewModel>(configExpression);

            MapBoth<User, RegistrationViewModel>(configExpression);

            MapBoth<Order, OrderViewModel>(configExpression);

            MapBoth<BankAccount, BankAccountViewModel>(configExpression);

            MapBoth<User, RocketProfileViewModel>(configExpression);

            MapBoth<Comfort, ComfortFormViewModel>(configExpression);

            MapBoth<AddShopRocket, ShopRocketViewModel>(configExpression);

            MapBoth<ShopRocketViewModel, AddShopRocket>(configExpression);

            MapBoth<Rocket, ShopRocketViewModel>(configExpression);

            MapBoth<ShortUserViewModel, User>(configExpression);

            MapBoth<ClientViewModel, Client>(configExpression);

            MapBoth<HumanOrderViewModel, Order>(configExpression);

            MapBoth<InsurancePrintViewModel, Insurance>(configExpression);

            MapBoth<InsuranceTypeViewModel, InsuranceType>(configExpression);

            MapBoth<InsuranceViewModel, Insurance>(configExpression);

            MapBoth<ComplexRocketShopViewModel, Order>(configExpression);

            MapBoth<Department, DepartmentViewModel>(configExpression);

            MapBoth<ExchangeRateToUsdCurrent, ExchangeRateToUsdHistory>(configExpression);

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
        public void Configure(IApplicationBuilder app,
            IWebHostEnvironment env,
            ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile("Logs/log-{Date}.txt");
            loggerFactory.AddFile("Logs/ERROR-{Date}.txt", LogLevel.Error);

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

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
