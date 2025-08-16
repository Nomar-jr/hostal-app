using FluentValidation;

namespace Hostal.Application.UsesCases.Room.Queries.GetAvailableRoomsQuery;

public class GetAvailableRoomsQueryValidator : AbstractValidator<GetAvailableRoomsQuery>
{
    public GetAvailableRoomsQueryValidator()
    {
        RuleFor(x => x.StartDate)
            .NotEmpty()
            .WithMessage("Start date is required.")
            .Must(BeAValidDate)
            .WithMessage("Start date must be a valid date.")
            .GreaterThanOrEqualTo(DateTime.Today)
            .WithMessage("Start date cannot be in the past.");

        RuleFor(x => x.EndDate)
            .NotEmpty()
            .WithMessage("End date is required.")
            .Must(BeAValidDate)
            .WithMessage("End date must be a valid date.")
            .GreaterThan(x => x.StartDate)
            .WithMessage("End date must be greater than start date.");

        RuleFor(x => x)
            .Must(x => (x.EndDate - x.StartDate).TotalDays >= 2)
            .WithMessage("Minimum reservation period is 3 days (including the last day).")
            .When(x => x.StartDate != default && x.EndDate != default);
    }

    private static bool BeAValidDate(DateTime date)
    {
        return date != default;
    }
}