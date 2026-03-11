using Application.Interfaces;
using Mapster;

namespace Infrastructure;

public class MapsterMapper<TSource, TDestination> : IMapper<TSource, TDestination>
{
    public TDestination Map(TSource source)
    {
        return source.Adapt<TDestination>();
    }

    public IEnumerable<TDestination> MapList(IEnumerable<TSource> sources)
    {
        return sources.Adapt<IEnumerable<TDestination>>();
    }

    public IQueryable<TDestination> ProjectTo(IQueryable<TSource> query)
    {
        return query.ProjectToType<TDestination>();
    }
}
