using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Server.Models.Admin;
using Server.Models.Analyse;
using Server.Models.CabinetMedicals;
using Server.Models.ChronicDiseases;
using Server.Models.Doctor;
using Server.Models.FileChronicDisease;
using Server.Models.fileMedical;
using Server.Models.LineAnalyseMedical;
using Server.Models.LineRadioMedical;
using Server.Models.MedicalAnalysis;
using Server.Models.MedicalOrder;
using Server.Models.MedicalPlannings;
using Server.Models.Pharmacist;
using Server.Models.PrescriptionLine;
using Server.Models.Prescriptions;
using Server.Models.Radiologys;
using Server.Models.RadioMedical;
using Server.Models.ResultAnalyses;
using Server.Models.ResultsRadio;
using Server.Models.secretary;
using Server.Models.SpecialisteAnalyses;
using Server.Models.Specialites;
using Server.Models.SpecialtieDoctor;
using Server.Models.UserAccount;
using Server.Models.UserRoles;
using Server.Models.WorkDoctor;



namespace Server.Data
{
    public class ServerDbContext : DbContext
    {
        public ServerDbContext(DbContextOptions<ServerDbContext> option) : base(option)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<CabinetMedical>().HasData(new CabinetMedical
            {
                Id = Guid.Parse("CF35304B-7896-4B81-8F57-D0DCCDCCB836"),
                NameCabinet = "Cabinet Medical El Balsem",
                Adress = "rue 112 -Kores-Biskra",
                JobTime = "8h->16h --dimanche a jeudi",
               
                numberPhone = "0541253658",
                MapAdress = "",
                Services = "chirurgie-Coudre blessé-Médecin Général",



            });
            modelBuilder.Entity<Role>().HasData(new Role
            {
                Id = Guid.Parse("CF35304B-0241-4B81-8F57-D0DCCDCCB836"),
                Name = "ADMIN"


            });
            modelBuilder.Entity<Role>().HasData(new Role
            {
                Id = Guid.Parse("2B102F8F-079C-4AE1-B093-487BA70CF183"),
                Name = "PATIENT"


            });
            modelBuilder.Entity<Role>().HasData(new Role
            {
                Id = Guid.Parse("0D518584-64A4-424B-B011-7283083394B8"),
                Name = "SECRITAIRE"


            });
            modelBuilder.Entity<Role>().HasData(new Role
            {
                Id = Guid.Parse("14E8987F-77B0-44A9-A641-6C6779B9564C"),
                Name = "MEDECIN"


            });
            modelBuilder.Entity<Role>().HasData(new Role
            {
                Id = Guid.Parse("03D2395F-A472-4A41-B95F-45828D5F8AF4"),
                Name = "Radiologue".ToUpper()


            });
            modelBuilder.Entity<Role>().HasData(new Role
            {
                Id = Guid.Parse("0916F1E5-FF87-4D4F-89B2-D6DBB922027E"),
                Name = "pharmacien".ToUpper()


            });
            modelBuilder.Entity<Role>().HasData(new Role
            {
                Id = Guid.Parse("232D07C5-711E-4802-A048-F2F73804EA40"),
                Name = "analyse".ToUpper()


            });


            //

            /* modelBuilder.Entity<Specialite>()
             .Property(f => f.Id)
             .HasValueGenerator();*/
            modelBuilder.Entity<Specialite>().HasData(new Specialite
            {
                Id = 1,
                NameSpecialite = "Anesthésiologie".ToUpper()


            });
            modelBuilder.Entity<Specialite>().HasData(new Specialite
            {
                Id = 2,
                NameSpecialite = "Cardiologie".ToUpper()


            });
            modelBuilder.Entity<Specialite>().HasData(new Specialite
            {
                Id = 3,
                NameSpecialite = "Chirurgie".ToUpper()


            });
            modelBuilder.Entity<Specialite>().HasData(new Specialite
            {
                Id = 4,
                NameSpecialite = "Dermatologie".ToUpper()


            });
            modelBuilder.Entity<Specialite>().HasData(new Specialite
            {
                Id = 5,
                NameSpecialite = "Endocrinologie".ToUpper()


            });
            modelBuilder.Entity<Specialite>().HasData(new Specialite
            {
                Id = 6,
                NameSpecialite = "Gastro entérologie".ToUpper()


            });
            modelBuilder.Entity<Specialite>().HasData(new Specialite
            {
                Id = 7,
                NameSpecialite = "Gynécologie".ToUpper()


            });
            modelBuilder.Entity<Specialite>().HasData(new Specialite
            {
                Id = 8,
                NameSpecialite = "Hématologie".ToUpper()


            });
            modelBuilder.Entity<Specialite>().HasData(new Specialite
            {
                Id = 9,
                NameSpecialite = "Infectiologie".ToUpper()


            });
            modelBuilder.Entity<Specialite>().HasData(new Specialite
            {
                Id = 10,
                NameSpecialite = "Médecine du travail".ToUpper()


            });
            modelBuilder.Entity<Specialite>().HasData(new Specialite
            {
                Id = 11,
                NameSpecialite = "Médecine interne".ToUpper()


            });
            modelBuilder.Entity<Specialite>().HasData(new Specialite
            {
                Id = 12,
                NameSpecialite = "Néphrologie".ToUpper()


            });
            modelBuilder.Entity<Specialite>().HasData(new Specialite
            {
                Id = 13,
                NameSpecialite = "Neurologie".ToUpper()


            });
            modelBuilder.Entity<Specialite>().HasData(new Specialite
            {
                Id = 14,
                NameSpecialite = "Obstétrique".ToUpper()


            });
            modelBuilder.Entity<Specialite>().HasData(new Specialite
            {
                Id = 15,
                NameSpecialite = "Oncologie".ToUpper()


            });
            modelBuilder.Entity<Specialite>().HasData(new Specialite
            {
                Id = 16,
                NameSpecialite = "Ophtalmologie".ToUpper()


            });
            modelBuilder.Entity<Specialite>().HasData(new Specialite
            {
                Id = 17,
                NameSpecialite = "Orthopédie".ToUpper()


            });
            modelBuilder.Entity<Specialite>().HasData(new Specialite
            {
                Id = 18,
                NameSpecialite = "Oto-rhino-laryngologie".ToUpper()


            });
            modelBuilder.Entity<Specialite>().HasData(new Specialite
            {
                Id = 19,
                NameSpecialite = "Pédiatrie".ToUpper()


            });
            modelBuilder.Entity<Specialite>().HasData(new Specialite
            {
                Id = 20,
                NameSpecialite = "Pneumologie".ToUpper()


            });
            modelBuilder.Entity<Specialite>().HasData(new Specialite
            {
                Id = 21,
                NameSpecialite = "Psychiatrie".ToUpper()


            });
            modelBuilder.Entity<Specialite>().HasData(new Specialite
            {
                Id = 22,
                NameSpecialite = "Radiologie".ToUpper()


            });
            modelBuilder.Entity<Specialite>().HasData(new Specialite
            {
                Id = 23,
                NameSpecialite = "Rhumatologie".ToUpper()


            });
            modelBuilder.Entity<Specialite>().HasData(new Specialite
            {
                Id = 24,
                NameSpecialite = "Urgentiste".ToUpper()


            });

