using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Snblog.Cache.Cache;
using Snblog.Cache.CacheUtil;
using Snblog.Controllers;
using Snblog.IRepository;
using Snblog.IService;
using Snblog.IService.IReService;
using Snblog.IService.IService;
using Snblog.Jwt;
using Snblog.Repository.Repository;
using Snblog.Service;
using Snblog.Service.AngleSharp;
using Snblog.Service.ReService;
using Snblog.Service.Service;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Snblog
{
    public class Startup
    {

        #region 版本控制枚举
        /// <summary>
        /// 版本控制
        /// </summary>
        public enum ApiVersion
        {
            /// <summary>
            /// v1版本
            /// </summary>
            V1 = 1,
            /// <summary>
            /// v2版本
            /// </summary>
            V2 = 2,
            /// <summary>
            /// AngleSharp
            /// </summary>
            AngleSharp = 3
        }
        #endregion

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        //运行时将调用此方法。 使用此方法将服务添加到容器。
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(option =>
              //忽略循环引用
              option.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
          );
            #region MiniProfiler 性能分析
            services.AddMiniProfiler(options =>
            options.RouteBasePath = "/profiler"
             );
            #endregion
            #region Swagger服务
            services.AddSwaggerGen(c =>
              {
                  // 添加文档信息
                  //遍历版本信息
                  typeof(ApiVersion).GetEnumNames().ToList().ForEach(version =>
                  {
                      c.SwaggerDoc(version, new OpenApiInfo
                      {
                          Title = "SN blog API", //标题
                          Description = "EFCore数据操作 ASP.NET Core Web API", //描述
                          TermsOfService = new Uri("https://example.com/terms"), //服务条款
                          Contact = new OpenApiContact
                          {
                              Name = "kai ouyang", //联系人
                              Email = string.Empty,  //邮箱
                              Url = new Uri("https://twitter.com/spboyer"),//网站
                          },
                          License = new OpenApiLicense
                          {
                              Name = "Use under LICX", //协议
                              Url = new Uri("https://example.com/license"), //协议地址
                          }
                      });
                  });

                  // 使用反射获取xml文件。并构造出文件的路径
                  var xmlfile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                  var xmlpath = Path.Combine(AppContext.BaseDirectory, xmlfile);
                  // 启用xml注释. 该方法第二个参数启用控制器的注释，默认为false.
                  c.IncludeXmlComments(xmlpath, true);
                  //Model 也添加注释说明
                  var xmlpath1 = Path.Combine("Snblog.Enties.xml");
                  var xmlpath2 = Path.Combine(AppContext.BaseDirectory, xmlpath1);
                  c.IncludeXmlComments(xmlpath2, true);
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
                            Array.Empty<string>()
                    }
                };

                  //注册到swagger中
                  c.AddSecurityDefinition("bearerAuth", securityScheme);
                  c.AddSecurityRequirement(securityRequirement);
                  #endregion
              });
            #endregion
            #region DbContext
            services.AddDbContext<snblogContext>(options => options.UseMySQL(Configuration.GetConnectionString("DefaultConnection")));
            #endregion
            # region jwt
            services.ConfigureJwt(Configuration);
            //注入JWT配置文件
            services.Configure<JwtConfig>(Configuration.GetSection("Authentication:JwtBearer"));
            #endregion
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
            #region DI依赖注入配置。



            // 在ASP.NET Core中所有用到EF的Service 都需要注册成Scoped
            services.AddScoped<IRepositoryFactory, RepositoryFactory>();//泛型工厂
            services.AddScoped<IConcardContext, snblogContext>();//db
            services.AddScoped<IArticleService, ArticleService>();//ioc
            services.AddScoped<ISnNavigationService, SnNavigationService>();
            services.AddScoped<IArticleTagService,ArticleTagService>();
            services.AddScoped<IArticleTypeService, ArticleTypeService>();
            services.AddScoped<ISnOneService, SnOneService>();
            services.AddScoped<IVideoService, VideoService>();
            services.AddScoped<ISnVideoTypeService, SnVideoTypeService>();
            services.AddScoped<ISnUserTalkService, SnUserTalkService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISnOneTypeService, SnOneTypeService>();
            services.AddScoped<ISnPictureService, SnPictureService>();
            services.AddScoped<ISnPictureTypeService, SnPictureTypeService>();
            services.AddScoped<ISnTalkService, SnTalkService>();
            services.AddScoped<ISnTalkTypeService, SnTalkTypeService>();
            services.AddScoped<ISnNavigationTypeService, SnNavigationTypeService>();
            services.AddScoped<ISnleaveService, SnleaveService>();
            services.AddScoped<ICacheUtil, CacheUtil>();
            services.AddScoped<ISnNavigationTypeService, SnNavigationTypeService>();
            services.AddScoped<ISnInterfaceService, SnInterfaceService>();
            services.AddScoped<ISnSetBlogService, SnSetBlogService>();
            services.AddScoped<ISnippetService, SnippetService>();
            services.AddScoped<ISnippetTagService,SnippetTagService>();
            services.AddScoped<ISnippetTypeService,SnippetTypeService>();
            services.AddScoped<ISnippetLabelService,SnippetLabelService>();
            //缓存-整个应用程序生命周期以内只创建一个实例 
            services.AddSingleton<ICacheManager, CacheManager>();

            services.AddScoped<IReSnArticleService, ReSnArticleService>();
            //services.AddScoped<IService.IReService.IArticleTagService,ReSnLabelsService>();
            services.AddScoped<IReSnNavigationService, ReSnNavigationService>();
            services.AddScoped<HotNewsAngleSharp, HotNewsAngleSharp>();

            #endregion
            #region 实体映射

            //services.AddAutoMapper(typeof(MappingProfile));

            //自动化注册
            services.AddAutoMapper(
               Assembly.Load("Snblog.Enties").GetTypes()
                   .Where(t => t.FullName.EndsWith("Mapper"))
                   .ToArray()
           );
            #endregion
            services.AddControllers();

        }


        /// <summary>
        ///   运行时将调用此方法。 使用此方法来配置HTTP请求管道。
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //对于开发模式，一旦报错就跳转到错误堆栈页面
                app.UseDeveloperExceptionPage();
            }
            #region Swagger+性能分析（MiniProfiler）+自定义页面

            //激活UseMiniProfiler
            app.UseMiniProfiler();
            //可以将Swagger的UI页面配置在Configure的开发环境之中
            // 启用Swagger中间件
            app.UseSwagger();

            //配置SwaggerUI
            app.UseSwaggerUI(c =>
            {
                typeof(ApiVersion).GetEnumNames().ToList().ForEach(version =>
                {
                    c.IndexStream = () => GetType().GetTypeInfo()
                         .Assembly.GetManifestResourceStream("Snblog.index.html");
                    ////设置首页为Swagger
                    c.RoutePrefix = string.Empty;
                    //自定义页面 集成性能分析
                    c.SwaggerEndpoint($"/swagger/{version}/swagger.json", version);
                    ////设置为none可折叠所有方法
                    c.DocExpansion(DocExpansion.None);
                    ////设置为-1 可不显示models
                    // c.DefaultModelsExpandDepth(-1);
                });
            });
            #endregion
            app.UseHttpsRedirection();
            app.UseRouting();
            #region 开启Cors跨域请求中间件
            app.UseCors("AllRequests");
            #endregion
            #region 启用jwt
            app.UseAuthentication();
            app.UseAuthorization();
            #endregion
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
