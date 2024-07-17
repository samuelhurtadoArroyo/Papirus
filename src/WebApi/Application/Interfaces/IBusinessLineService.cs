namespace Papirus.WebApi.Application.Interfaces;

public interface IBusinessLineService
{
    public Task<IEnumerable<BusinessLineDto>> GetAllAsync();
}