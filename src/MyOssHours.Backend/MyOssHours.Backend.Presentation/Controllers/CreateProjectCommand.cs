namespace MyOssHours.Backend.Presentation.Controllers;

public class CreateProjectCommand
{
    public required string Name { get; set; }
    public required string Description { get; set; }
}