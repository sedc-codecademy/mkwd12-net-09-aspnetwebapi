using Qinshift.NotesAppRefactored.Dtos.FruitDtos;

namespace Qinshift.NotesAppRefactored.Services.Interfaces
{
    public interface IFruitService
    {
        Task<FruitDto> GetFruitInfoAsync(string fruitName);
        Task<List<FruitDto>> GetFruitsByOrderAsync(string orderName);
        Task<List<FruitDto>> GetFruitsByGenusAsync(string genusName);
        Task<List<FruitDto>> GetFruitByFamilyAsync(string familyName);
        Task<List<FruitDto>> GetAllFruitsAsync();
    }
}
