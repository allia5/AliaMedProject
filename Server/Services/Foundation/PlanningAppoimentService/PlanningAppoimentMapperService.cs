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
        public static MedicalPlanning MapperToNewDelayeMedicalPlanning(DelayeAppoimentMedical updateStatusAppoiment, MedicalPlanning medicalPlanning)
        {
            var newMedicalPlanning = medicalPlanning;
            newMedicalPlanning.Status = (StatusPlaning)updateStatusAppoiment.statusPlaningDto;
            newMedicalPlanning.AppointmentDate = updateStatusAppoiment.DateAppoiment;
            return newMedicalPlanning;
        }

        public static MailRequest MapperMailRequestUpdateStatusAppoiment(UpdateStatusAppoimentDto updateStatusAppoiment , User user ,User UserDoctor)
        {
            return new MailRequest
            {
                ToEmail = user.Email,
                Subject = "Notification",
                Body = $"<div class=card>\r\n    <div class=card-header>\r\n       <h3> AliaMed.Com </h3>\r\n    </div>\r\n    <div class=card-body>\r\n      <h5 class=card-title> Reservation status notification  </h5>\r\n        <p class=card-text>status your appoiment has been chaged to  {updateStatusAppoiment.statusPlaningDto} By Doctor : {UserDoctor.Firstname} {UserDoctor.LastName}</p>\r\n        <a href=\"#\" class=btn-primary>Go somewhere</a>\r\n    </div>\r\n</div>"
            };
        }

        public static MailRequest MapperMailRequestDelayAppoiment(DelayeAppoimentMedical delayeAppoiment, User user, User UserDoctor,int CountPatient)
        {
            return new MailRequest
            {
                ToEmail = user.Email,
                Subject = "Notification",
                Body = " <h3> AliaMed.Com </h3> " +
                                $"<a> appoiment has been delayed  to date   {delayeAppoiment.DateAppoiment} and count of your appointment has been {CountPatient} By Doctor : {UserDoctor.Firstname} {UserDoctor.LastName} </a>" + "<br/>"
            };
        }

        public static MedicalPlanning MapperToNewMedicalPlanning(UpdateStatusAppoimentDto updateStatusAppoiment,MedicalPlanning medicalPlanning)
        {
            var newPlanningMedical = medicalPlanning;
            newPlanningMedical.Status = (StatusPlaning)updateStatusAppoiment.statusPlaningDto;
            return newPlanningMedical;
        }
        public static PatientAppoimentInformationDto MapperToPatientAppoimentInformationDto(MedicalPlanning medicalPlanning)
        {
            return new PatientAppoimentInformationDto
            {
AppoimentCount=medicalPlanning.AppointmentCount,
DateAppoiment=medicalPlanning.AppointmentDate,
Id=EncryptGuid( medicalPlanning.Id),
Status= (StatusPlaningDto)medicalPlanning.Status
            };
        }
        public static PatientInformationDto MppperToPatientInformationDto(User userAccount)
        {
            return new PatientInformationDto
            {
DateOfBirth = userAccount.DateOfBirth,
FirstName =userAccount.Firstname,
LastName =userAccount.LastName,
Id =EncryptGuid(Guid.Parse(userAccount.Id) ).ToString(),
NationalNumber = userAccount.NationalNumber,
NumberPhone=userAccount.PhoneNumber,
Sexe= (Sexe)userAccount.Sexe
            };
        }
        public static PlanningDto MapperToPlanningDto(PatientInformationDto patientInformationDto,PatientAppoimentInformationDto patientAppoimentInformationDto)
        {
            return new PlanningDto
            {
                PatientAppoimentInformation = patientAppoimentInformationDto,
                PatientInformation = patientInformationDto
            };
            
        }

        public static MailRequest MapperMailRequestDeleteMedicalAppoiment(User user, User DoctorUserAccount, int k)
        {
            return new MailRequest
            {
                ToEmail = user.Email,
                Subject = "Notification",
                Body = $"<div class=card>\r\n    <div class=card-header>\r\n       <h3> AliaMed.Com </h3>\r\n    </div>\r\n    <div class=card-body>\r\n      <h5 class=card-title> Reservation status notification  </h5>\r\n        <p class=card-text>Order OfYour Appointment has been changed To <br/> <h1 class=\"display-1\">{k}</h1><br/> by Doctor :{DoctorUserAccount.Firstname} ,{DoctorUserAccount.LastName}</p>\r\n        <a href=\"#\" class=btn-primary>Go somewhere</a>\r\n    </div>\r\n</div>" 
                               
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
