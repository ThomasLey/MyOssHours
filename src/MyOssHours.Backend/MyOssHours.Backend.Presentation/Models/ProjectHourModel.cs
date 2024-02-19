﻿namespace MyOssHours.Backend.Presentation.Models;

public class ProjectHourModel
{
    public Guid Uuid { get; set; }
    public DateTime Date { get; set; }
    public int DurationInMinutes { get; set; }
    public Guid User { get; set; }
}