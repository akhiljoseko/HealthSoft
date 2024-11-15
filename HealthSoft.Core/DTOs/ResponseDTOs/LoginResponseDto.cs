namespace HealthSoft.Core.DTOs.ResponseDTOs
{
    public class LoginResponseDto
    {
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
        public string? Email { get; set; }
        public string? AppUserId { get; set; }
        public string? Token { get; set; }
    }
}
