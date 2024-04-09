using FluentValidation;

namespace Snblog.Enties.Validator;

/// <summary>
/// 数据校验
/// </summary>
public class ArticleValidator :AbstractValidator<Article>
{
    /// <summary>
    /// Article数据校验
    /// </summary>
    public ArticleValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Sketch).NotEmpty();
        RuleFor(x => x.Text).NotEmpty().MinimumLength(10);
        RuleFor(x => x.Img).NotEmpty();
    }
}