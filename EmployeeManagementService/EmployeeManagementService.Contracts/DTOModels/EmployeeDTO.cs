namespace EmployeeManagementService.Contracts.DTOModels
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public int CompanyId { get; set; }
        public PassportDTO Passport { get; set; }
        public DepartmentDTO Department { get; set; }
    }
}
