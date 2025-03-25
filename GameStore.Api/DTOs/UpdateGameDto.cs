namespace GameStore.Api.DTOs;

public record class UpdateGameDto(
    string Name, 
    string Genre, 
    decimal Price, 
    DateOnly ReleaseDate);