using Domain.Commons.ObjectValues;
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

        
        builder.OwnsOne(rental => rental.PriceDetails, PriceDetailsBuilder =>
        {
            PriceDetailsBuilder.OwnsOne(details => details.RentalPeriodPrice, rentalPriceBuilder =>
            {
                rentalPriceBuilder.Property(price => price.currency)
                .HasConversion(c => c.Code, code => Currency.FromCode(code!));
            });

            PriceDetailsBuilder.OwnsOne(details => details.MaintainancePrice, MaintainancePriceBuilder =>
            {
                MaintainancePriceBuilder.Property(price => price.currency)
                .HasConversion(c => c.Code, code => Currency.FromCode(code!));
            });

            PriceDetailsBuilder.OwnsOne(details => details.PremiumAccessoriesPrice, PremiumAccessoriesBuilder =>
            {
                PremiumAccessoriesBuilder.Property(price => price.currency)
                .HasConversion(c => c.Code, code => Currency.FromCode(code!));
            });

            PriceDetailsBuilder.OwnsOne(details => details.TotalPrice, TotalPriceBuilder =>
            {
                TotalPriceBuilder.Property(price => price.currency)
                .HasConversion(c => c.Code, code => Currency.FromCode(code!));
            });
        });

        builder.OwnsOne(rental => rental.Duration);

        // representando relación de uno a muchos
        builder.HasOne<Vehicle>()
            .WithMany()
            .HasForeignKey(rental => rental.VehicleId);

        // representando relación de uno a muchos
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(rental => rental.UserId);
    }
}
