
#OARCapiRestTools

	- Project Template: ASP.Net Core Web API


##Nuget Packages

	- Swashbuckle.AspNetCore.Newtonsoft

##Instructions

	- In project Properties > build > Documentation File > chek Generate a file conteining API Documentation
	- In Program.cs modify builder.Services.AddControllers() to builder.Services.AddControllers().AddNewtonsoftJson(); < for patch operations


##Configuration

	- Create a file "env.json" into root forlder, using "env.template.json"
	- Set the "env.json" properties as:
		- Build action: Content
		- Copy to Output directory: Copy always
	- Make sure to exclude "env.json" file form the source control
	- Add to Program.cs > builder.Configuration.AddJsonFile("env.json", optional: true, reloadOnChange: true);

##Notes

	- On VisualStudio main menu > Edit > Paste Special > JSON as Classes

OARCapiRestTools
[
	*	Add Controller for email sending from oscar.rondon.c@gmail.com
]
## Todos