using AutoMapper;
using HRprojekat.Data;
using HRprojekat.DTO;
using HRprojekat.Models;
using HRprojekat.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HRprojekat.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CandidateService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CandidateReadDto> AddCandidateAsync(CandidateCreateDto dto)
        {
            var candidate = _mapper.Map<Candidate>(dto);

            if (dto.SkillIds != null && dto.SkillIds.Any())
            {
                var skills = await _context.Skills
                    .Where(s => dto.SkillIds.Contains(s.Id))
                    .ToListAsync();

                candidate.Skills = skills;
            }

            _context.Candidates.Add(candidate);
            await _context.SaveChangesAsync();

            var savedCandidate = await _context.Candidates
                .Include(c => c.Skills)
                .FirstOrDefaultAsync(c => c.Id == candidate.Id);

            return _mapper.Map<CandidateReadDto>(savedCandidate);
        }


        public async Task<List<CandidateReadDto>> GetAllCandidatesAsync()
        {
            var candidates = await _context.Candidates
                .Include(c => c.Skills)
                .ToListAsync();

            return _mapper.Map<List<CandidateReadDto>>(candidates);
        }

        public async Task<CandidateReadDto?> GetCandidateByIdAsync(int id)
        {
            var candidate = await _context.Candidates
                .Include(c => c.Skills)
                .FirstOrDefaultAsync(c => c.Id == id);

            return candidate == null ? null : _mapper.Map<CandidateReadDto>(candidate);
        }

        public async Task<bool> DeleteCandidateAsync(int id)
        {
            var candidate = await _context.Candidates.FindAsync(id);
            if (candidate == null) return false;

            _context.Candidates.Remove(candidate);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<CandidateReadDto?> UpdateCandidateSkillsAsync(int candidateId, List<int> skillIds)
        {
            var candidate = await _context.Candidates
                .Include(c => c.Skills)
                .FirstOrDefaultAsync(c => c.Id == candidateId);

            if (candidate == null) return null;

            var skills = await _context.Skills
                .Where(s => skillIds.Contains(s.Id))
                .ToListAsync();

            candidate.Skills = skills;
            await _context.SaveChangesAsync();

            return _mapper.Map<CandidateReadDto>(candidate);
        }

        public async Task<CandidateReadDto?> RemoveSkillFromCandidateAsync(int candidateId, int skillId)
        {
            var candidate = await _context.Candidates
                .Include(c => c.Skills)
                .FirstOrDefaultAsync(c => c.Id == candidateId);

            if (candidate == null) return null;

            var skill = candidate.Skills.FirstOrDefault(s => s.Id == skillId);
            if (skill != null)
            {
                candidate.Skills.Remove(skill);
                await _context.SaveChangesAsync();
            }

            return _mapper.Map<CandidateReadDto>(candidate);
        }

        public async Task<PagedResult<CandidateReadDto>> SearchCandidatesAsync(
            string? name, List<int>? skillIds, bool hasAll, int pageNumber, int pageSize)
        {
            var query = _context.Candidates
                .Include(c => c.Skills)
                .AsQueryable();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(c => c.FullName.Contains(name));

            if (skillIds != null && skillIds.Any())
            {
                if (hasAll)
                {
                    query = query.Where(c => skillIds.All(id => c.Skills.Any(s => s.Id == id)));
                }
                else
                {
                    query = query.Where(c => c.Skills.Any(s => skillIds.Contains(s.Id)));
                }
            }

            var totalCount = await query.CountAsync();

            var candidates = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var result = new PagedResult<CandidateReadDto>
            {
                Items = _mapper.Map<List<CandidateReadDto>>(candidates),
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return result;
        }
    }
}
