using GainFlow.Api.Shared.Abstractions;
using GainFlow.Api.Shared.Domain.Entities;

namespace GainFlow.Api.Shared.Persistence.Queries;

public sealed class ExerciseQuery : IQueryObject<Exercise, Exercise>
{
    private readonly List<IQueryObject<Exercise, Exercise>> _queryObjects = [];

    public ExerciseQuery ById(string exerciseId)
    {
        AddFilter(new ExerciseById(exerciseId));
        return this;
    }

    public IQueryable<Exercise> Apply(IQueryable<Exercise> query)
    {
        foreach (IQueryObject<Exercise, Exercise> queryObject in _queryObjects)
        {
            query = queryObject.Apply(query);
        }

        return query;
    }

    private void AddFilter(IQueryObject<Exercise, Exercise> query)
    {
        _queryObjects.Add(query);
    }


    private sealed class ExerciseById(string exerciseId) : IQueryObject<Exercise, Exercise>
    {
        public IQueryable<Exercise> Apply(IQueryable<Exercise> query)
        {
            return query.Where(e => e.Id == exerciseId);
        }
    }
}
