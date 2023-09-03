#Authentication System [1 h]

	- Project Template: Blazor WebAssembly
		- Authentication type: Individual Accouny
		- ASP.Net Core Hosted
	- DataBase: MS Sql Server
	- Source: https://www.youtube.com/watch?v=r3tytnzCuNw
	- Source code: https://github.com/patrickgod/BlazorGoogleAuthTutorial


##Nuget Packages

	- Microsoft.AspNetCore.Identity.EntityFrameworkCore
	- Microsoft.AspNetCore.Identity.UI
	- Microsoft.EntityFrameworkCore.SqlServer
	- Microsoft.EntityFrameworkCore.Tools
	- Microsoft.VisualStudio.Web.CodeGeneration.Design
	- Microsoft.VisualStudio.Web.CodeGeneration.Utils	

##Instructions

	- Create a new project [ASP.Net Web App (MVC)]
	- Right click on project > Add > New Scaffolded Item > Identity (choose the options of the presented form)
	- Add config files
	- Program.cs > Add Service for Razor Pages [builder.Services.AddRazorPages();]
	- Program.cs > Map Razor Pages [app.MapRazorPages();]
	- Modify Auth model
	- Run in Nuget Manager Console: Add-Migration "Initial Create"
	- Run in Nuget Manager Console: Update-Database (for data base creation)

##Configuration

	- Create a file "env.json" into root forlder, using "env.template.json"
	- Set the "env.json" properties as:
		- Build action: Content
		- Copy to Output directory: Copy always
	- Make sure to exclude "env.json" file form the source control
