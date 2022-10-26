
using MergerBay.Infrastructure.Interfaces.Common;
using MergerBay.Infrastructure.Interfaces.Sectors;
using MergerBay.Infrastructure.Interfaces.Seller;
using MergerBay.Services.Services.Common;
using MergerBay.Services.Services.Sectors;
using MergerBay.Services.Services.Seller;
using MergerBay.Utilities;
using MergerBay.Domain.Context;
using MergerBay.Infrastructure.Interfaces.Login;
using MergerBay.Infrastructure.Interfaces.Password;
using MergerBay.Infrastructure.Interfaces.Token;
using MergerBay.Infrastructure.Interfaces.UserManagment;
using MergerBay.Services.Services.Login;
using MergerBay.Services.Services.password;
using MergerBay.Services.Services.Token;
using Microsoft.EntityFrameworkCore;
using MergerBay.Services.Services.UserManagment;
using MergerBay.Infrastructure.Interfaces.UserProfile;
using MergerBay.Services.Services.UserProfile;
using MergerBay.Infrastructure.Interfaces.Blob;
using MergerBay.Services.Blob;
using MergerBay.Infrastructure.Interfaces.ApplicationLogs;
using MergerBay.Services.Services.ApplicationLogs;

namespace MergerBayApi.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationSerives(this IServiceCollection services,IConfiguration configuration)
        {
            //Add Application services here==========================
            services.AddDbContext<MergerBayContext>(options =>
            options.UseSqlServer(Config.ConnectionString));
            services.AddTransient<IPasswordHasher, PasswordHasher>();
            services.AddTransient<IUserManagement, UserManagement>();
            services.AddTransient<ILogin, Login>();
            services.AddTransient<IToken, Token>();
            services.AddTransient<IGenericRepository, GenericRepository>();
            services.AddTransient<ISellerRepository, SellerRepository>();
            services.AddTransient<ISectorRepository, SectorRepository>();
            services.AddTransient<IUserProfile, UserProfileSrv>();
            services.AddTransient<IAzureblob, Azureblob>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<ILogRepository, LogRepository>();
            return services;
        }
    }
}
