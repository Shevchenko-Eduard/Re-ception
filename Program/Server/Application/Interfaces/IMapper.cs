namespace Application.Interfaces;

public interface IMapper<TSource, TDestination>
{
    //Мэппер простой, один метод для конвертирования и кэширования конвертируемых типов
    TDestination Map(TSource source);
    IEnumerable<TDestination> MapList(IEnumerable<TSource> sources);
    IQueryable<TDestination> ProjectTo(IQueryable<TSource> query);
}