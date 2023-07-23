namespace EmployeeManagementService.Contracts.DTOModels
{
    public class AddEmployeeDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public int DepartmentId { get; set; }
        public string PassportType { get; set; }
        public string PassportNumber { get; set; }
    }
}
