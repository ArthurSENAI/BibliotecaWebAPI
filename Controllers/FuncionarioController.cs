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
            try
            {
                var funcionarios = _funcionarioRepo.GetAll();

                if (funcionarios == null || !funcionarios.Any())
                {
                    return NotFound(new { Mensagem = "Nenhum funcionario encontrado." });
                }

                var listaFun = funcionarios.Select(funcionario => new Funcionario
                {
                    Id = funcionario.Id,
                    Nome = funcionario.Nome,
                    Telefone = funcionario.Telefone,
                    Email = funcionario.Email,
                    Cargo = funcionario.Cargo
                }).ToList();

                return Ok(listaFun);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro ao buscar funcionarios.", Erro = ex.Message });
            }
        }

        // GET: api/Funcionario/{id}
        [HttpGet("{id}")]
        public ActionResult<Funcionario> GetById(int id)
        {
            try
            {
                var funcionario = _funcionarioRepo.GetById(id);

                if (funcionario == null)
                {
                    return NotFound(new { Mensagem = "Funcionario não encontrado." });
                }

                var funcionarioId = new Funcionario
                {
                    Id = funcionario.Id,
                    Nome = funcionario.Nome,
                    Telefone = funcionario.Telefone,
                    Email = funcionario.Email,
                    Cargo = funcionario.Cargo
                };

                return Ok(funcionarioId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro ao buscar funcionario.", Erro = ex.Message });
            }
        }

        // POST api/<FuncionarioController>
        [HttpPost]
        public ActionResult<object> Post([FromForm] FuncionarioDto novoFuncionario)
        {
            try
            {
                var funcionario = new Funcionario
                {
                    Nome = novoFuncionario.Nome,
                    Telefone = novoFuncionario.Telefone,
                    Email = novoFuncionario.Email,
                    Cargo = novoFuncionario.Cargo
                };

                _funcionarioRepo.Add(funcionario);

                var resultado = new
                {
                    Mensagem = "Funcionario cadastrado com sucesso!",
                    Nome = funcionario.Nome,
                    Telefone = funcionario.Telefone,
                    Email = funcionario.Email,
                    Cargo = funcionario.Cargo
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro ao cadastrar funcionario.", Erro = ex.Message });
            }
        }

        // PUT api/<FuncionarioController>/5
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] FuncionarioDto funcionarioAtualizado)
        {
            try
            {
                var funcionarioExistente = _funcionarioRepo.GetById(id);

                if (funcionarioExistente == null)
                {
                    return NotFound(new { Mensagem = "Funcionario não encontrado." });
                }

                funcionarioExistente.Nome = funcionarioAtualizado.Nome;
                funcionarioExistente.Telefone = funcionarioAtualizado.Telefone;
                funcionarioExistente.Email = funcionarioAtualizado.Email;
                funcionarioExistente.Cargo = funcionarioAtualizado.Cargo;

                _funcionarioRepo.Update(funcionarioExistente);

                var resultado = new
                {
                    Mensagem = "Funcionario atualizado com sucesso!",
                    Nome = funcionarioExistente.Nome,
                    Telefone = funcionarioExistente.Telefone,
                    Email = funcionarioExistente.Email,
                    Cargo = funcionarioExistente.Cargo
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro ao atualizar funcionario.", Erro = ex.Message });
            }
        }

        // DELETE api/<FuncionarioController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var funcionarioExistente = _funcionarioRepo.GetById(id);

                if (funcionarioExistente == null)
                {
                    return NotFound(new { Mensagem = "Funcionario não encontrado." });
                }

                _funcionarioRepo.Delete(id);

                var resultado = new
                {
                    Mensagem = "Funcionario excluído com sucesso!",
                    Nome = funcionarioExistente.Nome,
                    Telefone = funcionarioExistente.Telefone,
                    Email = funcionarioExistente.Email,
                    Cargo = funcionarioExistente.Cargo
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro ao excluir funcionario.", Erro = ex.Message });
            }
        }
    }
}
