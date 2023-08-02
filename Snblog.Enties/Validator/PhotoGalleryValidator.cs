using FluentValidation;

namespace Snblog.Enties.Validator
{
    /// <summary>
    /// 数据校验
    /// </summary>
    public class PhotoGalleryValidator :AbstractValidator<PhotoGallery>
    {
        /// <summary>
        /// 数据校验
        /// </summary>
        public PhotoGalleryValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Tag).NotEmpty();
        }
    }
}

