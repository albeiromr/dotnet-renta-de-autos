using Application.Commons.Interfaces;
using Domain.Commons.Clases;
using Domain.Users.Interfaces;
using Domain.Vehicles.Interfaces;
using Domain.Rentals.Interfaces;
using Domain.Rentals.Services;
using Domain.Commons.Interfaces;
using Domain.Users.Errors;
using Domain.Rentals.ObjectValues;
using Domain.Rentals.Errors;
using Domain.Rentals;

namespace Application.Rentals.BookRental;

// los handlers deben ser internal para que no queden expuestos
internal sealed class BookRentalCommandHandler : ICommandHandler<BookRentalCommand, Guid>
{
    private readonly IUserRepository? _userRepository;
    private readonly IVehicleRepository? _vehicleRepository;
    private readonly IRentalRepository? _rentalRepository;
    private readonly PriceService? _priceService;
    private readonly IUnitOfWork? _unitOfWork;
    private readonly IDateTimeProvider? _dateTimeProvider;

    public BookRentalCommandHandler(
        IUserRepository? userRepository,
        IVehicleRepository? vehicleRepository,
        IRentalRepository? rentalRepository,
        PriceService? priceService,
        IUnitOfWork? unitOfWork,
        IDateTimeProvider? dateTimePRovider
    )
    {
        _userRepository = userRepository;
        _vehicleRepository = vehicleRepository;
        _rentalRepository = rentalRepository;
        _priceService = priceService;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimePRovider;
    }

    public async Task<Result<Guid>> Handle(BookRentalCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository!.GetByIdAsync(request.userId, cancellationToken);
        if (user is null)
        {
            return Result.CreateWithFailureStatus<Guid>(VehicleErrors.NotFound);
        }

        var vehicle = await _vehicleRepository!.GetByIdAsync(request.vehicleId, cancellationToken);
        if (vehicle is null)
        {
            return Result.CreateWithFailureStatus<Guid>(VehicleErrors.NotFound);
        }

        var duration = DateRange.Create(request.startDate, request.endDate);
        if (await _rentalRepository!.IsOverlappingAsync(vehicle, duration, cancellationToken))
        {
            return Result.CreateWithFailureStatus<Guid>(RentalErrors.Overlap);
        }

        var rental = Rental.Book(
            vehicle,
            user.Id,
            duration,
            _dateTimeProvider!.CurrentTime,
            _priceService!
        );

        _rentalRepository.Add(rental);

        await _unitOfWork!.SaveChanges(cancellationToken);

        return rental.Id;

    }
}
