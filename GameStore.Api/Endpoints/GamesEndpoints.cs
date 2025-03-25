using System;
using GameStore.Api.DTOs;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndPointName = "GetGame";

    private static readonly List<GameDto> games = [
        new GameDto(1, "The Witcher 3: Wild Hunt", "RPG", 29.99m, new DateOnly(2015, 5, 19)),
        new GameDto(2, "FIFA 23", "Sports", 59.99m, new DateOnly(2022, 9, 30)),
        new GameDto(3, "Cyberpunk 2077", "Action RPG", 39.99m, new DateOnly(2020, 12, 10)),
        new GameDto(4, "Minecraft", "Sandbox", 19.99m, new DateOnly(2011, 11, 18))
    ];

    public static WebApplication MapGamesEndpoints(this WebApplication app)
    {
        //GET /games
        app.MapGet("games", () => games);


        //GET /games/1
        app.MapGet("games/{id}", (int id) =>
        {
            GameDto? game = games.Find(x => x.Id == id);

            return game is null ? Results.NotFound() : Results.Ok(game);
        }
        ).WithName(GetGameEndPointName);


        //POST /games
        app.MapPost("games", (CreateGameDto newGame) =>
        {
            GameDto game = new(
                games.Count + 1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate
            );

            games.Add(game);

            return Results.CreatedAtRoute(GetGameEndPointName, new { id = game.Id }, game);
        });


        //PUT /games/1
        app.MapPut("games/{id}", (int id, UpdateGameDto updateGame) =>
        {
            var index = games.FindIndex(x => x.Id == id);

            if (index == -1)
            {
                return Results.NotFound();
            }


            games[index] = new GameDto(
                id,
                updateGame.Name,
                updateGame.Genre,
                updateGame.Price,
                updateGame.ReleaseDate
            );

            return Results.NoContent();
        });


        // DELETE /games/1
        app.MapDelete("games/{id}", (int id) =>
        {
            games.RemoveAll(x => x.Id == id);

            return Results.NoContent();
        });


        return app;
    }
}
