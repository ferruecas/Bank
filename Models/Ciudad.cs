using System;
using System.Collections.Generic;

namespace Bank.Models;

public partial class Ciudad
{
    public int CiudadId { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    public virtual ICollection<Cuenta> Cuenta { get; set; } = new List<Cuenta>();

    public virtual ICollection<Transaccione> Transacciones { get; set; } = new List<Transaccione>();
}
