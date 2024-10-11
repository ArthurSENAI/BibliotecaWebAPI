namespace BibliotecaWebAPI.Model
{
    public class EmprestimoDto
    {
        public DateOnly DataEmprestimo { get; set; }

        public DateOnly DataDevolucao { get; set; }

        public int FkMembro { get; set; }

        public int FkLivro { get; set; }
    }
}
