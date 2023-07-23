namespace EmployeeManagementService.Contracts.DTOModels
{
    public class UpdateEmployeeDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Phone { get; set; }
        public PassportDTO? Passport { get; set; }
        public int? DepartmentId { get; set; }
    }
}
