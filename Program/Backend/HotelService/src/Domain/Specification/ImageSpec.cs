using Domain.Specification.BaseSpecification;

namespace Domain.Specification;

public record ImageSpec(
    Pagination? Pagination = null,
    SortType? SortType = null,
    FilterType? FilterType = null,
    string? SortBy = null,
    int? Id = null,
    int? HotelId = null,
    string? ImageKey = null
);