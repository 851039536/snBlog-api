# Snblog

Snblog是一个功能丰富的博客后端API，它利用了最新的.NET 7技术和Entity Framework Core (EF Core)来构建高效、稳定的系统。本项目旨在提供一个全面的博客管理解决方案，包括用户管理、文章管理、评论系统、阅读排行、搜索功能以及权限控制等。

### 技术栈

- .NET 7: 利用.NET 7的强大性能和最新特性，确保应用的高效运行。
- MySQL: 作为主要的数据库管理系统，提供稳定的数据存储和高效的数据检索。
- 缓存: 使用缓存技术提高数据访问速度，优化用户体验。
- JWT (JSON Web Tokens): 用于安全的用户认证和授权机制。
- Quartz: 一个强大的开源作业调度库，用于定时任务管理。
- 数据加密: 确保用户数据的安全性，防止未授权访问。

### 功能实现

#### 用户管理
- 用户注册: 提供用户注册功能，确保新用户可以轻松加入。
- 用户登录: 安全的登录系统，保护用户账户安全。
- 修改密码: 允许用户更改密码，增强账户安全性。

文章管理
- 增删改查: 管理员可以对文章进行添加、删除、修改和查询操作。
- 文章分类管理: 对文章进行分类，方便管理和检索。
- 文章标签管理: 通过标签系统，增强文章的组织和搜索功能。

#### 评论管理
评论系统: 允许用户对文章进行评论，增加互动性。
#### 阅读排行
排行展示: 根据文章的阅读量进行排行，展示最受欢迎的文章。
搜索
#### 关键词搜索: 用户可以通过关键词快速找到相关文章。
#### 分页
数据分页: 对文章、评论等数据进行分页展示，优化浏览体验。
#### 权限管理
用户权限控制: 通过API实现精细的用户权限管理，确保系统的安全性。
#### 日记
日记功能: 允许用户记录个人日记，增加博客的个性化和私密性。
#### 代码片段
代码片段管理: 提供代码片段的存储和管理功能，方便开发者分享和使用。


