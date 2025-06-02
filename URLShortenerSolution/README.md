# URL Shortener

## Solution

* Project Template [3 projects]: 
	* ASP.Net Core Web app (MVC) > Shortly.Client
	* ASP.Net Core Web API > Shortly.Redirect
	* .Net Core Class Library > Shortly.Data

* DataBases
	* MS SQL Server (loacal)


## Server side App (ASP.Net Core Web API)

### Configuration
```
	- Create a file "env.json" into root forlder, using "env.template.json"
	- Set the "env.json" properties as:
		- Build action: Content 
		- Copy to Output directory: Copy always
	- Register the new file to de app configuration in Program.cs
	- Make sure to exclude "env.json" file form the source control
```

### Nuget Packages
```

```

### Instructions
```
	- On Program.cs > builder.Services.AddDbContext (server)
	- Install or update Entity framework command line tools
		- run in Nuget Manager Console: dotnet ef
		- run in Nuget Manager Console: dotnet tool uninstall --global dotnet-ef
		- run in Nuget Manager Console: dotnet tool install --global dotnet-ef
	- Run in Nuget Manager Console (create migration): 
		- Place on server directory
		- run > dotnet ef migrations add "Initial Create"
		- run > dotnet ef database update
```


## Client side App (ASP.Net Core Web app (MVC))

### Configuration
```
```

### Nuget Packages
```
```

### Instuctions
```
	- Add the Shortly.Data project as a reference
	- On Program.cs > builder.Services.AddDbContext (server)
	- Create a file "env.json" into root forlder, using "env.template.json"
	- Set the "env.json" properties as:
		- Build action: Content 
		- Copy to Output directory: Copy always
	- Register the new file to de app configuration in Program.cs
	- Make sure to exclude "env.json" file form the source control
```

## Data side App (.Net Core Class Library)

### Configuration
```
	
```

### Nuget Packages
```
	- Microsoft.EntityFrameworkCore (base packege) with all EF core functionality)
	- Microsoft.EntityFrameworkCore.SqlServer (SQL Server provider)
	- Microsoft.EntityFrameworkCore.Tools (command line tools for migrations)
```

### Instructions
```
	- Install or update Entity framework command line tools
		- run in Nuget Manager Console: dotnet ef
		- run in Nuget Manager Console: dotnet tool uninstall --global dotnet-ef
		- run in Nuget Manager Console: dotnet tool install --global dotnet-ef
	- Run in Nuget Manager Console (create migration): 
		- run > dotnet ef migrations add "Initial Create"
		- run > dotnet ef database update
```

## Testing [14]

### Nuget Packages
```
```

URLShortenerSolution
[
	*	Add UrlsService
	*	Add UsersService
	*	Add Interface for UrlsService > IUrlsService
	*	Add Interface for UsersService > IUsersService
]


## Todos

- 