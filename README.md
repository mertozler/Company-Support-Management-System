# Company Support Manangment System
 

CompanyPanel was developed with .NET 5.0. N-Tier architectural structure was used in the project. The project was developed within the scope of the <a href="https://egebarkod.com/">EGEBARKOD</a> internship project. The project performs integration processes for companies.

## Features

- User registration and management with ASP.NET Identity.
- Company registration, service registration defined for companies, demand and file management.
- Managing user requests with personnel records.
- Background processes, scheduled tasks with Hangfire.
- Mail notifications with SMTP. Timed or instant notification messages to personnel in case of a request being assigned or not answered.
- Logging and viewing with Serilog and Serilog MySQLServer Sinks.
- Controllable, customizable settings for the admin user.
- Instant, fast and file sharing messaging between user and staff

## Installation
You have to do all the steps in order, otherwise the project will not work because the database is not created correctly. If you get an error it will be due to MYSQL server path. For this, you should go to `.\CompanyPanelUI\Startup.cs` and set the database paths in Startup.cs according to your own database. After this step, you need to go to `.\DataAccessLayer\Concrete \Context.cs` and follow the same step. And then open the terminal and follow the steps below in order.

```sh
$ cd .\CompanyPanelUI\
$ dotnet ef database update
$ cd..
$ cd .\DataAccessLayer\
$ dotnet ef database update
```


## License

MIT

**Free Software, Hell Yeah!**

