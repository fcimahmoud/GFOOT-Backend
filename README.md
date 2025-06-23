# 🌱 GFoot Backend – Carbon Footprint Management API

**GFoot** is a .NET 8-powered backend system built to estimate carbon footprints for individuals and organizations, and provide actionable AI-driven recommendations to reduce emissions. The project aims to promote environmental awareness and sustainability through accurate tracking and data visualization.

## 🚀 Features

### 🔐 Authentication & Authorization
- Secure JWT-based authentication system
- Social login (Google)
- OTP-based email verification and password reset
- Token refresh mechanism

### 👤 Individual Users
- Log personal activities like travel and home energy usage
- Calculate personal carbon footprint
- View rank among other users
- Receive personalized eco-friendly tips

### 🏢 Organization Dashboard
- Log emissions from various sources (transportation, electricity, waste, etc.)
- Calculate organization-level carbon footprint
- Get smart recommendations powered by AI to reduce emissions
- View emissions visualization charts

### 📊 Environmental Agents Dashboard
- Monitor organizations exceeding emission thresholds
- Generate and send reports (all or latest)
- View organization-specific emissions with filters
- Get a full report by calculation type

### 📊 Admin Dashboard
- CRUD operations for Organizations and Environmental Agents
- Manage Guests Requests and create accounts for them

### 📥 Guest Requests
- Allow unregistered users to request access or raise queries

### 📚 API Documentation
- Fully documented using **Swagger** and **Postman** for easy testing and understanding

---

## 🧱 Tech Stack

- **Framework:** .NET 8, ASP.NET Core Web API
- **Architecture:** Onion Architecture with Clean Code principles
- **ORM:** Entity Framework Core
- **Database:** SQL Server
- **Design Patterns:** Repository, Unit of Work, Service Layer
- **Mapping:** AutoMapper
- **Error Handling:** Global Exception Middleware
- **Documentation:** Swagger, Postman

---

## 🧩 Project Structure
``` bash
├── Core/                # Core business logic and abstractions
    ├── Domain
        ├── Contracts
        └── Entities
    ├── Services
    └── Services.Abstractions
├── Infrastructure/      # Data access and external integrations
    ├── Persistence
        ├── Data
        ├── Migrations
        └── Repositories
    └── Presentation
├── GFoot.API/           # API project
    ├── Extensions
    ├── Middlewares
    └── Program.cs
└── Shared               # DTOs - Data Transfer Objects
```

---

## ⚙️ Installation & Setup

1. **Clone the repository**

```bash
git clone https://github.com/fcimahmoud/GFOOT-Backend.git
cd GFOOT-Backend
```

2. **Set up the database**

- Ensure SQL Server is running.
- Run EF Core migrations:

``` bash
dotnet ef database update
```

3. **Run the project**

``` bash
dotnet run
```

4. **Access Swagger**

Visit: https://localhost:6001/swagger

---

## 🔗 Third-Party Integration
- Using HttpClient
- Integration with Fast APIs that using ML Models for Calculations and Recommendadtions for Individuals and Organizations.

---

## 🧑‍🎓 Developed As
- 🎓 Graduation Project – Faculty of Computers and Information, 2025
- By: Mahmoud and Marwan
- GitHub: github.com/fcimahmoud , github.com/Marwan-Farhat

---

## 🔍 Contact
For questions or feedback:
LinkedIn: https://www.linkedin.com/in/mahmoud-ahmed-3291b7229
Email: ma5740@fayoum.edu.eg

---

## 📄 License
This project is licensed under the MIT License. 

---
