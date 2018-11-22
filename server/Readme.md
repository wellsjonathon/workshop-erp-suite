# ERP Web API

### Running the server

1. In a command prompt, navigate to ERP.API
2. dotnet run
3. Ctrl-c to stop it

Note: Run with a command prompt, not a Git Bash prompt, otherwise the process won't be killed on exit

Note: Stop the server to create new controllers and migrations

### Adding additional models

1. Create model in ERP.Models
2. Create context in ERP.Repositories > Contexts
3. Register context in ERP.API > Startup.cs
4. Create controller in ERP.API > Controllers
5. Create the migration - Add-Migration InitialCreate -Context YourContext
6. Update the database - Update-Database -Context YourContext
