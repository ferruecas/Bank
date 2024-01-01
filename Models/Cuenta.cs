using System;
using System.Collections.Generic;

namespace Bank.Models;

public partial class Cuenta
{
    public int CuentaId { get; set; }

    public decimal? Saldo { get; set; }

    public int? TipoCuentaId { get; set; }

    public int? ClienteId { get; set; }

    public int? CiudadId { get; set; }

    public virtual Ciudad? Ciudad { get; set; }

    public virtual Cliente? Cliente { get; set; }

    public virtual TipoCuentum? TipoCuenta { get; set; }

    public virtual ICollection<Transaccione> Transacciones { get; set; } = new List<Transaccione>();
}
