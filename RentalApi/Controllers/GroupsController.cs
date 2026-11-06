using Microsoft.AspNetCore.Mvc;
using LanguageLearnApi.Data;
using LanguageLearnApi.Models;
namespace LanguageLearnApi.Models.Controllers;
[ApiController]
[Route("api/[controller]")]
public class GroupsController : ControllerBase
{
    private readonly IGroupRepository _repository;
    private readonly ILogger<GroupsController> _logger;

    public GroupsController(
        IGroupRepository repository,
        ILogger<GroupsController> logger)
    {
        _repository = repository;
        _logger = logger;
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Group>> GetById(
        int id, CancellationToken cancellationToken)
    {
        Group? item = await _repository.GetByIdAsync(id, cancellationToken);
        if (item is null)
            return NotFound();

        return Ok(item);
    }
    [HttpGet("{id}/availability")]
    public async Task<ActionResult<bool>> GetAvailability(
        int id, CancellationToken cancellationToken)
    {
        Group? item = await _repository.GetByIdAsync(id, cancellationToken);
        if (item is null)
            return NotFound();

        var isAvailable = await _repository.CheckAvailabilityAsync(id, cancellationToken);
        return Ok(isAvailable);
    }
    [HttpGet("{id}/participants")]
    public async Task<ActionResult<IReadOnlyList<int>>> GetParticipants(
        int id, CancellationToken cancellationToken)
    {
        Group? item = await _repository.GetByIdAsync(id, cancellationToken);
        if (item is null)
            return NotFound();

        var participants = await _repository.GetParticipantsAsync(id, cancellationToken);
        return Ok(participants);
    }
    [HttpPost("{id}/participants")]
    public async Task<IActionResult> AddParticipant(
        int id, [FromBody] AddParticipantRequest request, CancellationToken cancellationToken)
    {
        Group? item = await _repository.GetByIdAsync(id, cancellationToken);
        if (item is null)
            return NotFound();

        var participants = await _repository.GetParticipantsAsync(id, cancellationToken);
        if (participants.Contains(request.ParticipantId))
            return Conflict();

        var success = await _repository.AddParticipantAsync(id, request.ParticipantId, cancellationToken);
        if (!success)
            return BadRequest();

        return Ok();
    }
    [HttpPost("{id}/ping")]
    public async Task<IActionResult> PingGroup(
        int id, [FromBody] PingGroupRequest request, CancellationToken cancellationToken)
    {
        Group? item = await _repository.GetByIdAsync(id, cancellationToken);
        if (item is null)
            return NotFound();

        var success = await _repository.SendPingAsync(id, request.Message, cancellationToken);
        if (!success)
            return BadRequest();

        return Ok();
    }
    [HttpPost]
    public async Task<ActionResult<Group>> Create(
    [FromBody] CreateGroupRequest request, CancellationToken cancellationToken)
    {
        var allGroups = await _repository.GetAllAsync(cancellationToken);
        bool exists = allGroups.Any(g =>
            g.Language.Equals(request.Language, StringComparison.OrdinalIgnoreCase) &&
            g.Teacher.Equals(request.Teacher, StringComparison.OrdinalIgnoreCase));

        if (exists)
        {
            return Conflict($"Группа по языку '{request.Language}' с преподавателем {request.Teacher} уже существует.");
        }

        var group = new Group
        {
            Language = request.Language,
            Teacher = request.Teacher,
            Rating = request.Rating,
            Link = request.Link,
            Listners = new List<int>()
        };

        var created = await _repository.AddAsync(group, cancellationToken);
        _logger.LogInformation("Создана группа с ID {GroupId}", created.Id);

        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }
    [HttpDelete("{id}/participants/{participantId}")]
    public async Task<IActionResult> RemoveParticipant(
        int id, int participantId, CancellationToken cancellationToken)
    {
        Group? item = await _repository.GetByIdAsync(id, cancellationToken);
        if (item is null)
            return NotFound();

        var success = await _repository.RemoveParticipantAsync(id, participantId, cancellationToken);
        if (!success)
            return NotFound();

        return Ok();
    }
}