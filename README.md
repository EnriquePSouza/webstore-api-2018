# Web Store
![VSC Badge](https://img.shields.io/badge/WebStoreAPI-1.0-blue.svg) ![VSC Badge](https://img.shields.io/badge/VisualStudioCode-1.20.1-blue.svg) ![VSC Badge](https://img.shields.io/badge/.NetCore2.0-2.1.4-blue.svg) ![swagger Badge](https://img.shields.io/badge/SQLServerManagementStudio-v17.4-yellow.svg) ![swagger Badge](https://img.shields.io/badge/Swagger-1.0.0-green.svg)

## How to Execute Web Store API

Get a SQLServer 2008 or above and access this path to execute database scripts:
```
..\WebStore\WebStore.Infra\Scripts
```
Log-in your SQL Server and execute "create_database.sql" -- This file contains:
```
CREATE DATABASE, CREATE TABLES and first Data Mass for the Products Table
```
After this execute all stored procedure scripts and open the application in Visual Studio Community 2017 Version 15.5.2 or above,
or use Visual Studio Code Version 1.20.1 or above, and access this file to put your connection string.
```
..\WebStore\WebStore.Api\appsettings.json
```
Build the Project and Run:

In Visual Studio Community select this solution:
```
..\WebStore\WebStore.sln - Open Project Build and Run with the "WebStoreApi" set as Startup Project.
```
In Visual Studio Code, select the folder "WebStore".
Visual Studio Code ask you to create ".vscode" folder. Click "yes".
Open terminal (Ctrl+') and execute this commands:
```
cd .\WebStore.Api\
dotnet restore
dotnet build
dotnet run
```
Or after the build access Visual Studio Code Debug menu and run the project.

In command 'dotnet run' you need to open the browser and put your localhost informed in the Terminal.

After the project is in running state, access:
```
http://<LocalHost>/swagger/
```
Execute "POST /v1/customers" To Create your user, 
open this service and click in "Example Value" edit values in "Value" field and click in "Try it out!"

After this execute "POST /v1/authenticate" To get your token to authorize other services,
open this service and put your "username" and "password" values and click in "Try it out!"

After this catch your token in "Response Body" field without the quotation marks.
click in the header "Authorize" button and inform your token:
```
Available authorizations
Example: Bearer {token}
Put in the field: Bearer YOUR TOKEN
```
And click in "Authorize", after this you will be able to execute all services.

Everything should be ok up and running. =)

### Tests
You can execute tests:
in Visual Studio Community:
Open "Test Explorer" and run the tests or if the test don't show, select the "WebStore.Tests" and run the tests.

In Visual Studio Code:
Open Terminal (Ctrl+') go to "WebStore" folder and execute this commands:
```
cd .\WebStore.Tests\
dotnet build
dotnet test
```
### Comments
This first version of the system does not do the CRUD of the product table, this functionality will be added in the second version of the system, where I will add the system management interface.

More informations coming soon, so keep an eye on next commits! ;)