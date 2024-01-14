using Domain.Abstractions.Interfaces;
using System;

namespace Domain.Rentals.Events;

public sealed record RentalReservedDomainEvent(Guid id) : IDomainEvent;
