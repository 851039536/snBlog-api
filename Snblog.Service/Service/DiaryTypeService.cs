namespace Snblog.Service.Service
{
    public class DiaryTypeService : IDiaryTypeService
    {
        private readonly snblogContext _service;
        private readonly CacheUtil _cache;
        private int _resultInt;

        readonly EntityData<DiaryType> _ret = new();

        const string NAME = "diaryType_";
        private string _cacheKey;

        public DiaryTypeService(snblogContext service, ICacheUtil cache)
        {
            _service = service;
            _cache = (CacheUtil)cache;
        }

        public async Task<bool> AddAsync(DiaryType entity)
        {
            Log.Information($"{NAME}{ConstantString.ADD}{entity}");

            await _service.DiaryTypes.AddAsync(entity);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<int> GetSumAsync(bool cache)
        {
            _cacheKey = $"{NAME}{ConstantString.SUM}{cache}";
            Log.Information(_cacheKey);


            if (cache)
            {
                _resultInt = _cache.GetValue(_cacheKey, _resultInt);
                if (_resultInt != 0) return _resultInt;
            }

            _resultInt = await _service.DiaryTypes.CountAsync();
            _cache.SetValue(_cacheKey, _resultInt);
            return _resultInt;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _cacheKey = $"{NAME}{ConstantString.DEL}{id}";
            Log.Information(_cacheKey);

            var ret = await _service.DiaryTypes.FindAsync(id);

            if (ret == null) return false;

            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<List<DiaryType>> GetPagingAsync(int pageIndex, int pageSize, bool isDesc, bool cache)
        {
            _cacheKey = $"{NAME}{ConstantString.PAGING}{pageIndex}_{pageSize}_{isDesc}_{cache}";
            Log.Information(_cacheKey);

            if (cache)
            {
                _ret.EntityList = _cache.GetValue(_cacheKey, _ret.EntityList);
                if (_ret.EntityList != null) return _ret.EntityList;
            }

            await QPaging(pageIndex, pageSize, isDesc);
            _cache.SetValue(_cacheKey, _ret.EntityList);
            return _ret.EntityList;
        }

        private async Task QPaging(int pageIndex, int pageSize, bool isDesc)
        {
            if (isDesc)
            {
                _ret.EntityList = await _service.DiaryTypes
                    .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            else
            {
                _ret.EntityList = await _service.DiaryTypes.OrderBy(c => c.Id)
                    .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            }
        }

        public async Task<DiaryType> GetByIdAsync(int id, bool cache)
        {
            _cacheKey = $"{NAME}{ConstantString.BYID}{id}_{cache}";
            Log.Information(_cacheKey);
            DiaryType diaryType;

            if (cache)
            {
                diaryType = _cache.GetValue(_cacheKey, (DiaryType)null);
                if (diaryType != null) return diaryType;
            }

            diaryType = await _service.DiaryTypes.FindAsync(id);
            _cache.SetValue(_cacheKey, diaryType);
            return diaryType;
        }

        public async Task<DiaryType> GetTypeAsync(int type, bool cache)
        {
            _cacheKey = $"{NAME}{ConstantString.BYID}{type}_{cache}";
            Log.Information(_cacheKey);

            DiaryType diaryType;
            if (cache)
            {
                diaryType = _cache.GetValue(_cacheKey, (DiaryType)null);
                if (diaryType != null) return diaryType;
            }

            diaryType = await _service.DiaryTypes.FirstAsync(s => s.Id == type);
            _cache.SetValue(_cacheKey, diaryType);
            return diaryType;
        }

        public async Task<bool> UpdateAsync(DiaryType entity)
        {
            _cacheKey = $"{NAME}{ConstantString.UP}{entity.Id}";
            Log.Information(_cacheKey);

            _service.DiaryTypes.Update(entity);
            return await _service.SaveChangesAsync() > 0;
        }
    }
}