using Domain.Specification.BaseSpecification;

namespace Domain.Specification;

public record HotelSpec(
    Pagination? Pagination = null,
    SortType? SortType = null,
    FilterType? FilterType = null,
    string? SortBy = null,
    int? Id = null,
    double? Latitude = null,
    double? Longitude = null,
    string? Email = null,
    string? Phone = null
);