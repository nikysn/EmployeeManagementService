using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementService.API.Models.Requests
{
    public class AddCompanyRequest
    {
        [Required(ErrorMessage = "Необходимо ввести имя в поле.")]
        public string Name { get; set; }
    }
}
