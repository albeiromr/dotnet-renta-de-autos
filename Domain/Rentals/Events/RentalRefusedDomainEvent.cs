using Domain.Commons.Interfaces;
using System;

namespace Domain.Rentals.Events;

public sealed record RentalRefusedDomainEvent(Guid id) : IDomainEvent;
