using Domain.Abstractions;
using System;

namespace Domain.Rentals.events;

public sealed record RentalCreatedDomainEvent(Guid id) : IDomainEvent;
