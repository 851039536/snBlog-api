using FluentValidation;

namespace Snblog.Enties.Validator
{
    /// <summary>
    /// 数据校验
    /// </summary>
    public class UserTalkValidator :AbstractValidator<UserTalk>
    {
        /// <summary>
        /// 数据校验
        /// </summary>
        public UserTalkValidator()
        {
            RuleFor(x => x.Text).NotEmpty().MaximumLength(300);
        }
    }
}

