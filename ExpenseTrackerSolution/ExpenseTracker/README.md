﻿#Expense Tracker Solution

	- Project Template: ASP.Net Core Web App (MVC)
	- DataBase: MS Sql Server
	- Source: https://www.youtube.com/watch?v=zQ5eijfpuu8&list=PLjrh5YRNPzdRwEBBejxtFAHaRRA6ydyQy&index=18


##Nuget Packages

	- Microsoft.EntityFrameworkCore
	- Microsoft.EntityFrameworkCore.SqlServer
	- Microsoft.EntityFrameworkCore.Tools
	- Microsoft.VisualStudio.Web.CodeGeneration.Design
	- Microsoft.VisualStudio.Web.CodeGeneration.Utils
	- Syncfusion.EJ2.AspNet.Core [UI Library]
	- Syncfusion.Licensing
	- Newtonsonf.Json


##Times

	- General Coding			15
	- Review Documentation		13.5

##Instructions

	- Create a new project [ASP.Net Web App (MVC)]
	- Create Configuration rutines
	- Add Nuget Pacage
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

