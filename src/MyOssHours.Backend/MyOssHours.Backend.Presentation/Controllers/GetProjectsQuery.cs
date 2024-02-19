namespace MyOssHours.Backend.Presentation.Controllers;

public class GetProjectsQuery
{
    public int Offset { get; set; } = 0;
    public int Size { get; set; } = 20;
}