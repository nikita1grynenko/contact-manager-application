using ContactManagerApplication.Domain.Entities;
using FluentValidation;

namespace ContactManagerApplication.Application.Validators;

public class ContactValidator : AbstractValidator<Contact>
{
    public ContactValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters");

        RuleFor(c => c.DateOfBirth)
            .LessThan(DateTime.Now).WithMessage("Date of birth must be in the past");

        RuleFor(c => c.Phone)
            .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Invalid phone number");

        RuleFor(c => c.Salary)
            .GreaterThan(0).WithMessage("Salary must be greater than zero");
    }
}