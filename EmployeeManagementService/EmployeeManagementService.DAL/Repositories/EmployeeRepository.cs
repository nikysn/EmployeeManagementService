using Dapper;
using EmployeeManagementService.Contracts.Abstraction.Repositories;
using EmployeeManagementService.Contracts.DTOModels;
using EmployeeManagementService.DAL.Data;

namespace EmployeeManagementService.DAL.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DataContext _dataContext;

        public EmployeeRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<int> AddEmployee(AddEmployeeDTO addEmployee)
        {
            using var connection = _dataContext.CreateConnection();

            var sqlRequestAddEmployee = @"
            INSERT INTO Employees (Name, Surname, Phone, DepartmentId)
            VALUES (@Name, @Surname, @Phone, @DepartmentId)
            RETURNING Id;";

            var parameters = new
            {
                Name = addEmployee.Name,
                Surname = addEmployee.Surname,
                Phone = addEmployee.Phone,
                DepartmentId = addEmployee.DepartmentId
            };

            int newEmployeeId = await connection.ExecuteScalarAsync<int>(sqlRequestAddEmployee, parameters);

            var sqlRequestAddPassport = @"
        INSERT INTO Passports (EmployeeId, Type, Number)
        VALUES (@EmployeeId, @Type, @Number);";

            var parametersAddPassport = new
            {
                EmployeeId = newEmployeeId,
                Type = addEmployee.PassportType,
                Number = addEmployee.PassportNumber
            };

            await connection.ExecuteAsync(sqlRequestAddPassport, parametersAddPassport);

            return newEmployeeId;
        }

        public async Task DeleteEmployee(int employeeId)
        {
            using var connection = _dataContext.CreateConnection();

            var sqlRequest = "DELETE FROM Employees WHERE Id = @EmployeeId;";
            var parameters = new { EmployeeId = employeeId };

            await connection.ExecuteAsync(sqlRequest, parameters);
        }

        public async Task<IEnumerable<EmployeeDTO>> GetEmployeesByCompanyId(int companyId)
        {
            using var connection = _dataContext.CreateConnection();

            var sqlRequest = @"
      SELECT e.Id, e.Name, e.Surname, e.Phone,
             p.Id AS Id, p.Type AS Type, p.Number AS Number,
             d.Id AS Id, d.Name AS Name, d.Phone AS Phone, d.CompanyId AS CompanyId,
             c.Id AS Company_Id
      FROM Employees e
      INNER JOIN Passports p ON e.Id = p.EmployeeId
      INNER JOIN Departments d ON e.DepartmentId = d.Id
      INNER JOIN Companies c ON d.CompanyId = c.Id
      WHERE d.CompanyId = @CompanyId;";

            var employees = await connection.QueryAsync<EmployeeDTO, PassportDTO, DepartmentDTO, EmployeeDTO>(sqlRequest,
       (employee, passport, department) =>
       {
           employee.Passport = passport;
           employee.Department = department;
           employee.CompanyId = department.CompanyId;
           return employee;
       }, new { CompanyId = companyId }, splitOn: "Id, Id, Id");

            return employees.ToArray();
        }

        public async Task<IEnumerable<EmployeeDTO>> GetEmployeesByDepartmentId(int departmentId)
        {
            using var connection = _dataContext.CreateConnection();

            var sqlRequest = @"
        SELECT e.Id, e.Name, e.Surname, e.Phone,
               p.Id AS Id, p.Type AS Type, p.Number AS Number,
               d.Id AS Id, d.name AS Name, d.Phone AS Phone, d.CompanyId AS CompanyId,
               c.Id AS Company_Id
        FROM Employees e
        INNER JOIN Passports p ON e.Id = p.EmployeeId
        INNER JOIN Departments d ON e.DepartmentId = d.Id
        INNER JOIN Companies c ON d.CompanyId = c.Id
        WHERE d.Id = @DepartmentId;";

            IEnumerable<EmployeeDTO>? employees = await connection.QueryAsync<EmployeeDTO, PassportDTO, DepartmentDTO, EmployeeDTO>(sqlRequest,
                (employee, passport, department) =>
                {
                    employee.Passport = passport;
                    employee.Department = department;
                    employee.CompanyId = department.CompanyId;
                    return employee;
                }, new { DepartmentId = departmentId }, splitOn: "Id, Id, Id");

            return employees.ToArray();
        }

        public async Task<EmployeeDTO> UpdateEmployee(EmployeeDTO employeeDTO)
        {
            using var connection = _dataContext.CreateConnection();

            var sqlRequestUpdateEmployee = $@"
            UPDATE Employees
            SET Name = @Name, Surname = @Surname, Phone = @Phone, DepartmentId = @DepartmentId
            WHERE Id = @Id";

            await connection.ExecuteAsync(sqlRequestUpdateEmployee, employeeDTO);

            var sqlRequestUpdatePassport = @"
            UPDATE Passports
            SET Type = @PassportType, Number = @PassportNumber
            WHERE EmployeeId = @EmployeeId";

            await connection.ExecuteAsync(sqlRequestUpdatePassport, new
            {
                EmployeeId = employeeDTO.Id,
                PassportType = employeeDTO.Passport.Type,
                PassportNumber = employeeDTO.Passport.Number
            });

            return await GetEmployeeById(employeeDTO.Id);
        }

        public async Task<int> AddCompany(AddCompanyDTO addCompany)
        {
            using var connection = _dataContext.CreateConnection();

            var sqlRequest = @"
            INSERT INTO Companies (Name)
            VALUES (@Name)
            RETURNING Id;";

            var parameters = new
            {
                Name = addCompany.Name,
            };

            return await connection.ExecuteScalarAsync<int>(sqlRequest, parameters);
        }

        public async Task<int> AddDepartment(AddDepartmentDTO department)
        {
            using var connection = _dataContext.CreateConnection();

            var sqlRequest = @"
            INSERT INTO Departments (Name, Phone, CompanyId)
            VALUES (@Name, @Phone, @CompanyId)
            RETURNING Id;";

            var parameters = new
            {
                Name = department.Name,
                Phone = department.Phone,
                CompanyId = department.CompanyId
            };

            return await connection.ExecuteScalarAsync<int>(sqlRequest, parameters);
        }

        public async Task<EmployeeDTO?> GetEmployeeById(int employeeId)
        {
            using var connection = _dataContext.CreateConnection();

            var sqlRequest = @"
        SELECT e.Id, e.Name, e.Surname, e.Phone,
               p.Id AS Id, p.Type AS Type, p.Number AS Number,
               d.Id AS Id, d.Name AS Name, d.Phone AS Phone, d.CompanyId AS CompanyId,
               c.Id AS Company_Id
        FROM Employees e
        INNER JOIN Passports p ON e.Id = p.EmployeeId
        INNER JOIN Departments d ON e.DepartmentId = d.Id
        INNER JOIN Companies c ON d.CompanyId = c.Id
        WHERE e.Id = @employeeId;";

            var employee = await connection.QueryAsync<EmployeeDTO, PassportDTO, DepartmentDTO, EmployeeDTO>(
                sqlRequest,
                (employee, passport, department) =>
                {
                    employee.Passport = passport;
                    employee.Department = department;
                    employee.CompanyId = department.CompanyId;
                    return employee;
                },
                new { Id = employeeId },
                splitOn: "Id, Id, Id");

            return employee.Where(employee => employee.Id == employeeId).SingleOrDefault(); // Вернуть одного сотрудника или null, если ничего не найдено
        }

        public async Task<DepartmentDTO> GetDepartmentById(int departmentId)
        {
            using var connection = _dataContext.CreateConnection();

            var sqlRequest = "SELECT * FROM Departments WHERE Id = @DepartmentId;";

            var department = await connection.QuerySingleOrDefaultAsync<DepartmentDTO>(sqlRequest, new {DepartmentId = departmentId});

            return department;
        }

        public async Task<CompanyDTO> GetCompanyById(int companyId)
        {
            using var connection = _dataContext.CreateConnection();

            var sqlRequest = "SELECT * FROM Companies WHERE Id = @CompanyId";

            var company = await connection.QuerySingleOrDefaultAsync(sqlRequest, new {CompanyId = companyId});

            return company;
        }
    }
}
