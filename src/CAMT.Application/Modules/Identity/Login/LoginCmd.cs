namespace CAMT.Application.Modules.Identity.Login;

public record LoginCmd : IRequest<ErrorOr<LoginResponse>>
{
    public string Username { get; set; }
    public string Password { get; set; }
}
