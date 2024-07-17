using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Papirus.WebApi.Infrastructure.Data.Migrations;

/// <inheritdoc />
[ExcludeFromCodeCoverage]
public partial class InitialMigration : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "ActorTypes",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ActorTypes", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "DocumentTypes",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_DocumentTypes", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Firms",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                GuidIdentifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Firms", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "IdentificationTypes",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                Abbreviation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_IdentificationTypes", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Permissions",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Permissions", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "PersonTypes",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PersonTypes", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "ProcessTypes",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ProcessTypes", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Roles",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Roles", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Status",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Status", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Teams",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Teams", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "People",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                GuidIdentifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                PersonTypeId = table.Column<int>(type: "int", nullable: false),
                Name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                Email = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                IdentificationTypeId = table.Column<int>(type: "int", nullable: false),
                IdentificationNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                SupportFileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                SupportFilePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_People", x => x.Id);
                table.ForeignKey(
                    name: "FK_People_IdentificationTypes",
                    column: x => x.IdentificationTypeId,
                    principalTable: "IdentificationTypes",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_People_PersonTypes",
                    column: x => x.PersonTypeId,
                    principalTable: "PersonTypes",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "Processes",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                ProcessTypeId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Processes", x => x.Id);
                table.ForeignKey(
                    name: "FK_Processes_ProcessTypes",
                    column: x => x.ProcessTypeId,
                    principalTable: "ProcessTypes",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "RolePermissions",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                RoleId = table.Column<int>(type: "int", nullable: false),
                PermissionId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_RolePermissions", x => x.Id);
                table.ForeignKey(
                    name: "FK_RolePermissions_Permissions",
                    column: x => x.PermissionId,
                    principalTable: "Permissions",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_RolePermissions_Roles",
                    column: x => x.RoleId,
                    principalTable: "Roles",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "Users",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Email = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                HashedPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Salt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                RegistrationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                IsActive = table.Column<bool>(type: "bit", nullable: false),
                RoleId = table.Column<int>(type: "int", nullable: false),
                FirmId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Users", x => x.Id);
                table.ForeignKey(
                    name: "FK_Users_Firms",
                    column: x => x.FirmId,
                    principalTable: "Firms",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_Users_Roles",
                    column: x => x.RoleId,
                    principalTable: "Roles",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "SubProcesses",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                Abbreviation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                ProcessId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_SubProcesses", x => x.Id);
                table.ForeignKey(
                    name: "FK_SubProcesses_Processes",
                    column: x => x.ProcessId,
                    principalTable: "Processes",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "TeamMembers",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                TeamId = table.Column<int>(type: "int", nullable: false),
                MemberId = table.Column<int>(type: "int", nullable: false),
                IsLead = table.Column<bool>(type: "bit", nullable: false),
                MaxCases = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TeamMembers", x => x.Id);
                table.ForeignKey(
                    name: "FK_TeamMembers_Teams",
                    column: x => x.TeamId,
                    principalTable: "Teams",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_TeamMembers_Users",
                    column: x => x.MemberId,
                    principalTable: "Users",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "Cases",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                RegistrationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                GuidIdentifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Court = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                Amount = table.Column<decimal>(type: "money", nullable: false),
                SubmissionDate = table.Column<DateTime>(type: "datetime", nullable: true),
                SubmissionIdentifier = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                DeadLineDate = table.Column<DateTime>(type: "datetime", nullable: true),
                ProcessTypeId = table.Column<int>(type: "int", nullable: false),
                ProcessId = table.Column<int>(type: "int", nullable: false),
                SubProcessId = table.Column<int>(type: "int", nullable: true),
                FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                IsAssigned = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Cases", x => x.Id);
                table.ForeignKey(
                    name: "FK_Cases_ProcessTypes",
                    column: x => x.ProcessTypeId,
                    principalTable: "ProcessTypes",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_Cases_Processes",
                    column: x => x.ProcessId,
                    principalTable: "Processes",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_Cases_SubProcesses",
                    column: x => x.SubProcessId,
                    principalTable: "SubProcesses",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "ProcessTemplates",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                FirmId = table.Column<int>(type: "int", nullable: false),
                ProcessTypeId = table.Column<int>(type: "int", nullable: false),
                ProcessId = table.Column<int>(type: "int", nullable: false),
                SubProcessId = table.Column<int>(type: "int", nullable: true),
                FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ProcessTemplates", x => x.Id);
                table.ForeignKey(
                    name: "FK_ProcessTemplates_Firms",
                    column: x => x.FirmId,
                    principalTable: "Firms",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_ProcessTemplates_ProcessTypes",
                    column: x => x.ProcessTypeId,
                    principalTable: "ProcessTypes",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_ProcessTemplates_Processes",
                    column: x => x.ProcessId,
                    principalTable: "Processes",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_ProcessTemplates_SubProcesses",
                    column: x => x.SubProcessId,
                    principalTable: "SubProcesses",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "Actors",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                ActorTypeId = table.Column<int>(type: "int", nullable: false),
                PersonId = table.Column<int>(type: "int", nullable: false),
                CaseId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Actors", x => x.Id);
                table.ForeignKey(
                    name: "FK_Actors_ActorTypes",
                    column: x => x.ActorTypeId,
                    principalTable: "ActorTypes",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_Actors_Cases",
                    column: x => x.CaseId,
                    principalTable: "Cases",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_Actors_People",
                    column: x => x.PersonId,
                    principalTable: "People",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "Assignments",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                TeamMemberId = table.Column<int>(type: "int", nullable: false),
                CaseId = table.Column<int>(type: "int", nullable: false),
                StatusId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Assignments", x => x.Id);
                table.ForeignKey(
                    name: "FK_Assignments_Cases",
                    column: x => x.CaseId,
                    principalTable: "Cases",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_Assignments_Status",
                    column: x => x.StatusId,
                    principalTable: "Status",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_Assignments_TeamMembers",
                    column: x => x.TeamMemberId,
                    principalTable: "TeamMembers",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "ProcessDocumentTypes",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                ProcessId = table.Column<int>(type: "int", nullable: false),
                DocumentTypeId = table.Column<int>(type: "int", nullable: false),
                Mandatory = table.Column<bool>(type: "bit", nullable: false),
                DocOrder = table.Column<int>(type: "int", nullable: false),
                ProcessTemplateId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ProcessDocumentTypes", x => x.Id);
                table.ForeignKey(
                    name: "FK_ProcessDocumentTypes_DocumentTypes",
                    column: x => x.DocumentTypeId,
                    principalTable: "DocumentTypes",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_ProcessDocumentTypes_ProcessTemplates",
                    column: x => x.ProcessTemplateId,
                    principalTable: "ProcessTemplates",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_ProcessDocumentTypes_Processes",
                    column: x => x.ProcessId,
                    principalTable: "Processes",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "CaseProcessDocuments",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                DocumentTypeId = table.Column<int>(type: "int", nullable: false),
                ProcessDocumentTypeId = table.Column<int>(type: "int", nullable: false),
                CaseId = table.Column<int>(type: "int", nullable: false),
                FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_CaseProcessDocuments", x => x.Id);
                table.ForeignKey(
                    name: "FK_CaseProcessDocuments_Cases",
                    column: x => x.CaseId,
                    principalTable: "Cases",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_CaseProcessDocuments_DocumentTypes",
                    column: x => x.DocumentTypeId,
                    principalTable: "DocumentTypes",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_CaseProcessDocuments_ProcessDocumentTypes",
                    column: x => x.ProcessDocumentTypeId,
                    principalTable: "ProcessDocumentTypes",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "CaseDocumentFieldValues",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                CaseProcessDocumentId = table.Column<int>(type: "int", nullable: false),
                DocumentTypeId = table.Column<int>(type: "int", nullable: false),
                ProcessDocumentTypeId = table.Column<int>(type: "int", nullable: false),
                CaseId = table.Column<int>(type: "int", nullable: false),
                Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                Tag = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                Multiplicity = table.Column<int>(type: "int", nullable: true),
                FieldValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_CaseDocumentFieldValues", x => x.Id);
                table.ForeignKey(
                    name: "FK_CaseDocumentFieldValues_CaseProcessDocuments",
                    column: x => x.CaseProcessDocumentId,
                    principalTable: "CaseProcessDocuments",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_CaseDocumentFieldValues_DocumentTypes",
                    column: x => x.DocumentTypeId,
                    principalTable: "DocumentTypes",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_CaseDocumentFieldValues_ProcessDocumentTypes",
                    column: x => x.ProcessDocumentTypeId,
                    principalTable: "ProcessDocumentTypes",
                    principalColumn: "Id");
            });

        migrationBuilder.InsertData(
            table: "ActorTypes",
            columns: new[] { "Id", "Name" },
            values: new object[,]
            {
                { 1, "Reclamante" },
                { 2, "Defendido" }
            });

        migrationBuilder.InsertData(
            table: "DocumentTypes",
            columns: new[] { "Id", "Name" },
            values: new object[,]
            {
                { 1, "Certificado de Tradición" },
                { 2, "Escritura Pública" },
                { 3, "Pagaré" },
                { 4, "Liquidación de Crédito-F46" },
                { 5, "Endoso" },
                { 6, "Liquidación" },
                { 7, "Certicámara" },
                { 8, "Super Intendencia" },
                { 9, "Representación Legal Alianza" },
                { 10, "Representación Legal Abogado" },
                { 11, "Poder Especial" },
                { 12, "Soporte de Vinculación" },
                { 13, "Cámara Comercio Bancolombia" },
                { 14, "Cámara Comercio Alianza" },
                { 15, "Registro Nacional de Abogados (SIRNA)" },
                { 16, "Certificado de Dependientes" },
                { 17, "Carta de Instrucciones Requerida" },
                { 18, "Carta de Instrucciones Adicional" },
                { 19, "Carta de Demanda" }
            });

        migrationBuilder.InsertData(
            table: "Firms",
            columns: new[] { "Id", "GuidIdentifier", "Name" },
            values: new object[,]
            {
                { 1, new Guid("0babfb2b-19cc-4538-bbaf-77c6f2fbfbb4"), "Gómez Pineda Abogados" },
                { 2, new Guid("bcc332b6-b268-49d5-a6ab-e29964e7a850"), "Alianza" }
            });

        migrationBuilder.InsertData(
            table: "IdentificationTypes",
            columns: new[] { "Id", "Abbreviation", "Name" },
            values: new object[,]
            {
                { 1, "CC", "Cédula de Ciudadanía" },
                { 2, "CE", "Cédula de Extranjería" },
                { 3, "PT", "Pasaporte" },
                { 4, "NIT", "Número de Identificación Tributaria" },
                { 5, "RUT", "Registro Único Tributario" }
            });

        migrationBuilder.InsertData(
            table: "Permissions",
            columns: new[] { "Id", "Description", "Name" },
            values: new object[,]
            {
                { 1, "Administración de Demandas", "Demandas" },
                { 2, "Administración Tutelas", "Tutelas" },
                { 3, "Configuración", "Configuración" }
            });

        migrationBuilder.InsertData(
            table: "PersonTypes",
            columns: new[] { "Id", "Name" },
            values: new object[,]
            {
                { 1, "Persona Natural" },
                { 2, "Persona Jurídica" }
            });

        migrationBuilder.InsertData(
            table: "ProcessTypes",
            columns: new[] { "Id", "Name" },
            values: new object[,]
            {
                { 1, "Demandas" },
                { 2, "Tutelas" }
            });

        migrationBuilder.InsertData(
            table: "Roles",
            columns: new[] { "Id", "Name" },
            values: new object[,]
            {
                { 1, "Administrador" },
                { 2, "Usuario" },
                { 3, "Súper Usuario" }
            });

        migrationBuilder.InsertData(
            table: "Status",
            columns: new[] { "Id", "Name" },
            values: new object[,]
            {
                { 1, "Pendiente" },
                { 2, "Asignado" },
                { 3, "En Proceso" },
                { 4, "Terminado" },
                { 5, "Cerrado" }
            });

        migrationBuilder.InsertData(
            table: "Processes",
            columns: new[] { "Id", "Name", "ProcessTypeId" },
            values: new object[,]
            {
                { 1, "Ejecutivo Singular (Personal)", 1 },
                { 2, "Ejecutivo Hipotecario", 1 },
                { 3, "Pago Directo", 1 },
                { 4, "Abreviado de Restitución", 1 },
                { 5, "Reposición de Títulos", 1 },
                { 6, "Ejecutivo Prendario", 1 },
                { 7, "Ejecutivo Mixto", 1 },
                { 8, "Activas", 2 },
                { 9, "Pasivas", 2 }
            });

        migrationBuilder.InsertData(
            table: "RolePermissions",
            columns: new[] { "Id", "PermissionId", "RoleId" },
            values: new object[,]
            {
                { 1, 1, 1 },
                { 2, 2, 1 },
                { 3, 3, 1 },
                { 4, 1, 2 },
                { 5, 2, 2 },
                { 6, 3, 3 }
            });

        migrationBuilder.InsertData(
            table: "Users",
            columns: new[] { "Id", "Email", "FirmId", "FirstName", "HashedPassword", "IsActive", "LastName", "RegistrationDate", "RoleId", "Salt" },
            values: new object[,]
            {
                { 1, "Papirus.Administrator@email.com", 1, "Papirus", "tXJlkMcMxJhHvZ2RK6SQShIrBzWAJjPwkFHQLz23GTY=", true, "Administrador", new DateTime(2024, 2, 21, 17, 28, 46, 446, DateTimeKind.Local).AddTicks(5424), 1, "YzbbWdkVjn3JNFe1l/IJmA==" },
                { 2, "Basic.User@email.com", 1, "Basic", "TOFVyw0h3sWJMLk2s+gAljU0V2iNbgK2xBPWBX2gPsw=", true, "User", new DateTime(2024, 2, 21, 17, 28, 46, 446, DateTimeKind.Local).AddTicks(5442), 2, "JMHuzQKqE5CXuOTpPkqjDw==" },
                { 3, "Super.User@email.com", 1, "Super", "pZTwjSj8Iz7tLE/nKcW8v6Fl89YLPOMiLbQ4KmVNlLk=", true, "User", new DateTime(2024, 2, 21, 17, 28, 46, 446, DateTimeKind.Local).AddTicks(5444), 3, "/x2XaXyVuu6cAKTsIQJBgQ==" }
            });

        migrationBuilder.InsertData(
            table: "SubProcesses",
            columns: new[] { "Id", "Abbreviation", "Description", "ProcessId" },
            values: new object[,]
            {
                { 1, "PSAD-MD", "Pagaré sin abonos, 1 demandado con medida Deceval", 1 },
                { 2, "PCAD-MP", "Pagaré con abonos, 1 demandado y medidas previas", 1 },
                { 3, "PSAPCAD-MD", "Pagaré sin abono, 1 pagaré con abono, 1 demandado y con medida Deceval", 1 },
                { 4, "2PSAVD-MP", "Dos pagares sin abonos, varios demandados con medida previa", 1 },
                { 5, "VPAD-MD", "Varios pagares, 1 demandado con medida Deceval", 1 },
                { 6, null, "Derecho de Petición", 9 },
                { 7, null, "Salud", 9 },
                { 8, null, "Habeas Data", 9 }
            });

        migrationBuilder.CreateIndex(
            name: "IX_Actors_ActorTypeId",
            table: "Actors",
            column: "ActorTypeId");

        migrationBuilder.CreateIndex(
            name: "IX_Actors_CaseId",
            table: "Actors",
            column: "CaseId");

        migrationBuilder.CreateIndex(
            name: "IX_Actors_PersonId",
            table: "Actors",
            column: "PersonId");

        migrationBuilder.CreateIndex(
            name: "IX_Assignments_CaseId",
            table: "Assignments",
            column: "CaseId",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Assignments_StatusId",
            table: "Assignments",
            column: "StatusId");

        migrationBuilder.CreateIndex(
            name: "IX_Assignments_TeamMemberId",
            table: "Assignments",
            column: "TeamMemberId");

        migrationBuilder.CreateIndex(
            name: "IX_CaseDocumentFieldValues_CaseProcessDocumentId",
            table: "CaseDocumentFieldValues",
            column: "CaseProcessDocumentId");

        migrationBuilder.CreateIndex(
            name: "IX_CaseDocumentFieldValues_DocumentTypeId",
            table: "CaseDocumentFieldValues",
            column: "DocumentTypeId");

        migrationBuilder.CreateIndex(
            name: "IX_CaseDocumentFieldValues_ProcessDocumentTypeId",
            table: "CaseDocumentFieldValues",
            column: "ProcessDocumentTypeId");

        migrationBuilder.CreateIndex(
            name: "IX_CaseProcessDocuments_CaseId",
            table: "CaseProcessDocuments",
            column: "CaseId");

        migrationBuilder.CreateIndex(
            name: "IX_CaseProcessDocuments_DocumentTypeId",
            table: "CaseProcessDocuments",
            column: "DocumentTypeId");

        migrationBuilder.CreateIndex(
            name: "IX_CaseProcessDocuments_ProcessDocumentTypeId",
            table: "CaseProcessDocuments",
            column: "ProcessDocumentTypeId");

        migrationBuilder.CreateIndex(
            name: "IX_Cases_ProcessId",
            table: "Cases",
            column: "ProcessId");

        migrationBuilder.CreateIndex(
            name: "IX_Cases_ProcessTypeId",
            table: "Cases",
            column: "ProcessTypeId");

        migrationBuilder.CreateIndex(
            name: "IX_Cases_SubProcessId",
            table: "Cases",
            column: "SubProcessId");

        migrationBuilder.CreateIndex(
            name: "UQ_Cases_GuidIdentifier",
            table: "Cases",
            column: "GuidIdentifier",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "UQ_Firms_GuidIdentifier",
            table: "Firms",
            column: "GuidIdentifier",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_People_IdentificationTypeId",
            table: "People",
            column: "IdentificationTypeId");

        migrationBuilder.CreateIndex(
            name: "IX_People_PersonTypeId",
            table: "People",
            column: "PersonTypeId");

        migrationBuilder.CreateIndex(
            name: "UQ_People_Email",
            table: "People",
            column: "Email",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "UQ_People_GuidIdentifier",
            table: "People",
            column: "GuidIdentifier",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_ProcessDocumentTypes_DocumentTypeId",
            table: "ProcessDocumentTypes",
            column: "DocumentTypeId");

        migrationBuilder.CreateIndex(
            name: "IX_ProcessDocumentTypes_ProcessId",
            table: "ProcessDocumentTypes",
            column: "ProcessId");

        migrationBuilder.CreateIndex(
            name: "IX_ProcessDocumentTypes_ProcessTemplateId",
            table: "ProcessDocumentTypes",
            column: "ProcessTemplateId");

        migrationBuilder.CreateIndex(
            name: "IX_Processes_ProcessTypeId",
            table: "Processes",
            column: "ProcessTypeId");

        migrationBuilder.CreateIndex(
            name: "IX_ProcessTemplates_FirmId",
            table: "ProcessTemplates",
            column: "FirmId");

        migrationBuilder.CreateIndex(
            name: "IX_ProcessTemplates_ProcessId",
            table: "ProcessTemplates",
            column: "ProcessId");

        migrationBuilder.CreateIndex(
            name: "IX_ProcessTemplates_ProcessTypeId",
            table: "ProcessTemplates",
            column: "ProcessTypeId");

        migrationBuilder.CreateIndex(
            name: "IX_ProcessTemplates_SubProcessId",
            table: "ProcessTemplates",
            column: "SubProcessId");

        migrationBuilder.CreateIndex(
            name: "IX_RolePermissions_PermissionId",
            table: "RolePermissions",
            column: "PermissionId");

        migrationBuilder.CreateIndex(
            name: "IX_RolePermissions_RoleId",
            table: "RolePermissions",
            column: "RoleId");

        migrationBuilder.CreateIndex(
            name: "IX_SubProcesses_ProcessId",
            table: "SubProcesses",
            column: "ProcessId");

        migrationBuilder.CreateIndex(
            name: "IX_TeamMembers_MemberId",
            table: "TeamMembers",
            column: "MemberId");

        migrationBuilder.CreateIndex(
            name: "IX_TeamMembers_TeamId",
            table: "TeamMembers",
            column: "TeamId");

        migrationBuilder.CreateIndex(
            name: "IX_Users_FirmId",
            table: "Users",
            column: "FirmId");

        migrationBuilder.CreateIndex(
            name: "IX_Users_RoleId",
            table: "Users",
            column: "RoleId");

        migrationBuilder.CreateIndex(
            name: "UQ_Users_Email",
            table: "Users",
            column: "Email",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Actors");

        migrationBuilder.DropTable(
            name: "Assignments");

        migrationBuilder.DropTable(
            name: "CaseDocumentFieldValues");

        migrationBuilder.DropTable(
            name: "RolePermissions");

        migrationBuilder.DropTable(
            name: "ActorTypes");

        migrationBuilder.DropTable(
            name: "People");

        migrationBuilder.DropTable(
            name: "Status");

        migrationBuilder.DropTable(
            name: "TeamMembers");

        migrationBuilder.DropTable(
            name: "CaseProcessDocuments");

        migrationBuilder.DropTable(
            name: "Permissions");

        migrationBuilder.DropTable(
            name: "IdentificationTypes");

        migrationBuilder.DropTable(
            name: "PersonTypes");

        migrationBuilder.DropTable(
            name: "Teams");

        migrationBuilder.DropTable(
            name: "Users");

        migrationBuilder.DropTable(
            name: "Cases");

        migrationBuilder.DropTable(
            name: "ProcessDocumentTypes");

        migrationBuilder.DropTable(
            name: "Roles");

        migrationBuilder.DropTable(
            name: "DocumentTypes");

        migrationBuilder.DropTable(
            name: "ProcessTemplates");

        migrationBuilder.DropTable(
            name: "Firms");

        migrationBuilder.DropTable(
            name: "SubProcesses");

        migrationBuilder.DropTable(
            name: "Processes");

        migrationBuilder.DropTable(
            name: "ProcessTypes");
    }
}