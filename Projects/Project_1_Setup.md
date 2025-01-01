## Creating the project directory
```bash
mkdir <project_1_name> 
```
## Create new webapi project and add dependencies for EF
```bash
dotnet new webapi -n <projectName>.API
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.Tools
```
## Install EF tools (only need to do it once for your system)
```bash
dotnet tool install --global dotnet-ef
```
## Initialize user secrets vault
```bash
dotnet user-secrets init
dotnet user-secrets list
dotnet user-secrets set "<Name of secret>" "<value of secret>"
dotnet user-secrets remove <name of secret>
```
## Reference connection string (localhost)
```bash
Server=localhost;User Id=sa;Password=NotPassword@123;Database=master;Initial Catalog=<name of db>;TrustServerCertificate=true;
```
## Add connection string to vault
```bash
dotnet user-secrets set "ConnectionStrings:<name of db>" "<ConnectionString>"
```
## In Program.cs Add DBcontext for database
```bash
builder.Services.AddDbContext<DBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("name of db")));
```
## Add a new migration
```bash
dotnet ef migrations add <NameofMigration>
```
## Remove last migration made
```bash
dotnet ef migrations remove
```
## Apply migrations to DB up to latest
```bash
dotnet ef database update
```
## Apply up specific migration to DB
```bash
dotnet ef database update <name of migration>
```
## Remove all migrations from DB
```bash
dotnet ef database update 0
```
## To use mock for testing, in the test project run
```bash
dotnet add package Moq
```

## TEST:  dotnet new xunit -n name.TEST

## To generate test coverage
```bash
dotnet tool install --global dotnet-reportgenerator-globaltool
dotnet test --collect: "XPlat Code Coverage"
```

## To generate html human friendly report
## Find the code coverage folder GUID
```bash
reportgenerator -reports:".\TestResults\<guid>\coverage.cobertura.xml" -targetdir:"coveragereport" -reporttypes:Html 
```

## To filter out stuff you don't want
```bash
reportgenerator -reports:".\TestResults\<guid>\coverage.cobertura.xml" -targetdir:"coveragereport" -reporttypes:Html classfilters:"+Sample.API.Service.*;+some more stuff;-don't include this"
```