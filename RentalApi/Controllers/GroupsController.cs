using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using LanguageLearnApi.Data;
using LanguageLearnApi.Models;
namespace LanguageLearnApi.Models.Controllers;
[ApiController]
[Route("api/[controller]")]
public class GroupsController : ControllerBase
{
    private const string CategoriesCacheKey = "groups-categories";
    private readonly IGroupRepository _repository;
    private readonly ILogger<GroupsController> _logger;
    private readonly IMemoryCache _cache;
    public GroupsController(
        IGroupRepository repository,
        ILogger<GroupsController> logger,
        IMemoryCache cache)
    {
        _repository = repository;
        _logger = logger;
        _cache = cache;
    }

    /// <summary>
    /// Удаляет группу по её идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор группы.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="200">Группа успешно удалена.</response>
    /// <response code="404">Группа не найдена.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        Group? item = await _repository.GetByIdAsync(id, cancellationToken);
        if (item is null)
            return NotFound();

        await _repository.RemoveAsync(item, cancellationToken);
        _logger.LogInformation("Удалена группа с ID {GroupId}", id);

        return Ok();
    }

    /// <summary>
    /// Возвращает группу по её идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор группы.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="200">Группа найдена.</response>
    /// <response code="404">Группа с указанным ID не найдена.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Group>> GetById(
        int id, CancellationToken cancellationToken)
    {
        Group? item = await _repository.GetByIdAsync(id, cancellationToken);
        if (item is null)
            return NotFound();

        return Ok(item);
    }

    /// <summary>
    /// Проверяет наличие свободных мест в группе.
    /// </summary>
    /// <param name="id">Идентификатор группы.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="200">Возвращает true, если места есть, или false.</response>
    /// <response code="404">Группа не найдена.</response>
    [HttpGet("{id}/availability")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<bool>> GetAvailability(
        int id, CancellationToken cancellationToken)
    {
        Group? item = await _repository.GetByIdAsync(id, cancellationToken);
        if (item is null)
            return NotFound();

        var isAvailable = await _repository.CheckAvailabilityAsync(id, cancellationToken);
        return Ok(isAvailable);
    }

    /// <summary>
    /// Возвращает список всех учебных групп.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="200">Список групп успешно получен.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    /// <summary>
    /// Возвращает страницу учебных групп.
    /// </summary>
    /// <param name="page">Номер страницы (начиная с 1).</param>
    /// <param name="pageSize">Размер страницы (от 1 до 200).</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="200">Страница групп успешно получена.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<PagedResult<GroupListItem>>> GetAll(
    int page = 1,
    int pageSize = 50,
    CancellationToken cancellationToken = default)
    {
        page = Math.Max(1, page);
        pageSize = Math.Clamp(pageSize, 1, 200);
        int total = await _repository.CountAsync(cancellationToken);
        IReadOnlyList<Group> items = await _repository.GetPageAsync(page, pageSize, cancellationToken);
        var result = new PagedResult<GroupListItem>
        {
            Total = total,
            Page = page,
            PageSize = pageSize,
            Items = items.Select(g => new GroupListItem
            {
                Id = g.Id,
                Language = g.Language,
                Teacher = g.Teacher,
                Rating = g.Rating
            }).ToList()
        };

        return Ok(result);
    }

    /// <summary>
    /// Возвращает список участников группы.
    /// </summary>
    /// <param name="id">Идентификатор группы.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="200">Список участников получен.</response>
    /// <response code="404">Группа не найдена.</response>
    [HttpGet("{id}/participants")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyList<int>>> GetParticipants(
        int id, CancellationToken cancellationToken)
    {
        Group? item = await _repository.GetByIdAsync(id, cancellationToken);
        if (item is null)
            return NotFound();

        var participants = await _repository.GetParticipantsAsync(id, cancellationToken);
        return Ok(participants);
    }

    /// <summary>
    /// Добавляет нового участника в группу.
    /// </summary>
    /// <param name="id">Идентификатор группы.</param>
    /// <param name="request">Данные участника.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="200">Участник успешно добавлен.</response>
    /// <response code="400">Ошибка при добавлении.</response>
    /// <response code="404">Группа не найдена.</response>
    /// <response code="409">Участник уже состоит в этой группе.</response>
    [HttpPost("{id}/participants")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
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

    /// <summary>
    /// Отправляет оповещение участникам группы.
    /// </summary>
    /// <param name="id">Идентификатор группы.</param>
    /// <param name="request">Текст сообщения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="200">Оповещение отправлено.</response>
    /// <response code="400">Не удалось отправить сообщение.</response>
    /// <response code="404">Группа не найдена.</response>
    [HttpPost("{id}/ping")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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

    /// <summary>
    /// Создает новую учебную группу.
    /// </summary>
    /// <param name="request">Данные для создания группы.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="201">Группа успешно создана.</response>
    /// <response code="400">Ошибка валидации.</response>
    /// <response code="409">Группа с таким языком и преподавателем уже существует.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
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

    /// <summary>
    /// Удаляет участника из группы.
    /// </summary>
    /// <param name="id">Идентификатор группы.</param>
    /// <param name="participantId">Идентификатор участника.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="200">Участник успешно удален.</response>
    /// <response code="404">Группа или участник не найдены.</response>
    [HttpDelete("{id}/participants/{participantId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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
    /// <summary>
    /// Возвращает список уникальных языков/направлений групп (кэшируется на 10 минут).
    /// </summary>
    [HttpGet("categories")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<string>>> GetCategories(
        CancellationToken cancellationToken)
    {
        List<string>? categories = await _cache.GetOrCreateAsync(
            CategoriesCacheKey,
            async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                _logger.LogInformation("Категории загружены из хранилища");

                IReadOnlyList<Group> items = await _repository.GetAllAsync(cancellationToken);

                return items
                    .Select(g => g.Language)
                    .Where(c => !string.IsNullOrWhiteSpace(c))
                    .Distinct()
                    .OrderBy(c => c)
                    .ToList();
            });

        return Ok(categories ?? new List<string>());
    }

    /// <summary>
    /// Сбрасывает кэш списка категорий (языков).
    /// </summary>
    [HttpPost("reset-cache")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult ResetCache()
    {
        _cache.Remove(CategoriesCacheKey);
        return NoContent();
    }
}