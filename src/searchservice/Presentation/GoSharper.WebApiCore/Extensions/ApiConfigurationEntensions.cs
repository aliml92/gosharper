using GoSharper.Domain.Applications;
using GoSharper.Domain.Interfaces;
using GoSharper.Domain.Repositories;
using GoSharper.WebApiCore.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace GoSharper.WebApiCore.Extensions
{
    public static class ApiConfigurationExtensions
    {
        public static void AddApiConfiguration(this IServiceCollection services)
        {
            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddTransient<IPodcastsApplication, PodcastsApplication>();
            services.AddTransient<IPodcastsRepository, PodcastsRepository>();

            services.AddControllers();
        }

        public static void UseApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSerilogRequestLogging(opts => opts.EnrichDiagnosticContext = SerilogExtensions.EnrichFromRequest);

            app.UseMiddleware<RequestSerilLogMiddleware>();
            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
        }
    }
}
