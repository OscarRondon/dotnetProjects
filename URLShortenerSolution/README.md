# URL Shortener

## Solution

* Project Template [3 projects]: 
	* ASP.Net Core Web app (MVC)
	* ASP.Net Core Web API
	* .Net Core Class Library

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
	- Create a file "appsettings.json" into wwwroot folder
	- Create a Folder into de Project "Settings"
	- Create class for settings "ClientAppSettings"
	- Add the following into Program.cs
		var settings = new ClientAppSettings();
		builder.Configuration.Bind(settings);
		builder.Services.AddSingleton(settings);
```
### Nuget Packages
```
```

## Testing [14]

### Nuget Packages
```
```

URLShortenerSolution
[
	*	Login Form Data Validation with Data Annotations
]


## Todos

- 