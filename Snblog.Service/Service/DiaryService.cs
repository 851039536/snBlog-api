namespace Snblog.Service.Service
{
    public class DiaryService : IDiaryService
    {
        string cacheKey;
        const string NAME = "diary_";

        readonly EntityData<Diary> res = new();
        readonly EntityDataDto<DiaryDto> resDto = new();

        private readonly CacheUtil _cache;
        private readonly snblogContext _service;//DB
        private readonly IMapper _mapper;


        public DiaryService(snblogContext service,ICacheUtil cacheutil,IMapper mapper)
        {
            _service = service;
            _cache = (CacheUtil)cacheutil;
            _mapper = mapper;
        }

        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        public async Task<DiaryDto> GetByIdAsync(int id,bool cache)
        {
            cacheKey = $"{NAME}{ConstantString.BYID}{id}";
            Log.Information(cacheKey);

            if (cache) {
                resDto.Entity = _cache.GetValue(cacheKey,resDto.Entity);
                if (resDto.Entity != null) return resDto.Entity;
            }

            resDto.Entity = await _service.Diaries.SelectDiary()
              .SingleOrDefaultAsync(b => b.Id == id);

             _cache.SetValue(cacheKey,resDto.Entity);

            return resDto.Entity;
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
        public async Task<List<DiaryDto>> GetPagingAsync(int identity,string type,int pageIndex,int pageSize,string ordering,bool isDesc,bool cache)
        {

            cacheKey = $"{NAME}{ConstantString.PAGING}{identity}_{type}_{pageIndex}_{pageSize}_{ordering}_{isDesc}_{cache}";
            Log.Information(cacheKey);

            if (cache) {
                resDto.EntityList = _cache.GetValue(cacheKey,resDto.EntityList);
                if (resDto.EntityList == null) return resDto.EntityList;
                }

            switch (identity) {
                case 0:
                return await GetDiaryPaging(pageIndex,pageSize,ordering,isDesc);
                case 1:
                return await GetDiaryPaging(pageIndex,pageSize,ordering,isDesc,w => w.Type.Name == type);
                case 2:
                return await GetDiaryPaging(pageIndex,pageSize,ordering,isDesc,w => w.User.Name == type);
                default:
                return await GetDiaryPaging(pageIndex,pageSize,ordering,isDesc);
            }
        }

        private async Task<List<DiaryDto>> GetDiaryPaging(int pageIndex,int pageSize,string ordering,bool isDesc,Expression<Func<Diary,bool>> predicate = null)
        {
            IQueryable<Diary> diarys = _service.Diaries.AsQueryable();

            if (predicate != null) {
                diarys = diarys.Where(predicate);
            }
            switch (ordering) {
                case "id":
                diarys = isDesc ? diarys.OrderByDescending(c => c.Id) : diarys.OrderBy(c => c.Id);
                break;
                case "data":
                diarys = isDesc ? diarys.OrderByDescending(c => c.TimeCreate) : diarys.OrderBy(c => c.TimeCreate);
                break;
                case "read":
                diarys = isDesc ? diarys.OrderByDescending(c => c.Read) : diarys.OrderBy(c => c.Read);
                break;
                case "give":
                diarys = isDesc ? diarys.OrderByDescending(c => c.Give) : diarys.OrderBy(c => c.Give);
                break;
            }
            var data = await diarys.Skip(( pageIndex - 1 ) * pageSize).Take(pageSize)
            .SelectDiary().ToListAsync();
            _cache.SetValue(cacheKey,resDto.EntityList);
            return data;
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DelAsync(int id)
        {
            cacheKey = $"{NAME}{ConstantString.DEL}{id}";
            Log.Information(cacheKey);

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
            cacheKey = $"{NAME}{ConstantString.ADD}{entity}";
            Log.Information(cacheKey);
            _service.Diaries.Add(entity);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Diary entity)
        {
            cacheKey = $"{NAME}{ConstantString.UP}{entity}";
            Log.Information(cacheKey);
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
        public async Task<int> GetSumAsync(int identity,string type,bool cache)
        {
            cacheKey = $"{NAME}{ConstantString.SUM}{identity}_{type}_{cache}";
            Log.Information(cacheKey);

            if (cache) {
                res.EntityCount = _cache.GetValue(cacheKey,res.EntityCount);
                if (res.EntityCount != 0) return res.EntityCount;
            }

            return identity switch {
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
        private async Task<int> GetDiaryCountAsync(Expression<Func<Diary,bool>> predicate = null)
        {
            IQueryable<Diary> query = _service.Diaries.AsNoTracking();

            if (predicate != null) { //如果有筛选条件
                query = query.Where(predicate);
            }
            int count = await query.CountAsync();
            _cache.SetValue(cacheKey,count); 
            return count;
        }

        public async Task<int> CountTypeAsync(int type,bool cache)
        {
            cacheKey = $"{NAME}{ConstantString.SUM}{type}_{cache}";
            Log.Information(cacheKey);

            if (cache) {
                res.EntityCount = _cache.GetValue(cacheKey,res.EntityCount);
                if (res.EntityCount != 0) return res.EntityCount;
                }
         
                res.EntityCount = await _service.Diaries.CountAsync(s => s.TypeId == type);
                _cache.SetValue(cacheKey,res.EntityCount);
           
            return res.EntityCount;

        }

        public async Task<int> GetSumAsync(string type,bool cache)
        {
            cacheKey = $"{NAME}{ConstantString.SUM}{type}_{cache}";

            Log.Information(cacheKey);

            if (cache) {
                res.EntityCount = _cache.GetValue(cacheKey,res.EntityCount);
                if (res.EntityCount != 0) {
                    return res.EntityCount;
                }
            }
          
            res.EntityCount = await GetSum(type);
            _cache.SetValue(cacheKey,res.EntityCount);
            return res.EntityCount;
        }

        private async Task<int> GetSum(string type)
        {
            int num = 0;
            switch (type) //按类型查询
            {
                case "read":
                var read = await _service.Diaries.Select(c => c.Read).ToListAsync();
                foreach (var i in read) {
                    var item = i;
                    num += item;
                }

                break;
                case "text":
                var text = await _service.Diaries.Select(c => c.Text).ToListAsync();
                foreach (var t in text) {
                    num += t.Length;
                }

                break;
                case "give":
                var give = await _service.Diaries.Select(c => c.Give).ToListAsync();
                foreach (var i in give) {
                    var item = i;
                    num += item;
                }

                break;
            }

            return num;
        }

        public async Task<bool> UpdatePortionAsync(Diary entity,string typeName)
        {
            cacheKey = $"{NAME}{ConstantString.UP_PORTIOG}{typeName}_{entity}";
            Log.Information(cacheKey);

            var resulet = await _service.Diaries.FindAsync(entity.Id);
            if (resulet == null) return false;
            switch (typeName) {    //指定字段进行更新操作
                case "give":
                //date.Property("OneGive").IsModified = true;
                resulet.Give = entity.Give;
                break;
                case "read":
                //date.Property("OneRead").IsModified = true;
                resulet.Read = entity.Read;
                break;
            }
            return await _service.SaveChangesAsync() > 0;

        }

        public async Task<List<DiaryDto>> GetContainsAsync(int identity,string type,string name,bool cache)
        {
            var upNames = name.ToUpper();
            cacheKey = $"{NAME}{ConstantString.CONTAINS}{identity}_{type}_{name}_{cache}";
            Log.Information(cacheKey);

            if (cache) {
                resDto.EntityList = _cache.GetValue(cacheKey,resDto.EntityList);
                if (resDto.EntityList != null) return resDto.EntityList;
            }

            return identity switch {
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
        private async Task<List<DiaryDto>> GetDiaryContainsAsync(Expression<Func<Diary,bool>> predicate = null)
        {
            IQueryable<Diary> query = _service.Diaries.AsNoTracking();
            if (predicate != null) {

                resDto.EntityList = await query.Where(predicate).SelectDiary().ToListAsync();

                _cache.SetValue(cacheKey,resDto.EntityList);
            }
            return resDto.EntityList;
        }
    }
}
