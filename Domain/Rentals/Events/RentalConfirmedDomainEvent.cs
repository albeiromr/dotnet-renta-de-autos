using Domain.Commons.Interfaces;
using System;

namespace Domain.Rentals.Events;

public sealed record RentalConfirmedDomainEvent(Guid id) : IDomainEvent;
