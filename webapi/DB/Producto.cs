using System;
using System.Collections.Generic;

namespace webapi.DB;

public partial class Producto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Categoria { get; set; } = null!;

    public double Precio { get; set; }

    public int CantidadDisponible { get; set; }
}
