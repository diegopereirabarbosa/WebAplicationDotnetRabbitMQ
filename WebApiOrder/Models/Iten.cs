using System;
using System.Collections.Generic;

namespace WebApiOrder.Models;

public partial class Iten
{
    public int CodigoItem { get; set; }

    public string Produto { get; set; } = null!;

    public int Quantidade { get; set; }

    public decimal? Preco { get; set; }

    public int CodigoPedido { get; set; }

    public virtual Pedido CodigoPedidoNavigation { get; set; } = null!;
}
