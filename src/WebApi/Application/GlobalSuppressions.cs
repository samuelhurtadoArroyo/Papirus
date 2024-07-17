// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

[assembly: SuppressMessage("Style", "IDE0290:Use primary constructor")]
[assembly: SuppressMessage("Info Code Smell", "S1133:Deprecated code should be removed", Justification = "The Create method is deprecated. Please use the Register method in AuthenticationService instead.", Scope = "member", Target = "~M:Papirus.WebApi.Application.Services.UserService.Create(Papirus.WebApi.Domain.Entities.User)~System.Threading.Tasks.Task{Papirus.WebApi.Domain.Entities.User}")]