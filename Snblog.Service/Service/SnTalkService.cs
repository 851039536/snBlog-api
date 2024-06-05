namespace Snblog.Service.Service;

public class SnTalkService : ISnTalkService
{
    private readonly SnblogContext _service;//DB
    // private int rInt;
    //  private List<SnTalk> result_List = default;

    public SnTalkService(SnblogContext service)
    {
        _service = service;
    }

    public async Task<bool> AddAsync(SnTalk entity)
    {
        await _service.SnTalks.AddAsync(entity);
        return await _service.SaveChangesAsync() > 0;
    }

    public async Task<int> CountAsync()
    {
        return await _service.SnTalks.CountAsync();
    }

    public async Task<int> CountAsync(int type)
    {
        return await _service.SnTalks.Where(s => s.TypeId == type).CountAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var todoItem = await _service.SnTalks.FindAsync(id);
        if (todoItem == null)
        {
            return false;
        }

        _service.SnTalks.Remove(todoItem);
        return await _service.SaveChangesAsync() > 0;
    }

    public async Task<List<SnTalk>> GetAllAsync()
    {
        return await _service.SnTalks.ToListAsync();
    }

    public async Task<List<SnTalk>> GetAllAsync(int id)
    {
        return await _service.SnTalks.Where(s => s.Id == id).ToListAsync();
    }

    public async Task<List<SnTalk>> GetFyAllAsync(int pageIndex, int pageSize, bool isDesc)
    {
        if (isDesc)
        {
            return await _service.SnTalks.Where(s => true)
                .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();
        }
        else
        {
            return await _service.SnTalks.Where(s => true)
                .OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();
        }
    }

    public async Task<List<SnTalk>> GetFyTypeAllAsync(int type, int pageIndex, int pageSize, bool isDesc)
    {
        if (isDesc)
        {
            return await _service.SnTalks.Where(s => s.TypeId == type)
                .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();
        }
        else
        {
            return await _service.SnTalks.Where(s => s.TypeId == type)
                .OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();
        }
    }
    public async Task<bool> UpdateAsync(SnTalk entity)
    {
        _service.SnTalks.Update(entity);
        return await _service.SaveChangesAsync() > 0;
    }
}