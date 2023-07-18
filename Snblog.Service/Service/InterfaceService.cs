namespace Snblog.Service.Service
{
    /// <summary>
    /// 路由导航
    /// </summary>
    public class InterfaceService : IInterfaceService
    {
        const string NAME = "Interface_";

        private string _cacheKey;


        private readonly snblogContext _service;
        private readonly CacheUtil _cache;

        private readonly IMapper _mapper;
        private readonly EntityDataDto<InterfaceDto> _rDto = new();


        public InterfaceService(snblogContext service,ICacheUtil cache,IMapper mapper)
        {
            _service = service;
            _cache = (CacheUtil)cache;
            _mapper = mapper;
        }

        ///  <summary>
        /// 条件查询 
        ///  </summary>
        ///  <param name="identity">用户-分类: 0 | 用户: 1 | 分类: 2</param>
        ///  <param name="userName">用户名称</param>
        ///  <param name="type">类别</param>
        ///  <param name="cache">缓存</param>
        public async Task<List<InterfaceDto>> GetConditionAsync(int identity,string userName,string type,bool cache)
        {
            _cacheKey = $"{NAME}{Common.Contains}{identity}_{userName}_{type}_{cache}";
            Log.Information(_cacheKey);

            if (cache)
            {
                _rDto.EntityList = _cache.GetValue<List<InterfaceDto>>(_cacheKey);
                if (_rDto.EntityList != null) return _rDto.EntityList;
            }

            switch (identity)
            {
                case 0:
                _rDto.EntityList = _mapper.Map<List<InterfaceDto>>(await _service.Interfaces
                    .Where(s => s.Type.Name == type && s.User.Name == userName).AsNoTracking().ToListAsync());
                break;
                case 1:
                _rDto.EntityList = _mapper.Map<List<InterfaceDto>>(await _service.Interfaces
                    .Where(s => s.User.Name == userName).AsNoTracking().ToListAsync());
                break;
                case 2:
                _rDto.EntityList = _mapper.Map<List<InterfaceDto>>(await _service.Interfaces
                    .Where(s => s.Type.Name == type).AsNoTracking().ToListAsync());
                break;
            }

            _cache.SetValue(_cacheKey,_rDto.EntityList);
            return _rDto.EntityList;
        }


        private async Task<List<InterfaceDto>> GetInterfacesPaging(int pageIndex,int pageSize,bool isDesc,
            Expression<Func<Interface,bool>> predicate = null)
        {
            IQueryable<Interface> interfaces = _service.Interfaces.AsQueryable();

            if (predicate != null)
            {
                interfaces = interfaces.Where(predicate);
            }

            interfaces = isDesc ? interfaces.OrderByDescending(c => c.Id) : interfaces.OrderBy(c => c.Id);

            var data = await interfaces.Skip((pageIndex - 1) * pageSize).Take(pageSize).SelectInterface()
                .AsNoTracking().ToListAsync();
            _cache.SetValue(_cacheKey,_rDto.EntityList);
            return data;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="identity">所有:0|分类:1|用户名:2|用户-分类:3</param>
        /// <param name="type">类别参数, identity为0时可为空(null) 多条件以','分割</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">排序</param>
        /// <param name="cache">缓存</param>
        public async Task<List<InterfaceDto>> GetPagingAsync(int identity,string type,int pageIndex,int pageSize,
            bool isDesc,bool cache)
        {
            _cacheKey = $"{NAME}{Common.Paging}{identity}_{type}_{pageIndex}_{pageSize}_{isDesc}_{cache}";
            Log.Information(_cacheKey);

            if (cache)
            {
                _rDto.EntityList = _cache.GetValue<List<InterfaceDto>>(_cacheKey);
                if (_rDto.EntityList != null)
                {
                    return _rDto.EntityList;
                }
            }

            switch (identity) //查询条件
            {
                case 0:
                return await GetInterfacesPaging(pageSize,pageIndex,isDesc);

                case 1:
                return await GetInterfacesPaging(pageSize,pageIndex,isDesc,w => w.Type.Name == type);

                case 2:
                return await GetInterfacesPaging(pageSize,pageIndex,isDesc,w => w.User.Name == type);

                case 3:
                string[] sName = type.Split(',');
                return await GetInterfacesPaging(pageSize,pageIndex,isDesc,
                    w => w.User.Name == sName[0] && w.Type.Name == sName[1]);
            }

            return _rDto.EntityList;
        }

        public async Task<bool> AddAsync(Interface entity)
        {
            Log.Information($"{NAME}{Common.Add}");
            await _service.AddAsync(entity);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Interface entity)
        {
            Log.Information($"{NAME}{Common.Up}{entity}");
            _service.Interfaces.Update(entity);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Log.Information($"{NAME}{Common.Del}{id}");
            var ret = await _service.Interfaces.FindAsync(id);
            if (ret == null) return false;
            _service.Interfaces.Remove(ret); //删除单个
            _service.Remove(ret); //直接在context上Remove()方法传入model，它会判断类型
            return await _service.SaveChangesAsync() > 0;
        }


        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="cache">缓存</param>
        /// <returns>entity</returns>
        public async Task<InterfaceDto> GetByIdAsync(int id,bool cache)
        {
            _cacheKey = $"{NAME}{Common.Bid}{id}_{cache}";
            Log.Information(_cacheKey);

            if (cache)
            {
                _rDto.Entity = _cache.GetValue<InterfaceDto>(_cacheKey);
                if (_rDto.Entity != null) return _rDto.Entity;
            }

            _rDto.Entity = _mapper.Map<InterfaceDto>(await _service.Interfaces.FindAsync(id));
            _cache.SetValue(_cacheKey,_rDto.Entity);
            return _rDto.Entity;
        }
    }
}