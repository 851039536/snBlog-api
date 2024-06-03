namespace Snblog.Enties.AutoMapper;

/// <summary>
/// 文章对象
/// </summary>
public class ArticleMapper : Profile
{
    /// <summary>
    /// 配置构造函数，用来创建关系映射
    /// </summary>
    public ArticleMapper()
    {
        //构建实体映射规则添加映射对象  
        //如两个实体字段一致可直接映射关系
        //Article原对象类型，ArticleDto 目标对象类型  ReverseMap，可相互转换
        CreateMap<Article, ArticleDto>().ReverseMap(); 
    }
}