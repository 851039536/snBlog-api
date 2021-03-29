using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Snblog.IRepository;
using Snblog.IService;
using Snblog.IService.IService;
using Snblog.Jwt;
using Snblog.Models;
using Snblog.Repository;
using Snblog.Service;
using Snblog.Service.Service;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Snblog
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

            // 注册Swagger服务
            services.AddSwaggerGen(c =>
              {
                  // 添加文档信息
                  c.SwaggerDoc("v1", new OpenApiInfo
                  {
                      Version = "v1",
                      Title = "SN博客 API",
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
                  //var basePath2 = AppContext.BaseDirectory;// xml路径
                  //                                         // var xmlModelPath = Path.Combine(basePath2, "Snblog.Enties.xml");//Model层的xml文件名
                  //var corePath = Path.Combine(basePath2, "Snblog.xml");//API层的xml文件名
                  //                                                     //  c.IncludeXmlComments(xmlModelPath);
                  //c.IncludeXmlComments(corePath, true);
                  //添加对控制器的标签(描述)

                  // 使用反射获取xml文件。并构造出文件的路径
                  var xmlfile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                  var xmlpath = Path.Combine(AppContext.BaseDirectory, xmlfile);
                  // 启用xml注释. 该方法第二个参数启用控制器的注释，默认为false.
                  c.IncludeXmlComments(xmlpath, true);
                  c.CustomSchemaIds(type => type.FullName);// 可以解决相同类名会报错的问题



                  #region 配置Authorization
                   //Bearer 的scheme定义
                  var securityScheme = new OpenApiSecurityScheme()
                  {
                      Description = "JWT授权(数据将在请求头中进行传输) 参数结构: \"Authorization: Bearer {token}\"",
                      Name = "Authorization",
                      //参数添加在头部
                      In = ParameterLocation.Header,
                      //使用Authorize头部
                      Type = SecuritySchemeType.Http,
                      //内容为以 bearer开头
                      Scheme = "bearer",
                      BearerFormat = "JWT"
                  };

                  //把所有方法配置为增加bearer头部信息
                  var securityRequirement = new OpenApiSecurityRequirement
                {
                    {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "bearerAuth"
                                }
                            },
                            new string[] {}
                    }
                };

                  //注册到swagger中
                  c.AddSecurityDefinition("bearerAuth", securityScheme);
                  c.AddSecurityRequirement(securityRequirement);
                  #endregion
                  
                 

        });

            //注册DbContext
            services.AddDbContext<snblogContext>(options => options.UseMySql(Configuration.GetConnectionString("DefaultConnection")));

            //找一找教程网原创文章


            //配置jwt
            services.ConfigureJwt(Configuration);
            //注入JWT配置文件
            services.Configure<JwtConfig>(Configuration.GetSection("Authentication:JwtBearer"));

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
services.AddScoped<ISnNavigationService, SnNavigationService>();
services.AddScoped<ISnLabelsService, SnLabelsService>();
services.AddScoped<ISnSortService, SnSortService>();
services.AddScoped<ISnOneService, SnOneService>();
services.AddScoped<ISnVideoService, SnVideoService>();
services.AddScoped<ISnVideoTypeService, SnVideoTypeService>();
services.AddScoped<ISnUserTalkService, SnUserTalkService>();
services.AddScoped<ISnUserService, SnUserService>();
services.AddScoped<ISnOneTypeService, SnOneTypeService>();
services.AddScoped<ISnPictureService, SnPictureService>();
services.AddScoped<ISnPictureTypeService, SnPictureTypeService>();
services.AddScoped<ISnTalkService, SnTalkService>();
services.AddScoped<ISnTalkTypeService, SnTalkTypeService>();
services.AddScoped<ISnNavigationTypeService, SnNavigationTypeService>();
services.AddScoped<ISnleaveService, SnleaveService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    #region Swagger
    //可以将Swagger的UI页面配置在Configure的开发环境之中
    // 启用Swagger中间件
    app.UseSwagger();
    //配置SwaggerUI
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SN博客API");
                //设置首页为Swagger
                c.RoutePrefix = string.Empty;
                //设置为none可折叠所有方法
                c.DocExpansion(DocExpansion.None);
                //设置为-1 可不显示models
                c.DefaultModelsExpandDepth(-1);
    });
    #endregion

    app.UseHttpsRedirection();

    app.UseRouting();

    //开启Cors跨域请求中间件
    app.UseCors("AllRequests");
    //jwt
    app.UseAuthentication();

    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
}
    }
}
