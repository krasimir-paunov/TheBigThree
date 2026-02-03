# 🎮 The Big Three

A community‑driven web application where gamers curate **their personal Top 3 games of all time**, explain *why* they love them, why others should play them, and earn status based on community recognition.

---

## ✨ Core Idea

Every registered user can create **one personal collection** featuring their **Top 3 games of all time**.
Each game entry includes a personal description explaining:

* Why the user fell in love with the game
* Why others should experience it

Other users can explore collections, star the ones they like, and help creators climb the community ranking ladder.

---

## 👥 User Experiences

### 👀 Guest (Unregistered User)

Guests have access to the **Hub** area where they can:

* Browse all public collections
* View collection details and game descriptions
* See collection ratings (stars)
* Sort collections by:

  * Newest added
  * Most starred

> Guests **cannot** create, edit, delete, or star collections.

---

### 🧑‍💻 Registered User

Registered users have all guest capabilities **plus**:

* ⭐ Star other users’ collections

  * Only **1 star per collection**
  * Cannot star their **own** collection
* 📦 Create **their own Top 3 collection**
* ✏️ Edit or delete their own collection
* 👤 Access a **Personal Page** showing:

  * Collections they have starred
  * Status + additional info
* 🏆 Earn **status ranks** based on community stars

---

## 🏅 User Status & Ranking System

User status is determined dynamically based on the **total number of stars** their collection receives.

| Total Stars | Status              |
| ----------- | ------------------- |
| **100+**    | Legendary Collector |
| **30+**     | Superstar Collector |
| **10+**     | Popular Collector   |
| **5+**      | Rising Star         |
| **1+**      | Novice Collector    |
| **0**       | Newcomer            |

> Status updates automatically as stars increase, reflecting the creator’s reputation within the community.

---

## 🛠 Tech Stack

The application uses:

* ASP.NET Core MVC
* Entity Framework Core
* ASP.NET Identity (authentication & authorization)
* MS SQL Server
* Razor Views
* Bootstrap
* CSS

---

## 🌱 Seeded Example Data

To ensure the application is **not empty on first run**, the database includes:

* **2 pre-seeded example collections**:
  
* Each collection demonstrates:

  * Proper game entries
  * Descriptive recommendation text
  * Star-based ranking behavior

This allows examiners and users to immediately explore the Hub without needing to create data manually.

---

## 🚀 Getting Started 

Follow these steps to run the project locally:

### 1️⃣ Clone the Repository

```bash
git clone <repository-url>
```

Or download the repository as a ZIP and extract it.

---

### 2️⃣ Open the Project

* Open the solution file (`.sln`) in **Visual Studio**
* Ensure the startup project is set correctly

---

### 3️⃣ Configure Database Connection

> ⚠️ **Important:** This project stores its connection string in `appsettings.Development.json`, which **is included in the repository**.

1. Open `appsettings.Development.json`
2. Locate the `ConnectionStrings` section
3. Verify or adjust the connection string to match your local SQL setup

Example (LocalDB – recommended):

````json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\mssqllocaldb;Database=TheBigThree;Trusted_Connection=True;MultipleActiveResultSets=true"
}

````

---

### 4️⃣ Apply Migrations & Seed Data

1. Open **Package Manager Console** (`Tools` → `NuGet Package Manager` → `Package Manager Console`)
2. Ensure the **Default project** is set to **TheBigThree**
3. Run:

```powershell
Update-Database
```

This will:

* Create the database
* Apply all migrations
* Seed the example collections

---

### 5️⃣ Run the Application

* Press **F5** or click **Run** in Visual Studio
* The application will start in your browser

---

## 🔐 Authentication Notes

* Authentication is handled via **ASP.NET Identity**
* Guests can browse freely
* Registered users unlock full interaction features

---

## 📌 Project Highlights

* Clear **role‑based access control**
* Full **CRUD operations** for collections
* Custom business rules:

  * One collection per user
  * One star per user per collection
  * Cannot star own collection
* Dynamic ranking & status system
* Clean MVC separation
* User‑friendly UI and navigation

---

## 📄 License

This project is created for **educational purposes**.

---

🎮 *Built by a gamer, for gamers.*
