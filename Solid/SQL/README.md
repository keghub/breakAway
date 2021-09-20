## Database setup to run Breakaway Solid

- Install SQL Server (`choco install sql-server-2017`)
- In SSMS
  - Connect to server COMPUTERNAME using Windows Authentication
  - Create new database named `Breakaway`
  - Run SQL script to create tables and insert data
- In the solution, update the connection string in the `appsettings.json` with the COMPUTERNAME
