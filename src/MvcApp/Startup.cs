using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using AspNetCore.Routing.Translation.Extensions;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using MvcApp.Filters;

namespace MvcApp
{
    public class Startup
    {
        private string[] _supportedLanguages;
        private string _defaultLanguage;
        
        public Startup(IConfiguration configuration)
        {
            _supportedLanguages = configuration.GetValue<string>("SupportedLanguages").Split(",", StringSplitOptions.RemoveEmptyEntries);
            _defaultLanguage = configuration.GetValue<string>("DefaultLanguage");
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            var builder = services.AddControllersWithViews(options =>
            {
                options.AddCultureCookieFilter();
                options.Filters.Add<SetLanguageActionFilter>();
            });
            
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            builder.AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);
            builder.AddDataAnnotationsLocalization();
            
            services.AddRoutingLocalization(_supportedLanguages, _defaultLanguage);
            
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRoutingLocalization();
            app.UseStaticFiles();
            app.UseEndpointsLocalization(_supportedLanguages);
        }
    }
}