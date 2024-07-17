namespace Papirus.WebApi.Infrastructure.Data.Configurations;

[ExcludeFromCodeCoverage]
public class DocumentTypeConfiguration : IEntityTypeConfiguration<DocumentType>
{
    public void Configure(EntityTypeBuilder<DocumentType> builder)
    {
        builder.Property(e => e.Name).HasMaxLength(50);

        builder.HasData(
            new DocumentType { Id = 1, Name = "Certificado de Tradición" },
            new DocumentType { Id = 2, Name = "Escritura Pública" },
            new DocumentType { Id = 3, Name = "Pagaré" },
            new DocumentType { Id = 4, Name = "Liquidación de Crédito-F46" },
            new DocumentType { Id = 5, Name = "Endoso" },
            new DocumentType { Id = 6, Name = "Liquidación" },
            new DocumentType { Id = 7, Name = "Certicámara" },
            new DocumentType { Id = 8, Name = "Super Intendencia" },
            new DocumentType { Id = 9, Name = "Representación Legal Alianza" },
            new DocumentType { Id = 10, Name = "Representación Legal Abogado" },
            new DocumentType { Id = 11, Name = "Poder Especial" },
            new DocumentType { Id = 12, Name = "Soporte de Vinculación" },
            new DocumentType { Id = 13, Name = "Cámara Comercio Bancolombia" },
            new DocumentType { Id = 14, Name = "Cámara Comercio Alianza" },
            new DocumentType { Id = 15, Name = "Registro Nacional de Abogados (SIRNA)" },
            new DocumentType { Id = 16, Name = "Certificado de Dependientes" },
            new DocumentType { Id = 17, Name = "Carta de Instrucciones Requerida" },
            new DocumentType { Id = 18, Name = "Carta de Instrucciones Adicional" },
            new DocumentType { Id = 19, Name = "Carta de Demanda" },
            new DocumentType { Id = 20, Name = "AUTOADMITE" },
            new DocumentType { Id = 21, Name = "Correo electrónico" },
            new DocumentType { Id = 22, Name = "Contestación Tutela" },
            new DocumentType { Id = 23, Name = "Escrito de emergencia" }
        );
    }
}