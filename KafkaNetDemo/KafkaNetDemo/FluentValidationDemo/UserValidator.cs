using FluentValidation;

namespace FluentValidationDemo
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().MinimumLength(1).MaximumLength(128);
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Address)
                .Must(x => x != null && x.Contains("street"))
                .WithMessage("Address should contain street");
            RuleForEach(x => x.MemberShips).SetValidator(new UserExtendValidator());
        }
    }

    public class UserExtendValidator : AbstractValidator<MemberShip>
    {
        public UserExtendValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.RelationShip).Must(x => x.Contains("Is"));
        }
    }
}
