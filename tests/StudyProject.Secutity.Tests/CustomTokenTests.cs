using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FluentAssertions;
using StudyProject.Secutity;
using Xunit;

namespace StudyProject.Secutity.Tests;

public class CustomTokenTests
{
    private const string Key = "my-super-secret-signing-key-12345!!";
    private const string Issuer = "Issuer";
    private const string Audience = "Audience";

    [Fact]
    public void Should_Generate_Authenticated_Token()
    {
        var token = CustomToken.GenerateToken("juliano", Key, "1", Issuer, Audience, new List<Claim>());

        token.Authenticated.Should().BeTrue();
        token.Token.Should().NotBeNullOrWhiteSpace();
        token.Message.Should().Be("Token JWT OK");
    }

    [Fact]
    public void Should_Set_Expiration_In_The_Future()
    {
        var token = CustomToken.GenerateToken("juliano", Key, "1", Issuer, Audience, new List<Claim>());

        token.Expiration.Should().BeAfter(DateTime.UtcNow);
        token.Expiration.Should().BeWithin(TimeSpan.FromHours(2)).After(DateTime.UtcNow);
    }

    [Fact]
    public void Token_Should_Contain_UniqueName_Claim()
    {
        var generated = CustomToken.GenerateToken("juliano", Key, "1", Issuer, Audience, new List<Claim>());

        var jwt = new JwtSecurityTokenHandler().ReadJwtToken(generated.Token);

        jwt.Claims.Should().Contain(c => c.Type == JwtRegisteredClaimNames.UniqueName && c.Value == "juliano");
    }

    [Fact]
    public void Token_Should_Contain_Additional_UserClaims()
    {
        var role = new Claim(ClaimTypes.Role, "Admin");

        var generated = CustomToken.GenerateToken("juliano", Key, "1", Issuer, Audience, new List<Claim> { role });

        var jwt = new JwtSecurityTokenHandler().ReadJwtToken(generated.Token);

        jwt.Claims.Should().Contain(c => c.Type == ClaimTypes.Role && c.Value == "Admin");
    }
}