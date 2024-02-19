using MyOssHours.Backend.Domain.Attributes;

namespace MyOssHours.Backend.Domain.ValueObjects;

/// <summary>
///     Value Object for Project
/// </summary>
public class ProjectId : EntityId
{
    public ProjectId()
    {
    }

    public ProjectId(Guid uuid) : base(uuid)
    {
    }

    [CodeOfInterest("The operator allows an implicit conversion between a GUID and project id")]
    public static implicit operator ProjectId(Guid value)
    {
        return new ProjectId(value);
    }
}