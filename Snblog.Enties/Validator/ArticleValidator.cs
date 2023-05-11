using FluentValidation;

namespace Snblog.Enties.Validator
{
    public class ArticleValidator :AbstractValidator<Article>
    {
        public ArticleValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Sketch).NotEmpty();
            RuleFor(x => x.Text).NotEmpty().MinimumLength(10);
            RuleFor(x => x.Img).NotEmpty();
        }
    }
}
