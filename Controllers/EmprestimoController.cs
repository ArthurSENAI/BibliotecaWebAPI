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
            try
            {
                var emprestimos = _emprestimoRepo.GetAll();

                if (emprestimos == null || !emprestimos.Any())
                {
                    return NotFound(new { Mensagem = "Nenhum emprestimo encontrado." });
                }

                var listaEmp = emprestimos.Select(emprestimo => new Emprestimo
                {
                    Id = emprestimo.Id,
                    FkLivro = emprestimo.FkLivro,
                    FkMembro = emprestimo.FkMembro,
                    DataEmprestimo = emprestimo.DataEmprestimo,
                    DataDevolucao = emprestimo.DataDevolucao
                }).ToList();

                return Ok(listaEmp);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro ao buscar emprestimos.", Erro = ex.Message });
            }
        }

        // GET: api/Emprestimo/{id}
        [HttpGet("{id}")]
        public ActionResult<Emprestimo> GetById(int id)
        {
            try
            {
                var emprestimo = _emprestimoRepo.GetById(id);

                if (emprestimo == null)
                {
                    return NotFound(new { Mensagem = "Emprestimo não encontrado." });
                }

                var emprestimoId = new Emprestimo
                {
                    Id = emprestimo.Id,
                    FkLivro = emprestimo.FkLivro,
                    FkMembro = emprestimo.FkMembro,
                    DataEmprestimo = emprestimo.DataEmprestimo,
                    DataDevolucao = emprestimo.DataDevolucao
                };

                return Ok(emprestimoId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro ao buscar emprestimo.", Erro = ex.Message });
            }
        }

        // POST api/<EmprestimoController>
        [HttpPost]
        public ActionResult<object> Post([FromForm] EmprestimoDto novoEmprestimo)
        {
            try
            {
                var emprestimo = new Emprestimo
                {
                    FkLivro = novoEmprestimo.FkLivro,
                    FkMembro = novoEmprestimo.FkMembro,
                    DataEmprestimo = novoEmprestimo.DataEmprestimo,
                    DataDevolucao = novoEmprestimo.DataDevolucao
                };

                _emprestimoRepo.Add(emprestimo);

                var resultado = new
                {
                    Mensagem = "Emprestimo cadastrado com sucesso!",
                    FkLivro = emprestimo.FkLivro,
                    FkMembro = emprestimo.FkMembro,
                    DataEmprestimo = emprestimo.DataEmprestimo,
                    DataDevolucao = emprestimo.DataDevolucao
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro ao cadastrar emprestimo.", Erro = ex.Message });
            }
        }

        // PUT api/<EmprestimoController>/5
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] EmprestimoDto emprestimoAtualizado)
        {
            try
            {
                var emprestimoExistente = _emprestimoRepo.GetById(id);

                if (emprestimoExistente == null)
                {
                    return NotFound(new { Mensagem = "Emprestimo não encontrado." });
                }

                emprestimoExistente.FkLivro = emprestimoAtualizado.FkLivro;
                emprestimoExistente.FkMembro = emprestimoAtualizado.FkMembro;
                emprestimoExistente.DataEmprestimo = emprestimoAtualizado.DataEmprestimo;
                emprestimoExistente.DataDevolucao = emprestimoAtualizado.DataDevolucao;

                _emprestimoRepo.Update(emprestimoExistente);

                var resultado = new
                {
                    Mensagem = "Emprestimo atualizado com sucesso!",
                    FkLivro = emprestimoExistente.FkLivro,
                    FkMembro = emprestimoExistente.FkMembro,
                    DataEmprestimo = emprestimoExistente.DataEmprestimo,
                    DataDevolucao = emprestimoExistente.DataDevolucao
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro ao atualizar emprestimo.", Erro = ex.Message });
            }
        }

        // DELETE api/<EmprestimoController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var emprestimoExistente = _emprestimoRepo.GetById(id);

                if (emprestimoExistente == null)
                {
                    return NotFound(new { Mensagem = "Emprestimo não encontrado." });
                }

                _emprestimoRepo.Delete(id);

                var resultado = new
                {
                    Mensagem = "Emprestimo excluído com sucesso!",
                    FkLivro = emprestimoExistente.FkLivro,
                    FkMembro = emprestimoExistente.FkMembro,
                    DataEmprestimo = emprestimoExistente.DataEmprestimo,
                    DataDevolucao = emprestimoExistente.DataDevolucao
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro ao excluir emprestimo.", Erro = ex.Message });
            }
        }
    }
}
