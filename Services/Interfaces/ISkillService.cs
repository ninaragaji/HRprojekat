using HRprojekat.DTO;

namespace HRprojekat.Services.Interfaces
{
    public interface ISkillService
    {
        Task<SkillReadDto> AddSkillAsync(SkillCreateDto dto);
        Task<List<SkillReadDto>> GetAllSkillsAsync();
        Task<bool> DeleteSkillAsync(int id);
    }
}
