using Domain.Rentals;
using Domain.Reviews;
using Domain.Reviews.ObjectValues;
using Domain.Users;
using Domain.Vehicles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

internal sealed class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable("reviews");
        builder.HasKey(review => review.Id);

        builder.Property(review => review.Rating)
            .HasConversion(rating => rating!.Value, value => Rating.Create(value).Value);

        builder.Property(review => review.Comment)
            .HasMaxLength(200)
            .HasConversion(comment => comment!.value, value => new Comment(value));

        // representando relación de uno a muchos
        builder.HasOne<Vehicle>()
            .WithMany()
            .HasForeignKey(review => review.VehicleId);

        // representando relación de uno a muchos
        builder.HasOne<Rental>()
            .WithMany()
            .HasForeignKey(review => review.RentalId);

        // representando relación de uno a muchos
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(review => review.UserId);

        // este campo se agrega para evitar problemas de concurrencia
        // es decir que dos usuarios modifiquen e mismo record al tiempo
        // y se explica en el video 47 de udemy (concurrencia optimista)
        builder.Property<uint>("version").IsRowVersion();
    }
}
