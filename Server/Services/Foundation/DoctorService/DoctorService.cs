using DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using Newtonsoft.Json.Linq;
using Server.Managers.Storages.CabinetMedicalManager;
using Server.Managers.Storages.DoctorManager;
using Server.Managers.Storages.SpecialitiesManager;
using Server.Managers.Storages.WorkDoctorManager;
using Server.Models.UserAccount;
using static Server.Services.Foundation.DoctorService.DoctorServiceMapper;

namespace Server.Services.Foundation.DoctorService
{
    public partial class DoctorService : IDoctorService
    {
        public readonly UserManager<User> _userManager;
        public readonly IDoctorManager doctorManager;
        public readonly ICabinetMedicalManager cabinetMedicalManager;
        public readonly IWorkDoctorManager workDoctorManager;
        public readonly ISpecialitiesManager specialitiesManager;
        public DoctorService(UserManager<User> _userManager, IDoctorManager doctorManager, ICabinetMedicalManager cabinetMedicalManager, IWorkDoctorManager workDoctorManager, ISpecialitiesManager specialitiesManager)
        {
            this._userManager = _userManager;
            this.doctorManager = doctorManager;
            this.cabinetMedicalManager = cabinetMedicalManager;
            this.workDoctorManager = workDoctorManager;
            this.specialitiesManager = specialitiesManager;
        }
        public async Task<List<DoctorInformationDto>> GetListInformationFromDoctor(string Email) =>
       await TryCatch(async () =>
      {
          List<DoctorInformationDto> InformationsDoctor = new List<DoctorInformationDto>();
          var User = await this._userManager.FindByEmailAsync(Email);
          ValidateUserIsNull(User);
          var UsersOfRoleDoctor = await this.doctorManager.SelectDoctor();
          //  ValidateListUserIsEmpty(UsersOfRoleDoctor);
          var CabinetMedical = await this.cabinetMedicalManager.SelectCabinetMedicalByUserId(User.Id);
          ValidateCabinetMedicalIsNull(CabinetMedical);
          var ListOfWorksDoctor = await this.workDoctorManager.SelectWorksDoctorByIdCabinet(CabinetMedical.Id);

          bool Sign = true;
          foreach (var user in UsersOfRoleDoctor)
          {
              var Doctor = await this.doctorManager.SelectDoctorByIdUser(user.Id);
              foreach (var job in ListOfWorksDoctor)
              {
                  if (job.IdDoctor == Doctor.Id)
                  {
                      Sign = false;
                  }
              }
              if (Sign == true)
              {
                  var ListOfSpecialities = await this.specialitiesManager.SelectSpecialitiesByIdDoctor(Doctor.Id);
                  var informationDoctor = MapperToDoctorInformationDto(ListOfSpecialities, user);
                  InformationsDoctor.Add(informationDoctor);
                  ListOfSpecialities.Clear();

              }
              Sign = true;

          }
          return InformationsDoctor;


      });
    }
}
