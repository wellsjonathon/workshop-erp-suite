# ERP Web API

### Adding additional models

1. Create model in ERP.Models
2. Create context in ERP.Repositories > Contexts
3. Register context in ERP.API > Startup.cs
4. Create controller in ERP.API > Controllers
5. Create the migration - Add-Migration InitialCreate -Context YourContext
6. Update the database - Update-Database -Context YourContext
