﻿using Domain.Abstractions;

namespace Domain.Users.Events;

public sealed record UserCreatedDomainEvent(Guid userId) : IDomainEvent
{

}

