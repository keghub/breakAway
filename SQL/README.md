## Database setup to run Breakaway Pure

_IMPORTANT, prior to setting up the database, breakaway needs to be set up in IIS_

- Install SQL Server (`choco install sql-server-2017`)
- In SSMS
  - Connect to server COMPUTERNAME using Windows Authentication
  - Create new database named `Breakaway`
  - Run SQL script to create tables and insert data
  - Under server => Security => Logins, create new login for `IIS APPPOOL\AppPoolName` (using the AppPoolName set up in IIS), do not click search
  - Update the new login => Properties => User Mapping, add mapping to the `Breakaway` database, and add role `db_owner`
- In the solution, update the connection string in the `Web.config` with the COMPUTERNAME
