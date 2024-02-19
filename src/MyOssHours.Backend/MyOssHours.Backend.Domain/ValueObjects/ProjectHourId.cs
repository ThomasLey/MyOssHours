namespace MyOssHours.Backend.Domain.ValueObjects;

/// <summary>
///     Value Object for ProjectHour
/// </summary>
public class ProjectHourId : EntityId
{
    public ProjectHourId()
    {
    }

    public ProjectHourId(Guid uuid) : base(uuid)
    {
    }
}