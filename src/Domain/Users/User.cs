using Domain.Commons.Clases;
using Domain.Users.Events;
using Domain.Users.ObjectValues;
using System;

namespace Domain.Users;

// Representa la entidad user
public sealed class User : Entity
{
    public Name? Name { get; private set; }
    public LastName? LastName { get; private set; }
    public Email? Email { get; private set; }

    // el constructor es privado para proteger los detalles de la 
    // creación de esta entidad 
    private User(
        Guid id,
        Name name,
        LastName lastName,
        Email email
    ) : base(id)
    {
        Name = name;
        LastName = lastName;
        Email = email;
    }

    // para crear un nuevo objeto de tipo User se usa el método 
    // create junto con el constructor privado para que ningún ente 
    // o programa externo pueda crear objetos de tipo User
    public static User Create(
        Guid id,
        Name name,
        LastName lastName,
        Email email
    )
    {
        User user = new User(Guid.NewGuid(), name, lastName, email);
        user.DispatchDomainEvent(new UserCreatedDomainEvent(user.Id));
        return user;
    }
}

