namespace BibliotecaWebAPI.Model
{
    public class Reserva
    {
        public int Id { get; set; }

        public DateOnly DataReserva { get; set; }

        public int FkMembro { get; set; }

        public int FkLivro { get; set; }
    }
}
