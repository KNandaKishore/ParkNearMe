using MediatR;

public class RegisterCommand : IRequest<Guid>
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string FullName { get; set; }
}