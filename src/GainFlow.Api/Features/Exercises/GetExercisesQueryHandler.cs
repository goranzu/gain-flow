using GainFlow.Api.Shared.Abstractions;
using GainFlow.Api.Shared.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GainFlow.Api.Features.Exercises;

public sealed class
    GetExercisesQueryHandler : IQueryHandler<GetExercisesQuery, PaginatedResponse<GetExerciseResponse>>
{
    private readonly ApplicationDbContext _applicationDbContext;

    public GetExercisesQueryHandler(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<PaginatedResponse<GetExerciseResponse>> Handle(GetExercisesQuery query,
        CancellationToken cancellationToken)
    {
        IQueryable<GetExerciseResponse> exercisesQuery = _applicationDbContext.Exercises
            .AsNoTracking()
            .OrderBy(e => e.Name)
            .Include(ex => ex.MuscleGroups)
            .Select(ex => new GetExerciseResponse
            {
                Id = ex.Id,
                Name = ex.Name,
                PrimaryMuscles = ex.PrimaryMuscles.ToArray(),
                SecondaryMuscles = ex.SecondaryMuscles.ToArray()
            });

        var response = await PaginatedResponse<GetExerciseResponse>.Create(exercisesQuery,
            query.Page,
            query.PageSize,
            cancellationToken);

        return response;
    }
}
