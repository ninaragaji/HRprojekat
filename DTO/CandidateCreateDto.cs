namespace HRprojekat.DTO
{
    public class CandidateCreateDto
    {
        public string FullName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string ContactNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public List<int>? SkillIds { get; set; }
    }
}

