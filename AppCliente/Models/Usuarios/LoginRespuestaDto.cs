namespace AppCliente.Models.Usuarios
{
    public class LoginRespuestaDto
    {
        public string Token { get; set; }
        public UserDto User { get; set; }


        public class UserDto
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public string Correo { get; set; }
        }
    }

}

