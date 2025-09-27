using GainFlow.Api.Shared.Contracts.Responses;
using GainFlow.Api.Shared.Domain.Entities;

namespace GainFlow.Api.Shared.Abstractions;

public sealed class WorkoutProgramResponseProjection : IDataQuery<WorkoutProgram, WorkoutProgramResponse>
{
    public IQueryable<WorkoutProgramResponse> Apply(IQueryable<WorkoutProgram> query)
    {
        return query.Select(wp => new WorkoutProgramResponse
        {
            Id = wp.Id,
            Name = wp.Name,
            Description = wp.Description,
            DurationWeeks = wp.DurationWeeks,
            IsPublic = wp.IsPublic,
            CreatedAt = wp.CreatedAt
        });
    }
}