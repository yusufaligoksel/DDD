using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Management.API.Infrastructure.Middlewares;
using Management.Application.Features.Category.Commands.InsertCategoryCommad;
using Management.CrossCuttingConcerns.Logging.ElasticSearch.Abstract;
using Management.CrossCuttingConcerns.Logging.ElasticSearch.Concrete;
using Management.CrossCuttingConcerns.Logging.ElasticSearch.Extensions;
using Management.Domain.Settings;
using Management.Infrastructure.Repository;
using Management.Infrastructure.Services.Abstract;
using Management.Infrastructure.Services.Concrete;
using Management.Persistance.Context;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Management.API
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Management.API", Version = "v1" });
            });

            #region Options
            services.Configure<TokenOption>(Configuration.GetSection("TokenOption"));
            services.Configure<ElasticSearchOption>(Configuration.GetSection("ElasticSearch"));
            #endregion

            #region DataBase

            services.AddDbContextPool<ManagementContext>(
                options => options.UseMySql(Configuration.GetConnectionString("ManagementDb"), new MySqlServerVersion(new Version(5, 5, 52))));

            #endregion

            #region Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opts =>
            {
                var tokenOptions = Configuration.GetSection("TokenOption").Get<TokenOption>();
                //opts.SaveToken = true;
                opts.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidIssuer = tokenOptions.Issuer,
                    ValidAudience = tokenOptions.Audience[0],
                    IssuerSigningKey = SignService.GetSymmetricSecurityKey(tokenOptions.SecurityKey),
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
            #endregion
            
            #region ServiceInjection
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ILogService, ElasticSearchLogService>();
            #endregion

            #region MediatR
            services.AddMediatR(typeof(Startup));
            services.AddMediatR(typeof(InsertCategoryCommand));
            #endregion
            
            #region ElasticSearch
            services.AddElasticsearch(Configuration);
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Management.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<UserMiddleware>();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            
        }
    }
}