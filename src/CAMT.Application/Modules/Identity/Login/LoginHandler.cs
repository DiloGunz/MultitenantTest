using CAMT.Domain.Config;
using CAMT.Domain.Entities.Company;
using CAMT.Domain.Entities.Company.Interfaces;
using CAMT.Domain.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CAMT.Application.Modules.Identity.Login;

public class LoginHandler : IRequestHandler<LoginCmd, ErrorOr<LoginResponse>>
{
    private readonly JwtConfig _jwtConfig;
    private readonly UserManager<AppUser> _userManager;
    private readonly IAppUserRepository _appUserRepository;
    private readonly IOrganizationRepository _organizationRepository;
    private readonly ILogger<LoginHandler> _logger;

    public LoginHandler(
        UserManager<AppUser> userManager,
        IAppUserRepository appUserRepository,
        IOptions<JwtConfig> jwtConfig,
        IOrganizationRepository organizationRepository,
        ILogger<LoginHandler> logger)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _appUserRepository = appUserRepository ?? throw new ArgumentNullException(nameof(appUserRepository));
        _jwtConfig = jwtConfig.Value ?? throw new ArgumentNullException(nameof(jwtConfig));
        _organizationRepository = organizationRepository ?? throw new ArgumentNullException(nameof(organizationRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<ErrorOr<LoginResponse>> Handle(LoginCmd request, CancellationToken cancellationToken)
    {
        try
        {
            var userDb = await _appUserRepository.GetByUsernameAsync(request.Username);
            if (userDb == null)
            {
                return Error.NotFound(description: "User not found");
            }

            var identityUser = await _userManager.FindByIdAsync(userDb.Id.ToString());

            if (identityUser is null)
            {
                return Error.NotFound(description: "User not found");
            }

            var comparePass = await _userManager.CheckPasswordAsync(identityUser, request.Password);

            if (!comparePass)
            {
                return Error.Unauthorized(description: "Incorrect Password");
            }

            var organization = await _organizationRepository.GetByIdAsync(userDb.OrganizationId);

            string? tenantIdentifierByUser = null;

            if (organization != null)
            {
                tenantIdentifierByUser = organization.TenantIdentifier;
            }

            var response = new LoginResponse()
            {
                AccessToken = GenerateToken(identityUser, tenantIdentifierByUser)
            };

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Error.Unexpected();
        }
    }


    private string GenerateToken(AppUser user, string? tenantIdentifier)
    {
        var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

        var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email ?? ""),
                new Claim(Constants.TenantIdentifierClaim, tenantIdentifier ?? ""),
            };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_jwtConfig.ExpirationMinutes),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
            )
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var createdToken = tokenHandler.CreateToken(tokenDescriptor);

        var token = tokenHandler.WriteToken(createdToken);

        return token;
    }
}