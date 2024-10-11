using BibliotecaWebAPI.Model;
using BibliotecaWebAPI.ORM;

namespace BibliotecaWebAPI.Repositorio
{
    public class MembroRepositorio
    {
        private BdBibliotecaContext _context;
        public MembroRepositorio (BdBibliotecaContext context)
        {
            _context = context;
        }
        public void Add(Membro membro)
        {

            // Cria uma nova entidade do tipo tbFuncionario a partir do objeto Funcionario recebido
            var tbMembro = new TbMembro()
            {
                Nome = membro.Nome,
                Telefone = membro.Telefone,
                Email = membro.Email,
                TipoMembro = membro.TipoMembro,
                DataCadastro = membro.DataCadastro
            };

            // Adiciona a entidade ao contexto
            _context.TbMembros.Add(tbMembro);

            // Salva as mudanças no banco de dados
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbMembro = _context.TbMembros.FirstOrDefault(m => m.Id == id);

            // Verifica se a entidade foi encontrada
            if (tbMembro != null)
            {
                // Remove a entidade do contexto
                _context.TbMembros.Remove(tbMembro);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Membro não encontrado.");
            }
        }
        public List<Membro> GetAll()
        {
            List<Membro> listMem = new List<Membro>();

            var listTb = _context.TbMembros.ToList();

            foreach (var item in listTb)
            {
                var membro = new Membro
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Telefone = item.Telefone,
                    Email = item.Email,
                    TipoMembro = item.TipoMembro,
                    DataCadastro = item.DataCadastro
                };

                listMem.Add(membro);
            }

            return listMem;
        }
        public Membro GetById(int id)
        {
            // Busca o Membro pelo ID no banco de dados
            var item = _context.TbMembros.FirstOrDefault(m => m.Id == id);

            // Verifica se o Membro foi encontrado
            if (item == null)
            {
                return null; // Retorna null se não encontrar
            }

            // Mapeia o objeto encontrado para a classe Membro
            var membro = new Membro
            {
                Id = item.Id,
                Nome = item.Nome,
                Telefone = item.Telefone,
                Email = item.Email,
                TipoMembro = item.TipoMembro,
                DataCadastro = item.DataCadastro
            };

            return membro; // Retorna o cliente encontrado
        }
        public void Update(Membro membro)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbMembro = _context.TbMembros.FirstOrDefault(f => f.Id == membro.Id);

            // Verifica se a entidade foi encontrada
            if (tbMembro != null)
            {
                // Atualiza os campos da entidade com os valores do objeto Membro recebido
                tbMembro.Nome = membro.Nome;
                tbMembro.Telefone = membro.Telefone;
                tbMembro.Email = membro.Email;
                tbMembro.TipoMembro = membro.TipoMembro;
                tbMembro.DataCadastro = membro.DataCadastro;


                // Atualiza as informações no contexto
                _context.TbMembros.Update(tbMembro);

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
