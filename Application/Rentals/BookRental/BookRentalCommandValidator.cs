using FluentValidation;

namespace Application.Rentals.BookRental;

public class BookRentalCommandValidator : AbstractValidator<BookRentalCommand>
{
    public BookRentalCommandValidator()
    {
        RuleFor(c => c.userId).NotEmpty();
        RuleFor(c => c.vehicleId).NotEmpty();
        RuleFor(c => c.startDate).LessThan(c => c.endDate);
    }
}
