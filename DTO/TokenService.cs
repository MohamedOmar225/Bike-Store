using bike_store_2.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace bike_store_2.DTO
{
    //public class TokenService
    //{
    //    private readonly string _key;
    //    private readonly string _issuer;
    //    private readonly string _audience;

    //    public TokenService(IConfiguration configuration)
    //    {
    //        _key = configuration["Jwt:SecrityKey"];
    //        _issuer = configuration["Jwt:Issuer"];
    //        _audience = configuration["Jwt:Audience"];
    //    }

    //    public string GenerateToken(string username)
    //    {
    //        var claims = new[]
    //        {
    //        new Claim(ClaimTypes.Name, username),
    //        // Add more claims here as needed
    //        };

    //        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
    //        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    //        var token = new JwtSecurityToken(
    //            issuer: _issuer,
    //            audience: _audience,
    //            claims: claims,
    //            expires: DateTime.Now.AddHours(3),
    //            signingCredentials: creds
    //        );

    //        return new JwtSecurityTokenHandler().WriteToken(token);
    //    }





        //public string GenerateJwtToken(ApplicationUser user, IList<string> roles)
        //{
        //    var claims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.Name, user.UserName),
        //        new Claim(ClaimTypes.NameIdentifier, user.Id)
        //    };

        //    foreach (var role in roles)
        //    {
        //        claims.Add(new Claim(ClaimTypes.Role, role));
        //    }

        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
        //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //    var token = new JwtSecurityToken(
        //        issuer: _issuer,
        //        audience: _audience,
        //        claims: claims,
        //        expires: DateTime.UtcNow.AddDays(1),
        //        signingCredentials: creds
        //    );

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}








    
}
