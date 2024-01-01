using System;
using System.Collections.Generic;

namespace Bank.Models;

public partial class Transaccione
{
    public int TransaccionId { get; set; }

    public string? Tipo { get; set; }

    public decimal? Monto { get; set; }

    public DateTime? FechaTransaccion { get; set; }

    public int? CuentaId { get; set; }

    public int? CiudadId { get; set; }

    public virtual Ciudad? Ciudad { get; set; }

    public virtual Cuenta? Cuenta { get; set; }
}
