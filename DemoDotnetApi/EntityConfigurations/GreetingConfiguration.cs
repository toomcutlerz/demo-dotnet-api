using DemoDotnetApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoDotnetApi.EntityConfigurations;

public class GreetingConfiguration : IEntityTypeConfiguration<Greeting>
{
    public void Configure(EntityTypeBuilder<Greeting> builder)
    {
        builder.ToTable("Greetings");

        builder.HasKey(g => g.Id);

        builder.Property(g => g.Id)
            .ValueGeneratedOnAdd();

        builder.Property(g => g.Message)
            .HasMaxLength(500)
            .IsRequired();
    }
}

