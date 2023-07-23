using EmployeeManagementService.Contracts.DTOModels;

namespace EmployeeManagementService.Contracts.Abstraction.Services
{
    public interface IEmployeeService
    {
        /// <summary>
        /// Добавляет нового сотрудника.
        /// </summary>
        /// <param name="addEmployee">DTO с информацией о сотруднике, который будет добавлен.</param>
        /// <returns>Идентификатор вновь добавленного сотрудника.</returns>
        Task<int> AddEmployee(AddEmployeeDTO addEmployee);
        /// <summary>
        /// Удаляет сотрудника по его идентификатору.
        /// </summary>
        /// <param name="employeeId">Идентификатор сотрудника, которого нужно удалить.</param>
        Task DeleteEmployee(int employeeId);
        /// <summary>
        /// Возвращает список сотрудников по идентификатору компании.
        /// </summary>
        /// <param name="companyId">Идентификатор компании, для которой нужно получить список сотрудников.</param>
        /// <returns>Список сотрудников, принадлежащих указанной компании.</returns>
        Task<IEnumerable<EmployeeDTO>> GetEmployeesByCompanyId(int companyId);
        /// <summary>
        /// Возвращает список сотрудников по идентификатору отдела.
        /// </summary>
        /// <param name="departmentId">Идентификатор отдела, для которого нужно получить список сотрудников.</param>
        /// <returns>Список сотрудников, принадлежащих указанному отделу.</returns>
        Task<IEnumerable<EmployeeDTO>> GetEmployeesByDepartmentId(int departmentId);
        /// <summary>
        /// Обновляет информацию о сотруднике.
        /// </summary>
        /// <param name="updateEmployeeDTO">DTO с обновленной информацией о сотруднике.</param>
        /// <returns>DTO с обновленной информацией о сотруднике.</returns>
        Task<EmployeeDTO> UpdateEmployee(UpdateEmployeeDTO updateEmployeeDTO);
        /// <summary>
        /// Добавляет новую компанию.
        /// </summary>
        /// <param name="addCompany">DTO с информацией о компании, которая будет добавлена.</param>
        /// <returns>Идентификатор вновь добавленной компании.</returns>
        Task<int> AddCompany(AddCompanyDTO addCompany);
        /// <summary>
        /// Добавляет новый отдел.
        /// </summary>
        /// <param name="addDepartment">DTO с информацией об отделе, который будет добавлен.</param>
        /// <returns>Идентификатор вновь добавленного отдела.</returns>
        Task<int> AddDepartment(AddDepartmentDTO addDepartment);
    }
}
