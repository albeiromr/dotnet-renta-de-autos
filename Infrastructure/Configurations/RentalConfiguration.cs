using Domain.Rentals;
using Domain.Users;
using Domain.Vehicles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

internal sealed class RentalConfiguration : IEntityTypeConfiguration<Rental>
{
    public void Configure(EntityTypeBuilder<Rental> builder)
    {
        builder.ToTable("rentals");
        builder.HasKey(rental => rental.Id);

        builder.OwnsOne(rental => rental.Duration);

        // representando relación de uno a muchos
        builder.HasOne<Vehicle>()
            .WithMany()
            .HasForeignKey(rental => rental.VehicleId);

        // representando relación de uno a muchos
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(rental => rental.UserId);

        // este campo se agrega para evitar problemas de concurrencia
        // es decir que dos usuarios modifiquen e mismo record al tiempo
        // y se explica en el video 47 de udemy (concurrencia optimista)
        builder.Property<uint>("version").IsRowVersion();
    }
}
