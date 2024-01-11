using Domain.Abstractions;
using System;

namespace Domain.Rentals.events;

public sealed record ReservedRentalDomainEvent(Guid id) : IDomainEvent;
