using BibliotecaWebAPI.ORM;

namespace BibliotecaWebAPI.Model
{
    public class Emprestimo
    {
        public int Id { get; set; }

        public DateOnly DataEmprestimo { get; set; }

        public DateOnly DataDevolucao { get; set; }

        public int FkMembro { get; set; }

        public int FkLivro { get; set; }

    }
}
