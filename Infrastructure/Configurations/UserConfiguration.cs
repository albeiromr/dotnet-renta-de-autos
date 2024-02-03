using Domain.Users;
using Domain.Users.ObjectValues;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.HasKey(user => user.Id);

        builder.Property(user => user.Name)
            .HasMaxLength(200)
            .HasConversion(name => name!.name, value => new Name(value));

        builder.Property(user => user.LastName)
            .HasMaxLength(200)
            .HasConversion(lastName => lastName!.lastName, value => new LastName(value));

        builder.Property(user => user.Email)
            .HasMaxLength(200)
            .HasConversion(email => email!.email, value => new Email(value));
        
        //Haciendo que el email sea único
        builder.HasIndex(user => user.Email).IsUnique();

    }
}
