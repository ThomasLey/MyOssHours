using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyOssHours.Backend.Application.WorkItems;
using MyOssHours.Backend.Domain.ValueObjects;
using MyOssHours.Backend.Presentation.Models;
using MyOssHours.Backend.Presentation.Requests;

namespace MyOssHours.Backend.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorkItemController : ControllerBase
{
    private readonly IHttpContextAccessor _httpContext;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public WorkItemController(IMediator mediator, IMapper mapper, IHttpContextAccessor httpContext)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _httpContext = httpContext ?? throw new ArgumentNullException(nameof(httpContext));
    }

    // POST api/<ProjectController>
    [HttpPost]
    public async Task<WorkItemModel> Post([FromBody] CreateWorkItemCommand command)
    {
        var request = new CreateWorkItem.Command
        {
            Project = command.Project,
            Name = command.Name,
            Description = command.Description
        };
        var response = await _mediator.Send(request);
        return _mapper.Map<WorkItemModel>(response.WorkItem);
    }
}