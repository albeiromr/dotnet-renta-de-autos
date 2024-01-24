using Domain.Commons.Interfaces;
using System;

namespace Domain.Users.Events;

public sealed record UserCreatedDomainEvent(Guid userId) : IDomainEvent
{

}

