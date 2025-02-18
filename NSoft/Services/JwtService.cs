namespace NSoft.Services
{
    public interface IJwtService
    {
        string GenerarToken(int usuarioId, string correo, List <string> roels);
    }
    public class JwtService : IJwtService
    {
        public string GenerarToken(int usuarioId, string correo, List<string> roels)
        {
            throw new NotImplementedException();
        }
    }
}
