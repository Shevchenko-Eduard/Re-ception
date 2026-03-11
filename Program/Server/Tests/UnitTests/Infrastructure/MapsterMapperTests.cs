using Infrastructure;

namespace UnitTests.Infrastructure;

public class MapsterMapperTests
{
    private class Source
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public SourceIncluded? Included { get; set; }
        public class SourceIncluded
        {
            public int Id { get; set; }
            public string? Name { get; set; }
        }
    }
    private class Destination
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int IncludedId { get; set; }
        public string? IncludedName { get; set; }
    }
    [Fact]
    public void MapsterMapper_Map_ReturnsMappedObject()
    {
        var source = new Source { Id = 1, Name = "Test" };
        var MapsterMapper = new MapsterMapper<Source, Destination>();
        var destination = MapsterMapper.Map(source);
        Assert.Equal(source.Id, destination.Id);
        Assert.Equal(source.Name, destination.Name);
    }
    [Fact]
    public void MapsterMapper_MapList_ReturnsMappedList()
    {
        var sources = new List<Source>
        {
            new() { Id = 1, Name = "Test1" },
            new() { Id = 2, Name = "Test2" }
        };
        var MapsterMapper = new MapsterMapper<Source, Destination>();
        var destinations = MapsterMapper.MapList(sources).ToList();
        Assert.Equal(sources.Count, destinations.Count);
        for (int i = 0; i < sources.Count; i++)
        {
            Assert.Equal(sources[i].Id, destinations[i].Id);
            Assert.Equal(sources[i].Name, destinations[i].Name);
        }
    }
    [Fact]
    public void MapsterMapper_Map_ReturnsMappedIncludedObject()
    {
        var sources = new List<Source>
        {
            new() { Id = 1, Name = "Test1", Included = new Source.SourceIncluded { Id = 10, Name = "Included1" } },
            new() { Id = 2, Name = "Test2", Included = new Source.SourceIncluded { Id = 20, Name = "Included2" } }
        }.AsQueryable();
        var MapsterMapper = new MapsterMapper<Source, Destination>();
        var destinations = MapsterMapper.MapList(sources).ToList();
        Assert.Equal(sources.Count(), destinations.Count);
        for (int i = 0; i < sources.Count(); i++)
        {
            Assert.Equal(sources.ElementAt(i).Id, destinations[i].Id);
            Assert.Equal(sources.ElementAt(i).Name, destinations[i].Name);
            Assert.Equal(sources.ElementAt(i).Included!.Id, destinations[i].IncludedId);
            Assert.Equal(sources.ElementAt(i).Included!.Name, destinations[i].IncludedName);
        }
    }
}
