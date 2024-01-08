namespace Domain.Abstractions;
//Representa las propiedades que son comunes en cada entidad
public abstract class Entity
{
    //la palabra reservada [init] hace que una ves que se le 
    // da un valor a el [Id] nunca mas peda ser cambiado, se 
    //quedará con ese id por siempre
    public Guid Id { get; init; }

    // con protected hacemos que unicamente las clases que
    // hereden de [Entity] tengan acceso al constructor de
    // [Entity] mediante el método [base()]
    protected Entity(Guid id)
    {
        Id = id;
    }
}

