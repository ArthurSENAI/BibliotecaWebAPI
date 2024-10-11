using BibliotecaWebAPI.Model;
using BibliotecaWebAPI.ORM;

namespace BibliotecaWebAPI.Repositorio
{
    public class CategoriaRepositorio
    {
        private BdBibliotecaContext _context;
        public CategoriaRepositorio(BdBibliotecaContext context)
        {
            _context = context;
        }
        public void Add(Categoria categoria)
        {

            // Cria uma nova entidade do tipo tbCategoria a partir do objeto Funcionario recebido
            var tbCategoria = new TbCategoria()
            {
                Nome = categoria.Nome,
                Descricao = categoria.Descricao
            };

            // Adiciona a entidade ao contexto
            _context.TbCategorias.Add(tbCategoria);

            // Salva as mudanças no banco de dados
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbCategoria = _context.TbCategorias.FirstOrDefault(c => c.Id == id);

            // Verifica se a entidade foi encontrada
            if (tbCategoria != null)
            {
                // Remove a entidade do contexto
                _context.TbCategorias.Remove(tbCategoria);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Categoria não encontrado.");
            }
        }
        public List<Categoria> GetAll()
        {
            List<Categoria> listCat = new List<Categoria>();

            var listTb = _context.TbCategorias.ToList();

            foreach (var item in listTb)
            {
                var categoria = new Categoria
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Descricao = item.Descricao
                };

                listCat.Add(categoria);
            }

            return listCat;
        }
        public Categoria GetById(int id)
        {
            // Busca o Categoria pelo ID no banco de dados
            var item = _context.TbCategorias.FirstOrDefault(c => c.Id == id);

            // Verifica se o Categoria foi encontrado
            if (item == null)
            {
                return null; // Retorna null se não encontrar
            }

            // Mapeia o objeto encontrado para a classe Categoria
            var categoria = new Categoria
            {
                Id = item.Id,
                Nome = item.Nome,
                Descricao = item.Descricao
            };

            return categoria; // Retorna o Categoria encontrado
        }
        public void Update(Categoria categoria)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbCategoria = _context.TbCategorias.FirstOrDefault(f => f.Id == categoria.Id);

            // Verifica se a entidade foi encontrada
            if (tbCategoria != null)
            {
                // Atualiza os campos da entidade com os valores do objeto Categoria recebido
                tbCategoria.Nome = categoria.Nome;
                tbCategoria.Descricao = categoria.Descricao;

                // Atualiza as informações no contexto
                _context.TbCategorias.Update(tbCategoria);

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
