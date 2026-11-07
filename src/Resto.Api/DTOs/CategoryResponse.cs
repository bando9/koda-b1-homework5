namespace Resto.Api.DTOs;

public record CategoryResponse(
    int Id,
    string Name,
    DateTime CreatedAt
);