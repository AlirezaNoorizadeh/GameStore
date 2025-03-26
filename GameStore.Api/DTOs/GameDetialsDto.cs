namespace GameStore.Api.DTOs;

public record class GameDetialsDto
(
    int Id, 
    string Name, 
    int GenreId, 
    decimal Price, 
    DateOnly ReleaseDate
);

