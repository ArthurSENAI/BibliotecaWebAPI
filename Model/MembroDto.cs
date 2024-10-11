namespace BibliotecaWebAPI.Model
{
    public class MembroDto
    {
        public string Nome { get; set; }

        public string Email { get; set; }

        public string Telefone { get; set; }

        public DateOnly DataCadastro { get; set; }

        public string TipoMembro { get; set; }
    }
}
