using HRprojekat.DTO;
using HRprojekat.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HRprojekat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService _candidateService;

        public CandidateController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var candidates = await _candidateService.GetAllCandidatesAsync();
            return Ok(candidates);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var candidate = await _candidateService.GetCandidateByIdAsync(id);
            if (candidate == null)
                return NotFound($"Candidate with ID {id} not found.");
            return Ok(candidate);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CandidateCreateDto dto)
        {
            var created = await _candidateService.AddCandidateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}/skills")]
        public async Task<IActionResult> UpdateSkills(int id, [FromBody] List<int> skillIds)
        {
            var updated = await _candidateService.UpdateCandidateSkillsAsync(id, skillIds);
            if (updated == null)
                return NotFound($"Candidate with ID {id} not found.");
            return Ok(updated);
        }

        [HttpDelete("{id}/skills/{skillId}")]
        public async Task<IActionResult> RemoveSkill(int id, int skillId)
        {
            var updated = await _candidateService.RemoveSkillFromCandidateAsync(id, skillId);
            if (updated == null)
                return NotFound($"Candidate with ID {id} or skill {skillId} not found.");
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _candidateService.DeleteCandidateAsync(id);
            if (!deleted)
                return NotFound($"Candidate with ID {id} not found.");
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchCandidates(
            string? name,
            [FromQuery] List<int>? skillIds,
            bool hasAll = false,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var result = await _candidateService.SearchCandidatesAsync(name, skillIds, hasAll, pageNumber, pageSize);
            return Ok(result);
        }
    }
}
