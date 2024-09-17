# Online Quiz Application

![Build Status](https://docs.github.com/en/actions/monitoring-and-troubleshooting-workflows/adding-a-workflow-status-badge)

## Demo & Snippets

-   Hosted link: _[Insert your hosted link here]_
-   Screenshot of the app: _[Include images of your app here]_

---

## Requirements / Purpose

- **MVP**: Develop a basic online quiz application that allows users to create, edit, take quizzes, and view scores upon completion.
- **Purpose**: Provide a platform where users can manage and participate in quizzes.
- **Stack**:
    - Razor Pages for front-end.
    - Entity Framework Core for database operations.
    - SQLite as the database for flexibility and simplicity.

---

## Build Steps

1. Clone the repository:  
   ```bash
   git clone https://github.com/uwerrrr/quizzApp.git
   ```
Navigate to the project directory:
bash
Copy code
cd online-quiz-app
Install dependencies:
bash
Copy code
dotnet restore
Run database migrations:
bash
Copy code
dotnet ef database update
Run the application:
bash
Copy code
dotnet run
