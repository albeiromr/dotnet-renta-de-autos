using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Commons.Interfaces;

namespace Domain.Commons.Clases;

//Representa las propiedades que son comunes en cada entidad
public abstract class Entity
{
    // representa la lista de eventos de cada entidad
    private readonly List<IDomainEvent> _domainEvents = new();

    // la palabra reservada [init] hace que una ves que se le 
    // da un valor a el [Id] nunca mas peda ser cambiado, se 
    // quedará con ese id por siempre
    public Guid Id { get; init; }

    // con protected hacemos que unicamente las clases que
    // hereden de [Entity] tengan acceso al constructor de
    // [Entity] mediante el método [base()]
    protected Entity(Guid id)
    {
        Id = id;
    }

    // retorna una lista que contiene todos los eventos
    public IReadOnlyList<IDomainEvent> GetDomainEvents()
    {
        return _domainEvents.ToList();
    }

    // limpia toda la lista de los eventos
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    // Agrega un evento a la lista de eventos de la entidad
    protected void DispatchDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}

