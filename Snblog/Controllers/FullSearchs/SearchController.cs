namespace Snblog.Controllers.FullSearchs
{
    /// <summary>
    /// 全文搜索API
    /// </summary>
    [ApiExplorerSettings(GroupName = "V1")]
    [ApiController] //控制路由
    [Route("fullSearch")]
    public class SearchController : BaseController
    {
        private readonly SnblogContext _serviceContext;
        private readonly ServiceHelper _serviceHelper;
        public SearchController(SnblogContext serviceContext,ServiceHelper serviceHelper)
        {
            _serviceContext = serviceContext;
            _serviceHelper = serviceHelper;
        }

        private async Task<List<T>> FullTextSearch<T>(string keyword) where T : class
        {
            var searchFields = new Dictionary<Type,(string TextField,string NameField)>
            {
                { typeof(Article),("Text","Name") }, { typeof(Snippet),("Text","Name") },
                { typeof(Diary),("Text","Name") },
                { typeof(Navigation),("Url","Name") },
                // 添加其他实体类型的映射
            };

            var entityType = typeof(T);
            if(searchFields.TryGetValue(entityType,out var fields) && fields.TextField != null && fields.NameField != null)
            {
                // 构建复合查询条件：同时检查 Text 和 Name 字段
                var entityQuery = _serviceContext.Set<T>()
                                                 .Where(e => EF.Property<string>(e,fields.TextField).Contains(keyword) ||
                                                             EF.Property<string>(e,fields.NameField).Contains(keyword));

                var entityResults = await entityQuery.ToListAsync();
                return entityResults;
            }

            return new List<T>();
        }


        /// <summary>
        /// 全文搜索查询，包含Article，Snippet，Diary，Navigation
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <param name="cache">是否缓存</param>
        /// <returns>返回搜索结果</returns>
        [HttpGet("search")]
        public async Task<IActionResult> Search(string keyword,bool cache=false)
        {
            string cacheKey = $"search:{keyword}_{cache}";
            return await _serviceHelper.CheckAndExecuteCacheAsync(cacheKey,cache,async () =>
            {
                var results1 = await FullTextSearch<Article>(keyword);
                var results2 = await FullTextSearch<Snippet>(keyword);
                var results3 = await FullTextSearch<Diary>(keyword);
                var results4 = await FullTextSearch<Navigation>(keyword);
                // 合并指定字段的值
                var mergedResults = results1.Select(article =>
                    new SearchResult { Id = article.Id,Source = "article",Title = article.Name,Content = article.Text }).ToList();
                mergedResults.AddRange(results2.Select(snippet => new SearchResult
                {
                    Id = snippet.Id,Source = "snippet",Title = snippet.Name,Content = snippet.Text,
                }));

                mergedResults.AddRange(results3.Select(diary => new SearchResult
                {
                    Id = diary.Id,Source = "diary",Title = diary.Name,Content = diary.Text,
                }));

                mergedResults.AddRange(results4.Select(diary => new SearchResult
                {
                    Id = diary.Id,Source = "navigation",Title = diary.Name,Content = diary.Url,
                }));
                return ApiResponse(data:mergedResults,cache:cache);
            });
        }
    }
}