using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;
using System.Data;

namespace EmployeeManagementService.DAL.Data
{
    public class DataContext
    {
        private DbSettings _dbSettings;
        private const string DefaultDatabase = "postgres";

        public DataContext(IOptions<DbSettings> dbSettings)
        {
            _dbSettings = dbSettings.Value;
        }

        public IDbConnection CreateConnection(string database = null)
        {
            database ??= _dbSettings.Database;
            var connectionString = $"Server={_dbSettings.Server}; Database={database}; UserId={_dbSettings.UserId}; Password={_dbSettings.Password}";
            return new NpgsqlConnection(connectionString);
        }

        public async Task Init()
        {
            await InitDatabase();
            using var connection = CreateConnection();
            await InitTables(connection);
        }

        private async Task InitDatabase()
        {
            var connection = CreateConnection(DefaultDatabase);

            var sqlDbCount = $"SELECT COUNT(*) FROM pg_database WHERE datname = '{_dbSettings.Database}';";
            var dbCount = await connection.ExecuteScalarAsync<int>(sqlDbCount);
            if (dbCount == 0)
            {
                var request = $"CREATE DATABASE \"{_dbSettings.Database}\"";
                await connection.ExecuteAsync(request);
            }
        }

        private async Task InitTables(IDbConnection connection)
        {
            await CreateCompaniesTable(connection);
            await CreateDepartmentsTable(connection);
            await CreateEmployeesTable(connection);
            await CreatePassportsTable(connection);
        }

        private async Task CreateCompaniesTable(IDbConnection connection)
        {
            var sql = @"
        CREATE TABLE IF NOT EXISTS Companies (
            Id SERIAL  PRIMARY KEY,
            Name VARCHAR(255)
        );
    ";
            await connection.ExecuteAsync(sql);
        }

        private async Task CreateDepartmentsTable(IDbConnection connection)
        {
            var sql = @"
        CREATE TABLE IF NOT EXISTS Departments (
            Id SERIAL  PRIMARY KEY,
            Name VARCHAR(255),
            Phone VARCHAR(20),
            CompanyId INT,
            FOREIGN KEY (CompanyId) REFERENCES Companies(Id)
        );
    ";
            await connection.ExecuteAsync(sql);
        }

        private async Task CreateEmployeesTable(IDbConnection connection)
        {
            var sql = @"
        CREATE TABLE IF NOT EXISTS Employees (
            Id SERIAL  PRIMARY KEY,
            Name VARCHAR(255),
            Surname VARCHAR(255),
            Phone VARCHAR(20),
            DepartmentId INT,
            FOREIGN KEY (DepartmentId) REFERENCES Departments(Id)
        );
    ";
            await connection.ExecuteAsync(sql);
        }

        private async Task CreatePassportsTable(IDbConnection connection)
        {
            var sql = @"
        CREATE TABLE IF NOT EXISTS Passports (
            Id SERIAL PRIMARY KEY,
            EmployeeId INT,
            Type VARCHAR(50),
            Number VARCHAR(50),
            FOREIGN KEY (EmployeeId) REFERENCES Employees(Id)
        );
    ";
            await connection.ExecuteAsync(sql);
        }
    }
}
