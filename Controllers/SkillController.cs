using HRprojekat.DTO;
using HRprojekat.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HRprojekat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkillController : ControllerBase
    {
        private readonly ISkillService _skillService;

        public SkillController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        [HttpPost]
        public async Task<ActionResult<SkillReadDto>> AddSkill([FromBody] SkillCreateDto dto)
        {
            try
            {
                var result = await _skillService.AddSkillAsync(dto);
                return CreatedAtAction(nameof(GetAllSkills), new { id = result.Id }, result);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<SkillReadDto>>> GetAllSkills()
        {
            var skills = await _skillService.GetAllSkillsAsync();
            return Ok(skills);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSkill(int id)
        {
            var deleted = await _skillService.DeleteSkillAsync(id);
            if (!deleted) return NotFound(new { message = "Skill not found." });
            return NoContent();
        }
    }
}
