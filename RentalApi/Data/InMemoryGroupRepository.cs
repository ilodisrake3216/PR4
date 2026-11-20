using LanguageLearnApi.Models;

namespace LanguageLearnApi.Data;

public class InMemoryGroupRepository : IGroupRepository
{
    public InMemoryGroupRepository()
    {
        string[] languages = { "Английский", "Немецкий", "Испанский", "Французский", "Китайский" };
        string[] teachers = { "Иванова А.С.", "Петров Д.В.", "Смирнова Е.К.", "Волков М.И.", "Соколова О.Н." };
        string[] levels = { "A1", "A2", "B1", "B2", "C1" };

        for (int i = 0; i < 2000; i++)
        {
            _items.Add(new Group
            {
                Id = _nextId++,
                Language = $"{languages[i % languages.Length]} {levels[i % levels.Length]}",
                Teacher = $"{teachers[i % teachers.Length]} #{i + 1:0000}",
                Rating = 3.5m + (i % 16) * 0.1m,
                StarUser = (i % 5 == 0) ? (100 + i) : null,
                Link = $"https://meet.school.local/group-{i + 1:0000}",
                Listners = new List<int> { 1000 + i, 2000 + i }
            });
        }
        foreach (Group item in _items)
        {
            _byId[item.Id] = item;
        }
    }
    private readonly Dictionary<int, Group> _byId = new();
    private readonly List<Group> _sortedView = new();
    private bool _isDirty = true;
    public Task<IReadOnlyList<Group>> GetPageAsync(
        int page, int pageSize, CancellationToken cancellationToken = default)
    {
        lock (_sync)
        {
            List<Group> sorted = GetSortedViewLocked();
            IReadOnlyList<Group> items = sorted
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Task.FromResult(items);
        }
    }

    public Task<Group> AddAsync(Group group, CancellationToken cancellationToken = default)
    {
        lock (_sync)
        {
            group.Id = _nextId++;
            _items.Add(group);
            _byId[group.Id] = group;
            _isDirty = true;
            return Task.FromResult(group);
        }
    }

    public Task RemoveAsync(Group group, CancellationToken cancellationToken)
    {
        lock (_sync)
        {
            _items.Remove(group);
            _byId.Remove(group.Id);
            _isDirty = true;
            return Task.CompletedTask;
        }
    }

    public Task<Group> CreateAsync(Group group, CancellationToken cancellationToken = default)
    {
        return AddAsync(group, cancellationToken);
    }

    private readonly List<Group> _items = new()
    {
        new() { Id = 1, Language = "Английский", Teacher = "Иван Иванов", Rating = 4.5m, Link = "https://t.me" },
        new() { Id = 2, Language = "Немецкий", Teacher = "Анна Петрова", Rating = 4.2m },
    };
    private readonly object _sync = new();
    private int _nextId = 3;

    public Task<Group?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        lock (_sync)
        {
            return Task.FromResult(
                _byId.TryGetValue(id, out Group? item) ? item : null);
        }
    }

    public Task<bool> CheckAvailabilityAsync(
        int id, CancellationToken cancellationToken = default)
    {
        lock (_sync)
        {
            var group = _items.FirstOrDefault(g => g.Id == id);
            bool isAvailable = group != null && group.Listners.Count < 15;
            return Task.FromResult(isAvailable);
        }
    }

    public Task<IReadOnlyList<int>> GetParticipantsAsync(
        int id, CancellationToken cancellationToken = default)
    {
        lock (_sync)
        {
            var group = _items.FirstOrDefault(g => g.Id == id);
            IReadOnlyList<int> result = group != null ? group.Listners.ToList() : new List<int>();
            return Task.FromResult(result);
        }
    }

    public Task<bool> AddParticipantAsync(
        int groupId, int participantId, CancellationToken cancellationToken = default)
    {
        lock (_sync)
        {
            var group = _items.FirstOrDefault(g => g.Id == groupId);
            if (group == null || group.Listners.Contains(participantId))
            {
                return Task.FromResult(false);
            }

            group.Listners.Add(participantId);
            return Task.FromResult(true);
        }
    }

    public Task<bool> RemoveParticipantAsync(
        int groupId, int participantId, CancellationToken cancellationToken = default)
    {
        lock (_sync)
        {
            var group = _items.FirstOrDefault(g => g.Id == groupId);
            if (group == null)
            {
                return Task.FromResult(false);
            }

            bool removed = group.Listners.Remove(participantId);
            return Task.FromResult(removed);
        }
    }
    public Task<IReadOnlyList<Group>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        lock (_sync)
        {
            IReadOnlyList<Group> copy = _items.ToList();
            return Task.FromResult(copy);
        }
    }
    public Task<bool> SendPingAsync(
        int id, string message, CancellationToken cancellationToken = default)
    {
        lock (_sync)
        {
            var group = _items.FirstOrDefault(g => g.Id == id);
            if (group == null || string.IsNullOrWhiteSpace(message))
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }
    }
    public Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        lock (_sync)
        {
            return Task.FromResult(_items.Count);
        }
    }
    private List<Group> GetSortedViewLocked()
    {
        if (_isDirty)
        {
            _sortedView.Clear();
            _sortedView.AddRange(_items
                .OrderBy(g => g.Language, StringComparer.Ordinal)
                .ThenBy(g => g.Id));
            _isDirty = false;
        }

        return _sortedView;
    }
}
