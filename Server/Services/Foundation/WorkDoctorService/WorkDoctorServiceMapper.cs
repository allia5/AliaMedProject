using DTO;
using Microsoft.OpenApi.Extensions;
using Newtonsoft.Json.Linq;
using Server.Managers.Storages.DoctorManager;
using Server.Models.CabinetMedicals;
using Server.Models.Doctor;
using Server.Models.Specialites;
using Server.Models.UserAccount;
using Server.Models.WorkDoctor;
using System;
using static Server.Utility.Utility;

namespace Server.Services.Foundation.WorkDoctorService
{
    public static class WorkDoctorServiceMapper
    {

        public static MailRequest MapperToMailRequestWhenDeleteInvitationFromMedecin(User userDoctor, User userAdmin)
        {
            return new MailRequest
            {
                ToEmail = userAdmin.Email,
                Subject = "Notification",
                Body = " <h3> AliaMed.Com </h3> " +
                                           $"<a> {userDoctor.Email} rejected Invitation </a>" + "<br/>"
            };
        }
        public static WorkDoctors MapperToWorkDoctor(Guid IdCabinet, Guid IdDoctor)
        {
            return new WorkDoctors
            {
                Id = Guid.NewGuid(),
                DateInvitation = DateTime.Now,
                StatusWork = StatusWork.still,
                statusServcie = Models.WorkDoctor.StatusService.OutService,
                statusReservation = StatusReservation.NotAvailable,
                IdCabinet = IdCabinet,
                IdDoctor = IdDoctor,
                NbPatientAvailble = 0,
                TimeOfConsultation = 0,
                EndTime = DateTime.MinValue,
                ReadyTime = DateTime.MaxValue,
            };
        }
        public static MailRequest MapperMailRequestDeleteUser(User user, CabinetMedical cabinetMedical)
        {
            return new MailRequest
            {
                ToEmail = user.Email,
                Subject = "Notification",
                Body = " <h3> AliaMed.Com </h3> " +
                                $"<a>Your Job Delete from cabinet :{cabinetMedical.NameCabinet}</a>" + "<br/>"
            };
        }
        public static MailRequest MapperMailRequest(User user, CabinetMedical cabinetMedical)
        {
            return new MailRequest
            {
                ToEmail = user.Email,
                Subject = "Notification",
                Body = " <h3> AliaMed.Com </h3> " +
                                $"<a>you are recived Invitation from Cabine {cabinetMedical.NameCabinet}</a>" + "<br/>"
            };
        }
        public static MailRequest MapperMailRequestUpdateStatJobDoctor(User user, WorkDoctors workDoctors)
        {
            return new MailRequest
            {
                ToEmail = user.Email,
                Subject = "Notification",
                Body = " <h3> AliaMed.Com </h3> " +
                                $"<a>{user.Email}   he is {workDoctors.StatusWork.ToString()} Work </a>" + "<br/>"
            };
        }
        public static InvitationsDoctorDto MapperToInvitationsDoctorDto(CabinetMedical cabinet, WorkDoctors work)
        {
            try
            {
                return new InvitationsDoctorDto
                {
                    adress = cabinet.Adress,
                    DateInvitation = work.DateInvitation,
                    Id = EncryptGuid(work.Id),
                    NameCabinet = cabinet.NameCabinet,
                    Services = cabinet.Services

                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }


        }
        public static WorkDoctors MapperToNewWorkDoctorStatusService(WorkDoctors workDoctors, UpdateStatusWorkDoctorDto updateStatusWorkDoctorDto)
        {
            workDoctors.StatusWork = (StatusWork)updateStatusWorkDoctorDto.Status;
            return workDoctors;
        }
        public static JobsDoctorDto MapperToJobsDoctorDto(WorkDoctors JobId, CabinetMedical cabinetMedical)
        {
            return new JobsDoctorDto
            {
                Adress = cabinetMedical.Adress,
                IdJob = EncryptGuid(JobId.Id),
                Image = cabinetMedical.image,
                nameCabinet = cabinetMedical.NameCabinet,
                NumberPhone = cabinetMedical.numberPhone,
                Services = cabinetMedical.Services,
                timeJob = cabinetMedical.JobTime,
                StatusServiceDoctor = (StatusWorkDoctor)JobId.StatusWork
            };
        }
        public static JobSettingDto MapperToJobSetting(WorkDoctors workDoctors)
        {
            return new JobSettingDto
            {
                IdJob = EncryptGuid(workDoctors.Id),
                EndTime = workDoctors.EndTime,
                startTime = workDoctors.ReadyTime,
                NumberPatientAccepted = workDoctors.NbPatientAvailble,
                processingMinutes = workDoctors.TimeOfConsultation,
                StatusReservation = (DTO.StatusReservationDoctor)workDoctors.statusReservation,
                statusService = (StatusServiceDoctor)workDoctors.statusServcie

            };
        }
        public static WorkDoctors MapperToJobDoctorSetting(this WorkDoctors workDoctors, JobSettingDto jobSettingDto)
        {
            workDoctors.ReadyTime = jobSettingDto.startTime;
            workDoctors.statusReservation = (StatusReservation)jobSettingDto.StatusReservation;
            workDoctors.statusServcie = (Models.WorkDoctor.StatusService)jobSettingDto.statusService;
            workDoctors.EndTime = jobSettingDto.EndTime;
            workDoctors.NbPatientAvailble = jobSettingDto.NumberPatientAccepted;
            workDoctors.TimeOfConsultation = jobSettingDto.processingMinutes;
            return workDoctors;




        }
        public static DoctorCabinetDto MapperToDoctorCabinetDto(List<Specialite> specialities, Doctors doctors, JobSettingDto jobSettingDto, User user, WorkDoctors work)
        {
            List<string> names = specialities.Select(p => p.NameSpecialite).ToList();
            return new DoctorCabinetDto
            {
                Email = user.Email,
                FirstName = user.Firstname,
                LastName = user.LastName,
                NumberPhone = user.PhoneNumber,
                IdDoc = EncryptGuid(doctors.Id),
                StatusWork = (StatusWorkDoctor)work.StatusWork,
                JobSettingDto = jobSettingDto,
                Specialities = names
            };
        }



    }
}

