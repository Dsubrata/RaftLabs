using AutoMapper;
using Microsoft.OpenApi.Models;
using Polly;
using Polly.Extensions.Http;
using RaftLabs.Enterprise.Configuration;
using RaftLabs.Enterprise.WebAPI.Interfaces.Services;
using RaftLabs.Enterprise.WebAPI.Services;

namespace RaftLabs.Enterprise.WebAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            _ = services.AddControllers();
            _ = services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            _ = services.AddMemoryCache();
            _ = services.AddHttpClient("RetryOnFailure").SetHandlerLifetime(TimeSpan.FromSeconds(5)).AddPolicyHandler(ConfigureRetryPolicy());
            _ = services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RaftLabs Web API", Version = "v1" });
            });


            RegisterDependencies(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                _ = app.UseDeveloperExceptionPage();
            }

            _ = app.UseSwagger();
            _ = app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RaftLabs Web API v1"));
            _ = app.UseHttpsRedirection();
            _ = app.UseRouting();
            _ = app.UseAuthorization();
            _ = app.UseEndpoints(endpoints =>
            {
                _ = endpoints.MapControllers();
            });
        }

        public void RegisterDependencies(IServiceCollection services)
        {
            _ = services.AddSingleton<ISettings, Settings>();
            _ = services.AddTransient<IUserService, UserService>();
        }

        internal IAsyncPolicy<HttpResponseMessage> ConfigureRetryPolicy()
        {
            return HttpPolicyExtensions
                // Http Request Exception, 5XX  
                .HandleTransientHttpError()
                // 404  
                .OrResult(message => message.StatusCode == System.Net.HttpStatusCode.NotFound)
                // Retry 3 times after delay  
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }
    }
}
