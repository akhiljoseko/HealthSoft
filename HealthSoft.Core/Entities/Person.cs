namespace HealthSoft.Core.Entities
{
    public abstract class Person : BaseEntity
    {
        public required string FirstName { get; set; }
        public string? LastName { get; set; }
        public required string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public required string ContactNumber { get; set; }
        public required string Email { get; set; }
    }

}
