using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    public partial class init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "patients",
                columns: table => new
                {
                    Ipp = table.Column<string>(type: "NVARCHAR2(10)", nullable: false),
                    Nom_Patient_Fr = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Prenom_Patient_Fr = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Prenom_Pere_Fr = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Nom_Patient_Ar = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Prenom_Patient_Ar = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Prenom_Pere_Ar = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Date_Naissance = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Lieu_Naissance = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Sexe = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Carte_Identite = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Date_Cin = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Passeport = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Adresse_Fr = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Adresse_Ar = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Ville = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Code_Postal = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Telephone1 = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Telephone2 = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Email = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Photo = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    CreatedBy = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    CreatedAt = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    UpdateBy = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    UpdateAt = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Situation = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patients", x => x.Ipp);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nom = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Prenom = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Login = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Password = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Role = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Situation = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "patientMiltaires",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Identifiant = table.Column<string>(type: "NVARCHAR2(16)", maxLength: 16, nullable: false),
                    MatriculeRec = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    AnneeRec = table.Column<string>(type: "NVARCHAR2(4)", maxLength: 4, nullable: true),
                    Armee = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    CodeGrade = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CodeCorps = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CodeCategorie = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CodeSCategorie = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CreatedBy = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    CreatedAt = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    UpdateBy = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    UpdateAt = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    PatientsIpp = table.Column<string>(type: "NVARCHAR2(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patientMiltaires", x => x.Id);
                    table.ForeignKey(
                        name: "FK_patientMiltaires_patients_PatientsIpp",
                        column: x => x.PatientsIpp,
                        principalTable: "patients",
                        principalColumn: "Ipp");
                });

            migrationBuilder.CreateTable(
                name: "patientsAyantDroits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Identifiant = table.Column<string>(type: "NVARCHAR2(16)", maxLength: 16, nullable: false),
                    CodeCategorie = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CodeSCategorie = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CreatedBy = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    CreatedAt = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    UpdateBy = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    UpdateAt = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    PatientsIpp = table.Column<string>(type: "NVARCHAR2(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patientsAyantDroits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_patientsAyantDroits_patients_PatientsIpp",
                        column: x => x.PatientsIpp,
                        principalTable: "patients",
                        principalColumn: "Ipp");
                });

            migrationBuilder.CreateTable(
                name: "patientsBebe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Identifiant = table.Column<string>(type: "NVARCHAR2(16)", maxLength: 16, nullable: false),
                    NumAdmissionMere = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    CodeCategorie = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CodeSCategorie = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CreatedBy = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    CreatedAt = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    UpdateBy = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    UpdateAt = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    PatientsIpp = table.Column<string>(type: "NVARCHAR2(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patientsBebe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_patientsBebe_patients_PatientsIpp",
                        column: x => x.PatientsIpp,
                        principalTable: "patients",
                        principalColumn: "Ipp");
                });

            migrationBuilder.CreateTable(
                name: "patientsCnam",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Identifiant = table.Column<string>(type: "NVARCHAR2(16)", maxLength: 16, nullable: false),
                    IndexAssure = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Matricule_Assure = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Lien_Parente = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    CodeCategorie = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CodeSCategorie = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CreatedBy = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    CreatedAt = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    UpdateBy = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    UpdateAt = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    PatientsIpp = table.Column<string>(type: "NVARCHAR2(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patientsCnam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_patientsCnam_patients_PatientsIpp",
                        column: x => x.PatientsIpp,
                        principalTable: "patients",
                        principalColumn: "Ipp");
                });

            migrationBuilder.CreateTable(
                name: "patientsCp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Identifiant = table.Column<string>(type: "NVARCHAR2(16)", maxLength: 16, nullable: false),
                    CodeCategorie = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CodeSCategorie = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CreatedBy = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    CreatedAt = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    UpdateBy = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    UpdateAt = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    PatientsIpp = table.Column<string>(type: "NVARCHAR2(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patientsCp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_patientsCp_patients_PatientsIpp",
                        column: x => x.PatientsIpp,
                        principalTable: "patients",
                        principalColumn: "Ipp");
                });

            migrationBuilder.CreateTable(
                name: "porteurCarteSoins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Identifiant = table.Column<string>(type: "NVARCHAR2(16)", maxLength: 16, nullable: false),
                    CodeCategorie = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CodeSCategorie = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    NumCarteSoin = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    DateValidite = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Situation = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    CreatedBy = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    CreatedAt = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    UpdateBy = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    UpdateAt = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    PatientsIpp = table.Column<string>(type: "NVARCHAR2(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_porteurCarteSoins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_porteurCarteSoins_patients_PatientsIpp",
                        column: x => x.PatientsIpp,
                        principalTable: "patients",
                        principalColumn: "Ipp");
                });

            migrationBuilder.CreateIndex(
                name: "IX_patientMiltaires_PatientsIpp",
                table: "patientMiltaires",
                column: "PatientsIpp");

            migrationBuilder.CreateIndex(
                name: "IX_patientsAyantDroits_PatientsIpp",
                table: "patientsAyantDroits",
                column: "PatientsIpp");

            migrationBuilder.CreateIndex(
                name: "IX_patientsBebe_PatientsIpp",
                table: "patientsBebe",
                column: "PatientsIpp");

            migrationBuilder.CreateIndex(
                name: "IX_patientsCnam_PatientsIpp",
                table: "patientsCnam",
                column: "PatientsIpp");

            migrationBuilder.CreateIndex(
                name: "IX_patientsCp_PatientsIpp",
                table: "patientsCp",
                column: "PatientsIpp");

            migrationBuilder.CreateIndex(
                name: "IX_porteurCarteSoins_PatientsIpp",
                table: "porteurCarteSoins",
                column: "PatientsIpp");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "patientMiltaires");

            migrationBuilder.DropTable(
                name: "patientsAyantDroits");

            migrationBuilder.DropTable(
                name: "patientsBebe");

            migrationBuilder.DropTable(
                name: "patientsCnam");

            migrationBuilder.DropTable(
                name: "patientsCp");

            migrationBuilder.DropTable(
                name: "porteurCarteSoins");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "patients");
        }
    }
}
