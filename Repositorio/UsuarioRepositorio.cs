using BibliotecaWebAPI.ORM;

namespace BibliotecaWebAPI.Repositorio
{
    public class UsuarioRepositorio
    {
        private readonly BdBibliotecaContext _context;

        public UsuarioRepositorio(BdBibliotecaContext context)
        {
            _context = context;
        }

        public TbUsuario GetByCredentials(string usuario, string senha)
        {
            // Aqui você deve usar a lógica de hash para comparar a senha
            return _context.TbUsuarios.FirstOrDefault(u => u.Usuario == usuario && u.Senha == senha);
        }

        // Você pode adicionar métodos adicionais para gerenciar usuários
    }
}
