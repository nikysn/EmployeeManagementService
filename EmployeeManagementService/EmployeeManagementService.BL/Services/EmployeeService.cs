using EmployeeManagementService.Contracts.Abstraction.Repositories;
using EmployeeManagementService.Contracts.Abstraction.Services;
using EmployeeManagementService.Contracts.DTOModels;
using MapsterMapper;

namespace EmployeeManagementService.BL.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }
        public async Task<int> AddEmployee(AddEmployeeDTO addEmployee)
        {
            var department = await _employeeRepository.GetDepartmentById(addEmployee.DepartmentId);

            if(department == null)
            {
                throw new InvalidOperationException("Указанный отдел не существует.");
            }
            return await _employeeRepository.AddEmployee(addEmployee);
        }

        public async Task DeleteEmployee(int employeeId)
        {
            var currentEmployee = await _employeeRepository.GetEmployeeById(employeeId);
            if (currentEmployee == null)
            {
                throw new InvalidOperationException("Указанного сотрудника не существует");
            }

            await _employeeRepository.DeleteEmployee(employeeId);
        }

        public async Task<IEnumerable<EmployeeDTO>> GetEmployeesByCompanyId(int companyId)
        {
            return await _employeeRepository.GetEmployeesByCompanyId(companyId);
        }

        public async Task<IEnumerable<EmployeeDTO>> GetEmployeesByDepartmentId(int departmentId)
        {
            return await _employeeRepository.GetEmployeesByDepartmentId(departmentId);
        }

        public async Task<EmployeeDTO> UpdateEmployee(UpdateEmployeeDTO updateEmployeeDTO)
        {
            var currentEmployee = await _employeeRepository.GetEmployeeById(updateEmployeeDTO.Id);

            if(currentEmployee == null)
            {
                throw new InvalidOperationException("Указанного сотрудника не существует");
            }

            MapUpdateEmployeeDTOToEmployeeDTO(updateEmployeeDTO, currentEmployee);
            return await _employeeRepository.UpdateEmployee(currentEmployee);
        }

        public async Task<int> AddCompany(AddCompanyDTO addCompany)
        {
            return await _employeeRepository.AddCompany(addCompany);
        }

        public async Task<int> AddDepartment(AddDepartmentDTO addDepartment)
        {
            var company = await _employeeRepository.GetCompanyById(addDepartment.CompanyId);

            if (company == null)
            {
                throw new InvalidOperationException("Указанная компания не существует.");
            }
            return await _employeeRepository.AddDepartment(addDepartment);
        }

        private void MapUpdateEmployeeDTOToEmployeeDTO(UpdateEmployeeDTO source, EmployeeDTO destination)
        {
            if (!string.IsNullOrWhiteSpace(source.Name))
                destination.Name = source.Name;

            if (!string.IsNullOrWhiteSpace(source.Surname))
                destination.Surname = source.Surname;

            if (!string.IsNullOrWhiteSpace(source.Phone))
                destination.Phone = source.Phone;

            if (source.Passport != null && !string.IsNullOrWhiteSpace(source.Passport.Type))
                destination.Passport.Type = source.Passport.Type;

            if (source.Passport != null && !string.IsNullOrWhiteSpace(source.Passport.Number))
                destination.Passport.Number = source.Passport.Number;

            if (source.DepartmentId != null && source.DepartmentId != 0)
                destination.Department.Id = source.DepartmentId.Value;
        }
    }
}
