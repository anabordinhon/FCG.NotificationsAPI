namespace FCG.Users.Application.Users.Events
{
    public class UserCreatedEvent
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; } 
        public string NickName { get; set; } 
        public string Role { get; set; } 
        public DateTime CreatedAt { get; set; }
        public Guid EventId { get; set; }
        public Guid CorrelationId { get; set; }
    }

}
