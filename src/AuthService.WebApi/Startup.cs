using System.Text;
using System.Text.Json.Serialization;
using AuthService.Application;
using AuthService.EfCorePostgresProvider;
using AuthService.Model;
using AuthService.WebApi.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            AppConfig config = AddConfig(services);

            services.AddAutoMapper(typeof(EntitiesMappingProfile));
            services.AddDbContext<AppDbContext>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddSwaggerDocumentation();
            services
                .AddControllers()
                .AddJsonOptions(opts => { opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });
            AddAuthentication(services, config.Auth);
            services.AddScoped<ITokenProvider, JwtTokenProvider>();
            services.AddScoped<IUserService, UserService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwaggerDocumentation();
            app.UseExceptionMiddleware();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private AppConfig AddConfig(IServiceCollection services)
        {
            var appConfig = new AppConfig();
            Configuration.Bind(appConfig);
            services.AddSingleton(appConfig.Mongo);
            services.AddSingleton(appConfig.Auth);

            return appConfig;
        }

        private void AddAuthentication(IServiceCollection services, AuthConfig config)
        {
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config.SecurityKey)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }
    }
}
