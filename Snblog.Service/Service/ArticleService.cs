using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Snblog.Cache.CacheUtil;
using Snblog.Enties.Models;
using Snblog.Enties.ModelsDto;
using Snblog.IService.IService;
using Snblog.Repository.Repository;
using Snblog.Util.components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snblog.Service.Service
    {
    public class ArticleService : IArticleService
    {
        private readonly snblogContext _service;
        private readonly CacheUtil _cacheutil;
        private readonly ILogger<ArticleService> _logger;
        private readonly Res<Article> res = new();
        private readonly Dto<ArticleDto> resDto = new();
        private readonly IMapper _mapper;


        const string NAME = "article_";
        const string BYID = "BYID_";
        const string SUM = "SUM_";
        const string CONTAINS = "CONTAINS_";
        const string PAGING = "PAGING_";
        const string ALL = "ALL_";
        const string DEL = "DEL_";
        const string ADD = "ADD_";
        const string UP = "UP_";
        public ArticleService(ICacheUtil cacheUtil, snblogContext coreDbContext, ILogger<ArticleService> logger, IMapper mapper)
        {
            _service = coreDbContext;
            _cacheutil = (CacheUtil)cacheUtil;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _logger.LogInformation($"{NAME}{DEL}{id}");
            Article reslult = await _service.Articles.FindAsync(id);
            if (reslult == null)return false;
            _service.Articles.Remove(reslult);//删除单个
            _service.Remove(reslult);//直接在context上Remove()方法传入model，它会判断类型
            return await _service.SaveChangesAsync() > 0;
        }
        public async Task<ArticleDto> GetByIdAsync(int id, bool cache)
        {
            _logger.LogInformation($"{NAME}{BYID}{id}_{cache}");
            resDto.entityList = _cacheutil.CacheString($"{NAME}{BYID}{id}{cache}", resDto.entityList, cache);
            if (resDto.entityList == null)
            {
                resDto.entity = _mapper.Map<ArticleDto>(await _service.Articles.Include(i => i.User).Include(i => i.Type).Include(i => i.Tag).AsNoTracking().SingleOrDefaultAsync(b => b.Id == id));
                _cacheutil.CacheString($"{NAME}{BYID}{id}_{cache}", resDto.entity, cache);
            }
            return resDto.entity;
        }
        public async Task<List<ArticleDto>> GetTypeAsync(int identity, string type, bool cache)
        {
            _logger.LogInformation(message: $"Article条件查询=>{type} 缓存:{cache}");
            resDto.entityList = _cacheutil.CacheString("GetTypeAsync_SnArticle" + identity + type + cache, resDto.entityList, cache);
            if (resDto.entityList == null)
            {
                if (identity == 1)
                {
                    resDto.entityList = _mapper.Map<List<ArticleDto>>(await _service.Articles.Where(s => s.Type.Name == type).AsNoTracking().ToListAsync());
                }
                else
                {
                    resDto.entityList = _mapper.Map<List<ArticleDto>>(await _service.Articles.Where(s => s.Tag.Name == type).AsNoTracking().ToListAsync());
                }

                _cacheutil.CacheString("GetTypeAsync_SnArticle" + identity + type + cache, resDto.entityList, cache);
            }
            return resDto.entityList;
        }


        public async Task<bool> AddAsync(Article entity)
        {
            _logger.LogInformation($"{NAME}{ADD}{entity}");
            entity.TimeCreate = DateTime.Now;
            entity.TimeModified = DateTime.Now;
            await _service.Articles.AddAsync(entity);
            return await _service.SaveChangesAsync() > 0;
        }
        public async Task<bool> UpdateAsync(Article entity)
        {
            _logger.LogInformation($"{NAME}{UP}{entity}");
            entity.TimeModified = DateTime.Now; //更新时间
            var res = await _service.Articles.Where(w => w.Id == entity.Id).Select(
                s => new
                {
                    s.TimeCreate,
                }
                ).AsNoTracking().ToListAsync();
            entity.TimeCreate = res[0].TimeCreate;  //赋值表示更新时间不变
            _service.Articles.Update(entity);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<int> GetSumAsync(int identity, string type, bool cache)
        {
            _logger.LogInformation($"{NAME}{SUM}{identity}_{cache}");
            res.entityInt = _cacheutil.CacheNumber($"{NAME}{SUM}{identity}{type}{cache}", res.entityInt, cache);
            if (res.entityInt == 0)
            {
                switch (identity)
                {
                    case 0:
                        return await _service.Articles.AsNoTracking().CountAsync();
                    case 1:
                        return await _service.Articles.AsNoTracking().CountAsync(c => c.Type.Name == type);
                    case 2:
                        return await _service.Articles.AsNoTracking().CountAsync(c => c.Tag.Name == type);
                    case 3:
                        return await _service.Articles.AsNoTracking().CountAsync(c => c.User.Name == type);
                }
                _cacheutil.CacheNumber($"{NAME}{SUM}{identity}{type}{cache}", res.entityInt, cache);
            }
            return -1;
        }
        public async Task<List<ArticleDto>> GetAllAsync(bool cache)
        {
            _logger.LogInformation($"{NAME}{ALL}{cache}");
            resDto.entityList = _cacheutil.CacheString($"{NAME}{ALL}{cache}", resDto.entityList, cache);
            if (resDto.entityList == null)
            {
                resDto.entityList = _mapper.Map<List<ArticleDto>>(await _service.Articles.Include(i => i.User).Include(i => i.Tag).Include(i => i.Type).AsNoTracking().ToListAsync());
                _cacheutil.CacheString($"{NAME}{ALL}{cache}", resDto.entityList, cache);
            }
            return resDto.entityList;
        }

        public async Task<int> GetStrSumAsync(int identity, int type, string name, bool cache)
        {
            _logger.LogInformation($"Article统计{identity}_{type}_{cache}");
            res.entityInt = _cacheutil.CacheNumber($"GetStrSumAsync{identity}_{type}_{name}_{cache}", res.entityInt, cache);
            if (res.entityInt == 0)
            {
                switch (identity)
                {
                    case 0:
                        res.entityInt = await GetSum(type);
                        break;
                    case 1:
                        res.entityInt = await GetTypeSum(type, name);
                        break;
                    case 2:
                        res.entityInt = await GetTagSum(type, name);
                        break;
                    case 3:
                        res.entityInt = await GetUserSum(type, name);
                        break;
                }
                _cacheutil.CacheNumber($"GetStrSumAsync{identity}_{type}_{name}_{cache}", res.entityInt, cache);
            }
            return res.entityInt;
        }
        private async Task<int> GetUserSum(int type, string name)
        {
            int num = 0;
            switch (type) //按类型查询
            {
                case 2:
                    List<short> read = await _service.Articles.Where(w => w.User.Name == name).Select(c => c.Read).ToListAsync();
                    foreach (short i in read)
                    {
                        num += i;
                    }
                    break;
                case 1:
                    List<string> text = await _service.Articles.Where(w => w.User.Name == name).Select(c => c.Text).ToListAsync();
                    for (int i = 0; i < text.Count; i++)
                    {
                        num += text[i].Length;
                    }
                    break;
                case 3:
                    List<short> give = await _service.Articles.Where(w => w.User.Name == name).Select(c => c.Give).ToListAsync();
                    foreach (short i in give)
                    {
                        num += i;
                    }
                    break;
            }

            return num;
        }
        private async Task<int> GetTagSum(int type, string name)
        {
            int num = 0;
            switch (type) //按类型查询
            {
                case 2:
                    List<short> read = await _service.Articles.Where(w => w.Tag.Name == name).Select(c => c.Read).ToListAsync();
                    foreach (short i in read)
                    {
                        num += i;
                    }
                    break;
                case 1:
                    List<string> text = await _service.Articles.Where(w => w.Tag.Name == name).Select(c => c.Text).ToListAsync();
                    for (int i = 0; i < text.Count; i++)
                    {
                        num += text[i].Length;
                    }
                    break;
                case 3:
                    List<short> give = await _service.Articles.Where(w => w.Tag.Name == name).Select(c => c.Give).ToListAsync();
                    foreach (short i in give)
                    {
                        num += i;
                    }
                    break;
            }

            return num;
        }
        private async Task<int> GetTypeSum(int type, string name)
        {
            int num = 0;
            switch (type) //按类型查询
            {
                case 2:
                    List<short> read = await _service.Articles.Where(w => w.Type.Name == name).Select(c => c.Read).ToListAsync();
                    foreach (short i in read)
                    {
                        num += i;
                    }
                    break;
                case 1:
                    List<string> text = await _service.Articles.Where(w => w.Type.Name == name).Select(c => c.Text).ToListAsync();
                    for (int i = 0; i < text.Count; i++)
                    {
                        num += text[i].Length;
                    }
                    break;
                case 3:
                    List<short> give = await _service.Articles.Where(w => w.Type.Name == name).Select(c => c.Give).ToListAsync();
                    foreach (short i in give)
                    {
                        num += i;
                    }
                    break;
            }

            return num;
        }
        private async Task<int> GetSum(int type)
        {
            int num = 0;
            switch (type) 
            {
                case 2:
                    List<short> read = await _service.Articles.Select(c => c.Read).ToListAsync();
                    foreach (short i in read)
                    {
                        num += i;
                    }
                    break;
                case 1:
                    List<string> text = await _service.Articles.Select(c => c.Text).ToListAsync();
                    for (int i = 0; i < text.Count; i++)
                    {
                        num += text[i].Length;
                    }
                    break;
                case 3:
                    List<short> give = await _service.Articles.Select(c => c.Give).ToListAsync();
                    foreach (short i in give)
                    {
                        num += i;
                    }
                    break;
            }
            return num;
        }

        public async Task<List<ArticleDto>> GetPagingAsync(int identity, string type, int pageIndex, int pageSize, string ordering, bool isDesc, bool cache)
        {
            _logger.LogInformation($"{NAME}{PAGING}{identity}_{type}_{pageIndex}_{pageSize}_{ordering}_{isDesc}_{cache}");
            resDto.entityList = _cacheutil.CacheString($"{NAME}{PAGING}{identity}_{type}_{pageIndex}_{pageSize}_{ordering}_{isDesc}_{cache}", resDto.entityList, cache);
            if (resDto.entityList == null)
            {
                switch (identity)
                {
                    case 0:
                        await GetFyAll(pageIndex, pageSize, ordering, isDesc);
                        break;

                    case 1:
                        await GetFyType(type, pageIndex, pageSize, ordering, isDesc);
                        break;

                    case 2:
                        await GetFyTag(type, pageIndex, pageSize, ordering, isDesc);
                        break;
                    case 3:
                        await GetFyUser(type, pageIndex, pageSize, ordering, isDesc);
                        break;
                    case 4:
                        await GetFyUserTag(type, pageIndex, pageSize, ordering, isDesc);
                        break;
                }
                _cacheutil.CacheString($"{NAME}{PAGING}{identity}_{type}_{pageIndex}_{pageSize}_{ordering}_{isDesc}_{cache}", resDto.entityList, cache);
            }
            return resDto.entityList;
        }
        private async Task GetFyUserTag(string type, int pageIndex, int pageSize, string ordering, bool isDesc)
        {
            resDto.name = type.Split(',');
            if (isDesc)//降序
            {
                switch (ordering) //排序
                {
                    case "id":
                        resDto.entityList = _mapper.Map<List<ArticleDto>>(await _service.Articles.Where(w => w.Tag.Name == resDto.name[0]
                        && w.User.Name == resDto.name[1])
                .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).Select(e => new ArticleDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Sketch = e.Sketch,
                    Give = e.Give,
                    Read = e.Read,
                    Img = e.Img,
                    TimeCreate = e.TimeCreate,
                    TimeModified = e.TimeModified,
                    User = e.User,
                    Type = e.Type,
                    Tag = e.Tag
                }).AsNoTracking().ToListAsync());
                        break;
                    case "data":
                        resDto.entityList = _mapper.Map<List<ArticleDto>>(await _service.Articles.Where(w => w.Tag.Name == resDto.name[0]
                       && w.User.Name == resDto.name[1])
              .OrderByDescending(c => c.TimeCreate).Skip((pageIndex - 1) * pageSize)
               .Take(pageSize).Select(e => new ArticleDto
               {
                   Id = e.Id,
                   Name = e.Name,
                   Sketch = e.Sketch,
                   Give = e.Give,
                   Read = e.Read,
                   Img = e.Img,
                   TimeCreate = e.TimeCreate,
                   TimeModified = e.TimeModified,
                   User = e.User,
                   Type = e.Type,
                   Tag = e.Tag
               }).AsNoTracking().ToListAsync());
                        break;
                    case "read":
                        resDto.entityList = _mapper.Map<List<ArticleDto>>(await _service.Articles.Where(w => w.Tag.Name == resDto.name[0]
                      && w.User.Name == resDto.name[1])
             .OrderByDescending(c => c.Read).Skip((pageIndex - 1) * pageSize)
               .Take(pageSize).Select(e => new ArticleDto
               {
                   Id = e.Id,
                   Name = e.Name,
                   Sketch = e.Sketch,
                   Give = e.Give,
                   Read = e.Read,
                   Img = e.Img,
                   TimeCreate = e.TimeCreate,
                   TimeModified = e.TimeModified,
                   User = e.User,
                   Type = e.Type,
                   Tag = e.Tag
               }).AsNoTracking().ToListAsync());
                        break;
                    case "give":
                        resDto.entityList = _mapper.Map<List<ArticleDto>>(await _service.Articles.Where(w => w.Tag.Name == resDto.name[0]
                       && w.User.Name == resDto.name[1])
              .OrderByDescending(c => c.Give).Skip((pageIndex - 1) * pageSize)
               .Take(pageSize).Select(e => new ArticleDto
               {
                   Id = e.Id,
                   Name = e.Name,
                   Sketch = e.Sketch,
                   Give = e.Give,
                   Read = e.Read,
                   Img = e.Img,
                   TimeCreate = e.TimeCreate,
                   TimeModified = e.TimeModified,
                   User = e.User,
                   Type = e.Type,
                   Tag = e.Tag
               }).AsNoTracking().ToListAsync());
                        break;
                }
            }
            else //升序
            {
                switch (ordering) //排序
                {
                    case "id":
                        resDto.entityList = _mapper.Map<List<ArticleDto>>(await _service.Articles.Where(w => w.Tag.Name == resDto.name[0]
                      && w.User.Name == resDto.name[1])
             .OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
               .Take(pageSize).Select(e => new ArticleDto
               {
                   Id = e.Id,
                   Name = e.Name,
                   Sketch = e.Sketch,
                   Give = e.Give,
                   Read = e.Read,
                   Img = e.Img,
                   TimeCreate = e.TimeCreate,
                   TimeModified = e.TimeModified,
                   User = e.User,
                   Type = e.Type,
                   Tag = e.Tag
               }).AsNoTracking().ToListAsync());
                        break;
                    case "data":
                        resDto.entityList = _mapper.Map<List<ArticleDto>>(await _service.Articles.Where(w => w.Tag.Name == resDto.name[0]
                      && w.User.Name == resDto.name[1])
             .OrderBy(c => c.TimeCreate).Skip((pageIndex - 1) * pageSize)
               .Take(pageSize).Select(e => new ArticleDto
               {
                   Id = e.Id,
                   Name = e.Name,
                   Sketch = e.Sketch,
                   Give = e.Give,
                   Read = e.Read,
                   Img = e.Img,
                   TimeCreate = e.TimeCreate,
                   TimeModified = e.TimeModified,
                   User = e.User,
                   Type = e.Type,
                   Tag = e.Tag
               }).AsNoTracking().ToListAsync());
                        break;
                    case "read":
                        resDto.entityList = _mapper.Map<List<ArticleDto>>(await _service.Articles.Where(w => w.Tag.Name == resDto.name[0]
                      && w.User.Name == resDto.name[1])
             .OrderBy(c => c.Read).Skip((pageIndex - 1) * pageSize)
               .Take(pageSize).Select(e => new ArticleDto
               {
                   Id = e.Id,
                   Name = e.Name,
                   Sketch = e.Sketch,
                   Give = e.Give,
                   Read = e.Read,
                   Img = e.Img,
                   TimeCreate = e.TimeCreate,
                   TimeModified = e.TimeModified,
                   User = e.User,
                   Type = e.Type,
                   Tag = e.Tag
               }).AsNoTracking().ToListAsync());
                        break;
                    case "give":
                        resDto.entityList = _mapper.Map<List<ArticleDto>>(await _service.Articles.Where(w => w.Tag.Name == resDto.name[0]
                      && w.User.Name == resDto.name[1])
             .OrderBy(c => c.Give).Skip((pageIndex - 1) * pageSize)
               .Take(pageSize).Select(e => new ArticleDto
               {
                   Id = e.Id,
                   Name = e.Name,
                   Sketch = e.Sketch,
                   Give = e.Give,
                   Read = e.Read,
                   Img = e.Img,
                   TimeCreate = e.TimeCreate,
                   TimeModified = e.TimeModified,
                   User = e.User,
                   Type = e.Type,
                   Tag = e.Tag
               }).AsNoTracking().ToListAsync());
                        break;
                }
            }
        }
        private async Task GetFyUser(string type, int pageIndex, int pageSize, string ordering, bool isDesc)
        {
            if (isDesc)//降序
            {
                switch (ordering) //排序
                {
                    case "id":
                        resDto.entityList = _mapper.Map<List<ArticleDto>>(await _service.Articles.Where(w => w.User.Name == type)
                .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).Select(e => new ArticleDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Sketch = e.Sketch,
                    Give = e.Give,
                    Read = e.Read,
                    Img = e.Img,
                    TimeCreate = e.TimeCreate,
                    TimeModified = e.TimeModified,
                    User = e.User,
                    Type = e.Type,
                    Tag = e.Tag
                }).AsNoTracking().ToListAsync());
                        break;
                    case "data":
                        resDto.entityList = _mapper.Map<List<ArticleDto>>(await _service.Articles.Where(w => w.User.Name == type)
               .OrderByDescending(c => c.TimeCreate).Skip((pageIndex - 1) * pageSize)
               .Take(pageSize).Select(e => new ArticleDto
               {
                   Id = e.Id,
                   Name = e.Name,
                   Sketch = e.Sketch,
                   Give = e.Give,
                   Read = e.Read,
                   Img = e.Img,
                   TimeCreate = e.TimeCreate,
                   TimeModified = e.TimeModified,
                   User = e.User,
                   Type = e.Type,
                   Tag = e.Tag
               }).AsNoTracking().ToListAsync());
                        break;
                    case "read":
                        resDto.entityList = _mapper.Map<List<ArticleDto>>(await _service.Articles.Where(w => w.User.Name == type)
               .OrderByDescending(c => c.Read).Skip((pageIndex - 1) * pageSize)
               .Take(pageSize).Select(e => new ArticleDto
               {
                   Id = e.Id,
                   Name = e.Name,
                   Sketch = e.Sketch,
                   Give = e.Give,
                   Read = e.Read,
                   Img = e.Img,
                   TimeCreate = e.TimeCreate,
                   TimeModified = e.TimeModified,
                   User = e.User,
                   Type = e.Type,
                   Tag = e.Tag
               }).AsNoTracking().ToListAsync());
                        break;
                    case "give":
                        resDto.entityList = _mapper.Map<List<ArticleDto>>(await _service.Articles.Where(w => w.User.Name == type)
               .OrderByDescending(c => c.Give).Skip((pageIndex - 1) * pageSize)
               .Take(pageSize).Select(e => new ArticleDto
               {
                   Id = e.Id,
                   Name = e.Name,
                   Sketch = e.Sketch,
                   Give = e.Give,
                   Read = e.Read,
                   Img = e.Img,
                   TimeCreate = e.TimeCreate,
                   TimeModified = e.TimeModified,
                   User = e.User,
                   Type = e.Type,
                   Tag = e.Tag
               }).AsNoTracking().ToListAsync());
                        break;
                }
            }
            else //升序
            {
                switch (ordering) //排序
                {
                    case "id":
                        resDto.entityList = _mapper.Map<List<ArticleDto>>(await _service.Articles.Where(w => w.User.Name == type)
               .OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
               .Take(pageSize).Select(e => new ArticleDto
               {
                   Id = e.Id,
                   Name = e.Name,
                   Sketch = e.Sketch,
                   Give = e.Give,
                   Read = e.Read,
                   Img = e.Img,
                   TimeCreate = e.TimeCreate,
                   TimeModified = e.TimeModified,
                   User = e.User,
                   Type = e.Type,
                   Tag = e.Tag
               }).AsNoTracking().ToListAsync());
                        break;
                    case "data":
                        resDto.entityList = _mapper.Map<List<ArticleDto>>(await _service.Articles.Where(w => w.User.Name == type)
               .OrderBy(c => c.TimeCreate).Skip((pageIndex - 1) * pageSize)
               .Take(pageSize).Select(e => new ArticleDto
               {
                   Id = e.Id,
                   Name = e.Name,
                   Sketch = e.Sketch,
                   Give = e.Give,
                   Read = e.Read,
                   Img = e.Img,
                   TimeCreate = e.TimeCreate,
                   TimeModified = e.TimeModified,
                   User = e.User,
                   Type = e.Type,
                   Tag = e.Tag
               }).AsNoTracking().ToListAsync());
                        break;
                    case "read":
                        resDto.entityList = _mapper.Map<List<ArticleDto>>(await _service.Articles.Where(w => w.User.Name == type)
               .OrderBy(c => c.Read).Skip((pageIndex - 1) * pageSize)
               .Take(pageSize).Select(e => new ArticleDto
               {
                   Id = e.Id,
                   Name = e.Name,
                   Sketch = e.Sketch,
                   Give = e.Give,
                   Read = e.Read,
                   Img = e.Img,
                   TimeCreate = e.TimeCreate,
                   TimeModified = e.TimeModified,
                   User = e.User,
                   Type = e.Type,
                   Tag = e.Tag
               }).AsNoTracking().ToListAsync());
                        break;
                    case "give":
                        resDto.entityList = _mapper.Map<List<ArticleDto>>(await _service.Articles.Where(w => w.User.Name == type)
               .OrderBy(c => c.Give).Skip((pageIndex - 1) * pageSize)
               .Take(pageSize).Select(e => new ArticleDto
               {
                   Id = e.Id,
                   Name = e.Name,
                   Sketch = e.Sketch,
                   Give = e.Give,
                   Read = e.Read,
                   Img = e.Img,
                   TimeCreate = e.TimeCreate,
                   TimeModified = e.TimeModified,
                   User = e.User,
                   Type = e.Type,
                   Tag = e.Tag
               }).AsNoTracking().ToListAsync());
                        break;
                }
            }
        }
        private async Task GetFyTag(string type, int pageIndex, int pageSize, string ordering, bool isDesc)
        {
            if (isDesc)//降序
            {
                switch (ordering) //排序
                {
                    case "id":
                        resDto.entityList = _mapper.Map<List<ArticleDto>>(await _service.Articles.Where(w => w.Tag.Name == type)
                .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).Select(e => new ArticleDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Sketch = e.Sketch,
                    Give = e.Give,
                    Read = e.Read,
                    Img = e.Img,
                    TimeCreate = e.TimeCreate,
                    TimeModified = e.TimeModified,
                    User = e.User,
                    Type = e.Type,
                    Tag = e.Tag
                }).AsNoTracking().ToListAsync());
                        break;
                    case "data":
                        resDto.entityList = _mapper.Map<List<ArticleDto>>(await _service.Articles.Where(w => w.Tag.Name == type)
               .OrderByDescending(c => c.TimeCreate).Skip((pageIndex - 1) * pageSize)
               .Take(pageSize).Select(e => new ArticleDto
               {
                   Id = e.Id,
                   Name = e.Name,
                   Sketch = e.Sketch,
                   Give = e.Give,
                   Read = e.Read,
                   Img = e.Img,
                   TimeCreate = e.TimeCreate,
                   TimeModified = e.TimeModified,
                   User = e.User,
                   Type = e.Type,
                   Tag = e.Tag
               }).AsNoTracking().ToListAsync());
                        break;
                    case "read":
                        resDto.entityList = _mapper.Map<List<ArticleDto>>(await _service.Articles.Where(w => w.Tag.Name == type)
               .OrderByDescending(c => c.Read).Skip((pageIndex - 1) * pageSize)
               .Take(pageSize).Select(e => new ArticleDto
               {
                   Id = e.Id,
                   Name = e.Name,
                   Sketch = e.Sketch,
                   Give = e.Give,
                   Read = e.Read,
                   Img = e.Img,
                   TimeCreate = e.TimeCreate,
                   TimeModified = e.TimeModified,
                   User = e.User,
                   Type = e.Type,
                   Tag = e.Tag
               }).AsNoTracking().ToListAsync());
                        break;
                    case "give":
                        resDto.entityList = _mapper.Map<List<ArticleDto>>(await _service.Articles.Where(w => w.Tag.Name == type)
               .OrderByDescending(c => c.Give).Skip((pageIndex - 1) * pageSize)
               .Take(pageSize).Select(e => new ArticleDto
               {
                   Id = e.Id,
                   Name = e.Name,
                   Sketch = e.Sketch,
                   Give = e.Give,
                   Read = e.Read,
                   Img = e.Img,
                   TimeCreate = e.TimeCreate,
                   TimeModified = e.TimeModified,
                   User = e.User,
                   Type = e.Type,
                   Tag = e.Tag
               }).AsNoTracking().ToListAsync());
                        break;
                }
            }
            else //升序
            {
                switch (ordering) //排序
                {
                    case "id":
                        resDto.entityList = _mapper.Map<List<ArticleDto>>(await _service.Articles.Where(w => w.Tag.Name == type)
               .OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
               .Take(pageSize).Select(e => new ArticleDto
               {
                   Id = e.Id,
                   Name = e.Name,
                   Sketch = e.Sketch,
                   Give = e.Give,
                   Read = e.Read,
                   Img = e.Img,
                   TimeCreate = e.TimeCreate,
                   TimeModified = e.TimeModified,
                   User = e.User,
                   Type = e.Type,
                   Tag = e.Tag
               }).AsNoTracking().ToListAsync());
                        break;
                    case "data":
                        resDto.entityList = _mapper.Map<List<ArticleDto>>(await _service.Articles.Where(w => w.Tag.Name == type)
               .OrderBy(c => c.TimeCreate).Skip((pageIndex - 1) * pageSize)
               .Take(pageSize).Select(e => new ArticleDto
               {
                   Id = e.Id,
                   Name = e.Name,
                   Sketch = e.Sketch,
                   Give = e.Give,
                   Read = e.Read,
                   Img = e.Img,
                   TimeCreate = e.TimeCreate,
                   TimeModified = e.TimeModified,
                   User = e.User,
                   Type = e.Type,
                   Tag = e.Tag
               }).AsNoTracking().ToListAsync());
                        break;
                    case "read":
                        resDto.entityList = _mapper.Map<List<ArticleDto>>(await _service.Articles.Where(w => w.Tag.Name == type)
               .OrderBy(c => c.Read).Skip((pageIndex - 1) * pageSize)
               .Take(pageSize).Select(e => new ArticleDto
               {
                   Id = e.Id,
                   Name = e.Name,
                   Sketch = e.Sketch,
                   Give = e.Give,
                   Read = e.Read,
                   Img = e.Img,
                   TimeCreate = e.TimeCreate,
                   TimeModified = e.TimeModified,
                   User = e.User,
                   Type = e.Type,
                   Tag = e.Tag
               }).AsNoTracking().ToListAsync());
                        break;
                    case "give":
                        resDto.entityList = _mapper.Map<List<ArticleDto>>(await _service.Articles.Where(w => w.Tag.Name == type)
               .OrderBy(c => c.Give).Skip((pageIndex - 1) * pageSize)
               .Take(pageSize).Select(e => new ArticleDto
               {
                   Id = e.Id,
                   Name = e.Name,
                   Sketch = e.Sketch,
                   Give = e.Give,
                   Read = e.Read,
                   Img = e.Img,
                   TimeCreate = e.TimeCreate,
                   TimeModified = e.TimeModified,
                   User = e.User,
                   Type = e.Type,
                   Tag = e.Tag
               }).AsNoTracking().ToListAsync());
                        break;
                }
            }
        }
        private async Task GetFyType(string type, int pageIndex, int pageSize, string ordering, bool isDesc)
        {
            if (isDesc)//降序
            {
                switch (ordering) //排序
                {
                    case "id":
                        resDto.entityList = _mapper.Map<List<ArticleDto>>(await _service.Articles.Where(w => w.Type.Name == type)
                .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).Select(e => new ArticleDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Sketch = e.Sketch,
                    Give = e.Give,
                    Read = e.Read,
                    Img = e.Img,
                    TimeCreate = e.TimeCreate,
                    TimeModified = e.TimeModified,
                    User = e.User,
                    Type = e.Type,
                    Tag = e.Tag
                }).AsNoTracking().ToListAsync());
                        break;
                    case "data":
                        resDto.entityList = _mapper.Map<List<ArticleDto>>(await _service.Articles.Where(w => w.Type.Name == type)
                .OrderByDescending(c => c.TimeCreate).Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).Select(e => new ArticleDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Sketch = e.Sketch,
                    Give = e.Give,
                    Read = e.Read,
                    Img = e.Img,
                    TimeCreate = e.TimeCreate,
                    TimeModified = e.TimeModified,
                    User = e.User,
                    Type = e.Type,
                    Tag = e.Tag
                }).AsNoTracking().ToListAsync());
                        break;
                    case "read":
                        resDto.entityList = _mapper.Map<List<ArticleDto>>(await _service.Articles.Where(w => w.Type.Name == type)
                .OrderByDescending(c => c.Read).Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).Select(e => new ArticleDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Sketch = e.Sketch,
                    Give = e.Give,
                    Read = e.Read,
                    Img = e.Img,
                    TimeCreate = e.TimeCreate,
                    TimeModified = e.TimeModified,
                    User = e.User,
                    Type = e.Type,
                    Tag = e.Tag
                }).AsNoTracking().ToListAsync());
                        break;
                    case "give":
                        resDto.entityList = _mapper.Map<List<ArticleDto>>(await _service.Articles.Where(w => w.Type.Name == type)
                .OrderByDescending(c => c.Give).Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).Select(e => new ArticleDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Sketch = e.Sketch,
                    Give = e.Give,
                    Read = e.Read,
                    Img = e.Img,
                    TimeCreate = e.TimeCreate,
                    TimeModified = e.TimeModified,
                    User = e.User,
                    Type = e.Type,
                    Tag = e.Tag
                }).AsNoTracking().ToListAsync());
                        break;
                }
            }
            else //升序
            {
                switch (ordering) //排序
                {
                    case "id":
                        resDto.entityList = _mapper.Map<List<ArticleDto>>(await _service.Articles.Where(w => w.Type.Name == type)
                .OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).Select(e => new ArticleDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Sketch = e.Sketch,
                    Give = e.Give,
                    Read = e.Read,
                    Img = e.Img,
                    TimeCreate = e.TimeCreate,
                    TimeModified = e.TimeModified,
                    User = e.User,
                    Type = e.Type,
                    Tag = e.Tag
                }).AsNoTracking().ToListAsync());
                        break;
                    case "data":
                        resDto.entityList = _mapper.Map<List<ArticleDto>>(await _service.Articles.Where(w => w.Type.Name == type)
                .OrderBy(c => c.TimeCreate).Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).Select(e => new ArticleDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Sketch = e.Sketch,
                    Give = e.Give,
                    Read = e.Read,
                    Img = e.Img,
                    TimeCreate = e.TimeCreate,
                    TimeModified = e.TimeModified,
                    User = e.User,
                    Type = e.Type,
                    Tag = e.Tag
                }).AsNoTracking().ToListAsync());
                        break;
                    case "read":
                        resDto.entityList = _mapper.Map<List<ArticleDto>>(await _service.Articles.Where(w => w.Type.Name == type)
                .OrderBy(c => c.Read).Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).Select(e => new ArticleDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Sketch = e.Sketch,
                    Give = e.Give,
                    Read = e.Read,
                    Img = e.Img,
                    TimeCreate = e.TimeCreate,
                    TimeModified = e.TimeModified,
                    User = e.User,
                    Type = e.Type,
                    Tag = e.Tag
                }).AsNoTracking().ToListAsync());
                        break;
                    case "give":
                        resDto.entityList = _mapper.Map<List<ArticleDto>>(await _service.Articles.Where(w => w.Type.Name == type)
                .OrderBy(c => c.Give).Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).Select(e => new ArticleDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Sketch = e.Sketch,
                    Give = e.Give,
                    Read = e.Read,
                    Img = e.Img,
                    TimeCreate = e.TimeCreate,
                    TimeModified = e.TimeModified,
                    User = e.User,
                    Type = e.Type,
                    Tag = e.Tag
                }).AsNoTracking().ToListAsync());
                        break;
                }
            }
        }

        private async Task GetFyAll(int pageIndex, int pageSize, string ordering, bool isDesc)
        {
            if (isDesc)//降序
            {
                switch (ordering) //排序
                {
                    case "id":
                        resDto.entityList = _mapper.Map<List<ArticleDto>>(
                await _service.Articles
                .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).Select(e => new ArticleDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Sketch = e.Sketch,
                    Give = e.Give,
                    Read = e.Read,
                    Img = e.Img,
                    TimeCreate = e.TimeCreate,
                    TimeModified = e.TimeModified,
                    User = e.User,
                    Type = e.Type,
                    Tag = e.Tag
                }).AsNoTracking().ToListAsync());

                        break;
                    case "data":
                        resDto.entityList = _mapper.Map<List<ArticleDto>>(await _service.Articles
                .OrderByDescending(c => c.TimeCreate).Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).Select(e => new ArticleDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Sketch = e.Sketch,
                    Give = e.Give,
                    Read = e.Read,
                    Img = e.Img,
                    TimeCreate = e.TimeCreate,
                    TimeModified = e.TimeModified,
                    User = e.User,
                    Type = e.Type,
                    Tag = e.Tag
                }).AsNoTracking().ToListAsync());
                        break;
                    case "read":
                        resDto.entityList = _mapper.Map<List<ArticleDto>>(await _service.Articles
                .OrderByDescending(c => c.Read).Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).Select(e => new ArticleDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Sketch = e.Sketch,
                    Give = e.Give,
                    Read = e.Read,
                    Img = e.Img,
                    TimeCreate = e.TimeCreate,
                    TimeModified = e.TimeModified,
                    User = e.User,
                    Type = e.Type,
                    Tag = e.Tag
                }).AsNoTracking().ToListAsync());
                        break;
                    case "give":
                        resDto.entityList = _mapper.Map<List<ArticleDto>>(await _service.Articles
                .OrderByDescending(c => c.Give).Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).Select(e => new ArticleDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Sketch = e.Sketch,
                    Give = e.Give,
                    Read = e.Read,
                    Img = e.Img,
                    TimeCreate = e.TimeCreate,
                    TimeModified = e.TimeModified,
                    User = e.User,
                    Type = e.Type,
                    Tag = e.Tag
                }).AsNoTracking().ToListAsync());
                        break;
                }
            }
            else //升序
            {
                switch (ordering) //排序
                {
                    case "id":
                        resDto.entityList = _mapper.Map<List<ArticleDto>>(await _service.Articles
                .OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).Select(e => new ArticleDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Sketch = e.Sketch,
                    Give = e.Give,
                    Read = e.Read,
                    Img = e.Img,
                    TimeCreate = e.TimeCreate,
                    TimeModified = e.TimeModified,
                    User = e.User,
                    Type = e.Type,
                    Tag = e.Tag
                }).AsNoTracking().ToListAsync());
                        break;
                    case "data":
                        resDto.entityList = _mapper.Map<List<ArticleDto>>(await _service.Articles
                .OrderBy(c => c.TimeCreate).Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).Select(e => new ArticleDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Sketch = e.Sketch,
                    Give = e.Give,
                    Read = e.Read,
                    Img = e.Img,
                    TimeCreate = e.TimeCreate,
                    TimeModified = e.TimeModified,
                    User = e.User,
                    Type = e.Type,
                    Tag = e.Tag
                }).AsNoTracking().ToListAsync());
                        break;
                    case "read":
                        resDto.entityList = _mapper.Map<List<ArticleDto>>(await _service.Articles
                .OrderBy(c => c.Read).Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).Select(e => new ArticleDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Sketch = e.Sketch,
                    Give = e.Give,
                    Read = e.Read,
                    Img = e.Img,
                    TimeCreate = e.TimeCreate,
                    TimeModified = e.TimeModified,
                    User = e.User,
                    Type = e.Type,
                    Tag = e.Tag
                }).AsNoTracking().ToListAsync());
                        break;
                    case "give":
                        resDto.entityList = _mapper.Map<List<ArticleDto>>(await _service.Articles
                .OrderBy(c => c.Give).Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).Select(e => new ArticleDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Sketch = e.Sketch,
                    Give = e.Give,
                    Read = e.Read,
                    Img = e.Img,
                    TimeCreate = e.TimeCreate,
                    TimeModified = e.TimeModified,
                    User = e.User,
                    Type = e.Type,
                    Tag = e.Tag
                }).AsNoTracking().ToListAsync());
                        break;
                }
            }
        }
        public async Task<bool> UpdatePortionAsync(Article entity, string type)
        {
            _logger.LogInformation("SnArticle更新部分参数");
            Article resulet = await _service.Articles.FindAsync(entity.Id);
            if (resulet == null) return false;

            switch (type)
            {    //指定字段进行更新操作
                case "Read":
                    //修改属性，被追踪的league状态属性就会变为Modify
                    resulet.Read = entity.Read;
                    break;
                case "Give":
                    resulet.Give = entity.Give;
                    break;
                case "Comment":
                    resulet.CommentId = entity.CommentId;
                    break;
            }
            //执行数据库操作
            return await _service.SaveChangesAsync() > 0;
        }
        public async Task<List<ArticleDto>> GetContainsAsync(int identity, string type, string name, bool cache)
        {
            _logger.LogInformation($"{NAME}{CONTAINS}{identity}_{type}_{name}_{cache}");
            resDto.entityList = _cacheutil.CacheString($"{NAME}{CONTAINS}{identity}{type}{name}{cache}", resDto.entityList, cache);
            if (resDto.entityList == null)
            {
                switch (identity)
                {
                    case 0:
                        resDto.entityList = _mapper.Map<List<ArticleDto>>(
                    await _service.Articles
                      .Where(l => l.Name.Contains(name)).Select(e => new ArticleDto
                      {
                          Id = e.Id,
                          Name = e.Name,
                          Sketch = e.Sketch,
                          Give = e.Give,
                          Read = e.Read,
                          Img = e.Img,
                          TimeCreate = e.TimeCreate,
                          TimeModified = e.TimeModified,
                          User = e.User,
                          Type = e.Type,
                          Tag = e.Tag
                          }).AsNoTracking().ToListAsync());

                        break;
                    case 1:
                        resDto.entityList = _mapper.Map<List<ArticleDto>>(
                      await _service.Articles
                       .Where(l => l.Name.Contains(name) && l.Type.Name == type).Select(e => new ArticleDto
                       {
                           Id = e.Id,
                           Name = e.Name,
                           Sketch = e.Sketch,
                           Give = e.Give,
                           Read = e.Read,
                           Img = e.Img,
                           TimeCreate = e.TimeCreate,
                           TimeModified = e.TimeModified,
                           User = e.User,
                           Type = e.Type,
                           Tag = e.Tag
                           }).AsNoTracking().ToListAsync());
                        break;
                    case 2:
                        resDto.entityList = _mapper.Map<List<ArticleDto>>(
                       await _service.Articles
                         .Where(l => l.Name.Contains(name) && l.Tag.Name == type).Select(e => new ArticleDto
                         {
                             Id = e.Id,
                             Name = e.Name,
                             Sketch = e.Sketch,
                             Give = e.Give,
                             Read = e.Read,
                             Img = e.Img,
                             TimeCreate = e.TimeCreate,
                             TimeModified = e.TimeModified,
                             User = e.User,
                             Type = e.Type,
                             Tag = e.Tag
                         }).AsNoTracking().ToListAsync());
                        break;
                    default:
                        _mapper.Map<List<ArticleDto>>(
                    await _service.Articles
                      .Where(l => l.Name.Contains(name)).Select(e => new ArticleDto
                      {
                          Id = e.Id,
                          Name = e.Name,
                          Sketch = e.Sketch,
                          Give = e.Give,
                          Read = e.Read,
                          Img = e.Img,
                          TimeCreate = e.TimeCreate,
                          TimeModified = e.TimeModified,
                          User = e.User,
                          Type = e.Type,
                          Tag = e.Tag
                      }).AsNoTracking().ToListAsync());
                        break;
                }
                _cacheutil.CacheString($"{NAME}{CONTAINS}{identity}{type}{name}{cache}", resDto.entityList, cache);
            }
            return resDto.entityList;
        }
    }
}
