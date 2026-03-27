namespace Application.Interfaces;

public interface IMapper
{
    TDestination Map<TSource, TDestination>(TSource source);
    IEnumerable<TDestination> MapList<TSource, TDestination>(IEnumerable<TSource> sources);
    IQueryable<TDestination> ProjectTo<TSource, TDestination>(IQueryable<TSource> query);
}