            modelBuilder.Entity<ChronicDisease>().HasData(new ChronicDisease
            {
                Id = 1,
                NameChronicDiseases = "Diabète".ToUpper()


            });
            modelBuilder.Entity<ChronicDisease>().HasData(new ChronicDisease
            {
                Id = 2,
                NameChronicDiseases = "Hypertension-artérielle".ToUpper()


            });
            modelBuilder.Entity<ChronicDisease>().HasData(new ChronicDisease
            {
                Id = 3,
                NameChronicDiseases = "Maladies-respiratoires".ToUpper()


            });

            modelBuilder.Entity<ChronicDisease>().HasData(new ChronicDisease
            {
                Id = 4,
                NameChronicDiseases = "Maladies rénales".ToUpper()


            });

            modelBuilder.Entity<ChronicDisease>().HasData(new ChronicDisease
            {
                Id = 5,
                NameChronicDiseases = "Maladies articulaires".ToUpper()


            });

            modelBuilder.Entity<ChronicDisease>().HasData(new ChronicDisease
            {
                Id = 6,
                NameChronicDiseases = "Maladies du foie".ToUpper()


            });

            modelBuilder.Entity<ChronicDisease>().HasData(new ChronicDisease
            {
                Id = 7,
                NameChronicDiseases = "Maladies neurologiques".ToUpper()


            });

            modelBuilder.Entity<ChronicDisease>().HasData(new ChronicDisease
            {
                Id = 8,
                NameChronicDiseases = "Cancer".ToUpper()


            });

            modelBuilder.Entity<ChronicDisease>().HasData(new ChronicDisease
            {
                Id = 9,
                NameChronicDiseases = "Maladies-cardiovasculaires".ToUpper()


            });










            base.OnModelCreating(modelBuilder);


            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }


        }
        public DbSet<User> users { get; set; }
        public DbSet<Admins> admins { get; set; }
        public DbSet<Analyses> Analyses { get; set; }
        public DbSet<CabinetMedical> cabinetMedicals { get; set; }
        public DbSet<ChronicDisease> chronicDiseases { get; set; }
        public DbSet<Doctors> Doctors { get; set; }
        public DbSet<FileChronicDiseases> FileChronicDiseases { get; set; }
        public DbSet<fileMedicals> fileMedicals { get; set; }
        public DbSet<SpecialisteAnalyse> SpecialisteAnalyse { get; set; }
        public DbSet<MedicalOrdres> MedicalOrdres { get; set; }
        public DbSet<MedicalPlanning> medicalPlannings { get; set; }
        public DbSet<Pharmacists> Pharmacists { get; set; }
        public DbSet<PrescriptionLines> PrescriptionLines { get; set; }
        public DbSet<Prescription> prescriptions { get; set; }
        public DbSet<Radiology> radiologies { get; set; }
        public DbSet<Radio> Radio { get; set; }
        public DbSet<ResultAnalyse> resultAnalyses { get; set; }
        public DbSet<ResultRadio> RadioResult { get; set; }
        public DbSet<Secretarys> Secretarys { get; set; }
        public DbSet<Specialite> specialites { get; set; }
        public DbSet<SpecialtiesDoctor> specialtiesDoctors { get; set; }

        public DbSet<WorkDoctors> WorkDoctors { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<UserRole> userRoles { get; set; }
        public DbSet<LineRadioMedicals> LineRadioMedicals { get; set; }
        public DbSet<LineAnalyseMedicals> LineAnalyseMedicals { get; set; }
        
    }
}
