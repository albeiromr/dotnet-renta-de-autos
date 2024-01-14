using Domain.Abstractions.Interfaces;
using System;

namespace Domain.Rentals.events;

public sealed record VehicleCreatedDomainEvent(Guid id) : IDomainEvent;
