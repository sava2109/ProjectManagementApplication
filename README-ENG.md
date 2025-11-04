# 📊 Project Management Application

A modern web application for managing projects, teams, and resources, developed using **.NET Core** with the **Razor Pages** architecture.

**Tech Stack:**  
`.NET Core · SQLite · Entity Framework · Bootstrap`

---

## 🎯 About the Project

The application enables complete project management through a hierarchical structure:

**Projects → Work Packages → Tasks → Activities**

Additionally, it allows for managing employees, teams, and assigning activities to team members.

---

## ✨ Features

### 📁 Project Management
- ✅ CRUD operations for projects  
- ✅ Tracking work packages per project  
- ✅ Managing tasks and activities  
- ✅ Planning and tracking hours/days  

### 👥 Resource Management
- ✅ Employee records  
- ✅ Creating and managing teams  
- ✅ Adding members to teams  
- ✅ Assigning tasks and activities to employees  

### 🔍 Additional Features
- ✅ Search and sorting across all entities  
- ✅ Comparison of planned vs. actual hours  
- ✅ Responsive and modern UI design  
- ✅ Data validation  

---

## 🛠️ Technologies

| Layer | Technology |
|--------|-------------|
| **Backend** | .NET Core 8.0 (Razor Pages) |
| **ORM** | Entity Framework Core 8.0 |
| **Database** | SQLite |
| **Frontend** | Bootstrap 5.3, Bootstrap Icons |
| **Migrations** | EF Core Migrations |

---

## 📦 Installation & Setup

### Prerequisites
- .NET SDK **8.0** or later

### Steps to Run

**1. Clone the repository**
```bash
git clone https://github.com/your-username/ProjectManagementApplication.git
cd ProjectManagementApplication/ProjectManagementApplication/ProjectManagementApplication


2. Restore NuGet packages
dotnet restore

3. Build the project
dotnet build

4. Run the application
cd ProjectManagementApplication
dotnet run

5. Open in browser


HTTPS: https://localhost:7281


HTTP: http://localhost:5290 
```


## 📊 Database Structure
The application uses 8 relational tables:
TableDescriptionProjectsBasic project dataWorkPackagesWork packages within projectsTasksTasks within work packagesActivitiesActivities within tasksEmployeesEmployee dataTeamsEmployee teamsTeamMembersMany-to-many link between teams and employeesTaskAssignmentsAssigning activities to employees
```ER Diagram:
Project (1) ─── (N) WorkPackage (1) ─── (N) Task (1) ─── (N) Activity
    │                                                          │
    │                                                          │
    └─── (N) Team (1) ─── (N) TeamMember (N) ─── (1) Employee ─── (N) TaskAssignment
```

## 🗂️ Project Structure
```
ProjectManagementApplication/
├── ProjectManagementApplication/      # Main web app
│   ├── Pages/                         # Razor Pages
│   │   ├── Projects/                  # CRUD for projects
│   │   ├── WorkPackages/              # CRUD for work packages
│   │   ├── Tasks/                     # CRUD for tasks
│   │   ├── Activities/                # CRUD for activities
│   │   ├── EmployeesPages/            # CRUD for employees
│   │   ├── Teams/                     # CRUD for teams
│   │   ├── TeamMembers/               # Manage team members
│   │   ├── Assignments/               # Assign tasks
│   │   └── Shared/                    # Layout and shared components
│   ├── wwwroot/                       # Static files
│   └── Program.cs                     # Application entry point
│
├── DataBaseContext/                   # EF Core DbContext
│   ├── ApplicationDbContext.cs        # Database configuration
│   ├── DesignTimeDbContextFactory.cs  # Factory for migrations
│   └── Migrations/                    # EF migrations
│
└── DatabaseEntityLib/                 # Model library
    └── Models/                        # Database entities
```

## 🎨 UI/UX Features


🎯 Modern minimalist design


📱 Fully responsive layout


✨ Smooth hover animations


🎨 Modern color palette


🔍 Intuitive navigation


💡 Clearly grouped functionalities



## 📝 Usage Examples
🆕 Creating a New Project


Go to “Projects” from the homepage


Click “Create New”


Enter name, description, dates, and planned hours


Save the project


## 👨‍💼 Assigning an Activity to an Employee


Navigate to “Task Assignments”


Click “Create New”


Select an employee and an activity


Enter assignment date and planned hours


Track actual hours and completion status



## 🔧 Configuration
```
Connection String (appsettings.json):
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=projectmanagement.db"
  }
}
```
Creating a new migration:
dotnet ef migrations add MigrationName --project DataBaseContext

Applying migrations:
dotnet ef database update --project DataBaseContext


## 🚀 Deployment
The application automatically creates a SQLite database on first launch.
For production deployment:```
dotnet publish -c Release -o ./publish```


## 📄 License
This project was created for educational purposes.

## 👨‍💻 Author
Your Name / GitHub Username

## 🤝 Contributions
Suggestions and contributions are welcome!
Feel free to open an issue or a pull request.

## 📞 Contact
GitHub: @sava2109
Email: savadimitrijevic2109@gmail.com
⭐ If you like this project, give it a star on GitHub!

---

