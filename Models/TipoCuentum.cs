using System;
using System.Collections.Generic;

namespace Bank.Models;

public partial class TipoCuentum
{
    public int TipoCuentaId { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Cuenta> Cuenta { get; set; } = new List<Cuenta>();
}
