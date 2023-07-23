using EmployeeManagementService.Contracts.DTOModels;

namespace EmployeeManagementService.Contracts.Abstraction.Repositories
{
    public interface IEmployeeRepository
    {
        /// <summary>
        /// Добавляет нового сотрудника в хранилище.
        /// </summary>
        /// <param name="addEmployee">DTO с информацией о сотруднике, который будет добавлен.</param>
        /// <returns>Идентификатор вновь добавленного сотрудника.</returns>
        Task<int> AddEmployee(AddEmployeeDTO addEmployee);
        /// <summary>
        /// Удаляет сотрудника из базы данных по его идентификатору.
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
        /// <param name="employeeDTO">DTO с обновленной информацией о сотруднике.</param>
        /// <returns>DTO с обновленной информацией о сотруднике.</returns>
        Task<EmployeeDTO> UpdateEmployee(EmployeeDTO employeeDTO);
        /// <summary>
        /// Добавляет новую компанию в хранилище.
        /// </summary>
        /// <param name="addCompany">DTO с информацией о компании, которая будет добавлена.</param>
        /// <returns>Идентификатор вновь добавленной компании.</returns>
        Task<int> AddCompany(AddCompanyDTO addCompany);
        /// <summary>
        /// Добавляет новый отдел в хранилище.
        /// </summary>
        /// <param name="addDepartment">DTO с информацией об отделе, который будет добавлен.</param>
        /// <returns>Идентификатор вновь добавленного отдела.</returns>
        Task<int> AddDepartment(AddDepartmentDTO addDepartment);
        /// <summary>
        /// Возвращает информацию о сотруднике по его идентификатору.
        /// </summary>
        /// <param name="employeeId">Идентификатор сотрудника, для которого нужно получить информацию.</param>
        /// <returns>DTO с информацией о сотруднике или null, если сотрудник не найден.</returns>
        Task<EmployeeDTO?> GetEmployeeById(int employeeId);
        /// <summary>
        /// Получает информацию о отделе по его идентификатору.
        /// </summary>
        /// <param name="departmentId">Идентификатор отдела, для которого необходимо получить информацию.</param>
        /// <returns>Объект типа DepartmentDTO, представляющий информацию об отделе, или null, если отдел не найден.</returns>
        Task<DepartmentDTO> GetDepartmentById(int departmentId);
        /// <summary>
        /// Получает информацию о компании по его идентификатору.
        /// </summary>
        /// <param name="companyId">Идентификатор компании, для которого необходимо получить информацию.</param>
        /// <returns>Объект типа CompanyDTO, представляющий информацию об отделе, или null, если отдел не найден.</returns>
        Task<CompanyDTO> GetCompanyById(int companyId);
    }
}
