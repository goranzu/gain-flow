using GainFlow.Api.Shared.Abstractions;

namespace GainFlow.Api.Features.Exercises;

public sealed class GetExercisesQuery : IQuery<PaginatedResponse<GetExerciseResponse>>
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
