using FluentValidation;

namespace FluentValidationDemo
{
    public class UserManager
    {
        private readonly IValidator<User> validator;

        public UserManager(IValidator<User> validator)
        {
            this.validator = validator;
        }

        public async Task Validate(User model)
        {
            await validator.ValidateAndThrowAsync(model);
        }
    }
}
