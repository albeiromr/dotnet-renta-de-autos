using Domain.Commons.Interfaces;
using System;

namespace Domain.Reviews.Events;

public sealed record ReviewCreatedDomainEvent(Guid id) : IDomainEvent;
