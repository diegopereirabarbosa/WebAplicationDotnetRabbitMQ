using System;
using System.Collections.Generic;

namespace WebApiOrder.Models;

public partial class Cliente
{
    public int CodigoCliente { get; set; }

    public string NomeRazaoSocial { get; set; } = null!;

    public string? ApelidoFantasia { get; set; }

    public string CpfCnpj { get; set; } = null!;

    public string? RgInscricaoEstadual { get; set; }

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
