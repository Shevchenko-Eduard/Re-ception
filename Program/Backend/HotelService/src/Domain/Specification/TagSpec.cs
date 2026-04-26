using Domain.Specification.BaseSpecification;

namespace Domain.Specification;

public record TagSpec(
    Pagination? Pagination = null,
    SortType? SortType = null,
    FilterType? FilterType = null,
    string? SortBy = null,
    int? Id = null,
    string? Name = null,
    string? Description = null
);