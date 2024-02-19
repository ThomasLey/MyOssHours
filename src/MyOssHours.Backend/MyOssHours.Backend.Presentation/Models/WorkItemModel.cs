﻿namespace MyOssHours.Backend.Presentation.Models;

public class WorkItemModel
{
    public Guid Uuid { get; set; }
    public Guid Project { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public IEnumerable<ProjectHourModel> Hours { get; set; }
}