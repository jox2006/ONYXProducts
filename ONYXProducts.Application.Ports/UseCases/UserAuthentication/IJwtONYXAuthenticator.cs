namespace ONYXProducts.Application.UseCases.UserAuthentication
{
    public interface IJwtONYXAuthenticator
    {
        Task<string> AuthenticateAsync(string user, string password);
    }
}
