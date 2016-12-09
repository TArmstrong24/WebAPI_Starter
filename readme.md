# .Net Core WebAPI implementation with repository pattern starter

This project is intended to be a starter template for implementing a new WebAPI using the repository pattern. 

Here are some highlights of this project:

* Reusable generic repository - no need to have individual repository classes for each area
* Individual services to use for business logic/rules
* Using the latest (1.1.0) version of the .Net core framework
* Utlitizes Entity Framework for easy ORM



# 

# Getting started

Clone this project to your working folder:

```
git clone --depth=1 https://github.com/tarmstrong24/WebAPI_Starter/
cd WebAPI_Starter\src\WebAPIStarterRepo
```

Restore requirements:

```
dotnet Restore
```

Add required tables/sp to a db:

```
CREATE TABLE [dbo].[Samples](
	[SampleId] [int] IDENTITY(1,1) NOT NULL,
	[SampleData] [nvarchar](50) NULL
) ON [PRIMARY]
```

Update connection string in app.config


Configure NLog:
To utilize, update nlog.config with connection string and uncomment lines in startup.cs


