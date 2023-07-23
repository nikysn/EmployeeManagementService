using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementService.API.Models.Requests
{
    public class AddDepartmentRequest
    {
        [Required(ErrorMessage = "Необходимо ввести имя в поле.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Необходимо ввести номер телефона в поле.")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Необходимо ввести CompanyId к которой относится данный отдел .")]
        public int CompanyId { get; set; }
    }
}
