using AutoMapper;
using HRprojekat.Data;
using HRprojekat.DTO;
using HRprojekat.Models;
using HRprojekat.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HRprojekat.Services
{
    public class SkillService : ISkillService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public SkillService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SkillReadDto> AddSkillAsync(SkillCreateDto dto)
        {
            var existingSkill = await _context.Skills
                .FirstOrDefaultAsync(s => s.Name.ToLower() == dto.Name.ToLower());

            if (existingSkill != null)
                throw new InvalidOperationException($"Skill '{dto.Name}' already exists.");

            
            var skill = _mapper.Map<Skill>(dto);

            _context.Skills.Add(skill);
            await _context.SaveChangesAsync();

            return _mapper.Map<SkillReadDto>(skill);
        }

        public async Task<List<SkillReadDto>> GetAllSkillsAsync()
        {
            var skills = await _context.Skills.ToListAsync();
            return _mapper.Map<List<SkillReadDto>>(skills);
        }

        public async Task<bool> DeleteSkillAsync(int id)
        {
            var skill = await _context.Skills.FindAsync(id);
            if (skill == null)
                return false;

            _context.Skills.Remove(skill);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
