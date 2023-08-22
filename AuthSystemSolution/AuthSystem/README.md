#Authentication System

	- Project Template: ASP.Net Core Web App (MVC)
	- DataBase: MS Sql Server
	- Source: https://www.youtube.com/watch?v=wzaoQiS_9dI&list=PLjrh5YRNPzdQMQSFaxP8XrouoVq4QDw2H&index=16&t=47s


##Nuget Packages

	- Microsoft.AspNetCore.Identity.EntityFrameworkCore
	- Microsoft.AspNetCore.Identity.UI
	- Microsoft.EntityFrameworkCore.SqlServer
	- Microsoft.EntityFrameworkCore.Tools
	- Microsoft.VisualStudio.Web.CodeGeneration.Design
	- Microsoft.VisualStudio.Web.CodeGeneration.Utils


##Times

	- General Coding		0.5	
	- Review Documentation		

##Instructions

	- Create a new project [ASP.Net Web App (MVC)]
	- Right click on project > Add > New Scaffolded Item > Identity (choose the options of the presented form)
	- Add config files
	- Program.cs > Add Service for Razor Pages [builder.Services.AddRazorPages();]
	- Program.cs > Map Razor Pages [app.MapRazorPages();]
	- Create models
	- Create Db contex
	- Run in Nuget Manager Console: Add-Migration "Initial Create"
	- Run in Nuget Manager Console: Update-Database (for data base creation)

##Configuration

	- Create a file "env.json" into root forlder, using "env.template.json"
	- Set the "env.json" properties as:
		- Build action: Content
		- Copy to Output directory: Copy always
	- Make sure to exclude "env.json" file form the source control
