namespace BookingApp.Application.Common;

public record PageResponse<T>(
    int Page,
    int PageSize,
    int TotalCount,
    IReadOnlyCollection<T> Data) 
    where T : class;