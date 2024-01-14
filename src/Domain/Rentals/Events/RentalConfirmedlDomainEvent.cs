using Domain.Abstractions.Interfaces;
using System;

namespace Domain.Rentals.Events;

public sealed record RentalConfirmedDomainEvent(Guid id) : IDomainEvent;
