# Project Instructions

To run the project locally, follow these steps:

1. Clone this repository to your local machine
2. Navigate to the project directory (src)
3. Install the necessary dependencies using ....
4. Run command "dotnet run" in terminal
5. Access the website in your web browser and start exploring it!

## Setup Instructions

### Prerequisites
- Node.js and npm installed on your machine
- .NET SDK installed on your machine
- Visual Studio Code or any preferred text editor

### Frontend Setup (React.js)
1. Navigate to the `ClientApp` directory.
2. Install dependencies
   1. npm i axios
   2. npm i font-awesome

### Backend Setup (.NET and ASP.NET Core)
1. Navigate to the `src` directory.
2. Install the required dependecies.
   1. dotnet add package Microsoft.EntityFrameworkCore.SqlServer
   2. dotnet add package Microsoft.EntityFrameworkCore.Design
3. The backend should now be running and accessible at the specified port.

### Database Setup (Entity Framework Core)
1. Run Entity Framework Core migrations to create/update the database schema.
   1. How to create migrations
      1. dotnet ef migrations add InitialCreate
      2. dotnet ef database update

### Run whole application
1. Navigate to the src-folder
2. run "dotnet run"


## Troubleshooting
- If you encounter any issues during setup or while running the application, refer to the documentation of the respective technologies (React, .NET, ASP.NET Core, Entity Framework Core, Axios) or search for solutions online.

## Tests

To run the tests, follow these steps:

1. Navigate to the "tests"-folder
2. Install dependencies
   1. dotnet add package xunit --version 2.8.0
3. Run "dotnet test"