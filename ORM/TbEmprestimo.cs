using System;
using System.Collections.Generic;

namespace BibliotecaWebAPI.ORM;

public partial class TbEmprestimo
{
    public int Id { get; set; }

    public DateOnly DataEmprestimo { get; set; }

    public DateOnly DataDevolucao { get; set; }

    public int FkMembro { get; set; }

    public int FkLivro { get; set; }

    public virtual TbLivro FkLivroNavigation { get; set; } = null!;

    public virtual TbMembro FkMembroNavigation { get; set; } = null!;
}
