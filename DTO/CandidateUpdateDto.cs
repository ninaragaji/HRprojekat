namespace HRprojekat.DTO
{
    public class CandidateUpdateDto
    {
        public string? FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? ContactNumber { get; set; }
        public string? Email { get; set; }
        public List<int>? SkillIds { get; set; }
    }
}
