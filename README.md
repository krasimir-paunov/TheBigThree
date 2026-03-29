# 🎮 The Big Three

> *What are the three games that define you as a gamer?*

**The Big Three** is a community-driven ASP.NET Core web application where gamers curate and share their ultimate three-game collections — the games that shaped them, defined them, or simply brought them the most joy. Discover what others have chosen, give stars to collections you love, and leave your mark on the community.

[![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-8.0-blue?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com/)
[![EF Core](https://img.shields.io/badge/EF%20Core-8.0-purple?style=flat-square)](https://docs.microsoft.com/en-us/ef/core/)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-LocalDB-red?style=flat-square&logo=microsoftsqlserver)](https://www.microsoft.com/en-us/sql-server)
[![Bootstrap](https://img.shields.io/badge/Bootstrap-5-blueviolet?style=flat-square&logo=bootstrap)](https://getbootstrap.com/)
[![NUnit](https://img.shields.io/badge/Tests-97%20passing-brightgreen?style=flat-square)](https://nunit.org/)

---

## 📸 Screenshots

| Community Hub | Collection Details |
|---|---|
| ![Hub](https://raw.githubusercontent.com/krasimir-paunov/TheBigThree/main/docs/screenshots/hub.png) | ![Details](https://raw.githubusercontent.com/krasimir-paunov/TheBigThree/main/docs/screenshots/details.png) |

| Player Profile | Game Info Modal |
|---|---|
| ![Profile](https://raw.githubusercontent.com/krasimir-paunov/TheBigThree/main/docs/screenshots/profile.png) | ![Modal](https://raw.githubusercontent.com/krasimir-paunov/TheBigThree/main/docs/screenshots/modal.png) |

| Stats Page | Admin Panel |
|---|---|
| ![Stats](https://raw.githubusercontent.com/krasimir-paunov/TheBigThree/main/docs/screenshots/stats.png) | ![Admin](https://raw.githubusercontent.com/krasimir-paunov/TheBigThree/main/docs/screenshots/admin.png) |

---

## ✨ Features

### 🏠 Community Hub
- Browse all curated collections in a beautiful card-based layout
- Search by collection title or curator name
- Filter by game genre
- Sort by newest or most starred
- Pagination for easy browsing

### 📚 Collections
- Every registered user can create exactly one "Big Three" collection — their three defining games
- Each game slot includes a title, cover image, genre, and personal description
- Full edit and delete functionality for your own collection
- Star/unstar collections from other curators

### 🎮 RAWG API Integration
- **Autocomplete** — start typing a game title in the Add/Edit form and get live suggestions from the RAWG database, complete with cover thumbnails, release year, and rating
- **Game Details Modal** — click any game cover on a collection page to open a cinematic modal showing Metacritic score, release date, developer, platforms, genres, and a full screenshot gallery
- **Screenshot Lightbox** — click any screenshot to view it full size with prev/next navigation
- Graceful degradation — the app works fully without an API key; RAWG features are silently hidden

### 👤 User Profiles
- Public profile pages for every user — viewable by anyone
- Private dashboard showing your own collection, starred collections, rank, and stats
- Custom avatar via URL
- Collector Rank system based on stars earned

### 🏆 Rank System
| Stars Earned | Rank |
|---|---|
| 100+ | 🥇 Legendary Collector |
| 30+ | 🌟 Superstar Collector |
| 10+ | 🔥 Popular Collector |
| 5+ | ⬆️ Rising Star |
| 1+ | 🌱 Novice Collector |
| 0 | 👋 Newcomer |

### 📊 Stats & Leaderboard
- Community-wide statistics: total collections, members, stars, comments
- Top 3 collections podium
- Most popular genres bar chart
- Top 10 collectors and commenters leaderboard

### 💬 Comments
- Leave comments on any collection
- Delete your own comments
- Admins can moderate all comments

### 🛡️ Admin Panel
- Full user management: view, search, delete users
- Promote users to Administrator or demote them back to User
- Collection management: view and delete any collection
- Admin dashboard with community overview

---

## 🏗️ Architecture

The project follows a clean **N-Tier architecture** split across 7 projects:

```
TheBigThree/                        ← ASP.NET Core Web (MVC)
├── TheBigThree.Data/               ← DbContext, Migrations, Repository
├── TheBigThree.Data.Models/        ← Entity classes
├── TheBigThree.GCommon/            ← Shared helpers (RankHelper)
├── TheBigThree.Services/           ← Service implementations
├── TheBigThree.Services.Core/      ← Service interfaces, IRepository<T>
├── TheBigThree.Web.ViewModels/     ← ViewModels for all views
└── TheBigThree.Tests/              ← NUnit unit tests
```

### Layers

| Layer | Project | Responsibility |
|---|---|---|
| Presentation | TheBigThree | Controllers, Views, Areas, wwwroot |
| Application | TheBigThree.Services | Business logic, service implementations |
| Contracts | TheBigThree.Services.Core | Interfaces, Repository abstraction |
| Data | TheBigThree.Data | EF Core DbContext, migrations, generic repository |
| Domain | TheBigThree.Data.Models | Entity classes (Collection, Game, Genre, Like, Comment, ApplicationUser) |
| Shared | TheBigThree.GCommon | Cross-cutting helpers (RankHelper) |
| ViewModels | TheBigThree.Web.ViewModels | Data transfer objects for views |

### Generic Repository Pattern

All data access goes through `IRepository<T>`:

```csharp
public interface IRepository<T>
{
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
    Task SaveChangesAsync();
    IQueryable<T> All();
}
```

---

## 🗄️ Entity Models

```
ApplicationUser  ──────────────────────────────────────────
    │ Id, UserName, Email, AvatarUrl, CreatedOn            │
    │                                                       │
    ├──< Collection >──────────────────────────────────────┤
    │       Id, Title, TotalStars, CreatedOn, UserId        │
    │             │                                         │
    │             ├──< Game >────────────────────────────── │
    │             │       Id, Title, ImageUrl,              │
    │             │       Description, GenreId              │
    │             │             │                           │
    │             │             └──< Genre >                │
    │             │                   Id, Name              │
    │             │                                         │
    │             └──< Comment >──────────────────────────  │
    │                     Id, Content, CreatedOn,           │
    │                     UserId, CollectionId              │
    │                                                       │
    └──< Like >─────────────────────────────────────────── ─┘
            UserId (PK), CollectionId (PK)
            [Many-to-Many junction table]
```

---

## 🔧 Services

| Service | Responsibilities |
|---|---|
| `CollectionService` | CRUD for collections, search, filter, pagination, star system |
| `CommentService` | Add and delete comments with authorization checks |
| `LikeService` | Star/unstar collections, fetch starred collections |
| `ProfileService` | User profile data, own collection preview, public profiles |
| `StatsService` | Community statistics, top collections, leaderboard data |
| `AdminService` | User and collection management, promote/demote roles |
| `RawgService` | RAWG API integration — game search and detail fetching |

---

## ✅ Validation

### Server-Side
- All form inputs validated with Data Annotations
- `[Required]`, `[StringLength]`, `[RegularExpression]` on ViewModels
- Custom business logic validation in services (e.g., one collection per user)
- Authorization checks in service layer (not just controller)
- AntiForgeryToken on all POST forms

### Client-Side
- jQuery Unobtrusive Validation on all forms
- Inline error messages per field

### Database Level
- Required fields enforced at schema level
- Foreign key constraints
- Composite primary key on `Likes` (UserId + CollectionId)

---

## 🌱 Seeded Data

The application seeds the following on first run:

- **2 roles**: `User`, `Administrator`
- **7 users** including 1 admin (`CommanderShepard`)
- **19 genres** (Action, RPG, Horror, Strategy, etc.)
- **7 collections** with 3 games each, complete with cover images

Admin credentials for testing:
```
Username: CommanderShepard
Email:    shepard@normandy.com
Password: Spectre123!
```

---

## 🔐 Security

| Threat | Mitigation |
|---|---|
| SQL Injection | EF Core parameterized queries — no raw SQL |
| XSS | Razor auto-encodes all output; HTML tags escaped |
| CSRF | `[AutoValidateAntiforgeryToken]` globally applied |
| Unauthorized access | `[Authorize]` on all controllers; service-layer ownership checks |
| Account lockout | 3 failed attempts triggers 5-minute lockout |
| Secure cookies | HttpOnly, SecurePolicy.Always, SameSite.Strict |
| Parameter tampering | Ownership verified server-side before any mutation |

---

## 🎮 RAWG API Setup

The Big Three integrates with the [RAWG Video Games Database API](https://rawg.io/apidocs) to power:
- **Game autocomplete** when adding/editing a collection
- **Game details modal** with Metacritic scores, platforms, developers, and screenshots

### Getting Your API Key

1. Create a free account at [rawg.io](https://rawg.io)
2. Go to [rawg.io/apidocs](https://rawg.io/apidocs) and register your app
3. Copy your API key

### Configuring the Key (User Secrets — Recommended)

The API key is stored using **ASP.NET User Secrets** and is **never committed to the repository**.

From the project root (where `TheBigThree.csproj` lives):

```bash
dotnet user-secrets set "RawgApi:ApiKey" "YOUR_API_KEY_HERE" --project TheBigThree.csproj
```

> **Note:** Without an API key, the application works fully — RAWG features (autocomplete and game details modal) are silently disabled. No errors will occur.

---

## 🚀 Setup & Running Locally

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or later
- SQL Server LocalDB (included with Visual Studio)

### Steps

1. **Clone the repository**
```bash
git clone https://github.com/krasimir-paunov/TheBigThree.git
cd TheBigThree
```

2. **Open in Visual Studio**
   - Open `TheBigThree.sln`

3. **Configure the database connection string**

   Open `appsettings.json` and update the connection string to match your local SQL Server instance:

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=TheBigThreeDb;Trusted_Connection=True;MultipleActiveResultSets=true"
     }
   }
   ```

   > **Note:** If you are using a named SQL Server instance instead of LocalDB, replace `(localdb)\\mssqllocaldb` with your server name, for example `Server=.\\SQLEXPRESS` or `Server=YOUR_PC_NAME\\SQLEXPRESS`.

4. **Apply migrations and seed the database**
   - In Package Manager Console, set Default Project to `TheBigThree.Data`
   - Run:
```powershell
Update-Database
```

5. **(Optional) Configure RAWG API**
```bash
dotnet user-secrets set "RawgApi:ApiKey" "YOUR_KEY_HERE" --project TheBigThree.csproj
```

6. **Run the application**
   - Press `F5` or `Ctrl+F5` in Visual Studio
   - Navigate to `https://localhost:{port}`

7. **Login with the seeded admin account**
```
Username: CommanderShepard
Password: Spectre123!
```

---

## 🧪 Unit Tests

The project includes **97 unit tests** across 10 test classes, using **NUnit**, **Moq**, and **MockQueryable.Moq**, with **73.8% service layer coverage**.

```
TheBigThree.Tests/
├── Services/
│   ├── CollectionServiceTests.cs    (19 tests)
│   ├── CommentServiceTests.cs       (10 tests)
│   ├── LikeServiceTests.cs          (11 tests)
│   ├── ProfileServiceTests.cs       (10 tests)
│   ├── AdminServiceTests.cs         (11 tests)
│   └── StatsServiceTests.cs         (7 tests)
└── Controllers/
    ├── HomeControllerTests.cs       (5 tests)
    ├── StatsControllerTests.cs      (3 tests)
    ├── LeaderboardControllerTests.cs (3 tests)
    └── CollectionControllerTests.cs (18 tests)
```

### Running Tests

In Visual Studio: `Test → Run All Tests`

Or via CLI:
```bash
dotnet test
```

### Coverage Areas

**Services (73.8% line coverage)**
- Collection CRUD and business rules (one per user)
- Comment add/delete with authorization and admin bypass
- Like/unlike with duplicate prevention and star decrement
- Profile and public profile data loading
- Admin promote/demote/delete operations
- Community stats and leaderboard ordering

**Controllers**
- Action return types (ViewResult, RedirectToActionResult)
- Correct view model passing
- Redirect targets on not-found and error scenarios
- Service delegation verification
- Authenticated user context mocking
- Exception handling branches (Star/RemoveStar)

---

## 🌐 Deployment

The application is designed to be deployable to **Microsoft Azure** (App Service + Azure SQL). Deployment instructions will be added if a public hosting URL becomes available.

---

## 📁 Project Structure

```
TheBigThree/
├── Areas/
│   ├── Admin/                  ← Admin area (Dashboard, Users, Collections)
│   └── Identity/               ← Scaffolded Identity pages (Login, Register, Manage)
├── Controllers/                ← Main app controllers
├── Views/                      ← Razor views per controller
├── wwwroot/
│   ├── css/                    ← Per-page CSS files + site.css
│   ├── js/                     ← site.js, hub.js, rawg.js, public-profile.js
│   └── images/
│       ├── seed/               ← Seeded game cover images
│       └── backgrounds/        ← Page background textures
├── TheBigThree.Data/
│   ├── TheBigThreeDbContext.cs
│   ├── Migrations/
│   └── Repository.cs
├── TheBigThree.Data.Models/
│   ├── ApplicationUser.cs
│   ├── Collection.cs
│   ├── Game.cs
│   ├── Genre.cs
│   ├── Like.cs
│   └── Comment.cs
├── TheBigThree.Services/       ← Service implementations
├── TheBigThree.Services.Core/  ← Interfaces + IRepository<T>
├── TheBigThree.Web.ViewModels/ ← ViewModels
├── TheBigThree.GCommon/        ← RankHelper
└── TheBigThree.Tests/          ← NUnit tests
```

---

## 🛠️ Tech Stack

| Technology | Version | Purpose |
|---|---|---|
| ASP.NET Core MVC | 8.0 | Web framework |
| Entity Framework Core | 8.0 | ORM / data access |
| ASP.NET Identity | 8.0 | Authentication & authorization |
| SQL Server LocalDB | Latest | Database |
| Bootstrap | 5.3 | UI framework |
| Bootstrap Icons | 1.11 | Icon library |
| NUnit | 3.x | Unit testing |
| Moq | 4.x | Mocking |
| MockQueryable.Moq | 7.0 | IQueryable mocking |
| RAWG API | v1 | Game database |

---
