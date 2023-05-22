namespace EjercicioPOO.Application.Dto
{
    public class UsuarioV
    {
        public UsuarioV(int id, string usuario) 
        {
            this.Id = id;
            this.Usuario = usuario;
        }
        public int Id { get; set; }
        public string Usuario { get; set; }
    }
}
