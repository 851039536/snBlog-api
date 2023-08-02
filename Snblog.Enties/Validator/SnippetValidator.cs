using FluentValidation;

namespace Snblog.Enties.Validator
{
    /// <summary>
    /// 数据校验
    /// </summary>
    public class SnippetValidator :AbstractValidator<Snippet>
    {
        /// <summary>
        /// 数据校验
        /// </summary>
        public SnippetValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Text).NotEmpty().MinimumLength(10);
        }
    }
}

