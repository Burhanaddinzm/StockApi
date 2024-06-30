namespace TestApi.Models.Common;

/// <summary>
/// Base class for all entities in the system.
/// </summary>
public class BaseEntity
{
    /// <summary>
    /// Gets or sets the ID of the entity.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the entity is deleted.
    /// </summary>
    public bool IsDeleted { get; set; }
}
