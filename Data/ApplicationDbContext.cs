using Microsoft.EntityFrameworkCore;
using WebApplication1.Dto;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }



        /* pour résoudre le problème de mise à jour de table qui contient des declencheurs*/
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /* modelBuilder.Entity<intervention_details>()
                 .ToTable(tb => tb.HasTrigger("tr_UpdateInterventionStatus"));*/



            /*  modelBuilder.Entity<reparationMaintenance>()
                  .HasOne(rm => rm.Maintenance)
                  .WithMany(m => m.Reparations)
                  .HasForeignKey(rm => rm.maintenanceId);

              modelBuilder.Entity<reparationMaintenance>()
                  .HasOne(rm => rm.LieuReparation)
                  .WithMany(lr => lr.Reparations)
                  .HasForeignKey(rm => rm.lieuId);*/



            /*------------------------------------------------ BHSE.PATIENT --------------------------------*/
            modelBuilder.Entity<PatientBhse>().ToTable("PATIENT", schema: "BHSE");
            modelBuilder.Entity<PatientBhse>().HasKey(e=>e.IDENTIFIANT);


            /*--------------------------------bhse.categorie--------------------------------------------*/


            modelBuilder.Entity<Scategorie>().ToTable("SCATEGORIE", schema: "BHSE");
            modelBuilder.Entity<Scategorie>().HasNoKey();


            /*------------------------------------------- BHSE.CATEGORIE---------------------*/
           modelBuilder.Entity<Categorie>().ToTable("CATEGORIE", schema: "BHSE");
            modelBuilder.Entity<Categorie>().HasKey(e => e.CODE_CATEGORIE);

            /*-------------------------------- BHSE.GRADE-------------------------*/
             modelBuilder.Entity<Grade>().ToTable("GRADE", schema: "BHSE");
            modelBuilder.Entity<Grade>().HasKey(e => e.CODE_GRADE);


            /*-------------------------------- BHSE.VILLE-------------------------*/
            modelBuilder.Entity<Ville>().ToTable("VILLE", schema: "BHSE");
            modelBuilder.Entity<Ville>().HasKey(e => e.CODE_VILLE);


            /*---------------------------------BHSE.CORPS------------------------*/

            modelBuilder.Entity<Corps>().ToTable("CORPS", schema: "BHSE");
            modelBuilder.Entity<Corps>().HasKey(e => e.CODE_CORPS);


            /*-------------------------------------BHSE.ASSURE--------------------------------*/
            modelBuilder.Entity<Assure>().ToTable("ASSURE", schema: "BHSE");
            modelBuilder.Entity<Assure>().HasKey(e => e.MATRICULE_ASSURE);


        }



      
        public DbSet<User> users { get; set; }
        public DbSet<Patients> patients { get; set; }
        public DbSet<patientMiltaire> patientMiltaires { get; set; }
        public DbSet<porteurCarteSoins> porteurCarteSoins { get; set; }
        public DbSet<PatientCnam>? patientsCnam { get; set; }
        public DbSet<PatientAyantDroit>? patientsAyantDroits { get; set; }
        public DbSet<PatientBebe>? patientsBebe { get; set; }
        public DbSet<PatientCp>? patientsCp { get; set; }


        /*--------------------- schema Bhse -------------------------------*/

        public DbSet<PatientBhse> patientsBhse{ get; set; }
        public DbSet<Scategorie> Scategories { get; set; }
        public DbSet<Categorie> Categories { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Ville> Villes { get; set; }
       public DbSet<Corps> Corps { get; set; }
        public DbSet<Assure> Assures { get; set; }
















    }

}
