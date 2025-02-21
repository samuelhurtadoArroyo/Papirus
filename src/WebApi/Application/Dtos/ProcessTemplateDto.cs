﻿namespace Papirus.WebApi.Application.Dtos;

[ExcludeFromCodeCoverage]
public class ProcessTemplateDto
{
    public int FirmId { get; set; }

    public int ProcessTypeId { get; set; }

    public int ProcessId { get; set; }

    public int? SubProcessId { get; set; }

    public string FileName { get; set; } = null!;

    public string FilePath { get; set; } = null!;

    public virtual Firm Firm { get; set; } = null!;

    public virtual Process Process { get; set; } = null!;

    public virtual ICollection<ProcessDocumentType> ProcessDocumentTypes { get; set; } = [];

    public virtual ProcessType ProcessType { get; set; } = null!;
}