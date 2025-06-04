To launch this application, you need to place an appsettings.json configuration file in your project’s root directory. The file must contain these sections:

"Logging": Specify log levels, for example: { "Default": "Information", "Microsoft.AspNetCore": "Warning" }
"AllowedHosts": Set to "*"
"ConnectionStrings": Provide your database connection string under "DefaultConnection"
"Jwt": Include your secret JWT key, issuer ("DeviceHubUpdApp"), and audience ("DeviceHubUpdUsers")
Make sure to replace placeholder values (like the database connection string and JWT key) with your actual configuration details.
