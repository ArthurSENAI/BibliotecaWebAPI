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
    public class CategoriaController : ControllerBase
    {
        private readonly CategoriaRepositorio _categoriaRepo;

        public CategoriaController(CategoriaRepositorio categoriaRepo)
        {
            _categoriaRepo = categoriaRepo;
        }

        // GET: api/<CategoriaController>
        [HttpGet]
        public ActionResult<List<Categoria>> GetAll()
        {
            // Chama o repositório para obter todos os categorias
            var categorias = _categoriaRepo.GetAll();

            // Verifica se a lista de categorias está vazia
            if (categorias == null || !categorias.Any())
            {
                return NotFound(new { Mensagem = "Nenhum categoria encontrado." });
            }

            // Mapeia a lista de clientes para incluir a URL da foto
            var listaCat = categorias.Select(categoria => new Categoria
            {
                Id = categoria.Id,
                Nome = categoria.Nome,
                Descricao = categoria.Descricao

            }).ToList();

            // Retorna a lista de clientes com status 200 OK
            return Ok(listaCat);
        }

        // GET: api/Categoria/{id}
        [HttpGet("{id}")]
        public ActionResult<Categoria> GetById(int id)
        {
            // Chama o repositório para obter o categoria pelo ID
            var categoria = _categoriaRepo.GetById(id);

            // Se o categoria não for encontrado, retorna uma resposta 404
            if (categoria == null)
            {
                return NotFound(new { Mensagem = "Categoria não encontrado." }); // Retorna 404 com mensagem
            }

            // Mapeia o categoria encontrado para incluir a URL da foto
            var categoriaId = new Categoria
            {
                Id = categoria.Id,
                Nome = categoria.Nome,
                Descricao = categoria.Descricao

            };

            // Retorna o categoria com status 200 OK
            return Ok(categoriaId);
        }

        // POST api/<CategoriaController>
        [HttpPost]
        public ActionResult<object> Post([FromForm] CategoriaDto novoCategoria)
        {
            // Cria uma nova instância do modelo categoria a partir do DTO recebido
            var categoria = new Categoria
            {
                Nome = novoCategoria.Nome,
                Descricao = novoCategoria.Descricao

            };

            // Chama o método de adicionar do repositório, passando a foto como parâmetro
            _categoriaRepo.Add(categoria);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Categoria cadastrado com sucesso!",
                Nome = categoria.Nome,
                Descricao = categoria.Descricao
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // PUT api/<CategoriaController>/5
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] CategoriaDto categoriaAtualizado)
        {
            // Busca o categoria existente pelo Id
            var categoriaExistente = _categoriaRepo.GetById(id);

            // Verifica se o categoria foi encontrado
            if (categoriaExistente == null)
            {
                return NotFound(new { Mensagem = "Categoria não encontrado." });
            }

            // Atualiza os dados do categoria existente com os valores do objeto recebido
            categoriaExistente.Nome = categoriaAtualizado.Nome;
            categoriaExistente.Descricao = categoriaAtualizado.Descricao;



            // Chama o método de atualização do repositório, passando a nova foto
            _categoriaRepo.Update(categoriaExistente);


            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Categoria atualizado com sucesso!",
                Nome = categoriaExistente.Nome,
                Descricao = categoriaExistente.Descricao
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // DELETE api/<CategoriaController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            // Busca o categoria existente pelo Id
            var categoriaExistente = _categoriaRepo.GetById(id);

            // Verifica se o categoria foi encontrado
            if (categoriaExistente == null)
            {
                return NotFound(new { Mensagem = "Categoria não encontrado." });
            }

            // Chama o método de exclusão do repositório
            _categoriaRepo.Delete(id);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Categoria excluído com sucesso!",
                Nome = categoriaExistente.Nome,
                Descricao = categoriaExistente.Descricao

            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }
    }
}
