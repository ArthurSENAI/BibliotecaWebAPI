using BibliotecaWebAPI.Model;
using BibliotecaWebAPI.ORM;

namespace BibliotecaWebAPI.Repositorio
{
    public class EmprestimoRepositorio
    {
        private BdBibliotecaContext _context;
        public EmprestimoRepositorio(BdBibliotecaContext context)
        {
            _context = context;
        }
        public void Add(Emprestimo emprestimo)
        {

            // Cria uma nova entidade do tipo tbEmprestimo a partir do objeto Funcionario recebido
            var tbEmprestimo = new TbEmprestimo()
            {
                DataDevolucao = emprestimo.DataDevolucao,
                DataEmprestimo = emprestimo.DataEmprestimo,
                FkLivro = emprestimo.FkLivro,
                FkMembro = emprestimo.FkMembro
            };

            // Adiciona a entidade ao contexto
            _context.TbEmprestimos.Add(tbEmprestimo);

            // Salva as mudanças no banco de dados
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbEmprestimo = _context.TbEmprestimos.FirstOrDefault(e => e.Id == id);

            // Verifica se a entidade foi encontrada
            if (tbEmprestimo != null)
            {
                // Remove a entidade do contexto
                _context.TbEmprestimos.Remove(tbEmprestimo);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Emprestimo não encontrado.");
            }
        }
        public List<Emprestimo> GetAll()
        {
            List<Emprestimo> listEmp = new List<Emprestimo>();

            var listTb = _context.TbEmprestimos.ToList();

            foreach (var item in listTb)
            {
                var emprestimo = new Emprestimo
                {
                    Id = item.Id,
                    DataDevolucao = item.DataDevolucao,
                    DataEmprestimo = item.DataEmprestimo,
                    FkLivro = item.FkLivro,
                    FkMembro = item.FkMembro
                };

                listEmp.Add(emprestimo);
            }

            return listEmp;
        }
        public Emprestimo GetById(int id)
        {
            // Busca o Emprestimo pelo ID no banco de dados
            var item = _context.TbEmprestimos.FirstOrDefault(e => e.Id == id);

            // Verifica se o Emprestimo foi encontrado
            if (item == null)
            {
                return null; // Retorna null se não encontrar
            }

            // Mapeia o objeto encontrado para a classe Emprestimo
            var emprestimo = new Emprestimo
            {
                Id = item.Id,
                DataDevolucao = item.DataDevolucao,
                DataEmprestimo = item.DataEmprestimo,
                FkLivro = item.FkLivro,
                FkMembro = item.FkMembro
            };

            return emprestimo; // Retorna o Emprestimo encontrado
        }
        public void Update(Emprestimo emprestimo)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbEmprestimo = _context.TbEmprestimos.FirstOrDefault(e => e.Id == emprestimo.Id);

            // Verifica se a entidade foi encontrada
            if (tbEmprestimo != null)
            {
                // Atualiza os campos da entidade com os valores do objeto Membro recebido
                tbEmprestimo.DataDevolucao = emprestimo.DataDevolucao;
                tbEmprestimo.DataEmprestimo = emprestimo.DataEmprestimo;
                tbEmprestimo.FkLivro = emprestimo.FkLivro;
                tbEmprestimo.FkMembro = emprestimo.FkMembro;



                // Atualiza as informações no contexto
                _context.TbEmprestimos.Update(tbEmprestimo);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Emprestimo não encontrado.");
            }
        }
    }
}
