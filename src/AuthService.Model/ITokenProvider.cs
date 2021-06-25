namespace AuthService.Model
{
    public interface ITokenProvider
    {
        string GenerateToken(string login, UserRole role);
    }
}
