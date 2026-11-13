using LanguageLearnApi.Models;

namespace LanguageLearnApi.Data;

public class InMemoryGroupRepository : IGroupRepository
{
    public Task<Group> AddAsync(Group group, CancellationToken cancellationToken = default)
    {
        lock (_sync)
        {
            group.Id = _nextId++;
            _items.Add(group);
            return Task.FromResult(group);
        }
    }
    public Task<Group> CreateAsync(Group group, CancellationToken cancellationToken = default)
    {
        lock (_sync)
        {
            group.Id = _nextId++;
            _items.Add(group);
            return Task.FromResult(group);
        }
    }
    public Task RemoveAsync(Group group, CancellationToken cancellationToken)
    {
        _items.Remove(group);
        return Task.CompletedTask;
    }
    private readonly List<Group> _items = new()
    {
        new() { Id = 1, Language = "Английский", Teacher = "Иван Иванов", Rating = 4.5m, Link = "https://t.me" },
        new() { Id = 2, Language = "Немецкий", Teacher = "Анна Петрова", Rating = 4.2m },
    };
    private readonly object _sync = new();
    private int _nextId = 3;

    public Task<Group?> GetByIdAsync(
        int id, CancellationToken cancellationToken = default)
    {
        lock (_sync)
        {
            return Task.FromResult(_items.FirstOrDefault(g => g.Id == id));
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
}
