using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Papirus.WebApi.Application.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;

    private readonly IConfiguration _configuration;

    public AuthenticationService(
        IUserRepository userRepository,
        IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public string GenerateJwtToken(User user)
    {
        if (user.FirmId <= 0)
        {
            throw new InvalidOperationException("FirmId is required but missing for the user.");
        }

        var secretKey = _configuration["JwtBearer:SecretKey"];
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        var header = new JwtHeader(signingCredentials);

        var claims = new List<Claim>
        {
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new (JwtRegisteredClaimNames.NameId, user.Id.ToString()),
            new (JwtRegisteredClaimNames.Email, user.Email),
            new (ClaimTypes.Name, user.Email),
            new ("name", $"{user.FirstName} {user.LastName}"),
            new ("firmId", user.FirmId.ToString()),
            new ("roleId", user.RoleId.ToString())
        };

        var payload = new JwtPayload(
            issuer: _configuration["JwtBearer:Issuer"],
            audience: _configuration["JwtBearer:Audience"],
            claims: claims,
            notBefore: DateTime.Now,
            expires: DateTime.UtcNow.AddHours(24));

        var token = new JwtSecurityToken(header, payload);
        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

        return jwtSecurityTokenHandler.WriteToken(token);
    }

    public async Task<string> Login(string email, string password)
    {
        var user = await _userRepository.FindByEmailAsync(email);
        var token = string.Empty;
        if (user != null && VerifyPassword(user, password))
        {
            return GenerateJwtToken(user);
        }

        return token;
    }

    public async Task<User> Register(User user, string password, int firmId)
    {
        var userExists = await _userRepository.FindByEmailAsync(user.Email);
        if (userExists != null)
        {
            throw new InternalServerErrorException("A user with the given email already exists.");
        }

        string salt = GenerateSalt();
        string hashedPassword = GenerateHashedPassword(password, salt);

        var newUser = new User
        {
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            HashedPassword = hashedPassword,
            Salt = salt,
            RegistrationDate = DateTime.UtcNow,
            IsActive = true,
            MustChangePassword = true,
            RoleId = user.RoleId,
            FirmId = firmId
        };

        var createdUser = await _userRepository.AddAsync(newUser);

        return createdUser;
    }

    private static bool VerifyPassword(User user, string password)
    {
        return user.HashedPassword.Equals(GenerateHashedPassword(password, user.Salt));
    }

    public static string GenerateHashedPassword(string password, string salt)
    {
        var saltByteArray = Convert.FromBase64String(salt);

        string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: saltByteArray,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));

        return hashedPassword;
    }

    public static string GenerateSalt()
    {
        var salt = RandomNumberGenerator.GetBytes(16);
        return Convert.ToBase64String(salt);
    }
}