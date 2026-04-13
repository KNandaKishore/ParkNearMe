namespace SmartParking.Domain.Entities;
public class ChatbotLog
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string UserMessage { get; set; } = string.Empty;

    public string BotResponse { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public User User { get; set; } = null!;
}