using System;
using System.Collections.Generic;

namespace SantaUrsula.API.Data.Models;

public partial class Permiso
{
    public int Id { get; set; }

    public string Modulo { get; set; } = null!;

    public string Accion { get; set; } = null!;

    public virtual ICollection<Role> Rols { get; set; } = new List<Role>();
}
