namespace BibliotecaWebAPI.Model
{
    public class ReservaDto
    {
        public DateOnly DataReserva { get; set; }

        public int FkMembro { get; set; }

        public int FkLivro { get; set; }
    }
}
