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
using MazeCore;
using SpaceWeb.Models.Maze;
using MazeCore.Cells;
using MazeCore.GraphStuff;
using MazeCore.Maze;

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

            RegistrationPresentations(services);
            RegisterMapper(services);
            RegisterOldRepository(services);
            RegistrationRepositories(services);
            RegisterService(services);



            services.AddControllersWithViews();
            services.AddHttpContextAccessor();
        }

        private void RegisterService(IServiceCollection services)
        {
            services.AddScoped<ICurrencyService>(diContainer =>
                new CurrencyService(
                    diContainer.GetService<UserService>(),
                    diContainer.GetService<ExchangeRateToUsdCurrentRepository>(),
                    diContainer.GetService<ExchangeAccountHistoryRepository>(),
                    diContainer.GetService<ExchangeRateToUsdHistoryRepository>(),
                    diContainer.GetService<IMapper>()));

            //services.AddMyScoped<IUserService, UserService>();
            services.AddScoped<IUserService>(diContainer =>
              new UserService(
                  diContainer.GetService<IUserRepository>(),
                  diContainer.GetService<IHttpContextAccessor>()
              ));


            services.AddScoped<UserService>(diContainer =>
               new UserService(
                   diContainer.GetService<IUserRepository>(),
                   diContainer.GetService<IHttpContextAccessor>()
               ));

            services.AddScoped<IPathHelper>(diContainer =>
               new PathHelper(
                   diContainer.GetService<IWebHostEnvironment>()
               ));


           services.AddScoped<ISalaryService>(diContainer =>
                new SalaryService(
                    diContainer.GetService<IAccrualRepository>(),
                    diContainer.GetService<IPaymentRepository>(),
                    diContainer.GetService<IBankAccountRepository>(),
                    diContainer.GetService<IEmployeRepository>()
               ));
          
            services.AddSingleton<MazeBuilder>(x => new MazeBuilder());

            services.AddSingleton<BreadCrumbsService>(x => new BreadCrumbsService());
        }

        private void RegisterOldRepository(IServiceCollection services)
        {

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

            services.AddScoped<OrderRepository>(diContainer =>
                new OrderRepository(diContainer.GetService<SpaceDbContext>()));
            services.AddControllersWithViews();

            services.AddScoped<AdditionRepository>(diContainer =>
                new AdditionRepository(diContainer.GetService<SpaceDbContext>()));

            services.AddScoped<ShopRocketRepository>(diContainer =>
                new ShopRocketRepository(diContainer.GetService<SpaceDbContext>()));
        }

        private void RegistrationPresentations(IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var types = assembly.GetTypes();
            var humanPresentationTypes = types.Where(t => t.FullName.Contains("Presentation") && t.IsInterface).ToArray();

            foreach (var iPresentation in humanPresentationTypes)
            {
                var realization = types.Single(x => x.GetInterfaces().Contains(iPresentation));
                services.AddScoped(
                    iPresentation,
                    diContainer => ConstructorExecutor(realization, diContainer));
            }
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
                    diContainer => ConstructorExecutor(realization, diContainer));
            }
        }

        private object ConstructorExecutor(Type realization, IServiceProvider diContainer)
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
        }

        private void RegisterMapper(IServiceCollection services)
        {
            var configExpression = new MapperConfigurationExpression();

            //configExpression.CreateMap<User, QuestionaryViewModel>()
            //    .ForMember(nameof(QuestionaryViewModel.FullName),
            //        config => config
            //            .MapFrom(dbModel => $"{dbModel.Name}, {dbModel.SurName} Mr"));

            configExpression.CreateMap<Employe, ShortEmployeViewModel>()
                .ForMember(nameof(ShortEmployeViewModel.Id), config => config.MapFrom(x => x.Id))
                .ForMember(nameof(ShortEmployeViewModel.Name), config => config.MapFrom(x => x.User.Name))
                .ForMember(nameof(ShortEmployeViewModel.Surname), config => config.MapFrom(x => x.User.SurName))
                .ForMember(nameof(ShortEmployeViewModel.SalaryPerHour), config => config.MapFrom(x => x.SalaryPerHour))
                .ForMember(nameof(ShortEmployeViewModel.Position), config => config.MapFrom(x => x.Position.GetDisplayableName()))
                .ForMember(nameof(ShortEmployeViewModel.AvatarUrl), config => config.MapFrom(x => x.User.AvatarUrl));

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

            configExpression.CreateMap<Vertex, CellViewModel>()
                .ForMember(nameof(CellViewModel.X), config => config.MapFrom(vertex => vertex.BaseCell.X))
                .ForMember(nameof(CellViewModel.Y), config => config.MapFrom(vertex => vertex.BaseCell.Y));

            configExpression.CreateMap<Graph, WayViewModel>()
                .ForMember(nameof(WayViewModel.Cells), config => config.MapFrom(graph => graph.Vertices));


            //configExpression.CreateMap<Relic, RelicViewModel>();
            //configExpression.CreateMap<RelicViewModel, Relic>();

            MapBoth<Relic, RelicViewModel>(configExpression);
            MapBoth<User, QuestionaryViewModel>(configExpression);
            MapBoth<User, BanksCardViewModel>(configExpression);

            MapBoth<Transaction, TransactionCardViewModel>(configExpression);
            MapBoth<BanksCard, TransactionCardViewModel>(configExpression);

            MapBoth<Questionary, QuestionaryViewModel>(configExpression);

            MapBoth<Questionary, ProfileViewModel>(configExpression);

            MapBoth<AdvImage, AdvImageViewModel>(configExpression);

            MapBoth<User, RegistrationViewModel>(configExpression);

            MapBoth<Order, OrderViewModel>(configExpression);

            MapBoth<BankAccount, BankAccountViewModel>(configExpression);
            MapBoth<BanksCard, BanksCardViewModel>(configExpression);

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

            configExpression.CreateMap<MazeLevel, MazeViewModel>()
                .ForMember(nameof(MazeLevel.Cells), x => x.Ignore())
                .AfterMap(MazeLevelToViewModel);

            var mapperConfiguration = new MapperConfiguration(configExpression);
            var mapper = new Mapper(mapperConfiguration);
            services.AddScoped<IMapper>(c => mapper);
        }

        private void MazeLevelToViewModel(MazeLevel mazeLevel, MazeViewModel viewModel)
        {
            viewModel.Cells = new CellType[mazeLevel.Height, mazeLevel.Width];

            foreach (var cell in mazeLevel.Cells
                .OrderBy(x => x.Y)
                .ThenBy(x => x.X))
            {
                viewModel.Cells[cell.Y, cell.X] = CellTypeMapper(cell);
            }
        }



        private CellType CellTypeMapper(BaseCell cell)
        {
            if (cell is Wall)
            {
                return CellType.Wall;
            }
            else if (cell is Ground)
            {
                return CellType.Road;
            }
            else if (cell is Gold)
            {
                return CellType.Gold;
            }
            else
            {
                throw new Exception("Uknown type of cell");
            }
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

            //Who am I?
            app.UseAuthentication();

            //Waht can I see?
            app.UseAuthorization();

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseMiddleware<LocalizationNiceMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
