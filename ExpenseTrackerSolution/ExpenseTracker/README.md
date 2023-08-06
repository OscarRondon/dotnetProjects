#Expense Tracker Solution

	- Project Template: ASP.Net Core Web App (MVC)
	- DataBase: MS Sql Server


##Nuget Packages

	- Microsoft.EntityFrameworkCore
	- Microsoft.EntityFrameworkCore.SqlServer
	- Microsoft.EntityFrameworkCore.Tools
	- Syncfusion.EJ2.AspNet.Core [UI Library]
	- Syncfusion.Licensing
	- Newtonsonf.Json


##Times

	- General Coding			2.75
	- Review Documentation		10.5

##Instructions

	- Create a new project [ASP.Net Web App (MVC)]
	- Create Configuration rutines
	- Add Nuget Pacage
	- Create models
	- Create Db contex
	- Run in Nuget Manager Console: Add-Migration "Initial Create"
	- Run in Nuget Manager Console: Update-Database

##Configuration

	- Create a file "env.json" into root forlder, using "env.template.json"
	- Set the "env.json" properties as:
		- Build action: Content
		- Copy to Output directory: Copy always
	- Make sure to exclude "env.json" file form the source control

