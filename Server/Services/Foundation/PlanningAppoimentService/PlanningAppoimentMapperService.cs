using DTO;
using Server.Models.CabinetMedicals;
using Server.Models.Doctor;
using Server.Models.MedicalPlannings;
using Server.Models.UserAccount;
using Server.Models.UtilityModel;
using Server.Models.WorkDoctor;
using System.ComponentModel;
using static Server.Utility.Utility;

namespace Server.Services.Foundation.PlanningAppoimentService
{
    public static class PlanningAppoimentMapperService
    {

        public static MailRequest MapperMailRequestDeleteMedicalAppoiment(User user, User DoctorUserAccount, int k)
        {
            return new MailRequest
            {
                ToEmail = user.Email,
                Subject = "Notification",
                Body = " <h3> AliaMed.Com </h3> " +
                                $"<a>The order of your reservation number at the doctor {DoctorUserAccount.Firstname} {DoctorUserAccount.LastName} has been changed to {k}</a>" + "<br/>"
            };
        }
        public static MedicalPlanning MapperToNewMedicalPlanning(MedicalPlanning medicalPlanning, int CountPatient)
        {
            medicalPlanning.AppointmentCount = CountPatient;
            return medicalPlanning;
        }
        public static MedicalPlanning mapperToMedicalPlanning(PlanningInformationModel planningInformationModel, Doctors doctors, CabinetMedical cabinetMedical, string UserId)
        {
            return new MedicalPlanning
            {
                AppointmentCount = planningInformationModel.CountOfPatient,
                AppointmentDate = planningInformationModel.DateAppoiment,
                Id = Guid.NewGuid(),
                IdCabinet = cabinetMedical.Id,
                IdDoctor = doctors.Id,
                IdUser = UserId,
                Status = StatusPlaning.Still,
                AppointmentAdress = cabinetMedical.Adress,


            };
        }
        public static CabinetInformationAppointmentDto MapperToCabinetInformationAppointmentDto(CabinetMedical cabinetMedical)
        {
            return new CabinetInformationAppointmentDto
            {
                Id = EncryptGuid(cabinetMedical.Id),
                Adress = cabinetMedical.Adress,
                AdressMap = cabinetMedical.MapAdress,
                Image = cabinetMedical.image,
                Name = cabinetMedical.NameCabinet,
                NumberPhone = cabinetMedical.numberPhone,
                Services = cabinetMedical.Services


            };
        }
        public static DoctorInformationAppointmentDto mapperToDoctorInformationAppointmentDto(User user, List<string> Specialities, WorkDoctors workDoctors)
        {
            return new DoctorInformationAppointmentDto
            {
                FirstName = user.Firstname,
                LastName = user.LastName,
                Sexe = (Sexe)user.Sexe,
                Specialities = Specialities,
                TimeEnd = workDoctors.EndTime,
                TimeReady = workDoctors.ReadyTime,
                Id = EncryptGuid(Guid.Parse(user.Id)),
                NumberPatientAccepted=workDoctors.NbPatientAvailble
            };
        }
        public static AppointmentInformationDto MapperToAppointmentInformationDto(DoctorInformationAppointmentDto doctors, CabinetInformationAppointmentDto cabinetMedical, MedicalPlanning medicalPlanning)
        {
            return new AppointmentInformationDto
            {
                Id = EncryptGuid(medicalPlanning.Id),
                CountOfPatient = medicalPlanning.AppointmentCount,
                DateAppoiment = medicalPlanning.AppointmentDate,
                CabinetInformation = cabinetMedical,
                DoctorInformation = doctors


            };
        }
    }
}
