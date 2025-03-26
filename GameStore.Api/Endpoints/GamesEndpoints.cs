using System;
using GameStore.Api.Data;
using GameStore.Api.DTOs;
using GameStore.Api.Entities;
using GameStore.Api.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndPointName = "GetGame";

    // private static readonly List<GameSummaryDto> games = [
    //     new GameSummaryDto(1, "The Witcher 3: Wild Hunt", "RPG", 29.99m, new DateOnly(2015, 5, 19)),
    //     new GameSummaryDto(2, "FIFA 23", "Sports", 59.99m, new DateOnly(2022, 9, 30)),
    //     new GameSummaryDto(3, "Cyberpunk 2077", "Action RPG", 39.99m, new DateOnly(2020, 12, 10)),
    //     new GameSummaryDto(4, "Minecraft", "Sandbox", 19.99m, new DateOnly(2011, 11, 18))
    // ];

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        // .WithParameterValidation() is an extension method from the MinimalApis.Extensions 
        // NuGet package for enabling DTO annotation validation.
        var group = app.MapGroup("games").WithParameterValidation();

        //GET /games
        group.MapGet("/", (GameStoreContext dbContext) => dbContext.Games
                                                    .Include(x => x.Genre)
                                                    .Select(x => x.ToGameSummaryDto())
                                                    .AsNoTracking());


        //GET /games/1
        group.MapGet("/{id}", (int id, GameStoreContext dbContext) =>
        {
            Game? game = dbContext.Games.Find(id);

            return game is null ? Results.NotFound() : Results.Ok(game.ToGameDetialsDto());
        }
        ).WithName(GetGameEndPointName);


        //POST /games
        group.MapPost("/", (CreateGameDto newGame, GameStoreContext dbContext) =>
        {
            //simple validation (not recommended)
            // if (string.IsNullOrEmpty(newGame.Name))
            // {
            //     return Results.BadRequest("Name is required");
            // }


            // add mapping to use it instead of this
            // Game game = new()
            // {
            //     Name = newGame.Name,
            //     Genre = dbContext.Genres.Find(newGame.GenreId),
            //     GenreId = newGame.GenreId,
            //     Price = newGame.Price,
            //     ReleaseDate = newGame.ReleaseDate
            // };

            Game game = newGame.ToEntity();

            dbContext.Games.Add(game);
            dbContext.SaveChanges();

            // add mapping to use it instead of this
            // GameDto gameDto = new(
            //     game.Id,
            //     game.Name,
            //     game.Genre!.Name,
            //     game.Price,
            //     game.ReleaseDate
            // );

            GameDetialsDto gameDto = game.ToGameDetialsDto();

            return Results.CreatedAtRoute(GetGameEndPointName, new { id = game.Id }, gameDto);
        });


        //PUT /games/1
        group.MapPut("/{id}", (int id, UpdateGameDto updateGame, GameStoreContext dbContext) =>
        {
            // var index = games.FindIndex(x => x.Id == id);
            var existingGame = dbContext.Games.Find(id);

            if (existingGame is null)
            {
                return Results.NotFound();
            }


            // games[index] = new GameSummaryDto(
            //     id,
            //     updateGame.Name,
            //     updateGame.Genre,
            //     updateGame.Price,
            //     updateGame.ReleaseDate
            // );

            dbContext.Entry(existingGame).CurrentValues.SetValues(updateGame.ToEntity(id));
            dbContext.SaveChanges();

            return Results.NoContent();
        });


        // DELETE /games/1
        group.MapDelete("/{id}", (int id, GameStoreContext dbContext) =>
        {
            // games.RemoveAll(x => x.Id == id);

            dbContext.Games.Where(x => x.Id == id).ExecuteDelete();

            return Results.NoContent();
        });


        return group;
    }
}
