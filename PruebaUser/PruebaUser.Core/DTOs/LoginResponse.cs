namespace PruebaUser.Core.DTOs
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public bool Resultado { get; set; }
        public string Mensaje { get; set; }
        public LoginResponse(string token, bool resultado, string mensaje)
        {
            Token = token;
            Resultado = resultado;
            Mensaje = mensaje;
        }

      

    }
}
