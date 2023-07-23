using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementService.API.Models.Requests
{
    public class UpdateEmployeeRequest
    {
        [Required(ErrorMessage = "Необходимо ввести Id в поле.")]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Phone { get; set; }
        public string? TypePassport { get; set; }
        public string? NumberPassport { get; set; }
        public int? DepartmentId { get; set; }
    }
}
