namespace TestApi.Models.Common;

/// <summary>
/// Represents the base class for all entities that are auditable. 
/// It includes properties for tracking who created and modified the entity, 
/// when it was created and modified, and the IP address of the user.
/// </summary>
/// <remarks>This class inherits from <see cref="BaseEntity"/>.</remarks>
public class BaseAuditableEntity : BaseEntity
{
    /// <summary>
    /// The IP address of the user who created the entity.
    /// </summary>
    public string IP { get; set; } = null!;

    /// <summary>
    /// The username of the user who created the entity.
    /// </summary>
    public string CreatedBy { get; set; } = null!;

    /// <summary>
    /// The date and time when the entity was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// The username of the user who last modified the entity,
    /// or null if the entity has not been modified.
    /// </summary>
    public string? ModifiedBy { get; set; }

    /// <summary>
    /// The date and time when the entity was last modified,
    /// or null if the entity has not been modified.
    /// </summary>
    public DateTime? ModifiedAt { get; set; }
}
