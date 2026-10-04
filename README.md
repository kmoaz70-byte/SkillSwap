
# 🔄 SkillSwap

### Bridging Skills, Connecting People

A peer-to-peer skill exchange web application where people trade skills with each other, with no money involved.

---

## 📖 About the Project

**SkillSwap** lets users exchange skills instead of paying for them. Someone who knows guitar can swap with someone who knows graphic design. Users list the skills they can offer and the skills they want to learn, browse other members, send swap requests, chat in real time, and rate each other after a swap.

This is my **Final Year Project** for the BS Computer Science degree (batch 2022–2026) at the **University of Swat, Department of Computer Science & Technology**.

---

## ✨ Features

### 👤 For Users
- **Authentication:** register, log in and log out with ASP.NET Core Identity
- **Profile management:** edit personal details and profile information
- **Skill management:** add, edit and remove the skills you offer and want to learn
- **Browse and search:** discover skills and members, with search and filtering
- **Swap requests:** send, receive, accept and reject skill swap requests
- **Ratings and reviews:** rate and review your swap partner after a swap
- **Real-time chat:** one-to-one messaging powered by SignalR
- **Notifications:** get notified about requests and activity

### 🛡️ For Admins
- Role-based access control (User / Admin)
- Admin panel to manage users and platform content

### 🎨 UI / UX
- Responsive design with Bootstrap 5 (Bootswatch Flatly base) and a custom navy + teal theme
- Client-side validation and AJAX interactions with jQuery
- Smooth animations and UI transitions

---


## 🧰 Tech Stack

| Layer | Technology |
|-------|-----------|
| **Framework** | ASP.NET Core MVC (.NET 8) |
| **Language** | C# |
| **ORM** | Entity Framework Core (Code First, Migrations) |
| **Database** | Microsoft SQL Server |
| **Authentication** | ASP.NET Core Identity (roles) |
| **Real-time** | SignalR |
| **Frontend** | Razor Views, Bootstrap 5, Bootswatch Flatly, jQuery, Bootstrap Icons |
| **Patterns** | Generic Repository, Unit of Work, Dependency Injection |
| **IDE / Tools** | Visual Studio, SQL Server Management Studio, Git |

---

## 🏗️ Architecture

The solution follows an **N-Tier architecture** with a clear separation of concerns:

```
┌──────────────────────────────────┐
│   SkillSwapWEB  (Presentation)   │  Controllers, Views, SignalR Hubs
└────────────────┬─────────────────┘
                 │
┌────────────────▼─────────────────┐
│   Businesslayer  (Services)      │  Business logic
└────────────────┬─────────────────┘
                 │
┌────────────────▼─────────────────┐
│   DataAccess  (Data)             │  DbContext, Repositories, UnitOfWork
└────────────────┬─────────────────┘
                 │
┌────────────────▼─────────────────┐
│   Models  (Shared Library)       │  Entities and ViewModels
└──────────────────────────────────┘
```

**Key design decisions**
- **Generic Repository + Unit of Work:** one reusable data access abstraction and a single transaction boundary
- **Dependency Injection:** services and repositories are registered in the DI container, so layers stay loosely coupled
- **Shared Models project:** entities and ViewModels live in one library referenced by all layers
- **Code First with EF Core Migrations:** the database schema is generated from the entity classes

---

## 📁 Project Structure

```
SkillSwap/
│
├── SkillSwap.sln
├── .gitignore
├── .gitattributes
├── README.md
│
├── SkillSwapWEB/            # Presentation layer (ASP.NET Core MVC)
│   ├── Controllers/         # MVC controllers
│   ├── Views/               # Razor views
│   ├── wwwroot/             # Static files (CSS, JS, images, theme.css)
│   ├── appsettings.json     # Configuration (connection string)
│   └── Program.cs           # App startup, DI and middleware
│
├── Businesslayer/           # Business logic / services
│
├── DataAccess/              # Data access layer
│   ├── DbContext            # EF Core DbContext
│   ├── Repository/          # Generic repository and implementations
│   ├── UnitOfWork           # Unit of Work
│   └── Migrations/          # EF Core migrations
│
├── Models/                  # Entities and ViewModels (shared library)
│
└── docs/
    └── screenshots/         # README images
```

> Folder names reflect the solution layout. Inner folders may differ slightly.

---

## 🚀 Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) with the **ASP.NET and web development** workload
- [SQL Server](https://www.microsoft.com/sql-server) (Express or LocalDB is fine) and SSMS (optional)
- [Git](https://git-scm.com/)

### Installation

**1. Clone the repository**
```bash
git clone https://github.com/kmoaz70-byte/SkillSwap.git
cd SkillSwap
```

**2. Open the solution**

Open `SkillSwap.sln` in Visual Studio.

**3. Configure the database connection**

In `SkillSwapWEB/appsettings.json`, update the connection string to match your SQL Server instance:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=SkillSwapDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

**4. Apply migrations**

In Visual Studio open **Tools → NuGet Package Manager → Package Manager Console**, set the **Default project** to `DataAccess`, and run:

```powershell
Update-Database
```

Or from the terminal, in the solution folder:

```bash
dotnet ef database update --project DataAccess --startup-project SkillSwapWEB
```

**5. Run the application**

Set `SkillSwapWEB` as the **Startup Project** and press `F5` (or `Ctrl + F5`).

### Default Admin Account

> The app seeds an admin account on first run. Add the credentials here.

| Role | Email | Password |
|------|-------|----------|
| Admin | _your-admin-email_ | _your-admin-password_ |

⚠️ Change the default credentials before deploying anywhere public.

---

## 🧪 Testing

The application was tested manually using a functional testing checklist covering authentication, profile and skill management, browsing, swap requests, ratings, chat, notifications and the admin panel.

---

## 🗺️ Roadmap

- [ ] Unit and integration tests
- [ ] Docker support
- [ ] Deploy to Azure App Service
- [ ] Email notifications
- [ ] Skill categories and advanced filters

---

## 🙏 Acknowledgements

- Architecture pattern (N-Tier with Generic Repository and Unit of Work) inspired by the **Bulky Book** course by Bhrugen Patel (DotNetMastery)
- [Bootswatch](https://bootswatch.com/) (Flatly theme) and [Bootstrap Icons](https://icons.getbootstrap.com/)
- University of Swat, Department of Computer Science & Technology

---

## 👨‍💻 Author

**Syed Muhammad Muaaz (Moaz)**
BS Computer Science, University of Swat (2022–2026)
Junior .NET Developer | ASP.NET Core · C# · EF Core · SQL Server

- 💼 LinkedIn: [linkedin.com/in/syedmuaaz](https://linkedin.com/in/syedmuaaz)
- 🐙 GitHub: [@kmoaz70-byte](https://github.com/kmoaz70-byte)
- 📧 Email: Kmoaz70@gmail.com

⭐ If you like this project, give it a star.
