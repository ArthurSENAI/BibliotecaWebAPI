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
    public class EmprestimoController : ControllerBase
    {
        private readonly EmprestimoRepositorio _emprestimoRepo;

        public EmprestimoController(EmprestimoRepositorio emprestimoRepo)
        {
            _emprestimoRepo = emprestimoRepo;
        }

        // GET: api/<EmprestimoController>
        [HttpGet]
        public ActionResult<List<Emprestimo>> GetAll()
        {
            // Chama o repositório para obter todos os emprestimos
            var emprestimos = _emprestimoRepo.GetAll();

            // Verifica se a lista de emprestimos está vazia
            if (emprestimos == null || !emprestimos.Any())
            {
                return NotFound(new { Mensagem = "Nenhum emprestimo encontrado." });
            }

            // Mapeia a lista de clientes para incluir a URL da foto
            var listaEmp = emprestimos.Select(emprestimo => new Emprestimo
            {
                Id = emprestimo.Id,
                FkLivro = emprestimo.FkLivro,
                FkMembro = emprestimo.FkMembro,
                DataEmprestimo = emprestimo.DataEmprestimo,
                DataDevolucao = emprestimo.DataDevolucao
            }).ToList();

            // Retorna a lista de clientes com status 200 OK
            return Ok(listaEmp);
        }

        // GET: api/Emprestimo/{id}
        [HttpGet("{id}")]
        public ActionResult<Emprestimo> GetById(int id)
        {
            // Chama o repositório para obter o emprestimo pelo ID
            var emprestimo = _emprestimoRepo.GetById(id);

            // Se o emprestimo não for encontrado, retorna uma resposta 404
            if (emprestimo == null)
            {
                return NotFound(new { Mensagem = "Emprestimo não encontrado." }); // Retorna 404 com mensagem
            }

            // Mapeia o emprestimo encontrado para incluir a URL da foto
            var emprestimoId = new Emprestimo
            {
                Id = emprestimo.Id,
                FkLivro = emprestimo.FkLivro,
                FkMembro = emprestimo.FkMembro,
                DataEmprestimo = emprestimo.DataEmprestimo,
                DataDevolucao = emprestimo.DataDevolucao
            };

            // Retorna o emprestimo com status 200 OK
            return Ok(emprestimoId);
        }

        // POST api/<EmprestimoController>
        [HttpPost]
        public ActionResult<object> Post([FromForm] EmprestimoDto novoEmprestimo)
        {
            // Cria uma nova instância do modelo emprestimo a partir do DTO recebido
            var emprestimo = new Emprestimo
            {
                FkLivro = novoEmprestimo.FkLivro,
                FkMembro = novoEmprestimo.FkMembro,
                DataEmprestimo = novoEmprestimo.DataEmprestimo,
                DataDevolucao = novoEmprestimo.DataDevolucao
            };

            // Chama o método de adicionar do repositório, passando a foto como parâmetro
            _emprestimoRepo.Add(emprestimo);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Emprestimo cadastrado com sucesso!",
                FkLivro = emprestimo.FkLivro,
                FkMembro = emprestimo.FkMembro,
                DataEmprestimo = emprestimo.DataEmprestimo,
                DataDevolucao = emprestimo.DataDevolucao
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // PUT api/<EmprestimoController>/5
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] EmprestimoDto emprestimoAtualizado)
        {
            // Busca o emprestimo existente pelo Id
            var emprestimoExistente = _emprestimoRepo.GetById(id);

            // Verifica se o emprestimo foi encontrado
            if (emprestimoExistente == null)
            {
                return NotFound(new { Mensagem = "Emprestimo não encontrado." });
            }

            // Atualiza os dados do emprestimo existente com os valores do objeto recebido
            emprestimoExistente.FkLivro = emprestimoAtualizado.FkLivro;
            emprestimoExistente.FkMembro = emprestimoAtualizado.FkMembro;
            emprestimoExistente.DataEmprestimo = emprestimoAtualizado.DataEmprestimo;
            emprestimoExistente.DataDevolucao = emprestimoAtualizado.DataDevolucao;



            // Chama o método de atualização do repositório, passando a nova foto
            _emprestimoRepo.Update(emprestimoExistente);


            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Emprestimo atualizado com sucesso!",
                FkLivro = emprestimoExistente.FkLivro,
                FkMembro = emprestimoExistente.FkMembro,
                DataEmprestimo = emprestimoExistente.DataEmprestimo,
                DataDevolucao = emprestimoExistente.DataDevolucao
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // DELETE api/<EmprestimoController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            // Busca o emprestimo existente pelo Id
            var emprestimoExistente = _emprestimoRepo.GetById(id);

            // Verifica se o emprestimo foi encontrado
            if (emprestimoExistente == null)
            {
                return NotFound(new { Mensagem = "Emprestimo não encontrado." });
            }

            // Chama o método de exclusão do repositório
            _emprestimoRepo.Delete(id);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Emprestimo excluído com sucesso!",
                FkLivro = emprestimoExistente.FkLivro,
                FkMembro = emprestimoExistente.FkMembro,
                DataEmprestimo = emprestimoExistente.DataEmprestimo,
                DataDevolucao = emprestimoExistente.DataDevolucao
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }
    }
}
