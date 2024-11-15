namespace HealthSoft.Core.DTOs.RequestDTOs
{
    public class AddPatientRequestDto : CreateUserAccountDto
    {
        public string? Address { get; set; }
    }
}
