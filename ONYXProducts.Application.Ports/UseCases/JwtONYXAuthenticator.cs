using Microsoft.IdentityModel.Tokens;
using ONYXProducts.Application.UseCases.UserAuthentication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ONYXProducts.Application.UseCases
{
    public class JwtONYXAuthenticator : IJwtONYXAuthenticator
    {
        private readonly IDictionary<string, string> users = new Dictionary<string, string>() {
            { "UserName1","password1"},
            { "UserName2","password2"},
            { "UserName3","password3"}
        };
        private readonly string _encryptionKey;
        public JwtONYXAuthenticator(string encryptionKey)
        {
            _encryptionKey = encryptionKey;
        }
        public async Task<string> AuthenticateAsync(string username, string password)
        {
            //in a real scenario this should read from the database or active directory where the users repository lives.
            if (!users.Any(u => u.Key == username && u.Value == password))
            {
                return await Task.FromResult(string.Empty);
            }

            var validToken = GetNewToken(username);
            return await Task.FromResult(validToken);

        }

        private string GetNewToken(string username)
        {
            var tokendHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_encryptionKey);

            //Define the token:
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                                                { new Claim(ClaimTypes.Name, username)}
                                             ),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
                                                                new SymmetricSecurityKey(tokenKey),
                                                                SecurityAlgorithms.HmacSha256Signature
                                                                )
            };

            var token = tokendHandler.CreateToken(tokenDescriptor);
            return tokendHandler.WriteToken(token);
        }
    }
}
