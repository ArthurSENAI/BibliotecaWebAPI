using BibliotecaWebAPI.Model;
using BibliotecaWebAPI.Repositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BibliotecaWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReservaController : ControllerBase
    {
        private readonly ReservaRepositorio _reservaRepo;

        public ReservaController(ReservaRepositorio reservaRepo)
        {
            _reservaRepo = reservaRepo;
        }

        // GET: api/<ReservaController>
        [HttpGet]
        public ActionResult<List<Reserva>> GetAll()
        {
            // Chama o repositório para obter todos os reservas
            var reservas = _reservaRepo.GetAll();

            // Verifica se a lista de reservas está vazia
            if (reservas == null || !reservas.Any())
            {
                return NotFound(new { Mensagem = "Nenhum reserva encontrado." });
            }

            // Mapeia a lista de clientes para incluir a URL da foto
            var listaRes = reservas.Select(reserva => new Reserva
            {
                Id = reserva.Id,
                FkLivro = reserva.FkLivro,
                FkMembro = reserva.FkMembro,
                DataReserva = reserva.DataReserva
            }).ToList();

            // Retorna a lista de clientes com status 200 OK
            return Ok(listaRes);
        }

        // GET: api/Reserva/{id}
        [HttpGet("{id}")]
        public ActionResult<Reserva> GetById(int id)
        {
            // Chama o repositório para obter o reserva pelo ID
            var reserva = _reservaRepo.GetById(id);

            // Se o reserva não for encontrado, retorna uma resposta 404
            if (reserva == null)
            {
                return NotFound(new { Mensagem = "Reserva não encontrado." }); // Retorna 404 com mensagem
            }

            // Mapeia o reserva encontrado para incluir a URL da foto
            var reservaId = new Reserva
            {
                Id = reserva.Id,
                FkLivro = reserva.FkLivro,
                FkMembro = reserva.FkMembro,
                DataReserva = reserva.DataReserva
            };

            // Retorna o reserva com status 200 OK
            return Ok(reservaId);
        }

        // POST api/<ReservaController>
        [HttpPost]
        public ActionResult<object> Post([FromForm] ReservaDto novoReserva)
        {
            // Cria uma nova instância do modelo reserva a partir do DTO recebido
            var reserva = new Reserva
            {
                FkLivro = novoReserva.FkLivro,
                FkMembro = novoReserva.FkMembro,
                DataReserva = novoReserva.DataReserva,
            };

            // Chama o método de adicionar do repositório, passando a foto como parâmetro
            _reservaRepo.Add(reserva);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Reserva cadastrada com sucesso!",
                FkLivro = reserva.FkLivro,
                FkMembro = reserva.FkMembro,
                DataReserva = reserva.DataReserva
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // PUT api/<ReservaController>/5
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] ReservaDto reservaAtualizado)
        {
            // Busca o reserva existente pelo Id
            var reservaExistente = _reservaRepo.GetById(id);

            // Verifica se o reserva foi encontrado
            if (reservaExistente == null)
            {
                return NotFound(new { Mensagem = "Reserva não encontrado." });
            }

            // Atualiza os dados do reserva existente com os valores do objeto recebido
            reservaExistente.FkLivro = reservaAtualizado.FkLivro;
            reservaExistente.FkMembro = reservaAtualizado.FkMembro;
            reservaExistente.DataReserva = reservaAtualizado.DataReserva;



            // Chama o método de atualização do repositório, passando a nova foto
            _reservaRepo.Update(reservaExistente);


            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Emprestimo atualizado com sucesso!",
                FkLivro = reservaExistente.FkLivro,
                FkMembro = reservaExistente.FkMembro,
                DataReserva = reservaExistente.DataReserva
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // DELETE api/<ReservaController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            // Busca o reserva existente pelo Id
            var reservaExistente = _reservaRepo.GetById(id);

            // Verifica se o reserva foi encontrado
            if (reservaExistente == null)
            {
                return NotFound(new { Mensagem = "Reserva não encontrada." });
            }

            // Chama o método de exclusão do repositório
            _reservaRepo.Delete(id);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Reserva excluído com sucesso!",
                FkLivro = reservaExistente.FkLivro,
                FkMembro = reservaExistente.FkMembro,
                DataReserva = reservaExistente.DataReserva
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }
    }
}
