namespace CAMT.Application.Modules.Identity.Login;

public class LoginCmdValidator : AbstractValidator<LoginCmd>
{
    public LoginCmdValidator()
    {
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}