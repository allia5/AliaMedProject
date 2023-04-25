using DTO;
using Microsoft.AspNetCore.Identity;
using Server.Managers.Storages.AnalyseManager;
using Server.Managers.Storages.DoctorManager;
using Server.Managers.Storages.FileMedicalManager;
using Server.Managers.Storages.LineAnalyseMedicalManager;
using Server.Managers.Storages.LineRadioMedicalManager;
using Server.Managers.Storages.OrdreMedicalManager;
using Server.Managers.Storages.RadiologyManager;
using Server.Managers.Storages.RadioManager;
using Server.Managers.Storages.RadioResultManager;
using Server.Managers.Storages.SpecialisteAnalyseManager;
using Server.Managers.UserManager;
using Server.Models.UserAccount;
using Server.Services.Foundation.MailService;

namespace Server.Services.Foundation.ResultAnalyseService
{
    public partial class ResultAnalyseService : IResultAnalyseService
    {
        public readonly IFileMedicalManager FileMedicalManager;
        public readonly IUserManager userManager;
        public readonly IDoctorManager doctorManager;
        public readonly UserManager<User> _UserManager;
        public readonly IOrdreMedicalManager ordreMedicalManager;
        public readonly IAnalyseManager AnalyseManager;
        public readonly ILineAnalyseMedicalManager lineAnalyseMedicalManager;
        public readonly ISpecialisteAnalyseManager SpecialisteAnalyseManager;
        public readonly IMailService mailService;
        public ResultAnalyseService(IMailService mailService,ISpecialisteAnalyseManager SpecialisteAnalyseManager, ILineAnalyseMedicalManager lineAnalyseMedicalManager,IAnalyseManager AnalyseManager,IOrdreMedicalManager ordreMedicalManager,IFileMedicalManager FileMedicalManager, IUserManager userManager, IDoctorManager doctorManager, UserManager<User> _UserManager)
        { 
            this.mailService = mailService;
            this.doctorManager = doctorManager;
            this.userManager = userManager;
            this._UserManager = _UserManager;
            this.lineAnalyseMedicalManager = lineAnalyseMedicalManager;
            this.SpecialisteAnalyseManager = SpecialisteAnalyseManager;
            this.FileMedicalManager = FileMedicalManager;
            this.ordreMedicalManager = ordreMedicalManager;
            this.AnalyseManager= AnalyseManager;
        }
       
       
    }
}
