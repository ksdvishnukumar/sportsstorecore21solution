# Sports Store ASP Net Core 2.1 MVC Application

- Nuget Packages for the Project
  - For Azure Storage
    1. Install-Package WindowsAzure.Storage (9.3.3)
  - For KeyVault
    1. Install-Package Microsoft.Azure.Services.AppAuthentication -Version 1.2.0 (or latest)
    2. Install-Package Microsoft.Azure.KeyVault -Version 3.0.3 (or latest)
    3. Install-Package Microsoft.Extensions.Configuration.AzureKeyVault -Version 2.1.1 (For ASP.Net Core 2.1)
  - Application Insights
    - Install-Package Microsoft.ApplicationInsights.AspNetCore -Version 2.7.1
  - For Redis Cache
    -  Install-Package Microsoft.Extensions.Caching.Redis -Version 2.1.1 (For ASP.Net Core 2.1)

- Bower Packages for the Project
  1. "jquery": "3.3.1"
  2. "bootstrap": "4.3.1"
  3. "font-awesome": "4.7.0"
  4. "jquery-validation": "1.17.0"
  5. "jquery-validation-unobtrusive": "3.2.10"

- EntityFramework Core Commands
  1. dotnet ef database update (this will create the database)
  2. dotnet ef migrations add InitialDb -o DataMigrations\Migrations (this will create the class with the table schema)
  3. dotnet ef database update (this will create the database if exist then update it with the table from the migrations folder) 
  4. dotnet ef migrations remove (will remove the migrations)
  5. dotnet ef database drop (this will drop the database)

#### Git Branches
- 01Start
- 02MVC
- 03EFCore
- 04AzureLocal
- 05AzureCloud