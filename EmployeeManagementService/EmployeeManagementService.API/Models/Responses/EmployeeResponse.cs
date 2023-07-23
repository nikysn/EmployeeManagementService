using EmployeeManagementService.Contracts.DTOModels;

namespace EmployeeManagementService.API.Models.Responses
{
    public class EmployeeResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public PassportResponse PassportResponse { get; set; }
        public DepartmentResponse DepartmentResponse { get; set; }
    }
}
