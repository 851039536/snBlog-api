using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
using Newtonsoft.Json;
using Snblog.IRepository;
using Snblog.IService;
using Snblog.Models;
using Snblog.Repository;
using Snblog.Service;

namespace Snblog
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";//名字随便起

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //注册swagger服务
            services.AddSwaggerGen(c =>
              {
                  c.SwaggerDoc("v1", new OpenApiInfo
                  {
                      Version = "v1",
                      Title = "ToDo API",
                      Description = "EFCore数据操作 ASP.NET Core Web API",
                      TermsOfService = new Uri("https://example.com/terms"),
                      Contact = new OpenApiContact
                      {
                          Name = "Shayne Boyer",
                          Email = string.Empty,
                          Url = new Uri("https://twitter.com/spboyer"),
                      },
                      License = new OpenApiLicense
                      {
                          Name = "Use under LICX",
                          Url = new Uri("https://example.com/license"),
                      }
                  });

                  // 为 Swagger 设置xml文档注释路径
                  var basePath2 = AppContext.BaseDirectory;// xml路径
                  var xmlModelPath = Path.Combine(basePath2, "Snblog.Enties.xml");//Model层的xml文件名
                  var corePath = Path.Combine(basePath2, "Snblog.xml");//API层的xml文件名
                  c.IncludeXmlComments(xmlModelPath);
                  c.IncludeXmlComments(corePath, true);
                  //添加对控制器的标签(描述)
                  c.CustomSchemaIds(type => type.FullName);// 可以解决相同类名会报错的问题

                  // c.OperationFilter<AddResponseHeadersFilter>();
                  // c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                  //
                  // c.OperationFilter<SecurityRequirementsOperationFilter>();

              });

            //注册DbContext
            services.AddDbContext<snblogContext>(options => options.UseMySql(Configuration.GetConnectionString("DefaultConnection")));

            //找一找教程网原创文章

            #region Cors跨域请求
            services.AddCors(c =>
            {
                c.AddPolicy("AllRequests", policy =>
                {
                    policy
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();

                });
            });
            #endregion
            services.AddControllers();

            //DI依赖注入配置。

            services.AddScoped<IRepositoryFactory, RepositoryFactory>();//泛型工厂
            services.AddScoped<IconcardContext, snblogContext>();//db
            services.AddScoped<ISnArticleService, SnArticleService>();//ioc
            services.AddScoped<ISnNavigationService, SnNavigationService>();//ioc
            services.AddScoped<ISnLabelsService, SnLabelsService>();//ioc
            services.AddScoped<ISnSortService, SnSortService>();//ioc
            services.AddScoped<ISnOneService, SnOneService>();//ioc
            services.AddScoped<ISnVideoService, SnVideoService>();//ioc
            services.AddScoped<ISnVideoTypeService, SnVideoTypeService>();//ioc
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();


                #region Swagger
                //可以将Swagger的UI页面配置在Configure的开发环境之中
                app.UseSwagger();
                //和Swagger UI
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                    c.RoutePrefix = string.Empty;
                });
                #endregion
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //开启Cors跨域请求中间件
            app.UseCors("AllRequests");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
