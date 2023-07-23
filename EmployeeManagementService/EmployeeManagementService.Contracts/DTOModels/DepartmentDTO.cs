namespace EmployeeManagementService.Contracts.DTOModels
{
    public class DepartmentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }

        public int CompanyId { get; set; }
    }
}
