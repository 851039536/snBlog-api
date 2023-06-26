namespace Snblog.Service.Service
{
    public class DiaryService : IDiaryService
    {
        private string _cacheKey;
        const string NAME = "diary_";

        readonly EntityData<Diary> _ret = new();
        readonly EntityDataDto<DiaryDto> _rDto = new();

        private readonly CacheUtil _cache;
        private readonly snblogContext _service;


        public DiaryService(snblogContext service, ICacheUtil cache)
        {
            _service = service;
            _cache = (CacheUtil)cache;
        }

        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        public async Task<DiaryDto> GetByIdAsync(int id, bool cache)
        {
            _cacheKey = $"{NAME}{Common.Bid}{id}";
            Log.Information(_cacheKey);

            if (cache)
            {
                _rDto.Entity = _cache.GetValue(_cacheKey, _rDto.Entity);
                if (_rDto.Entity != null) return _rDto.Entity;
            }

            _rDto.Entity = await _service.Diaries.SelectDiary()
                .SingleOrDefaultAsync(b => b.Id == id);

            _cache.SetValue(_cacheKey, _rDto.Entity);

            return _rDto.Entity;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="identity">所有:0 || 分类:1 || 用户:2</param>
        /// <param name="type">类别参数, identity 0 可不填</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">是否倒序[true/false]</param>
        /// <param name="cache">是否开启缓存</param>
        /// <param name="ordering">排序条件[data:时间 read:阅读 give:点赞 按id排序]</param>
        public async Task<List<DiaryDto>> GetPagingAsync(int identity, string type, int pageIndex, int pageSize,
            string ordering, bool isDesc, bool cache)
        {
            _cacheKey =
                $"{NAME}{Common.Paging}{identity}_{type}_{pageIndex}_{pageSize}_{ordering}_{isDesc}_{cache}";
            Log.Information(_cacheKey);

            if (cache)
            {
                _rDto.EntityList = _cache.GetValue(_cacheKey, _rDto.EntityList);
                if (_rDto.EntityList != null) return _rDto.EntityList;
            }

            return identity switch
            {
                0 => await GetDiaryPaging(pageIndex, pageSize, ordering, isDesc),
                1 => await GetDiaryPaging(pageIndex, pageSize, ordering, isDesc, w => w.Type.Name == type),
                2 => await GetDiaryPaging(pageIndex, pageSize, ordering, isDesc, w => w.User.Name == type),
                _ => await GetDiaryPaging(pageIndex, pageSize, ordering, isDesc)
            };
        }

        private async Task<List<DiaryDto>> GetDiaryPaging(int pageIndex, int pageSize, string ordering, bool isDesc,
            Expression<Func<Diary, bool>> predicate = null)
        {
            IQueryable<Diary> diary = _service.Diaries.AsQueryable();

            if (predicate != null)
            {
                diary = diary.Where(predicate);
            }

            switch (ordering)
            {
                case "id":
                    diary = isDesc ? diary.OrderByDescending(c => c.Id) : diary.OrderBy(c => c.Id);
                    break;
                case "data":
                    diary = isDesc ? diary.OrderByDescending(c => c.TimeCreate) : diary.OrderBy(c => c.TimeCreate);
                    break;
                case "read":
                    diary = isDesc ? diary.OrderByDescending(c => c.Read) : diary.OrderBy(c => c.Read);
                    break;
                case "give":
                    diary = isDesc ? diary.OrderByDescending(c => c.Give) : diary.OrderBy(c => c.Give);
                    break;
            }

            var data = await diary.Skip((pageIndex - 1) * pageSize).Take(pageSize)
                .SelectDiary().ToListAsync();
            _cache.SetValue(_cacheKey, _rDto.EntityList);
            return data;
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DelAsync(int id)
        {
            _cacheKey = $"{NAME}{Common.Del}{id}";
            Log.Information(_cacheKey);

            var result = await _service.Diaries.FindAsync(id);
            if (result == null) return false;
            _service.Diaries.Remove(result);
            return await _service.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> AddAsync(Diary entity)
        {
            _cacheKey = $"{NAME}{Common.Add}{entity}";
            Log.Information(_cacheKey);
            _service.Diaries.Add(entity);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Diary entity)
        {
            _cacheKey = $"{NAME}{Common.Up}{entity}";
            Log.Information(_cacheKey);
            _service.Diaries.Update(entity);
            return await _service.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 查询总数 
        /// </summary>
        /// <param name="identity">所有:0 || 分类:1 || 用户2  </param>
        /// <param name="type">条件(identity为0则null) </param>
        /// <param name="cache"></param>
        /// <returns>int</returns>
        public async Task<int> GetSumAsync(int identity, string type, bool cache)
        {
            _cacheKey = $"{NAME}{Common.Sum}{identity}_{type}_{cache}";
            Log.Information(_cacheKey);

            if (cache)
            {
                _ret.EntityCount = _cache.GetValue(_cacheKey, _ret.EntityCount);
                if (_ret.EntityCount != 0) return _ret.EntityCount;
            }

            return identity switch
            {
                0 => await GetDiaryCountAsync(),
                1 => await GetDiaryCountAsync(w => w.Type.Name == type),
                2 => await GetDiaryCountAsync(w => w.User.Name == type),
                _ => throw new NotImplementedException(),
            };
        }

        /// <summary>
        /// 获取文章的数量
        /// </summary>
        /// <param name="predicate">筛选文章的条件</param>
        /// <returns>返回文章的数量</returns>
        private async Task<int> GetDiaryCountAsync(Expression<Func<Diary, bool>> predicate = null)
        {
            IQueryable<Diary> query = _service.Diaries.AsNoTracking();

            if (predicate != null)
            {
                //如果有筛选条件
                query = query.Where(predicate);
            }

            int count = await query.CountAsync();
            _cache.SetValue(_cacheKey, count);
            return count;
        }

        public async Task<int> CountTypeAsync(int type, bool cache)
        {
            _cacheKey = $"{NAME}{Common.Sum}{type}_{cache}";
            Log.Information(_cacheKey);

            if (cache)
            {
                _ret.EntityCount = _cache.GetValue(_cacheKey, _ret.EntityCount);
                if (_ret.EntityCount != 0) return _ret.EntityCount;
            }

            _ret.EntityCount = await _service.Diaries.CountAsync(s => s.TypeId == type);
            _cache.SetValue(_cacheKey, _ret.EntityCount);

            return _ret.EntityCount;
        }

        public async Task<int> GetSumAsync(string type, bool cache)
        {
            _cacheKey = $"{NAME}{Common.Sum}{type}_{cache}";

            Log.Information(_cacheKey);

            if (cache)
            {
                _ret.EntityCount = _cache.GetValue(_cacheKey, _ret.EntityCount);
                if (_ret.EntityCount != 0)
                {
                    return _ret.EntityCount;
                }
            }

            _ret.EntityCount = await GetSum(type);
            _cache.SetValue(_cacheKey, _ret.EntityCount);
            return _ret.EntityCount;
        }

        private async Task<int> GetSum(string type)
        {
            int num = 0;
            //按类型查询
            switch (type)
            {
                case "read":
                    var read = await _service.Diaries.Select(c => c.Read).ToListAsync();
                    num += read.Sum();
                    break;
                case "text":
                    var text = await _service.Diaries.Select(c => c.Text).ToListAsync();
                    num += text.Sum(t => t.Length);
                    break;
                case "give":
                    var give = await _service.Diaries.Select(c => c.Give).ToListAsync();
                    num += give.Sum();
                    break;
            }

            return num;
        }

        public async Task<bool> UpdatePortionAsync(Diary entity, string typeName)
        {
            _cacheKey = $"{NAME}{Common.UpPortiog}{typeName}_{entity}";
            Log.Information(_cacheKey);

            var result = await _service.Diaries.FindAsync(entity.Id);
            if (result == null) return false;

            switch (typeName)
            {
                //指定字段进行更新操作
                case "give":
                    //date.Property("OneGive").IsModified = true;
                    result.Give = entity.Give;
                    break;
                case "read":
                    //date.Property("OneRead").IsModified = true;
                    result.Read = entity.Read;
                    break;
            }

            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<List<DiaryDto>> GetContainsAsync(int identity, string type, string name, bool cache)
        {
            var upNames = name.ToUpper();
            _cacheKey = $"{NAME}{Common.Contains}{identity}_{type}_{name}_{cache}";
            Log.Information(_cacheKey);

            if (cache)
            {
                _rDto.EntityList = _cache.GetValue(_cacheKey, _rDto.EntityList);
                if (_rDto.EntityList != null) return _rDto.EntityList;
            }

            return identity switch
            {
                0 => await GetDiaryContainsAsync(l => l.Name.ToUpper().Contains(upNames)),
                1 => await GetDiaryContainsAsync(l => l.Name.ToUpper().Contains(name) && l.Type.Name == type),
                2 => await GetDiaryContainsAsync(l => l.Name.ToUpper().Contains(name) && l.User.Name == type),
                _ => null,
            };
        }

        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="predicate">筛选条件</param>
        private async Task<List<DiaryDto>> GetDiaryContainsAsync(Expression<Func<Diary, bool>> predicate = null)
        {
            IQueryable<Diary> query = _service.Diaries.AsNoTracking();
            if (predicate != null)
            {
                _rDto.EntityList = await query.Where(predicate).SelectDiary().ToListAsync();

                _cache.SetValue(_cacheKey, _rDto.EntityList);
            }

            return _rDto.EntityList;
        }
    }
}