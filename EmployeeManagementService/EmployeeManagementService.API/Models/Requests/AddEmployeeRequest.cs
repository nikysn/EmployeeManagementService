using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementService.API.Models.Requests
{
    public class AddEmployeeRequest
    {
        [Required(ErrorMessage = "Необходимо ввести имя в поле.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Необходимо ввести фамилию в поле.")]
        public string Surname { get; set; }

        public string? Phone { get; set; }

        [Required(ErrorMessage = "Необходимо ввести DepartmentId в поле.")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Необходимо ввести тип пасспорта в поле.")]
        [StringLength(40, ErrorMessage = "Поле PassportType должно содержать не более 20 символов.")]
        public string PassportType { get; set; }

        [Required(ErrorMessage = "The 'PassportNumber' field is required.")]
        [StringLength(20, ErrorMessage = "The 'PassportNumber' field must be at most {1} characters long.")]
        public string PassportNumber { get; set; }
    }
}
