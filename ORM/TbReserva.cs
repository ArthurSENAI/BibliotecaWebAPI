using System;
using System.Collections.Generic;

namespace BibliotecaWebAPI.ORM;

public partial class TbReserva
{
    public int Id { get; set; }

    public DateOnly DataReserva { get; set; }

    public int FkMembro { get; set; }

    public int FkLivro { get; set; }

    public virtual TbLivro FkLivroNavigation { get; set; } = null!;

    public virtual TbMembro FkMembroNavigation { get; set; } = null!;
}
