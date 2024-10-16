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
            try
            {
                var membros = _membroRepo.GetAll();

                if (membros == null || !membros.Any())
                {
                    return NotFound(new { Mensagem = "Nenhum membro encontrado." });
                }

                var listaMem = membros.Select(membro => new Membro
                {
                    Id = membro.Id,
                    Nome = membro.Nome,
                    Telefone = membro.Telefone,
                    Email = membro.Email,
                    TipoMembro = membro.TipoMembro,
                    DataCadastro = membro.DataCadastro
                }).ToList();

                return Ok(listaMem);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro ao buscar membros.", Erro = ex.Message });
            }
        }

        // GET: api/Membro/{id}
        [HttpGet("{id}")]
        public ActionResult<Membro> GetById(int id)
        {
            try
            {
                var membro = _membroRepo.GetById(id);

                if (membro == null)
                {
                    return NotFound(new { Mensagem = "Membro não encontrado." });
                }

                var membroId = new Membro
                {
                    Id = membro.Id,
                    Nome = membro.Nome,
                    Telefone = membro.Telefone,
                    Email = membro.Email,
                    TipoMembro = membro.TipoMembro,
                    DataCadastro = membro.DataCadastro
                };

                return Ok(membroId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro ao buscar membro.", Erro = ex.Message });
            }
        }

        // POST api/<MembroController>
        [HttpPost]
        public ActionResult<object> Post([FromForm] MembroDto novoMembro)
        {
            try
            {
                var membro = new Membro
                {
                    Nome = novoMembro.Nome,
                    Telefone = novoMembro.Telefone,
                    Email = novoMembro.Email,
                    TipoMembro = novoMembro.TipoMembro,
                    DataCadastro = novoMembro.DataCadastro
                };

                _membroRepo.Add(membro);

                var resultado = new
                {
                    Mensagem = "Membro cadastrado com sucesso!",
                    Nome = membro.Nome,
                    Telefone = membro.Telefone,
                    Email = membro.Email,
                    TipoMembro = membro.TipoMembro,
                    DataCadastro = membro.DataCadastro
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro ao cadastrar membro.", Erro = ex.Message });
            }
        }

        // PUT api/<MembroController>/5
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] MembroDto membroAtualizado)
        {
            try
            {
                var membroExistente = _membroRepo.GetById(id);

                if (membroExistente == null)
                {
                    return NotFound(new { Mensagem = "Membro não encontrado." });
                }

                membroExistente.Nome = membroAtualizado.Nome;
                membroExistente.Telefone = membroAtualizado.Telefone;
                membroExistente.Email = membroAtualizado.Email;
                membroExistente.TipoMembro = membroAtualizado.TipoMembro;
                membroExistente.DataCadastro = membroAtualizado.DataCadastro;

                _membroRepo.Update(membroExistente);

                var resultado = new
                {
                    Mensagem = "Membro atualizado com sucesso!",
                    Nome = membroExistente.Nome,
                    Telefone = membroExistente.Telefone,
                    Email = membroExistente.Email,
                    TipoMembro = membroExistente.TipoMembro,
                    DataCadastro = membroExistente.DataCadastro
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro ao atualizar membro.", Erro = ex.Message });
            }
        }

        // DELETE api/<MembroController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var membroExistente = _membroRepo.GetById(id);

                if (membroExistente == null)
                {
                    return NotFound(new { Mensagem = "Membro não encontrado." });
                }

                _membroRepo.Delete(id);

                var resultado = new
                {
                    Mensagem = "Membro excluído com sucesso!",
                    Nome = membroExistente.Nome,
                    Telefone = membroExistente.Telefone,
                    Email = membroExistente.Email,
                    TipoMembro = membroExistente.TipoMembro,
                    DataCadastro = membroExistente.DataCadastro
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro ao excluir membro.", Erro = ex.Message });
            }
        }
    }
}
