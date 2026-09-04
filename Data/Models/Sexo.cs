using System;
using System.Collections.Generic;

namespace SantaUrsula.API.Data.Models;

public partial class Sexo
{
    public byte Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Paciente> Pacientes { get; set; } = new List<Paciente>();
}
