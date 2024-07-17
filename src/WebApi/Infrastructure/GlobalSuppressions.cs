// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

[assembly: SuppressMessage("Style", "IDE0290:Use primary constructor")]
[assembly: SuppressMessage("Performance", "CA1862:Use the 'StringComparison' method overloads to perform case-insensitive string comparisons")]
[assembly: SuppressMessage("Roslynator", "RCS1155:Use StringComparison when comparing strings")]
[assembly: SuppressMessage("Major Code Smell", "S3358:Ternary operators should not be nested")]
[assembly: SuppressMessage("Major Code Smell", "S2139:Exceptions should be either logged or rethrown but not both", Justification = "<Pending>", Scope = "member", Target = "~M:Papirus.WebApi.Infrastructure.Services.DataManagerService.UploadFileAsync(Papirus.WebApi.Application.Common.Models.FileAttachment)~System.Threading.Tasks.Task{System.String}")]
[assembly: SuppressMessage("Major Code Smell", "S2139:Exceptions should be either logged or rethrown but not both", Justification = "<Pending>", Scope = "member", Target = "~M:Papirus.WebApi.Infrastructure.Services.HttpService.SendGetRequestAsync``1(System.String)~System.Threading.Tasks.Task{``0}")]
[assembly: SuppressMessage("Major Code Smell", "S2139:Exceptions should be either logged or rethrown but not both", Justification = "<Pending>", Scope = "member", Target = "~M:Papirus.WebApi.Infrastructure.Services.HttpService.SendPostRequestAsync``2(System.String,``0,Papirus.WebApi.Application.Common.Models.FileAttachment,System.String)~System.Threading.Tasks.Task{``1}")]