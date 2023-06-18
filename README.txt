# Web-App

## Technologies Used

- Frontend: Angular
- Backend: .NET
- Database: SQL Server Express

## Description

Project sharing web page

## Installation

1. Clone this repository to your local machine.
2. Install frontend dependencies by running the following command in the project's root folder:

   ```
   npm install
   ```

3. Set up the backend:

   - Download and install the .NET SDK from [https://dotnet.microsoft.com/download](https://dotnet.microsoft.com/download).
   - Open the backend solution in Visual Studio.
   - Restore the necessary NuGet packages.
   - Configure the connection to the SQL Server Express database in the appropriate configuration file.

4. Set up the database:

   - Download and install SQL Server Express from [https://www.microsoft.com/sql-server/sql-server-downloads](https://www.microsoft.com/sql-server/sql-server-downloads).
   - Create a new database.
   - Execute the table creation and initial data scripts in the database.

5. Start the frontend by running the following command in the project's root folder:

   ```
   ng serve
   ```

6. Start the backend from Visual Studio Code.

7. Access the application through your web browser at [http://localhost:4200](http://localhost:4200).

## Collaborators

- Francisco Matias Fernandez
- Ana Lucia Beneitez

## Contributing

1. Fork this repository.
2. Create a branch for the new feature: `git checkout -b new-feature`.
3. Make the necessary changes and commit them: `git commit -m 'Add new feature'`.
4. Push your changes to the branch: `git push origin new-feature`.
5. Open a pull request in this repository.