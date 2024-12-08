using System;
using System.Collections.Generic;

namespace WebApiSendOrder.Models;

public partial class Pedido
{
    public int CodigoPedido { get; set; }

    public int CodigoCliente { get; set; }

    public virtual ICollection<Iten> Itens { get; set; } = new List<Iten>();
}
