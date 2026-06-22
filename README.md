# Snblog

一个功能丰富的博客后端 API，基于 .NET 7 + EF Core + MySQL 构建，涵盖文章管理、用户管理、日记、代码片段、导航、相册、说说、视频、全文搜索、权限控制等模块。

---

## 技术栈

| 分类 | 技术 | 说明 |
|------|------|------|
| **运行时** | .NET 7.0 | ASP.NET Core Web API |
| **数据库** | MySQL 8.0 + Pomelo.EntityFrameworkCore.MySql 7.0 | ORM 数据访问 |
| **认证授权** | JWT Bearer + 动态权限策略 | 精细化用户授权 |
| **缓存** | MemoryCache + Redis (Infrastructure) | 多级缓存支持 |
| **对象映射** | AutoMapper 12.0 | 实体 ↔ DTO 转换 |
| **数据验证** | FluentValidation 11.3 | 请求参数校验 |
| **日志** | Serilog 2.12 | 结构化日志记录 |
| **API 文档** | Swashbuckle (Swagger) 6.5 | 在线 API 文档 + 调试 |
| **性能分析** | MiniProfiler 4.3 | 请求级别性能诊断 |
| **弹性策略** | Polly 8.4 | 自动重试与容错 |
| **DI 扫描** | Scrutor 4.2 | 程序集级依赖注入注册 |
| **健康检查** | Microsoft.Extensions.Diagnostics.HealthChecks | 数据库 + 外部服务 |
| **限流** | ASP.NET Core Rate Limiter (FixedWindow) | 接口请求频率控制 |
| **序列化** | Newtonsoft.Json | JSON 序列化（忽略循环引用） |
| **机器学习** | ML.NET 1.7 | 文本分类模型服务 |
| **代码质量** | Qodana (JetBrains) | 静态代码分析 |
| **部署** | Docker + Docker Compose | 容器化编排 |

---

## 项目架构

```
Snblog.sln
├── Snblog/                    # 主 Web API 项目 (net7.0)
│   ├── Controllers/           # 28 个 API 控制器（按模块分目录）
│   ├── Jwt/                   # JWT 认证 + 动态授权
│   ├── Startup.cs             # 服务注册 + 中间件管道
│   └── Program.cs             # 应用入口 + Serilog 配置
│
├── Snblog.Enties/             # 实体模型层 (net7.0)
│   ├── Models/                # 35 个数据库实体类
│   ├── ModelsDto/             # 16 个数据传输对象
│   ├── AutoMapper/            # 16 个实体映射配置
│   └── Validator/             # 4 个 FluentValidation 验证器
│
├── Snblog.IService/           # 服务接口层 (net7.0)
│   └── IService/              # 24 个接口定义
│
├── Snblog.Service/            # 服务实现层 (net7.0)
│   ├── Service/               # 24 个服务实现
│   ├── Extensions/            # 7 个扩展方法
│   └── Pollys/                # Polly 重试策略
│
├── Snblog.Cache/              # 缓存层 (net7.0)
│   └── CacheUtil/             # MemoryCache 缓存工具
│
├── Snblog.Util/               # 工具层 (net7.0)
│   ├── Exceptions/            # 全局异常中间件
│   ├── components/            # 通用组件
│   └── Paginated/             # 分页工具
│
├── Snblog.Infrastructure/     # 基础设施层 (netcoreapp3.1)
│   └── Redis + NWebsec        # Redis 缓存 + 安全组件
│
└── TextMLModel_WebApi1/       # ML.NET 文本模型服务 (net6.0)
    └── 独立的机器学习 API
```

### 依赖关系

```
Snblog → Snblog.Enties, Snblog.IService, Snblog.Service, Snblog.Util
Snblog.Service → Snblog.IService, Snblog.Cache, Snblog.Util
Snblog.IService → Snblog.Enties, Snblog.Util
Snblog.Util → Snblog.Enties
```

---

## 功能模块

### 文章管理 (Articles)
| 控制器 | 路由前缀 | 功能 |
|--------|----------|------|
| `ArticleController` | `article` | 文章增删改查、条件查询、分页、统计 |
| `ArticleTagController` | `articleTag` | 文章标签增删改查 |
| `ArticleTypeController` | `articleType` | 文章分类增删改查 |

### 用户管理 (Users)
| 控制器 | 路由前缀 | 功能 |
|--------|----------|------|
| `UserController` | `user` | 用户注册、登录、信息修改、好友管理 |

### 日记管理 (Diarys)
| 控制器 | 路由前缀 | 功能 |
|--------|----------|------|
| `DiaryController` | `diary` | 日记增删改查 |
| `DiaryTypeController` | `diaryType` | 日记分类管理 |

### 代码片段 (Snippets)
| 控制器 | 路由前缀 | 功能 |
|--------|----------|------|
| `SnippetController` | `snippet` | 片段增删改查 |
| `SnippetTagController` | `snippetTag` | 片段标签管理 |
| `SnippetTypeController` | `snippetType` | 片段分类管理 |
| `SnippetTypeSubController` | `snippetTypeSub` | 片段子分类管理 |
| `SnippetVersionController` | `snippetVersion` | 片段版本管理 |

