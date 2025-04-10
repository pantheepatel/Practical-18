# Practical 18: ASP.NET CRUD Implementation

This project demonstrates the implementation of CRUD operations.

## Configuration

Update the connection string in `appsettings.json` according to your SQL Server setup:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.\\SQLEXPRESS;Database=Practical18;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

## Applying Migrations

Since migration files are already included in the project, you only need to apply them to create the database structure:

1. Open the Package Manager Console (Tools > NuGet Package Manager > Package Manager Console)
2. Run the following command:
   ```
   Update-Database
   ```

