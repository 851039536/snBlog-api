using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Scrutor;
using Snblog.Enties.Validator;
using Snblog.Jwt;
using Snblog.Service.AngleSharp;
using Snblog.Util.Exceptions;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using ArticleService = Snblog.Service.Service.Articles.ArticleService;

namespace Snblog;

/// <summary>
/// Startup
/// </summary>
public class Startup
{
    private IConfiguration Configuration { get; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="configuration"></param>
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    /// <summary>
    /// 运行时将调用此方法。 使用此方法将服务添加到容器。
    /// </summary>
    /// <param name="services"></param>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers().AddNewtonsoftJson(option =>
            //忽略循环引用
            option.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        );

        #region 限流

        services.AddRateLimiter(_ => _
            .AddFixedWindowLimiter(policyName: "fixed",options =>
            {
                options.PermitLimit = 3; //窗口阈值，即每个窗口时间范围内，最多允许的请求个数。这里指定为最多允许4个请求。该值必须 > 0
                options.Window = TimeSpan.FromSeconds(10); // 窗口大小，即时间长度。该值必须 > TimeSpan.Zero
                options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst; //排队请求的处理顺序。这里设置为优先处理先来的请求
                options.QueueLimit = 2; //当窗口请求数达到最大时，后续请求会进入排队，用于设置队列的大小（即允许几个请求在里面排队等待）
            }));

        #endregion

        #region MiniProfiler 性能分析

        services.AddMiniProfiler(options =>
            options.RouteBasePath = "/profiler"
        );

        #endregion

        #region Swagger服务

        services.AddSwaggerGen(c =>
        {
            //遍历版本信息
            typeof(ApiVersion).GetEnumNames().ToList().ForEach(version =>
            {
                c.SwaggerDoc(version,new OpenApiInfo
                {
                    Title = "SN Blog API", //标题
                    Description = "EFCore数据操作 ASP.NET Core Web API",
                    TermsOfService = new Uri("https://example.com/terms"), //服务条款
                    Contact = new OpenApiContact
                    {
                        Name = "Kai OuYang", //联系人
                        Email = string.Empty, //邮箱
                        Url = new Uri("https://twitter.com/spboyer"), //网站
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX", //协议
                        Url = new Uri("https://example.com/license"), //协议地址
                    }
                });
            });

            // 使用反射获取xml文件。并构造出文件的路径
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            // 获取xml文件的路径
            var filePath = Path.Combine(AppContext.BaseDirectory,xmlFile);
            // 启用xml注释. 该方法第二个参数启用控制器的注释，默认为false.
            c.IncludeXmlComments(filePath,true);

            // 使用反射获取xml文件。并构造出文件的路径
            var xmlModel = Path.Combine("Snblog.Enties.xml");
            // 获取xml文件的路径
            var modelPath = Path.Combine(AppContext.BaseDirectory,xmlModel);
            c.IncludeXmlComments(modelPath,true);
            // 可以解决相同类名会报错的问题
            c.CustomSchemaIds(type => type.FullName);

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
            c.AddSecurityDefinition("bearerAuth",securityScheme);
            c.AddSecurityRequirement(securityRequirement);

            #endregion
        });

        #endregion

        #region 数据库连接池

        services.AddDbContext<SnblogContext>(
            options => options
                .UseMySQL(Configuration.GetConnectionString("MysqlConnection") ?? string.Empty
                ));

        #endregion

        #region JWT身份授权

        services.ConfigureJwt(Configuration);
        //注入JWT配置文件
        services.Configure<JwtConfig>(Configuration.GetSection("Authentication:JwtBearer"));

        #endregion

        #region Cors跨域请求

        services.AddCors(c =>
        {
            c.AddPolicy("AllRequests",policy =>
            {
                policy
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

        #endregion

        #region DI依赖注入配置

        //scrutor的方式
        services.Scan(selector => selector
            //加载ArticleService 类所在的程序集 , 将加载ArticleService所在的程序集中所有的类都注册进去
            .FromAssemblyOf<ArticleService>()
            // 过滤程序集中需要注册的类，只选择那些类名以"Service"结尾的类型  
            .AddClasses(classes => classes.Where(t => t.Name.EndsWith("Service")))

            // 暴露匹配的接口，即注册服务为其实现的接口，且接口名称与服务类名称相同（除了后缀"Service"）
            .AsMatchingInterface()
            // 注释中提到了其他几种暴露服务的方式，它们分别是：  
            // .AsImplementedInterfaces() // 将服务注册为它实现的所有接口  
            // .As(t => t.GetInterfaces()) // 同样是将服务注册为它实现的所有接口，但提供了更多的灵活性  
            // .AsSelf() // 将服务注册为它自己，通常在没有接口的情况下使用  

            // 设置服务的生命周期为Scoped，意味着在同一个请求范围内，服务实例将被重用 
            .WithScopedLifetime()
            );



        services.AddScoped<DataBaseSql,DataBaseSql>();
        services.AddScoped<ServiceHelper,ServiceHelper>();

        //IValidator
        services.AddTransient<IValidator<Article>,ArticleValidator>();
        services.AddTransient<IValidator<Snippet>,SnippetValidator>();
        services.AddTransient<IValidator<UserTalk>,UserTalkValidator>();
        services.AddTransient<IValidator<PhotoGallery>,PhotoGalleryValidator>();

        //整个应用程序生命周期以内只创建一个实例 
        services.AddSingleton<ICacheManager,CacheManager>();
        services.AddSingleton<ICacheUtil,CacheUtils>();

        #endregion

        #region 实体映射

        //自动化注册
        services.AddAutoMapper(
            Assembly.Load("Snblog.Enties").GetTypes()
                .Where(t => t.FullName != null && t.FullName.EndsWith("Mapper"))
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
    public void Configure(IApplicationBuilder app,IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            //对于开发模式，一旦报错就跳转到错误堆栈页面
            app.UseDeveloperExceptionPage();
        } else
        {
            app.UseExceptionMiddleware();
        }

        app.UseRateLimiter();


        #region Swagger+性能分析（MiniProfiler）+自定义页面

        app.UseMiniProfiler();


        //启用Swagger中间件,可以将Swagger的UI页面配置在Configure的开发环境之中
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
                c.SwaggerEndpoint($"/swagger/{version}/swagger.json",version);
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

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}