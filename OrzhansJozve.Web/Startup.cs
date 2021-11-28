using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using GoogleReCaptcha.V3;
using GoogleReCaptcha.V3.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using OrzhansJozve.DataLayer.Repositories;
using OrzhansJozve.DataLayer.Services;
using OrzhansJozve.Utilities;
using OrzhansJozve.DataLayer.Context;
using WebMarkupMin.AspNetCore3;

namespace OrzhansJozve.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
            });
            #region ConectionString
            services.AddDbContext<OrzhansJozve_DbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("OrzhansJozve_CoonectionString")));
            services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<OrzhansJozve_DbContext>();
            #endregion
            #region GoogleReCaptcha
            services.AddHttpClient<ICaptchaValidator, GoogleReCaptchaValidator>();
            #endregion
            #region IoC
            services.AddScoped<IMasterPageRepository, MasterPageGroupService>();
            services.AddScoped<IPageRepository, PageService>();
            services.AddScoped<IPageGroupRepository, PageGroupService>();
            services.AddScoped<ICommentRepository, CommentService>();
            services.AddScoped<IAuthorRepository, AuthorService>();
            services.AddScoped<INewsAgencyPeopleRepository, NewsAgencyPeopleServices>();
            services.AddScoped<IAdsRepository, AdsService>();
            services.AddScoped<IInsideAticelImageRepository, InsideAticelImageService>();
            services.AddScoped<IFooterLinkRepository, FooterLinkService>();
            #endregion
            services.AddWebMarkupMin()
                .AddHtmlMinification()
                .AddHttpCompression()
                .AddXhtmlMinification()
                .AddXmlMinification();
            services.AddRazorPages();
            services.AddAuthentication();
            services.AddResponseCaching();
            services.AddResponseCompression();
            services.Configure<MvcOptions>(option => { option.Filters.Add(new RequireHttpsAttribute()); });
            #region Gzip
            services.Configure<GzipCompressionProviderOptions>
            (options =>
            {
                options.Level = CompressionLevel.Fastest;
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //env.EnvironmentName = Microsoft.AspNetCore.Hosting.EnvironmentName.Production;
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseStatusCodePagesWithRedirects("/error/{0}");
                app.UseExceptionHandler("/error");
                app.UseHsts();
            }
            // Add redirection and use a rewriter
            RedirectHttpsWwwNonWwwRule rule = new RedirectHttpsWwwNonWwwRule
            {
                status_code = 301,
                redirect_to_https = true,
                redirect_to_non_www = true,
                hosts_to_ignore = new string[] { "localhost" }
            };
            RewriteOptions options = new RewriteOptions();
            options.Rules.Add(rule);
            app.UseRewriter(options);
            app.UseWebMarkupMin();
            app.UseHttpsRedirection();
            app.UseResponseCompression();
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    // Cache static files for 30 days
                    ctx.Context.Response.Headers.Append("Cache-Control", "public,max-age=25920000");
                    ctx.Context.Response.Headers.Append("Expires", DateTime.UtcNow.AddDays(300).ToString("R", CultureInfo.InvariantCulture));
                }
            });
            app.UseResponseCaching();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapRazorPages(); });
            app.UseCookiePolicy();
        }
    }
}
