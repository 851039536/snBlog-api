﻿using Snblog.IService.IService.Snippets;

namespace Snblog.Service.Service.Snippets;

public class SnippetVersionService : ISnippetVersionService
{
    private readonly SnblogContext _service;
    private readonly ServiceHelper _serviceHelper;
    private readonly IMapper _mapper;
    private const string Name = "snippetVersion_";

    public SnippetVersionService(SnblogContext coreDbContext,IMapper mapper,ServiceHelper serviceHelper)
    {
        _service = coreDbContext;
        _mapper = mapper;
        _serviceHelper = serviceHelper;
    }

    #region 查询总数

    public async Task<int> GetSumAsync(int identity,int snippetId,bool cache)
    {
        string cacheKey = $"{Name}{ServiceConfig.Sum}{identity}{cache}";

        return await _serviceHelper.CheckAndExecuteCacheAsync(cacheKey,cache,async () =>
        {
            if(identity != 1)
            {
                return await _service.SnippetVersions.AsNoTracking().CountAsync();
            }

            return await _service.SnippetVersions.AsNoTracking().CountAsync(c => c.SnippetId == snippetId);
        });
    }

    #endregion

    #region 主键查询

    public async Task<SnippetVersionDto> GetByIdAsync(int id,bool cache)
    {
        string cacheKey = $"{Name}{ServiceConfig.Bid}{id}_{cache}";
        return await _serviceHelper.CheckAndExecuteCacheAsync(cacheKey,cache,async () =>
        {
            var ret = await _service.SnippetVersions.AsNoTracking().SingleOrDefaultAsync(b => b.Id == id);
            return _mapper.Map<SnippetVersionDto>(ret);
        });
    }

    #endregion

    #region 根据snippet表的主键查询

    public async Task<List<SnippetVersionDto>> GetAllBySnId(int id,bool cache)
    {
        string cacheKey = $"{Name}{ServiceConfig.Bid}{id}{cache}";

        return await _serviceHelper.CheckAndExecuteCacheAsync(cacheKey,cache,async () =>
        {
            var ret = await _service.SnippetVersions.Where(s => s.SnippetId == id).AsNoTracking().ToListAsync();
            return _mapper.Map<List<SnippetVersionDto>>(ret);
        });
    }

    #endregion

    public async Task<bool> DeleteAsync(int id)
    {
        Log.Information($"{Name}{ServiceConfig.Del}{id}");

        var ret = await _service.SnippetVersions.FindAsync(id);
        if(ret == null) return false;
        _service.SnippetVersions.Remove(ret); //删除单个
        _service.Remove(ret); //直接在context上Remove()方法传入model，它会判断类型
        return await _service.SaveChangesAsync() > 0;
    }

    public async Task<bool> AddAsync(SnippetVersion entity)
    {
        Log.Information($"{Name}{ServiceConfig.Add}{entity}");
        int num = await GetSumAsync(1,entity.SnippetId,false);
        entity.Count = num + 1;
        entity.TimeCreate = DateTime.Now;
        await _service.SnippetVersions.AddAsync(entity);
        return await _service.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateAsync(SnippetVersion entity)
    {
        Log.Information($"{Name}{ServiceConfig.Up}{entity.Id}_{entity}");

        //entity.TimeModified = DateTime.Now; //更新时间
        _service.SnippetVersions.Update(entity);
        return await _service.SaveChangesAsync() > 0;
    }


    public async Task<bool> UpdatePortionAsync(SnippetVersion entity,string type)
    {
        Log.Information($"{Name}{ServiceConfig.Paging} {entity.Id}_{type}");
        var result = await _service.Snippets.FindAsync(entity.Id);
        if(result == null) return false;
        switch(type)
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
}