namespace Snblog.Service.Service
{
    public class SnPictureTypeService : ISnPictureTypeService
    {
        private readonly snblogContext _service;//DB
        private readonly CacheUtil _cache;
        private int _resultInt;
        private List<SnPictureType> _resultList;
        public SnPictureTypeService(snblogContext service, ICacheUtil cache)
        {
            _service = service;
            _cache = (CacheUtil)cache;
        }


        public async Task<bool> AddAsync(SnPictureType entity)
        {
            await _service.SnPictureTypes.AddAsync(entity);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<List<SnPictureType>> GetAllAsync()
        {
            _resultList = _cache.CacheString1("SnPictureType_GetAllAsync", _resultList);
            if (_resultList != null)
            {
                return _resultList;
            }
            _resultList = await _service.SnPictureTypes.ToListAsync();
            _cache.CacheString1("SnPictureType_GetAllAsync", _resultList);
            return _resultList;
        }

        public async Task<int> CountAsync()
        {
            _resultInt = _cache.CacheNumber1("SnPictureType_CountAsync", _resultInt);
            if (_resultInt != 0)
            {
                return _resultInt;
            }
            _resultInt = await _service.SnPictureTypes.CountAsync();
            _cache.CacheNumber1("SnPictureType_CountAsync", _resultInt);
            return _resultInt;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            // _service.SnPictureType.Remove(Entity);
            //return await _service.SaveChangesAsync()>0;
            var todoItem = await _service.SnPictureTypes.FindAsync(id);
            _service.SnPictureTypes.Remove(todoItem);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<SnPictureType> GetByIdAsync(int id)
        {
            SnPictureType result = default;
            result = _cache.CacheString1("SnPictureType_GetByIdAsync" + id, result);
            if (result != null)
            {
                return result;
            }
            result = await _service.SnPictureTypes.FindAsync(id);
            _cache.CacheString1("SnPictureType_GetByIdAsync" + id, result);
            return result;
        }

        public async Task<bool> UpdateAsync(SnPictureType entity)
        {
            _service.SnPictureTypes.Update(entity);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<List<SnPictureType>> GetFyAllAsync(int pageIndex, int pageSize, bool isDesc)
        {
            _resultList = _cache.CacheString1("SnPictureType_GetFyAllAsync" + pageIndex + pageSize + isDesc, _resultList);
            if (_resultList != null)
            {
                return _resultList;
            }
            _resultList = await GetFyAll(pageIndex, pageSize, isDesc);
            _cache.CacheString1("SnPictureType_GetFyAllAsync" + pageIndex + pageSize + isDesc, _resultList);
            return _resultList;
        }

        private async Task<List<SnPictureType>> GetFyAll(int pageIndex, int pageSize, bool isDesc)
        {
            if (isDesc)
            {
                return await _service.SnPictureTypes.OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            else
            {
                return await _service.SnPictureTypes.OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            }
        }

        public Task<List<SnPictureType>> GetFyTypeAllAsync(int type, int pageIndex, int pageSize, bool isDesc)
        {
            throw new NotImplementedException();
        }
    }
}
