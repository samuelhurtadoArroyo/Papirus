namespace Papirus.WebApi.Infrastructure.Data.Configurations;

[ExcludeFromCodeCoverage]
public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.Property(e => e.Name).HasMaxLength(50);
        builder.Property(e => e.Description).HasMaxLength(300);

        builder.HasData(
            new Permission { Id = 1, Name = "Procesos", Description = "Procesos", PermissionLabelCode = "processes.view" },
            new Permission { Id = 2, Name = "Configuracion", Description = "Configuración - Listar", PermissionLabelCode = "config.view" },
            new Permission { Id = 3, Name = "Configuración", Description = "Configuración - Editar", PermissionLabelCode = "config.edit" },
            new Permission { Id = 4, Name = "Usuarios", Description = "Usuarios - Listar", PermissionLabelCode = "users.view" },
            new Permission { Id = 5, Name = "Usuarios", Description = "Usuarios - Editar", PermissionLabelCode = "users.edit" },
            new Permission { Id = 6, Name = "Usuarios", Description = "Usuarios - Crear", PermissionLabelCode = "users.create" },
            new Permission { Id = 7, Name = "Usuarios", Description = "Usuarios - Buscar", PermissionLabelCode = "users.search" },
            new Permission { Id = 8, Name = "Equipos", Description = "Equipos - Listar", PermissionLabelCode = "teams.view" },
            new Permission { Id = 9, Name = "Equipos", Description = "Equipos - Asignar", PermissionLabelCode = "teams.assign" },
            new Permission { Id = 10, Name = "Equipos", Description = "Equipos - Crear", PermissionLabelCode = "teams.create" },
            new Permission { Id = 11, Name = "Equipos", Description = "Equipos - Editar", PermissionLabelCode = "teams.edit" },
            new Permission { Id = 12, Name = "Equipos", Description = "Equipos - Buscar", PermissionLabelCode = "teams.search" },
            new Permission { Id = 13, Name = "Equipos", Description = "Equipos - Eliminar", PermissionLabelCode = "teams.delete" },
            new Permission { Id = 14, Name = "Tutelas", Description = "Tutelas - Listar", PermissionLabelCode = "guardianships.view" },
            new Permission { Id = 15, Name = "Tutelas", Description = "Tutelas - Asignar", PermissionLabelCode = "guardianships.assign" },
            new Permission { Id = 16, Name = "Tutelas", Description = "Tutelas - Descargar", PermissionLabelCode = "guardianships.download" },
            new Permission { Id = 17, Name = "Tutelas", Description = "Tutelas - Contestar", PermissionLabelCode = "guardianships.answered" },
            new Permission { Id = 18, Name = "Tutelas", Description = "Tutelas - Buscar", PermissionLabelCode = "guardianships.search" },
            new Permission { Id = 19, Name = "Documentos", Description = "Documentos - Listar", PermissionLabelCode = "documents.view" },
            new Permission { Id = 20, Name = "Documentos", Description = "Documentos - Descargar", PermissionLabelCode = "documents.download" },
            new Permission { Id = 21, Name = "Documento", Description = "Documento - Listar", PermissionLabelCode = "document.view" },
            new Permission { Id = 22, Name = "Documento", Description = "Documento - Descargar", PermissionLabelCode = "document.download" },
            new Permission { Id = 23, Name = "Demandas", Description = "Demandas - Listar", PermissionLabelCode = "demands.view" },
            new Permission { Id = 24, Name = "Demandas", Description = "Demandas - Buscar", PermissionLabelCode = "demands.search" },
            new Permission { Id = 25, Name = "Datos Extraidos", Description = "Datos Extraidos - Listar", PermissionLabelCode = "extractedData.view" },
            new Permission { Id = 26, Name = "Datos Extraidos", Description = "Datos Extraidos - Editar", PermissionLabelCode = "extractedData.edit" },
            new Permission { Id = 27, Name = "Datos Extraidos", Description = "Datos Extraidos - Grabar", PermissionLabelCode = "extractedData.save" },
            new Permission { Id = 28, Name = "Generación Documento", Description = "Generación Documento - Listar", PermissionLabelCode = "generateDocument.view" },
            new Permission { Id = 29, Name = "Generación Documento", Description = "Generación Documento - Emergencia", PermissionLabelCode = "generateDocument.emergency" },
            new Permission { Id = 30, Name = "Generación Documento", Description = "Generación Documento - Responder", PermissionLabelCode = "generateDocument.responseDocument" }

        );
    }
}