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
    public class LivroController : ControllerBase
    {
        private readonly LivroRepositorio _livroRepo;

        public LivroController(LivroRepositorio livroRepo)
        {
            _livroRepo = livroRepo;
        }

        // GET: api/<LivroController>
        [HttpGet]
        public ActionResult<List<Livro>> GetAll()
        {
            // Chama o repositório para obter todos os livros
            var livros = _livroRepo.GetAll();

            // Verifica se a lista de livros está vazia
            if (livros == null || !livros.Any())
            {
                return NotFound(new { Mensagem = "Nenhum livro encontrado." });
            }

            // Mapeia a lista de livro para incluir a URL da foto
            var listaLiv = livros.Select(livro => new Livro
            {
                Id = livro.Id,
                Titulo = livro.Titulo,
                Autor = livro.Autor,
                FkCategoria = livro.FkCategoria,
                Disponibilidade = livro.Disponibilidade


            }).ToList();

            // Retorna a lista de livro com status 200 OK
            return Ok(listaLiv);
        }

        // GET: api/Livro/{id}
        [HttpGet("{id}")]
        public ActionResult<Livro> GetById(int id)
        {
            // Chama o repositório para obter o livro pelo ID
            var livro = _livroRepo.GetById(id);

            // Se o livro não for encontrado, retorna uma resposta 404
            if (livro == null)
            {
                return NotFound(new { Mensagem = "Livro não encontrado." }); // Retorna 404 com mensagem
            }

            // Mapeia o livro encontrado para incluir a URL da foto
            var livroId = new Livro
            {
                Id = livro.Id,
                Titulo = livro.Titulo,
                Autor = livro.Autor,
                FkCategoria = livro.FkCategoria,
                Disponibilidade = livro.Disponibilidade
            };

            // Retorna o livro com status 200 OK
            return Ok(livroId);
        }

        // POST api/<LivroController>
        [HttpPost]
        public ActionResult<object> Post([FromForm] LivroDto novoLivro)
        {
            // Cria uma nova instância do modelo livro a partir do DTO recebido
            var livro = new Livro
            {
                Titulo = novoLivro.Titulo,
                Autor = novoLivro.Autor,
                AnoPublicacao = novoLivro.AnoPublicacao,
                FkCategoria = novoLivro.FkCategoria,
                Disponibilidade = novoLivro.Disponibilidade
            };

            // Chama o método de adicionar do repositório, passando a foto como parâmetro
            _livroRepo.Add(livro);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Livro cadastrado com sucesso!",
                Titulo = livro.Titulo,
                Autor = livro.Autor,
                AnoPublicacao = livro.AnoPublicacao,
                FkCategoria = livro.FkCategoria,
                Disponibilidade = livro.Disponibilidade
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // PUT api/<LivroController>/5
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] LivroDto livroAtualizado)
        {
            // Busca o livro existente pelo Id
            var livroExistente = _livroRepo.GetById(id);

            // Verifica se o livro foi encontrado
            if (livroExistente == null)
            {
                return NotFound(new { Mensagem = "Livro não encontrado." });
            }

            // Atualiza os dados do livro existente com os valores do objeto recebido
            livroExistente.Titulo = livroAtualizado.Titulo;
            livroExistente.Autor = livroAtualizado.Autor;
            livroExistente.AnoPublicacao = livroAtualizado.AnoPublicacao;
            livroExistente.FkCategoria = livroAtualizado.FkCategoria;
            livroExistente.Disponibilidade = livroAtualizado.Disponibilidade;

            // Chama o método de atualização do repositório, passando a nova foto
            _livroRepo.Update(livroExistente);


            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Membro atualizado com sucesso!",
                Titulo = livroExistente.Titulo,
                Autor = livroExistente.Autor,
                AnoPublicacao = livroExistente.AnoPublicacao,
                FkCategoria = livroExistente.FkCategoria,
                Disponibilidade = livroExistente.Disponibilidade
        };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // DELETE api/<LivroController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            // Busca o livro existente pelo Id
            var livroExistente = _livroRepo.GetById(id);

            // Verifica se o livro foi encontrado
            if (livroExistente == null)
            {
                return NotFound(new { Mensagem = "Livro não encontrado." });
            }

            // Chama o método de exclusão do repositório
            _livroRepo.Delete(id);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Livro excluído com sucesso!",
                Titulo = livroExistente.Titulo,
                Autor = livroExistente.Autor,
                AnoPublicacao = livroExistente.AnoPublicacao,
                FkCategoria = livroExistente.FkCategoria,
                Disponibilidade = livroExistente.Disponibilidade
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }
    }
}
