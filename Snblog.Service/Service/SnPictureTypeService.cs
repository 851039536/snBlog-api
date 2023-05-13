namespace Snblog.Service.Service
{
    public class SnPictureTypeService : ISnPictureTypeService
    {
        private readonly snblogContext _service;//DB
        private readonly CacheUtil _cacheutil;
        private int result_Int = default;
        private List<SnPictureType> result_List = default;
        public SnPictureTypeService(snblogContext service, ICacheUtil cacheutil)
        {
            _service = service;
            _cacheutil = (CacheUtil)cacheutil;
        }


        public async Task<bool> AddAsync(SnPictureType entity)
        {
            await _service.SnPictureTypes.AddAsync(entity);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<List<SnPictureType>> GetAllAsync()
        {
            result_List = _cacheutil.CacheString1("SnPictureType_GetAllAsync", result_List);
            if (result_List != null)
            {
                return result_List;
            }
            result_List = await _service.SnPictureTypes.ToListAsync();
            _cacheutil.CacheString1("SnPictureType_GetAllAsync", result_List);
            return result_List;
        }

        public async Task<int> CountAsync()
        {
            result_Int = _cacheutil.CacheNumber1("SnPictureType_CountAsync", result_Int);
            if (result_Int != 0)
            {
                return result_Int;
            }
            result_Int = await _service.SnPictureTypes.CountAsync();
            _cacheutil.CacheNumber1("SnPictureType_CountAsync", result_Int);
            return result_Int;
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
            result = _cacheutil.CacheString1("SnPictureType_GetByIdAsync" + id, result);
            if (result != null)
            {
                return result;
            }
            result = await _service.SnPictureTypes.FindAsync(id);
            _cacheutil.CacheString1("SnPictureType_GetByIdAsync" + id, result);
            return result;
        }

        public async Task<bool> UpdateAsync(SnPictureType entity)
        {
            _service.SnPictureTypes.Update(entity);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<List<SnPictureType>> GetFyAllAsync(int pageIndex, int pageSize, bool isDesc)
        {
            result_List = _cacheutil.CacheString1("SnPictureType_GetFyAllAsync" + pageIndex + pageSize + isDesc, result_List);
            if (result_List != null)
            {
                return result_List;
            }
            result_List = await GetFyAll(pageIndex, pageSize, isDesc);
            _cacheutil.CacheString1("SnPictureType_GetFyAllAsync" + pageIndex + pageSize + isDesc, result_List);
            return result_List;
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
