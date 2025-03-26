# GameStore API Project

This project is a RESTful API for managing games and genres in a game store. It demonstrates how to perform CRUD (Create, Read, Update, Delete) operations using .NET and Entity Framework Core with a SQLite database.

---

## 🚀 How to Run the Project

### 1. Clone the Repository
First, clone the repository to your local machine:
```bash
git clone https://github.com/AlirezaNoorizadeh/GameStore.git
```

### 2. Database Setup
The project uses SQLite as the database, which is configured automatically:
- The database file `GameStore.db` will be created when you run the application for the first time.
- No additional connection string configuration is needed for development.

### 3. Apply Database Migrations
Run the following command to apply the existing migrations:
```bash
dotnet ef database update
```

### 4. Build and Run the Project
- Build the project:
```bash
dotnet build
```
- Run the project:
```bash
dotnet run
```

### 5. Test the API
The project includes `.http` files for testing the API endpoints:
- Use `games.http` to test game-related endpoints
- Use `genres.http` to test genre-related endpoints

---

## 🎯 Project Goal
The goal of this project is to demonstrate how to:
- Create a RESTful API with .NET
- Implement CRUD operations for games and genres
- Use Entity Framework Core with SQLite
- Implement proper DTOs and mapping
- Organize a clean project structure

---

## 📂 Project Structure
Here's an overview of the project structure:

| Folder/File               | Description                                  |
|---------------------------|----------------------------------------------|
| **📁 Data**               | Contains database-related files             |
| - `GameStoreContext.cs`   | DbContext for Entity Framework Core         |
| - `DataExtensions.cs`     | Extension methods for data operations      |
| - `Migrations/`          | Entity Framework Core migration files      |
|                           |                                              |
| **📁 DTOs**               | Data Transfer Objects                      |
| - `CreateGameDto.cs`     | DTO for creating a game                    |
| - `GameDetailsDto.cs`    | DTO for game details                       |
| - `GameSummaryDto.cs`    | DTO for game summaries                     |
| - `GenreDto.cs`          | DTO for genres                             |
| - `UpdateGameDto.cs`     | DTO for updating a game                    |
|                           |                                              |
| **📁 Endpoints**          | API endpoint definitions                   |
| - `GamesEndpoints.cs`    | Game-related endpoints                     |
| - `GenresEndpoint.cs`    | Genre-related endpoints                    |
|                           |                                              |
| **📁 Entities**           | Database entities                          |
| - `Game.cs`              | Game entity model                          |
| - `Genre.cs`             | Genre entity model                         |
|                           |                                              |
| **📁 Mapping**            | Mapping configurations                     |
| - `GameMapping.cs`       | Mapping between Game and DTOs              |
| - `GenreMapping.cs`      | Mapping between Genre and DTOs             |
|                           |                                              |
| **📄 Program.cs**         | Application entry point and configuration  |
| **📄 appsettings.json**   | Configuration file                         |
| **📄 *.http**             | Files for testing API endpoints            |

---

## 🔗 Database Relationships
The project uses the following entity relationships:
- **Game to Genre**: Many-to-One relationship (Many games can belong to one genre)
  - Each `Game` has a `GenreId` foreign key
  - Each `Game` has a navigation property `Genre`
  - Each `Genre` can be associated with multiple `Game` entities

---

## 🌐 API Endpoints

### Games Endpoints (`/games`)
| Method | Endpoint    | Description                          | Request Body           | Response              |
|--------|-------------|--------------------------------------|------------------------|-----------------------|
| GET    | /games      | Get all games (summary view)         | None                   | List of GameSummaryDto |
| GET    | /games/{id} | Get a specific game (detailed view)  | None                   | GameDetailsDto        |
| POST   | /games      | Create a new game                    | CreateGameDto          | Created GameDetailsDto |
| PUT    | /games/{id} | Update an existing game              | UpdateGameDto          | No content (204)      |
| DELETE | /games/{id} | Delete a game                        | None                   | No content (204)      |

### Genres Endpoints (`/genres`)
| Method | Endpoint    | Description                          | Request Body | Response          |
|--------|-------------|--------------------------------------|--------------|-------------------|
| GET    | /genres     | Get all genres                      | None         | List of GenreDto  |

---

## 🔒 Validation
The API includes input validation using:
- **MinimalApis.Extensions** NuGet package for automatic model validation
- Data annotations on DTOs for validation rules

---

## 🛠️ Tools and Technologies Used
- **.NET** for the API framework
- **Entity Framework Core** for database operations
- **SQLite** as the database
- **MinimalApis.Extensions** for validation
- **Visual Studio/VSCode** as development environments

---

## 📜 License [ [![MIT License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE) ]
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

## 🙏 Acknowledgments
Special thanks to:
- The .NET and Entity Framework Core communities for their excellent documentation and support.
- **[Julio Casal](https://www.youtube.com/@juliocasal)** for his comprehensive tutorial video ["ASP.NET Core Full Course For Beginners"](https://youtu.be/AhAxLiGC7Pc) which served as the foundation for this implementation. This project follows the architecture and patterns demonstrated in his tutorial.

---

The implementation in this project closely follows the concepts and structure taught in Julio Casal's tutorial video, adapted specifically for a game store management system. The clean architecture, endpoint organization, and data access patterns all reflect the approaches demonstrated in that excellent beginner-friendly course.
