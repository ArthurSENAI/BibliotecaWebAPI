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
    public class MembroController : ControllerBase
    {
        private readonly MembroRepositorio _membroRepo;

        public MembroController(MembroRepositorio membroRepo)
        {
            _membroRepo = membroRepo;
        }

        // GET: api/<MembroController>
        [HttpGet]
        public ActionResult<List<Membro>> GetAll()
        {
            // Chama o repositório para obter todos os membros
            var membros = _membroRepo.GetAll();

            // Verifica se a lista de membros está vazia
            if (membros == null || !membros.Any())
            {
                return NotFound(new { Mensagem = "Nenhum membro encontrado." });
            }

            // Mapeia a lista de clientes para incluir a URL da foto
            var listaMem = membros.Select(membro => new Membro
            {
                Id = membro.Id,
                Nome = membro.Nome,
                Telefone = membro.Telefone,
                Email = membro.Email,
                TipoMembro = membro.TipoMembro,
                DataCadastro = membro.DataCadastro

            }).ToList();

            // Retorna a lista de clientes com status 200 OK
            return Ok(listaMem);
        }

        // GET: api/Membro/{id}
        [HttpGet("{id}")]
        public ActionResult<Membro> GetById(int id)
        {
            // Chama o repositório para obter o membro pelo ID
            var membro = _membroRepo.GetById(id);

            // Se o membro não for encontrado, retorna uma resposta 404
            if (membro == null)
            {
                return NotFound(new { Mensagem = "Membro não encontrado." }); // Retorna 404 com mensagem
            }

            // Mapeia o membro encontrado para incluir a URL da foto
            var membroId = new Membro
            {
                Id = membro.Id,
                Nome = membro.Nome,
                Telefone = membro.Telefone,
                Email = membro.Email,
                TipoMembro = membro.TipoMembro,
                DataCadastro = membro.DataCadastro
            };

            // Retorna o membro com status 200 OK
            return Ok(membroId);
        }

        // POST api/<MembroController>
        [HttpPost]
        public ActionResult<object> Post([FromForm] MembroDto novoMembro)
        {
            // Cria uma nova instância do modelo membro a partir do DTO recebido
            var membro = new Membro
            {
                Nome = novoMembro.Nome,
                Telefone = novoMembro.Telefone,
                Email = novoMembro.Email,
                TipoMembro = novoMembro.TipoMembro,
                DataCadastro = novoMembro.DataCadastro
            };

            // Chama o método de adicionar do repositório, passando a foto como parâmetro
            _membroRepo.Add(membro);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Membro cadastrado com sucesso!",
                Nome = membro.Nome,
                Telefone = membro.Telefone,
                Email = membro.Email,
                TipoMembro = membro.TipoMembro,
                DataCadastro = membro.DataCadastro
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // PUT api/<MembroController>/5
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] MembroDto membroAtualizado)
        {
            // Busca o membro existente pelo Id
            var membroExistente = _membroRepo.GetById(id);

            // Verifica se o membro foi encontrado
            if (membroExistente == null)
            {
                return NotFound(new { Mensagem = "Membro não encontrado." });
            }

            // Atualiza os dados do membro existente com os valores do objeto recebido
            membroExistente.Nome = membroAtualizado.Nome;
            membroExistente.Telefone = membroAtualizado.Telefone;
            membroExistente.Email = membroAtualizado.Email;
            membroExistente.TipoMembro = membroAtualizado.TipoMembro;
            membroExistente.DataCadastro = membroAtualizado.DataCadastro;


            // Chama o método de atualização do repositório, passando a nova foto
            _membroRepo.Update(membroExistente);


            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Membro atualizado com sucesso!",
                Nome = membroExistente.Nome,
                Idade = membroExistente.Telefone,
                Email = membroExistente.Email,
                TipoMembro = membroExistente.TipoMembro,
                DataCadastro = membroExistente.DataCadastro

            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // DELETE api/<MembroController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            // Busca o membro existente pelo Id
            var membroExistente = _membroRepo.GetById(id);

            // Verifica se o membro foi encontrado
            if (membroExistente == null)
            {
                return NotFound(new { Mensagem = "Membro não encontrado." });
            }

            // Chama o método de exclusão do repositório
            _membroRepo.Delete(id);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Membro excluído com sucesso!",
                Nome = membroExistente.Nome,
                Idade = membroExistente.Telefone,
                Email = membroExistente.Email,
                TipoMembro = membroExistente.TipoMembro,
                DataCadastro = membroExistente.DataCadastro
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }
    }
}
