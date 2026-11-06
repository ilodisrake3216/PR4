using LanguageLearnApi.Models;

namespace LanguageLearnApi.Data;

public interface IGroupRepository
{
    Task<IReadOnlyList<Group>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Group> AddAsync(Group group, CancellationToken cancellationToken = default);
    Task<Group> CreateAsync(Group group, CancellationToken cancellationToken = default);
    Task<Group?> GetByIdAsync(
        int id, CancellationToken cancellationToken = default);

    Task<bool> CheckAvailabilityAsync(
        int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<int>> GetParticipantsAsync(
        int id, CancellationToken cancellationToken = default);

    Task<bool> AddParticipantAsync(
        int groupId, int participantId, CancellationToken cancellationToken = default);

    Task<bool> RemoveParticipantAsync(
        int groupId, int participantId, CancellationToken cancellationToken = default);

    Task<bool> SendPingAsync(
        int id, string message, CancellationToken cancellationToken = default);
}