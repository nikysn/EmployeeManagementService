using EmployeeManagementService.API.Models.Requests;
using EmployeeManagementService.API.Models.Responses;
using EmployeeManagementService.Contracts.DTOModels;
using Mapster;

namespace EmployeeManagementService.API.Mapping
{
    public class Mapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<AddEmployeeDTO, AddEmployeeRequest>();
            config.ForType<AddCompanyDTO, AddCompanyRequest>();
            config.ForType<AddDepartmentDTO, AddDepartmentRequest>();
            config.ForType<UpdateEmployeeDTO, AddEmployeeRequest>();

            TypeAdapterConfig<EmployeeDTO, EmployeeResponse>.NewConfig()
                    .Map(dest => dest.Id, src => src.Id)
                    .Map(dest => dest.Name, src => src.Name)
                    .Map(dest => dest.Surname, src => src.Surname)
                    .Map(dest => dest.Phone, src => src.Phone)
                    .Map(dest => dest.PassportResponse, src => src.Passport.Adapt<PassportResponse>())
                    .Map(dest => dest.DepartmentResponse, src => src.Department.Adapt<DepartmentResponse>());
        }
    }
}
