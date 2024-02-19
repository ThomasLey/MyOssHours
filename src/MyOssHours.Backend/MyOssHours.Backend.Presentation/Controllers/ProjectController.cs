using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyOssHours.Backend.Application.Projects;
using MyOssHours.Backend.Domain.Entities;
using MyOssHours.Backend.Presentation.Models;
using static MyOssHours.Backend.Application.Projects.GetProjects;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyOssHours.Backend.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectController : ControllerBase
{
    private readonly IHttpContextAccessor _httpContext;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public ProjectController(IMediator mediator, IMapper mapper, IHttpContextAccessor httpContext)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _httpContext = httpContext ?? throw new ArgumentNullException(nameof(httpContext));
    }

    // GET: api/<ProjectController>
    [HttpGet]
    public async Task<IEnumerable<ProjectModel>> Get([FromQuery] GetProjectsQuery query)
    {
        var request = new Query
        {
            Offset = query.Offset,
            Size = query.Size
        };
        var response = await _mediator.Send(request);
        return response.Projects.Select(x => _mapper.Map<ProjectModel>(x));
    }

    // GET api/<ProjectController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/<ProjectController>
    [HttpPost]
    public async Task<ProjectModel> Post([FromBody] CreateProjectCommand command)
    {
        var request = new CreateProject.Command
        {
            Name = command.Name,
            Description = command.Description,
            Owner = ((User)_httpContext.HttpContext.Items["User"]).Uuid
        };
        var response = await _mediator.Send(request);
        return _mapper.Map<ProjectModel>(response.Project);
    }

    // PUT api/<ProjectController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<ProjectController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}