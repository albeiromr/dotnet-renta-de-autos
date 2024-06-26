﻿using Domain.Vehicles;
using Domain.Vehicles.ObjectValues;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

//esta configuración de tablas se explica en la clase 41 del curso de udemy
internal sealed class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.ToTable("vehicles");
        builder.HasKey(vehicle => vehicle.Id);

        //explicación de lo siguiente en el video 41 11m: 30s
        //Property se usa para value objects que tienen solo una propiedad
        builder.Property(vehicle => vehicle.Model)
            .HasMaxLength(200)
            .HasConversion(model => model!.value, value => new Model(value));

        builder.Property(vehicle => vehicle.Vin)
            .HasMaxLength(500)
            .HasConversion(vin => vin!.value, value => new Vin(value));

        //OwnsOne se usa para value objects que tienen mas de una propiedad
        builder.OwnsOne(vehicle => vehicle.Location);
        

        // este campo se agrega para evitar problemas de concurrencia
        // es decir que dos usuarios modifiquen e mismo record al tiempo
        // y se explica en el video 47 de udemy (concurrencia optimista)
        builder.Property<uint>("version").IsRowVersion();
    }
}
