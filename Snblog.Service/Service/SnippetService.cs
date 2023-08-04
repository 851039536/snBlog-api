namespace Snblog.Service.Service
{
    public class SnippetService : ISnippetService
    {
        private readonly SnblogContext _service;
        private readonly CacheUtil _cache;
        private readonly EntityData<Snippet> _ret = new();
        private readonly EntityDataDto<SnippetDto> _rDto = new();

        const string NAME = "Snippet_";

        public SnippetService(ICacheUtil cacheUtil, SnblogContext coreDbContext)
        {
            _service = coreDbContext;
            _cache = (CacheUtil)cacheUtil;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Common.CacheInfo($"{NAME}{Common.Del}{id}");

            Snippet snippet = await _service.Snippets.FindAsync(id);
            if (snippet == null) return false;
            _service.Snippets.Remove(snippet); //删除单个
            _service.Remove(snippet); //直接在context上Remove()方法传入model，它会判断类型
            return await _service.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 主键查询 
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="cache">缓存</param>
        /// <returns>entity</returns>
        public async Task<SnippetDto> GetByIdAsync(int id, bool cache)
        {
            Common.CacheInfo($"{NAME}{Common.Bid}{id}_{cache}");
            if (cache)
            {
                _rDto.Entity = _cache.GetValue<SnippetDto>(Common.CacheKey);
                if (_rDto.Entity != null) return _rDto.Entity;
            }

            _rDto.Entity = await _service.Snippets.SelectSnippet().AsNoTracking().SingleOrDefaultAsync(b => b.Id == id);
            _cache.SetValue(Common.CacheKey, _rDto.Entity);

            return _rDto.Entity;
        }


        public async Task<bool> AddAsync(Snippet entity)
        {
            Common.CacheInfo($"{NAME}{Common.Add}{entity}");
            await _service.Snippets.AddAsync(entity);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Snippet entity)
        {
            Common.CacheInfo($"{NAME}{Common.Up}{entity.Id}_{entity}");

            //entity.TimeModified = DateTime.Now; //更新时间
            _service.Snippets.Update(entity);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<int> GetSumAsync(int identity, string type, bool cache)
        {
            Common.CacheInfo($"{NAME}{Common.Sum}{identity}_{type}_{cache}");

            if (cache)
            {
                _ret.EntityCount = _cache.GetValue<int>(Common.CacheKey);
                if (_ret.EntityCount != 0) return _ret.EntityCount;
            }

            switch (identity)
            {
                case 0:
                    _ret.EntityCount = await _service.Snippets.AsNoTracking().CountAsync();
                    break;
                case 1:
                    _ret.EntityCount = await _service.Snippets.AsNoTracking().CountAsync(c => c.Type.Name == type);
                    break;
                case 2:
                    _ret.EntityCount = await _service.Snippets.AsNoTracking().CountAsync(c => c.Tag.Name == type);
                    break;
                case 3:
                    _ret.EntityCount = await _service.Snippets.AsNoTracking().CountAsync(c => c.User.Name == type);
                    break;
            }

            _cache.SetValue(Common.CacheKey, _ret.EntityCount);

            return _ret.EntityCount;
        }

        /// <summary>
        /// 内容统计
        /// </summary>
        /// <param name="identity">所有:0|分类:1|标签:2|用户:3</param>
        /// <param name="name">查询参数</param>
        /// <param name="cache">缓存</param>
        /// <returns>int</returns>
        public async Task<int> GetStrSumAsync(int identity, string name, bool cache)
        {
            Common.CacheInfo($"{NAME}统计_{identity}_{name}_{cache}");

            if (cache)
            {
                _ret.EntityCount = _cache.GetValue<int>(Common.CacheKey);
                if (_ret.EntityCount != 0) return _ret.EntityCount;
            }

            int num = 0;
            switch (identity)
            {
                case 0:
                    List<string> all = await _service.Snippets.Select(c => c.Text).ToListAsync();
                    num += all.Sum(t => t.Length);

                    _ret.EntityCount = num;
                    break;
                case 1:
                    List<string> types = await _service.Snippets.Where(w => w.Type.Name == name).Select(c => c.Text)
                        .ToListAsync();
                    num += types.Sum(t => t.Length);
                    _ret.EntityCount = num;
                    break;
                case 2:
                    List<string> tags = await _service.Snippets.Where(w => w.Tag.Name == name)
                        .Select(c => c.Text).ToListAsync();
                    num += tags.Sum(t => t.Length);
                    _ret.EntityCount = num;
                    break;
                case 3:
                    List<string> users = await _service.Snippets.Where(w => w.User.Name == name).Select(c => c.Text)
                        .ToListAsync();
                    num += users.Sum(t => t.Length);
                    _ret.EntityCount = num;
                    break;
            }

            _cache.SetValue(Common.CacheKey, _ret.EntityCount);

            return _ret.EntityCount;
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
        public async Task<List<SnippetDto>> GetPagingAsync(int identity, string type, int pageIndex, int pageSize,
            bool isDesc, bool cache)
        {
            Common.CacheInfo($"{NAME}{Common.Paging}{identity}_{type}_{pageIndex}_{pageSize}_{isDesc}_{cache}");
            if (cache)
            {
                _rDto.EntityList = _cache.GetValue<List<SnippetDto>>(Common.CacheKey);
                if (_rDto.EntityList != null) return _rDto.EntityList;
            }

            switch (identity)
            {
                case 0:
                    await GetPaging(pageIndex, pageSize, isDesc);
                    break;
                case 1:
                    if (isDesc)
                    {
                        _rDto.EntityList = await _service.Snippets
                            .Where(w => w.Type.Name == type)
                            .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).SelectSnippet().ToListAsync();
                    }
                    else
                    {
                        _rDto.EntityList = await _service.Snippets
                            .Where(w => w.Type.Name == type)
                            .OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).SelectSnippet().ToListAsync();
                    }

                    break;
                case 2:
                    await GetPagingTag(type, pageIndex, pageSize, isDesc);
                    break;
                case 3:
                    await GetPagingUser(type, pageIndex, pageSize, isDesc);
                    break;
                case 4:
                    if (isDesc)
                    {
                        _rDto.EntityList = await _service.Snippets
                            .Where(w => w.TypeSub.Name == type)
                            .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).SelectSnippet().ToListAsync();
                    }
                    else
                    {
                        _rDto.EntityList = await _service.Snippets
                            .Where(w => w.TypeSub.Name == type)
                            .OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).SelectSnippet().ToListAsync();
                    }

                    break;
            }

            _cache.SetValue(Common.CacheKey, _rDto.EntityList);

            return _rDto.EntityList;
        }

        private async Task GetPagingUser(string type, int pageIndex, int pageSize, bool isDesc)
        {
            if (isDesc)
            {
                _rDto.EntityList = await _service.Snippets.Where(w => w.User.Name == type)
                    .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize).SelectSnippet().ToListAsync();
            }
            else
            {
                _rDto.EntityList = await _service.Snippets.Where(w => w.User.Name == type)
                    .OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize).SelectSnippet().ToListAsync();
            }
        }

        private async Task GetPagingTag(string type, int pageIndex, int pageSize, bool isDesc)
        {
            if (isDesc)
            {
                _rDto.EntityList = await _service.Snippets.Where(w => w.Tag.Name == type)
                    .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize).SelectSnippet().ToListAsync();
            }
            else
            {
                _rDto.EntityList = await _service.Snippets.Where(w => w.Tag.Name == type)
                    .OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize).SelectSnippet().ToListAsync();
            }
        }

        private async Task GetPaging(int pageIndex, int pageSize, bool isDesc)
        {
            if (isDesc)
            {
                _rDto.EntityList = await _service.Snippets.OrderByDescending(c => c.Id)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize).SelectSnippet().ToListAsync();
            }
            else
            {
                _rDto.EntityList = await _service.Snippets
                    .OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize).SelectSnippet().ToListAsync();
            }
        }

        public async Task<bool> UpdatePortionAsync(Snippet entity, string type)
        {
            Common.CacheInfo($"{NAME}{Common.Paging} {entity.Id}_{type}");

            Snippet result = await _service.Snippets.FindAsync(entity.Id);
            if (result == null) return false;
            switch (type)
            {
                //指定字段进行更新操作
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
        /// <param name="identity">所有:0|分类:1|标签:2|用户名:3|内容:4|标题:5</param>
        /// <param name="type">查询参数(多条件以','分割)</param>
        /// <param name="name">查询字段</param>
        /// <param name="cache">缓存</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <returns>list-entity</returns>
        public async Task<List<SnippetDto>> GetContainsAsync(int identity, string type, string name, bool cache,
            int pageIndex, int pageSize)
        {
            //将字符串转换为大写字母的操作移到查询之前进行，以减少每个查询条件的计算量；对多个查询条件之间的关系进行优化，避免重复计算
            var uppercaseName = name.ToUpper();

            Common.CacheInfo($"{NAME}{Common.Contains}{identity}_{type}_{name}_{cache}");
            if (cache)
            {
                _rDto.EntityList = _cache.GetValue<List<SnippetDto>>(Common.CacheKey);
                if (_rDto.EntityList != null) return _rDto.EntityList;
            }

            switch (identity)
            {
                case 0: //所有 查分类,标题,内容
                    _rDto.EntityList = await _service.Snippets
                        .Where(w => w.Name.ToUpper().Contains(uppercaseName) ||
                                    w.TypeSub.Name.ToUpper()
                                        .Contains(uppercaseName) ||
                                    w.Text.ToUpper().Contains(uppercaseName))
                        .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .SelectSnippet().ToListAsync();

                    break;
                case 1:
                    _rDto.EntityList =
                        await _service.Snippets
                            .Where(l => l.Name.ToUpper().Contains(uppercaseName) && l.Type.Name == type)
                            .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize)
                            .SelectSnippet().ToListAsync();
                    break;
                case 2:
                    _rDto.EntityList =
                        await _service.Snippets
                            .Where(l => l.Name.ToUpper().Contains(uppercaseName) && l.Tag.Name == type)
                            .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize)
                            .SelectSnippet().ToListAsync();
                    break;
                case 3:
                    _rDto.EntityList =
                        await _service.Snippets
                            .Where(l => l.Name.ToUpper().Contains(uppercaseName) && l.User.Name == type)
                            .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize)
                            .SelectSnippet().ToListAsync();
                    break;
                case 4:
                    _rDto.EntityList =
                        await _service.Snippets
                            .Where(l => l.Text.ToUpper().Contains(uppercaseName))
                            .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize)
                            .SelectSnippet().ToListAsync();
                    break;
                case 5:
                    _rDto.EntityList =
                        await _service.Snippets
                            .Where(l => l.Name.ToUpper().Contains(uppercaseName))
                            .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize)
                            .SelectSnippet().ToListAsync();
                    break;
                default:
                    return null;
            }

            _cache.SetValue(Common.CacheKey, _rDto.EntityList);
            return _rDto.EntityList;
        }
    }
}