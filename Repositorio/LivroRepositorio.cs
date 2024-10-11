using BibliotecaWebAPI.Model;
using BibliotecaWebAPI.ORM;

namespace BibliotecaWebAPI.Repositorio
{
    public class LivroRepositorio
    {
        private BdBibliotecaContext _context;
        public LivroRepositorio(BdBibliotecaContext context)
        {
            _context = context;
        }
        public void Add(Livro livro)
        {

            // Cria uma nova entidade do tipo tbLivro a partir do objeto Livro recebidotbFuncionario a partir do objeto Funcionario recebido
            var tbLivro = new TbLivro()
            {
                Titulo = livro.Titulo,
                Autor = livro.Autor,
                AnoPublicacao = livro.AnoPublicacao,
                FkCategoria = livro.FkCategoria,
                Disponibilidade = livro.Disponibilidade

            };

            // Adiciona a entidade ao contexto
            _context.TbLivros.Add(tbLivro);

            // Salva as mudanças no banco de dados
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbLivro = _context.TbLivros.FirstOrDefault(l => l.Id == id);

            // Verifica se a entidade foi encontrada
            if (tbLivro != null)
            {
                // Remove a entidade do contexto
                _context.TbLivros.Remove(tbLivro);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Livro não encontrado.");
            }
        }
        public List<Livro> GetAll()
        {
            List<Livro> listLiv = new List<Livro>();

            var listTb = _context.TbLivros.ToList();

            foreach (var item in listTb)
            {
                var livro = new Livro
                {
                    Id = item.Id,
                    Titulo = item.Titulo,
                    Autor = item.Autor,
                    AnoPublicacao = item.AnoPublicacao,
                    FkCategoria = item.FkCategoria,
                    Disponibilidade = item.Disponibilidade
                };

                listLiv.Add(livro);
            }

            return listLiv;
        }
        public Livro GetById(int id)
        {
            // Busca o Livro pelo ID no banco de dados
            var item = _context.TbLivros.FirstOrDefault(l => l.Id == id);

            // Verifica se o Livro foi encontrado
            if (item == null)
            {
                return null; // Retorna null se não encontrar
            }

            // Mapeia o objeto encontrado para a classe Livro
            var livro = new Livro
            {
                Id = item.Id,
                Titulo = item.Titulo,
                Autor = item.Autor,
                AnoPublicacao = item.AnoPublicacao,
                FkCategoria = item.FkCategoria,
                Disponibilidade = item.Disponibilidade
            };

            return livro; // Retorna o Livro encontrado
        }
        public void Update(Livro livro)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbLivro = _context.TbLivros.FirstOrDefault(f => f.Id == livro.Id);

            // Verifica se a entidade foi encontrada
            if (tbLivro != null)
            {
                // Atualiza os campos da entidade com os valores do objeto Membro recebido
                tbLivro.Titulo = livro.Titulo;
                tbLivro.Autor = livro.Autor;
                tbLivro.FkCategoria = livro.FkCategoria;
                tbLivro.AnoPublicacao = livro.AnoPublicacao;



                // Atualiza as informações no contexto
                _context.TbLivros.Update(tbLivro);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Funcionário não encontrado.");
            }
        }
    }
}