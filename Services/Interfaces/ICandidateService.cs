using HRprojekat.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRprojekat.Services.Interfaces
{
    public interface ICandidateService
    {
        Task<CandidateReadDto> AddCandidateAsync(CandidateCreateDto dto);
        Task<List<CandidateReadDto>> GetAllCandidatesAsync();
        Task<CandidateReadDto?> GetCandidateByIdAsync(int id);
        Task<bool> DeleteCandidateAsync(int id);

        Task<CandidateReadDto?> UpdateCandidateSkillsAsync(int candidateId, List<int> skillIds);
        Task<CandidateReadDto?> RemoveSkillFromCandidateAsync(int candidateId, int skillId);

        Task<PagedResult<CandidateReadDto>> SearchCandidatesAsync(
            string? name,
            List<int>? skillIds,
            bool hasAll,
            int pageNumber,
            int pageSize);
    }
}
