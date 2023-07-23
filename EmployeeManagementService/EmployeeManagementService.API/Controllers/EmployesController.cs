using EmployeeManagementService.API.Models.Requests;
using EmployeeManagementService.API.Models.Responses;
using EmployeeManagementService.Contracts.Abstraction.Services;
using EmployeeManagementService.Contracts.DTOModels;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public EmployesController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }

        [HttpGet("company/{companyId}")]
        public async Task<IActionResult> GetEmployeesByCompanyId(int companyId)
        {
            var employees = await _employeeService.GetEmployeesByCompanyId(companyId);

            if (employees == null || !employees.Any())
            {
                return NotFound(new { Message = "В указанной компании не найдены сотрудники."});
            }

            return Ok(employees.Adapt<EmployeeResponse[]>());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeesById(int id)
        {
            var employees = await _employeeRepository.GetEmployeeById(id);

            // Добавить обработку успешного получения списка сотрудников
            return Ok(employees);
        }

        [HttpGet("department/{departmentId}")]
        public async Task<IActionResult> GetEmployeesByDepartmentId(int departmentId)
        {
            var employees = await _employeeService.GetEmployeesByDepartmentId(departmentId);
            if (employees == null || !employees.Any())
            {
                return NotFound(new { Message = "В указанном отделе не найдены сотрудники." });
            }
            
            return Ok(employees.Adapt<EmployeeResponse[]>());
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] AddEmployeeRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var addEmployeeDTO = _mapper.Map<AddEmployeeDTO>(request);

            try
            {
                var newEmployeeId = await _employeeService.AddEmployee(addEmployeeDTO);
                return Ok(new { EmployeeId = newEmployeeId, Message = "Сотрудник успешно добавлен." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost("company")]
        public async Task<IActionResult> AddCompany([FromBody] AddCompanyRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var addCompany = _mapper.Map<AddCompanyDTO>(request);

            try
            {
                var companyId = await _employeeService.AddCompany(addCompany);
                return Ok(new { CompanyId = companyId, Message = "Компания успешно добавлена." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Не удалось добавить компанию.", Error = ex.Message });
            }
        }

        [HttpPost("department")]
        public async Task<IActionResult> AddDepartment([FromBody] AddDepartmentRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var addDepartment = _mapper.Map<AddDepartmentDTO>(request);

            try
            {
                var departmentId = await _employeeService.AddDepartment(addDepartment);
                return Ok(new { DepartmentId = departmentId, Message = "Отдел успешно добавлен." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Не удалось добавить отдел.", Error = ex.Message });
            }
        }

        [HttpDelete("{employeeId}")]
        public async Task<IActionResult> DeleteEmployee(int employeeId)
        {
            try
            {
                await _employeeService.DeleteEmployee(employeeId);
                return Ok(new { Message = "Сотрудник успешно удален." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Не удалось удалить сотрудника.", Error = ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployee([FromBody] UpdateEmployeeRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updateEmployeeDTO = _mapper.Map<UpdateEmployeeDTO>(request);

            try
            {
                var employeeDTO = await _employeeService.UpdateEmployee(updateEmployeeDTO);
                employeeDTO.Adapt<EmployeeResponse>();

                return Ok(new { Message = "Сотрудник успешно обновлен.", employeeDTO });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Не удалось обновить сотрудника.", Error = ex.Message });
            }
        }
    }
}
