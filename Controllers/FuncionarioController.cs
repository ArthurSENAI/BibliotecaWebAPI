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
    public class FuncionarioController : ControllerBase
    {
        private readonly FuncionarioRepositorio _funcionarioRepo;

        public FuncionarioController(FuncionarioRepositorio funcionarioRepo)
        {
            _funcionarioRepo = funcionarioRepo;
        }

        // GET: api/<FuncionarioController>
        [HttpGet]
        public ActionResult<List<Funcionario>> GetAll()
        {
            // Chama o repositório para obter todos os funcionarios
            var funcionarios = _funcionarioRepo.GetAll();

            // Verifica se a lista de funcionarios está vazia
            if (funcionarios == null || !funcionarios.Any())
            {
                return NotFound(new { Mensagem = "Nenhum funcionario encontrado." });
            }

            // Mapeia a lista de clientes para incluir a URL da foto
            var listaFun = funcionarios.Select(funcionario => new Funcionario
            {
                Id = funcionario.Id,
                Nome = funcionario.Nome,
                Telefone = funcionario.Telefone,
                Email = funcionario.Email,
                Cargo = funcionario.Cargo
            }).ToList();

            // Retorna a lista de clientes com status 200 OK
            return Ok(listaFun);
        }

        // GET: api/Funcionario/{id}
        [HttpGet("{id}")]
        public ActionResult<Funcionario> GetById(int id)
        {
            // Chama o repositório para obter o funcionário pelo ID
            var funcionario = _funcionarioRepo.GetById(id);

            // Se o funcionário não for encontrado, retorna uma resposta 404
            if (funcionario == null)
            {
                return NotFound(new { Mensagem = "Funcionario não encontrado." }); // Retorna 404 com mensagem
            }

            // Mapeia o funcionário encontrado para incluir a URL da foto
            var funcionarioId = new Funcionario
            {
                Id = funcionario.Id,
                Nome = funcionario.Nome,
                Telefone = funcionario.Telefone,
                Email= funcionario.Email,
                Cargo = funcionario.Cargo
            };

            // Retorna o funcionário com status 200 OK
            return Ok(funcionarioId);
        }

        // POST api/<FuncionarioController>
        [HttpPost]
        public ActionResult<object> Post([FromForm] FuncionarioDto novoFuncionario)
        {
            // Cria uma nova instância do modelo Funcionario a partir do DTO recebido
            var funcionario = new Funcionario
            {
                Nome = novoFuncionario.Nome,
                Telefone = novoFuncionario.Telefone,
                Email = novoFuncionario.Email,
                Cargo = novoFuncionario.Cargo
            };

            // Chama o método de adicionar do repositório, passando a foto como parâmetro
            _funcionarioRepo.Add(funcionario);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Funcionario cadastrado com sucesso!",
                Nome = funcionario.Nome,
                Telefone = funcionario.Telefone,
                Email = funcionario.Email,
                Cargo = funcionario.Cargo
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // PUT api/<FuncionarioController>/5
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] FuncionarioDto funcionarioAtualizado)
        {
            // Busca o funcionário existente pelo Id
            var funcionarioExistente = _funcionarioRepo.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (funcionarioExistente == null)
            {
                return NotFound(new { Mensagem = "Funcionario não encontrado." });
            }

            // Atualiza os dados do funcionário existente com os valores do objeto recebido
            funcionarioExistente.Nome = funcionarioAtualizado.Nome;
            funcionarioExistente.Telefone = funcionarioAtualizado.Telefone;
            funcionarioExistente.Email = funcionarioAtualizado.Email;
            funcionarioExistente.Cargo = funcionarioAtualizado.Cargo;


            // Chama o método de atualização do repositório, passando a nova foto
            _funcionarioRepo.Update(funcionarioExistente);


            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Funcionario atualizado com sucesso!",
                Nome = funcionarioExistente.Nome,
                Idade = funcionarioExistente.Telefone,
                Email = funcionarioExistente.Email,
                Cargo = funcionarioExistente.Cargo

            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // DELETE api/<FuncionarioController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            // Busca o funcionário existente pelo Id
            var funcionarioExistente = _funcionarioRepo.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (funcionarioExistente == null)
            {
                return NotFound(new { Mensagem = "Funcionario não encontrado." });
            }

            // Chama o método de exclusão do repositório
            _funcionarioRepo.Delete(id);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Usuário excluído com sucesso!",
                Nome = funcionarioExistente.Nome,
                Idade = funcionarioExistente.Telefone,
                Email = funcionarioExistente.Email,
                Cargo = funcionarioExistente.Cargo
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }
    }
}
