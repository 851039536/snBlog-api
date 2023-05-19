namespace Snblog.Service.Service
{
    public class SnippetService : ISnippetService
    {
        private readonly snblogContext _service;
        private readonly CacheUtil _cache;
        private readonly EntityData<Snippet> _res = new();
        private readonly EntityDataDto<SnippetDto> _rDto = new();
        private readonly IMapper _mapper;


        const string NAME = "Snippet_";
        const string BYID = "BYID_";
        const string SUM = "SUM_";
        const string CONTAINS = "CONTAINS_";
        const string PAGING = "PAGING_";
        const string DEL = "DEL_";
        const string ADD = "ADD_";
        const string UP = "UP_";
        public SnippetService(ICacheUtil cacheUtil,snblogContext coreDbContext,IMapper mapper)
        {
            _service = coreDbContext;
            _cache = (CacheUtil)cacheUtil;
            _mapper = mapper;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Log.Information($"{NAME}{DEL}{id}");
            Snippet result = await _service.Snippets.FindAsync(id);
            if (result == null) return false;
            _service.Snippets.Remove(result);//删除单个
            _service.Remove(result);//直接在context上Remove()方法传入model，它会判断类型
            return await _service.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 主键查询 
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="cache">缓存</param>
        /// <returns>entity</returns>
        public async Task<SnippetDto> GetByIdAsync(int id,bool cache)
        {
            Log.Information($"{NAME}{BYID}{id}_{cache}");
            _rDto.EntityList = _cache.CacheString($"{NAME}{BYID}{id}{cache}",_rDto.EntityList,cache);
            if (_rDto.EntityList == null) {
                _rDto.Entity = _mapper.Map<SnippetDto>(await _service.Snippets.Select(e => new SnippetDto {
                    Id = e.Id,
                    Name = e.Name,
                    Text = e.Text,
                    User = e.User,
                    Tag = e.Tag,
                    Type = e.Type,
                    Label = e.Label,
                }).AsNoTracking().SingleOrDefaultAsync(b => b.Id == id));
                _cache.CacheString($"{NAME}{BYID}{id}_{cache}",_rDto.Entity,cache);
            }
            return _rDto.Entity;
        }


        public async Task<bool> AddAsync(Snippet entity)
        {
            Log.Information($"{NAME}{ADD}{entity}");
            //entity.TimeCreate = DateTime.Now;
            //entity.TimeModified = DateTime.Now;
            await _service.Snippets.AddAsync(entity);
            return await _service.SaveChangesAsync() > 0;
        }
        public async Task<bool> UpdateAsync(Snippet entity)
        {
            Log.Information($"{NAME}{UP}{entity}");
            //entity.TimeModified = DateTime.Now; //更新时间
            _service.Snippets.Update(entity);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<int> GetSumAsync(int identity,string type,bool cache)
        {
            Log.Information($"{NAME}{SUM}{identity}_{cache}");
            _res.EntityCount = _cache.CacheNumber($"{NAME}{SUM}{identity}{type}{cache}",_res.EntityCount,cache);
            if (_res.EntityCount == 0) {
                switch (identity) {
                    case 0:
                    _res.EntityCount = await _service.Snippets.AsNoTracking().CountAsync();
                    break;
                    case 1:
                    _res.EntityCount = await _service.Snippets.AsNoTracking().CountAsync(c => c.Type.Name == type);
                    break;
                    case 2:
                    _res.EntityCount = await _service.Snippets.AsNoTracking().CountAsync(c => c.Tag.Name == type);
                    break;
                    case 3:
                    _res.EntityCount = await _service.Snippets.AsNoTracking().CountAsync(c => c.User.Name == type);
                    break;
                }
                _cache.CacheNumber($"{NAME}{SUM}{identity}{type}{cache}",_res.EntityCount,cache);
            }
            return _res.EntityCount;
        }

        /// <summary>
        /// 内容统计
        /// </summary>
        /// <param name="identity">所有:0|分类:1|标签:2|用户:3</param>
        /// <param name="name">查询参数</param>
        /// <param name="cache">缓存</param>
        /// <returns>int</returns>
        public async Task<int> GetStrSumAsync(int identity,string name,bool cache)
        {
            Log.Information($"{NAME}统计_{identity}_{cache}");
            _res.EntityCount = _cache.CacheNumber($"{NAME}GetStrSumAsync{identity}{name}{cache}",_res.EntityCount,cache);
            if (_res.EntityCount == 0) {
                int num = 0;
                switch (identity) {
                    case 0:
                    List<string> text = await _service.Snippets.Select(c => c.Text).ToListAsync();
                    for (int i = 0 ; i < text.Count ; i++) num += text[i].Length;
                    _res.EntityCount = num;
                    break;
                    case 1:
                    List<string> ttext = await _service.Snippets.Where(w => w.Type.Name == name).Select(c => c.Text).ToListAsync();
                    for (int i = 0 ; i < ttext.Count ; i++) num += ttext[i].Length;
                    _res.EntityCount = num;
                    break;
                    case 2:
                    List<string> tagtext = await _service.Snippets.Where(w => w.Tag.Name == name).Select(c => c.Text).ToListAsync();
                    for (int i = 0 ; i < tagtext.Count ; i++) num += tagtext[i].Length;
                    _res.EntityCount = num;
                    break;
                    case 3:
                    List<string> utext = await _service.Snippets.Where(w => w.User.Name == name).Select(c => c.Text).ToListAsync();
                    for (int i = 0 ; i < utext.Count ; i++) num += utext[i].Length;
                    _res.EntityCount = num;
                    break;
                }
                _cache.CacheNumber($"{NAME}GetStrSumAsync{identity}{name}{cache}",_res.EntityCount,cache);
            }
            return _res.EntityCount;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="identity">所有:0|分类:1|标签:2|用户名:3|子标签:4</param>
        /// <param name="type">查询参数(多条件以','分割)</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">排序</param>
        /// <param name="cache">缓存</param>
        /// <returns>list-entity</returns>
        public async Task<List<SnippetDto>> GetPagingAsync(int identity,string type,int pageIndex,int pageSize,bool isDesc,bool cache)
        {
            Log.Information($"{NAME}{PAGING}{identity}_{type}_{pageIndex}_{pageSize}_{isDesc}_{cache}");
            _rDto.EntityList = _cache.CacheString($"{NAME}{PAGING}{identity}{type}{pageIndex}{pageSize}{isDesc}{cache}",_rDto.EntityList,cache);
            if (_rDto.EntityList == null) {
                switch (identity) {
                    case 0:
                    await GetPaging(pageIndex,pageSize,isDesc);
                    break;
                    case 1:
                    if (isDesc) {
                        _rDto.EntityList = _mapper.Map<List<SnippetDto>>(await _service.Snippets.Where(w => w.Type.Name == type)
                   .OrderByDescending(c => c.Id).Skip(( pageIndex - 1 ) * pageSize)
                    .Take(pageSize).Select(e => new SnippetDto {
                        Id = e.Id,
                        Name = e.Name,
                        Text = e.Text,
                        User = e.User,
                        Tag = e.Tag,
                        Label = e.Label,
                        Type = e.Type
                    }).AsNoTracking().ToListAsync());
                    } else {
                        _rDto.EntityList = _mapper.Map<List<SnippetDto>>(await _service.Snippets.Where(w => w.Type.Name == type)
                     .OrderBy(c => c.Id).Skip(( pageIndex - 1 ) * pageSize)
                     .Take(pageSize).Select(e => new SnippetDto {
                         Id = e.Id,
                         Name = e.Name,
                         Text = e.Text,
                         User = e.User,
                         Tag = e.Tag,
                         Label = e.Label,
                         Type = e.Type
                     }).AsNoTracking().ToListAsync());
                    }
                    break;
                    case 2:
                    await GetPagingTag(type,pageIndex,pageSize,isDesc);
                    break;
                    case 3:
                    await GetPagingUser(type,pageIndex,pageSize,isDesc);
                    break;
                    case 4:
                    if (isDesc) {
                        _rDto.EntityList = _mapper.Map<List<SnippetDto>>(await _service.Snippets.Where(w => w.Label.Name == type)
                   .OrderByDescending(c => c.Id).Skip(( pageIndex - 1 ) * pageSize)
                    .Take(pageSize).Select(e => new SnippetDto {
                        Id = e.Id,
                        Name = e.Name,
                        Text = e.Text,
                        User = e.User,
                        Tag = e.Tag,
                        Label = e.Label,
                        Type = e.Type
                    }).AsNoTracking().ToListAsync());
                    } else {
                        _rDto.EntityList = _mapper.Map<List<SnippetDto>>(await _service.Snippets.Where(w => w.Label.Name == type)
                     .OrderBy(c => c.Id).Skip(( pageIndex - 1 ) * pageSize)
                     .Take(pageSize).Select(e => new SnippetDto {
                         Id = e.Id,
                         Name = e.Name,
                         Text = e.Text,
                         User = e.User,
                         Tag = e.Tag,
                         Label = e.Label,
                         Type = e.Type
                     }).AsNoTracking().ToListAsync());
                    }
                    break;
                }
                _cache.CacheString($"{NAME}{PAGING}{identity}{type}{pageIndex}{pageSize}{isDesc}{cache}",_rDto.EntityList,cache);
            }
            return _rDto.EntityList;
        }
        private async Task GetPagingUser(string type,int pageIndex,int pageSize,bool isDesc)
        {
            if (isDesc) {
                _rDto.EntityList = _mapper.Map<List<SnippetDto>>(await _service.Snippets.Where(w => w.User.Name == type)
               .OrderByDescending(c => c.Id).Skip(( pageIndex - 1 ) * pageSize)
               .Take(pageSize).Select(e => new SnippetDto {
                   Id = e.Id,
                   Name = e.Name,
                   Text = e.Text,
                   User = e.User,
                   Tag = e.Tag,
                   Label = e.Label,
                   Type = e.Type
               }).AsNoTracking().ToListAsync());
            } else {
                _rDto.EntityList = _mapper.Map<List<SnippetDto>>(await _service.Snippets.Where(w => w.User.Name == type)
              .OrderBy(c => c.Id).Skip(( pageIndex - 1 ) * pageSize)
              .Take(pageSize).Select(e => new SnippetDto {
                  Id = e.Id,
                  Name = e.Name,
                  Text = e.Text,
                  User = e.User,
                  Tag = e.Tag,
                  Label = e.Label,
                  Type = e.Type
              }).AsNoTracking().ToListAsync());
            }
        }
        private async Task GetPagingTag(string type,int pageIndex,int pageSize,bool isDesc)
        {
            if (isDesc) {
                _rDto.EntityList = _mapper.Map<List<SnippetDto>>(await _service.Snippets.Where(w => w.Tag.Name == type)
               .OrderByDescending(c => c.Id).Skip(( pageIndex - 1 ) * pageSize)
               .Take(pageSize).Select(e => new SnippetDto {
                   Id = e.Id,
                   Name = e.Name,
                   Text = e.Text,
                   User = e.User,
                   Tag = e.Tag,
                   Label = e.Label,
                   Type = e.Type
               }).AsNoTracking().ToListAsync());
            } else {
                _rDto.EntityList = _mapper.Map<List<SnippetDto>>(await _service.Snippets.Where(w => w.Tag.Name == type)
                .OrderBy(c => c.Id).Skip(( pageIndex - 1 ) * pageSize)
                .Take(pageSize).Select(e => new SnippetDto {
                    Id = e.Id,
                    Name = e.Name,
                    Text = e.Text,
                    User = e.User,
                    Tag = e.Tag,
                    Label = e.Label,
                    Type = e.Type
                }).AsNoTracking().ToListAsync());
            }
        }
        private async Task GetPaging(int pageIndex,int pageSize,bool isDesc)
        {
            if (isDesc) {
                _rDto.EntityList = _mapper.Map<List<SnippetDto>>(await _service.Snippets.OrderByDescending(c => c.Id).Skip(( pageIndex - 1 ) * pageSize)
        .Take(pageSize).Select(e => new SnippetDto {
            Id = e.Id,
            Name = e.Name,
            Text = e.Text,
            User = e.User,
            Tag = e.Tag,
            Label = e.Label,
            Type = e.Type
        }).AsNoTracking().ToListAsync());
            } else {
                _rDto.EntityList = _mapper.Map<List<SnippetDto>>(await _service.Snippets
        .OrderBy(c => c.Id).Skip(( pageIndex - 1 ) * pageSize)
        .Take(pageSize).Select(e => new SnippetDto {
            Id = e.Id,
            Name = e.Name,
            Text = e.Text,
            User = e.User,
            Tag = e.Tag,
            Type = e.Type
        }).AsNoTracking().ToListAsync());

            }
        }
        public async Task<bool> UpdatePortionAsync(Snippet entity,string type)
        {
            //Log.Information("SnArticle更新部分参数");
            Snippet result = await _service.Snippets.FindAsync(entity.Id);
            if (result == null) return false;
            switch (type) {    //指定字段进行更新操作
                case "text":
                //修改属性，被追踪的league状态属性就会变为Modify
                result.Text = entity.Text;
                break;
                case "name":
                result.Name = entity.Name;
                break;
            }
            //执行数据库操作
            return await _service.SaveChangesAsync() > 0;
        }
        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="identity">所有:0|分类:1|标签:2|用户名:3|内容:4</param>
        /// <param name="type">查询参数(多条件以','分割)</param>
        /// <param name="name">查询字段</param>
        /// <param name="cache">缓存</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <returns>list-entity</returns>
        public async Task<List<SnippetDto>> GetContainsAsync(int identity,string type,string name,bool cache,int pageIndex,int pageSize)
        {
            //将字符串转换为大写字母的操作移到查询之前进行，以减少每个查询条件的计算量；对多个查询条件之间的关系进行优化，避免重复计算
            var uppercaseName = name.ToUpper();
            Log.Information($"{NAME}{CONTAINS}{identity}_{type}_{name}_{cache}");
            _rDto.EntityList = _cache.CacheString($"{NAME}{CONTAINS}{identity}{type}{name}{cache}",_rDto.EntityList,cache);
            if (_rDto.EntityList == null) {
                switch (identity) {
                    case 0: //所有 查分类,标题,内容
                    _rDto.EntityList = _mapper.Map<List<SnippetDto>>(
                    await _service.Snippets
                    .Where(w => w.Name.ToUpper().Contains(uppercaseName) || w.Label.Name.ToUpper().Contains(uppercaseName) || w.Text.ToUpper().Contains(uppercaseName))
                     .OrderByDescending(c => c.Id).Skip(( pageIndex - 1 ) * pageSize)
                    .Take(pageSize)
                    .Select(e => new SnippetDto {
                        Id = e.Id,
                        Name = e.Name,
                        Text = e.Text,
                        User = e.User,
                        Tag = e.Tag,
                        Label = e.Label,
                        Type = e.Type
                    }).AsNoTracking().ToListAsync()
                );
                    break;
                    case 1:
                    _rDto.EntityList = _mapper.Map<List<SnippetDto>>(
                  await _service.Snippets
                   .Where(l => l.Name.ToUpper().Contains(uppercaseName) && l.Type.Name == type)
                        .OrderByDescending(c => c.Id).Skip(( pageIndex - 1 ) * pageSize)
                    .Take(pageSize)
                   .Select(e => new SnippetDto {
                       Id = e.Id,
                       Name = e.Name,
                       Text = e.Text,
                       User = e.User,
                       Tag = e.Tag,
                       Label = e.Label,
                       Type = e.Type
                   }).AsNoTracking().ToListAsync());
                    break;
                    case 2:
                    _rDto.EntityList = _mapper.Map<List<SnippetDto>>(
                   await _service.Snippets
                     .Where(l => l.Name.ToUpper().Contains(uppercaseName) && l.Tag.Name == type)
                     .OrderByDescending(c => c.Id).Skip(( pageIndex - 1 ) * pageSize)
                    .Take(pageSize)
                     .Select(e => new SnippetDto {
                         Id = e.Id,
                         Name = e.Name,
                         Text = e.Text,
                         User = e.User,
                         Tag = e.Tag,
                         Label = e.Label,
                         Type = e.Type
                     }).AsNoTracking().ToListAsync());
                    break;
                    case 3:
                    _rDto.EntityList = _mapper.Map<List<SnippetDto>>(
                   await _service.Snippets
                     .Where(l => l.Name.ToUpper().Contains(uppercaseName) && l.User.Name == type)
                     .OrderByDescending(c => c.Id).Skip(( pageIndex - 1 ) * pageSize)
                    .Take(pageSize)
                     .Select(e => new SnippetDto {
                         Id = e.Id,
                         Name = e.Name,
                         Text = e.Text,
                         User = e.User,
                         Tag = e.Tag,
                         Label = e.Label,
                         Type = e.Type
                     }).AsNoTracking().ToListAsync());
                    break;
                    case 4:
                    _rDto.EntityList = _mapper.Map<List<SnippetDto>>(
                   await _service.Snippets
                     .Where(l => l.Text.ToUpper().Contains(uppercaseName))
                    .OrderByDescending(c => c.Id).Skip(( pageIndex - 1 ) * pageSize)
                    .Take(pageSize)
                     .Select(e => new SnippetDto {
                         Id = e.Id,
                         Name = e.Name,
                         Text = e.Text,
                         User = e.User,
                         Tag = e.Tag,
                         Label = e.Label,
                         Type = e.Type
                     }).AsNoTracking().ToListAsync());
                    break;
                    default:
                    return null;
                }
                _cache.CacheString($"{NAME}{CONTAINS}{identity}{type}{name}{cache}",_rDto.EntityList,cache);
            }
            return _rDto.EntityList;
        }
    }
}
