namespace GainFlow.Api.Shared.Contracts.Responses;

public sealed record WorkoutProgramResponse
{
    public string Id { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public int DurationWeeks { get; init; }
    public bool IsPublic { get; init; }
    public DateTime CreatedAt { get; init; }
}
