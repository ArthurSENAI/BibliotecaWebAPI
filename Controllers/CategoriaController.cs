using BibliotecaWebAPI.Model;
using BibliotecaWebAPI.Repositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

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
            try
            {
                var categorias = _categoriaRepo.GetAll();

                if (categorias == null || !categorias.Any())
                {
                    return NotFound(new { Mensagem = "Nenhum categoria encontrado." });
                }

                var listaCat = categorias.Select(categoria => new Categoria
                {
                    Id = categoria.Id,
                    Nome = categoria.Nome,
                    Descricao = categoria.Descricao
                }).ToList();

                return Ok(listaCat);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro ao buscar categorias.", Erro = ex.Message });
            }
        }

        // GET: api/Categoria/{id}
        [HttpGet("{id}")]
        public ActionResult<Categoria> GetById(int id)
        {
            try
            {
                var categoria = _categoriaRepo.GetById(id);

                if (categoria == null)
                {
                    return NotFound(new { Mensagem = "Categoria não encontrado." });
                }

                var categoriaId = new Categoria
                {
                    Id = categoria.Id,
                    Nome = categoria.Nome,
                    Descricao = categoria.Descricao
                };

                return Ok(categoriaId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro ao buscar categoria.", Erro = ex.Message });
            }
        }

        // POST api/<CategoriaController>
        [HttpPost]
        public ActionResult<object> Post([FromForm] CategoriaDto novoCategoria)
        {
            try
            {
                var categoria = new Categoria
                {
                    Nome = novoCategoria.Nome,
                    Descricao = novoCategoria.Descricao
                };

                _categoriaRepo.Add(categoria);

                var resultado = new
                {
                    Mensagem = "Categoria cadastrado com sucesso!",
                    Nome = categoria.Nome,
                    Descricao = categoria.Descricao
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro ao cadastrar categoria.", Erro = ex.Message });
            }
        }

        // PUT api/<CategoriaController>/5
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] CategoriaDto categoriaAtualizado)
        {
            try
            {
                var categoriaExistente = _categoriaRepo.GetById(id);

                if (categoriaExistente == null)
                {
                    return NotFound(new { Mensagem = "Categoria não encontrado." });
                }

                categoriaExistente.Nome = categoriaAtualizado.Nome;
                categoriaExistente.Descricao = categoriaAtualizado.Descricao;

                _categoriaRepo.Update(categoriaExistente);

                var resultado = new
                {
                    Mensagem = "Categoria atualizado com sucesso!",
                    Nome = categoriaExistente.Nome,
                    Descricao = categoriaExistente.Descricao
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro ao atualizar categoria.", Erro = ex.Message });
            }
        }

        // DELETE api/<CategoriaController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var categoriaExistente = _categoriaRepo.GetById(id);

                if (categoriaExistente == null)
                {
                    return NotFound(new { Mensagem = "Categoria não encontrado." });
                }

                _categoriaRepo.Delete(id);

                var resultado = new
                {
                    Mensagem = "Categoria excluído com sucesso!",
                    Nome = categoriaExistente.Nome,
                    Descricao = categoriaExistente.Descricao
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro ao excluir categoria.", Erro = ex.Message });
            }
        }
    }
}
