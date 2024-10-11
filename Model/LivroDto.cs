namespace BibliotecaWebAPI.Model
{
    public class LivroDto
    {

        public string Titulo { get; set; }

        public string Autor { get; set; }

        public int AnoPublicacao { get; set; }

        public int FkCategoria { get; set; }

        public bool Disponibilidade { get; set; } 
    }
}
