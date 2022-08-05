using FluentValidation;

namespace MinimalAPIDemo.Validation;

public class UserModelValidator : AbstractValidator<UserModel>
{
	public UserModelValidator()
	{
		RuleFor(x => x.FirstName).NotEmpty().MinimumLength(2);
		RuleFor(x => x.LastName).NotEmpty().MinimumLength(2);
	}
}
