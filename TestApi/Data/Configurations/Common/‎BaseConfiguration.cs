using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestApi.Models.Common;

namespace TestApi.Data.Configurations.Common;

/// <summary>
/// Represents the base configuration for an entity that inherits from BaseAuditableEntity.
/// </summary>
/// <typeparam name="T">The type of the entity.</typeparam>
public class BaseConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseAuditableEntity
{
    /// <summary>
    /// Configures the entity of type <typeparamref name="T"/>.
    /// </summary>
    /// <param name="builder">The builder to be used to configure the entity type.</param>
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CreatedAt).HasColumnType("datetime2").IsRequired();
        builder.Property(x => x.ModifiedAt).HasColumnType("datetime2").IsRequired(false);
        builder.Property(x => x.CreatedBy).IsRequired();
        builder.Property(x => x.ModifiedBy).IsRequired(false);
        builder.Property(x => x.IP).IsRequired();
        builder.Property(x => x.IsDeleted).HasDefaultValue(false).IsRequired();
        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