### 导航管理 (Navigations)
| 控制器 | 路由前缀 | 功能 |
|--------|----------|------|
| `NavigationController` | `navigation` | 导航增删改查 |
| `NavigationTypeController` | `navigationType` | 导航分类管理 |

### 视频管理 (Videos)
| 控制器 | 路由前缀 | 功能 |
|--------|----------|------|
| `VideoController` | `video` | 视频增删改查 |

### 相册与图片 (Photo / Picture)
| 控制器 | 路由前缀 | 功能 |
|--------|----------|------|
| `PhotoGalleryController` | `photoGallery` | 相册增删改查 |
| `SnPictureController` | `snPicture` | 图片增删改查 |
| `SnPictureTypeController` | `snPictureType` | 图片分类管理 |

### 说说管理 (Talk)
| 控制器 | 路由前缀 | 功能 |
|--------|----------|------|
| `SnTalkController` | `snTalk` | 说说增删改查 |
| `SnTalkTypeController` | `snTalkType` | 说说分类管理 |
| `UserTalkController` | `userTalk` | 用户说说互动 |

### 博客设置 (SetBlog)
| 控制器 | 路由前缀 | 功能 |
|--------|----------|------|
| `SnSetBlogController` | `snSetBlog` | 博客全局配置管理 |

### 接口管理 (Interface)
| 控制器 | 路由前缀 | 功能 |
|--------|----------|------|
| `InterfaceController` | `interface` | API 接口元数据管理 |

### 视频分类 (SnVideoType)
| 控制器 | 路由前缀 | 功能 |
|--------|----------|------|
| `SnVideoTypeController` | `snVideoType` | 视频分类管理 |

### 全文搜索 (FullSearch)
| 控制器 | 路由前缀 | 功能 |
|--------|----------|------|
| `SearchController` | `search` | 全文关键词搜索 |

### 数据库管理 (DataBases)
| 控制器 | 路由前缀 | 功能 |
|--------|----------|------|
| `DataBaseController` | `dataBase` | 数据库备份操作 |

---

## 认证与安全

- **JWT 认证**: 基于 `Microsoft.AspNetCore.Authentication.JwtBearer`，配置 Issuer / Audience / SecretKey 三重校验
- **动态授权**: 自定义 `DynamicAuthorizationHandler` 实现运行时权限策略，策略通过 `JPermissions` 集中配置
- **限流**: FixedWindow 策略，每 10 秒窗口最多 3 个请求，排队上限 2 个
- **CORS**: 开放跨域策略 `AllRequests`（AllowAnyOrigin / AllowAnyMethod / AllowAnyHeader）

---

## 中间件管道

```
DeveloperExceptionPage / ExceptionMiddleware
→ HealthChecks (/health)
→ RateLimiter
→ MiniProfiler (/profiler)
→ Swagger + SwaggerUI
→ HttpsRedirection
→ Routing
→ CORS
→ Authentication
→ Authorization
→ Endpoints (MapControllers)
```

---

## 安装与运行

### 环境要求

- .NET SDK 7.0
- MySQL 8.0+
- Docker & Docker Compose（可选）

### 手动运行

```bash
# 1. 克隆仓库
git clone <repository-url>
cd Snblog

# 2. 配置数据库连接（appsettings.json）
# 修改 ConnectionStrings.MysqlConnection

# 3. 还原依赖
dotnet restore

# 4. 运行
dotnet run --project Snblog
```

### Docker 部署

```bash
# 一键构建并启动（含 MySQL）
docker-compose up -d --build

# 停止服务
docker-compose down
```

服务端口映射：
- API: `http://localhost:8088`
- MySQL: `localhost:3307`

### Swagger API 文档

启动后在浏览器访问 `http://localhost:8088` 即可查看 Swagger UI。

---

## 常用命令

| 命令 | 说明 |
|------|------|
| `dotnet restore` | 还原 NuGet 包 |
| `dotnet build` | 构建解决方案 |
| `dotnet run --project Snblog` | 运行 Web API |
| `dotnet ef migrations add <Name>` | 添加 EF Core 迁移 |
| `dotnet ef database update` | 应用数据库迁移 |
| `docker-compose up -d --build` | Docker 构建并启动 |
| `docker-compose down` | 停止并移除容器 |

---

## 代码质量

使用 JetBrains Qodana 进行静态代码分析，配置文件 `qodana.yaml`:

```yaml
linter: jetbrains/qodana-dotnet:latest
profile: qodana.starter
```

---

## 项目说明

| 项目 | 目标框架 | 说明 |
|------|----------|------|
| `Snblog` | net7.0 | 主 Web API，ASP.NET Core |
| `Snblog.Enties` | net7.0 | 实体 / DTO / AutoMapper / FluentValidation |
| `Snblog.IService` | net7.0 | 服务接口定义 |
| `Snblog.Service` | net7.0 | 服务实现 + Polly 重试 + 扩展方法 |
| `Snblog.Cache` | net7.0 | MemoryCache 缓存层 |
| `Snblog.Util` | net7.0 | 通用工具（分页、异常中间件） |
| `Snblog.Infrastructure` | netcoreapp3.1 | Redis + NWebsec 安全组件 |
| `TextMLModel_WebApi1` | net6.0 | ML.NET 文本分类模型独立服务 |

